using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Door.MultiCard;
using FCARDIO.Protocol.Door.FC89H.Door.MultiCard;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Door.MultiCard
{
    public class WriteMultiCard : FCARDIO.Protocol.Door.FC8800.Door.MultiCard.WriteMultiCard
    {
        /// <summary>
        /// 多卡参数
        /// </summary>
        private WriteMultiCard_Parameter mMultiCardPar;

        private int mIndex = 0;

        public WriteMultiCard(INCommandDetail cd, WriteMultiCard_Parameter value) : base(cd, value)
        {

            mMultiCardPar = value;
        }

        /// <summary>
        /// 二十七 多卡开门A组设置
        /// </summary>
        protected virtual void WriteMultiCardData(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != 3)
            {
                throw new ArgumentException("door Error!");
            }
            databuf.WriteByte(mGroupType);
            databuf.WriteByte(mGroupNum);
            int count = 0;
            if (mGroupType == GroupTypeA)
            {
                count = mMultiCardPar.GroupA[mGroupNum - 1].Count;
                databuf.WriteByte(count);
            }
            if (mGroupType == GroupTypeB)
            {
                count = mMultiCardPar.GroupB[mGroupNum - 1].Count;
                databuf.WriteByte(count);
            }
            Packet(0x03, 0x18, 0x02, 3, databuf);
            _ProcessStep++;
            if (count > 0)
            {
                Step = 3;
            }

            CommandReady();
        }

        /// <summary>
        /// 设置组中的卡号
        /// </summary>
        /// <param name="model"></param>
        protected virtual void WriteMultiCardDataNext()
        {

        }

        protected override void WriteMultiCard_GroupAB()//IByteBuffer databuf
        {
            IByteBuffer databuf;
            int count = 20;
            int startIndex = mIndex;

            int totalCount = 0;
            if (mGroupType == GroupTypeA)
            {
                totalCount = mMultiCardPar.GroupA[mGroupNum - 1].Count;
                count = mMultiCardPar.GroupA[mGroupNum - 1].Count > 20 ? 20 : mMultiCardPar.GroupA[mGroupNum - 1].Count;
                count = count < totalCount - mIndex ? count : totalCount - mIndex;
                databuf = GetCmdDataBuf(1 + 9 * count);
                databuf.WriteByte(mIndex + 1);

                for (int i = 0; i < count; i++)
                {
                    if (startIndex + i >= totalCount)
                    {
                        break;
                    }
                    ulong iCard = mMultiCardPar.GroupA[mGroupNum - 1][startIndex + i];
                    databuf.WriteByte(0);
                    databuf.WriteLong((uint)iCard);
                    mIndex++;
                }
            }
            else
            {
                totalCount = mMultiCardPar.GroupB[mGroupNum - 1].Count;
                count = totalCount > 20 ? 20 : totalCount;
                count = count < totalCount - mIndex ? count : totalCount - mIndex;
                databuf = GetCmdDataBuf(1 + 9 * count);
                databuf.WriteByte(mIndex + 1);

                for (int i = 0; i < count; i++)
                {
                    if (startIndex + i >= totalCount)
                    {
                        break;
                    }
                    ulong iCard = mMultiCardPar.GroupB[mGroupNum - 1][startIndex + i];
                    databuf.WriteByte(0);
                    databuf.WriteLong((uint)iCard);
                    mIndex++;
                }
            }

            //本组上传完，换下一组
            if (mIndex == totalCount)
            {
                MoveNextGroup();

            }

            if (mGroupType == GroupTypeB && mGroupNum == 21)
            {
                CommandCompleted();
                Step = 0;
            }
            else
            {
                Packet(0x03, 0x18, 0x52, (uint)(1 + 9 * count), databuf);
                CommandReady();
            }
        }

        protected void MoveNextGroup()
        {
            mIndex = 0;
            mGroupNum++;
            Step = 2;
            if (mGroupType == GroupTypeA && mGroupNum == 6)
            {
                mGroupType = GroupTypeB;
                mGroupNum = 1;
            }
        }

        /// <summary>
        /// 接收到响应，开始处理下一步命令
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {

            switch (Step)
            {
                case 1://二十六、设置 多卡开门验证方式
                    Packet(0x03, 0x18, 0x00, 4, mMultiCardPar.VerifyType_GetBytes(GetCmdDataBuf(4)));
                    _ProcessStep = 2;
                    if (mMultiCardPar.mProtocolType.Contains("MC58"))
                    {
                        CommandCompleted();
                    }
                    else
                    {
                        CommandReady();//设定命令当前状态为准备就绪，等待发送
                        Step = 2;
                        mGroupType = GroupTypeA;
                        mGroupNum = 1;
                    }

                    break;
                case 2:
                    //检查需要发送的内容
                    switch (mMultiCardPar.VerifyType)
                    {
                        case 1://多卡AB组
                               //开始写A组

                            WriteMultiCardData(GetCmdDataBuf(3));


                            break;
                        case 2://固定组合
                            //开始写第一个固定组合
                            mGroupNum = 1;
                            WriteMultiCard_GroupFix();
                            Step = 4;
                            break;
                        default://其他方式
                            CommandCompleted();
                            break;
                    }

                    break;
                case 3://继续写AB组
                    WriteMultiCard_GroupAB();

                    break;
                case 4://继续写固定组
                    mGroupNum++;
                    if (mGroupNum > 10)
                    {
                        CommandCompleted();
                        Step = 0;
                        return;
                    }
                    WriteMultiCard_GroupFix();
                    break;
                default:
                    break;
            }


        }

        protected override void WriteMultiCard_GroupFix()
        {

            //先找到对应的组
            MultiCard_GroupFix Fix;
            List<UInt64> group = null;
            int iMax = 8;
            int iCount = 0;

            Fix = mMultiCardPar.GroupFix[mGroupNum - 1];
            group = Fix.CardList;

            iCount = group.Count;

            var buf = GetCmdDataBuf(4 + 9 * iCount);

            if (iCount > iMax) iCount = iMax;

            buf.WriteByte(mMultiCardPar.DoorNum);
            buf.WriteByte(mGroupNum);
            buf.WriteByte(iCount);
            buf.WriteByte(Fix.GroupType);

            for (int i = 0; i < iCount; i++)
            {
                buf.WriteByte(0);
                buf.WriteLong((uint)group[i]);
            }


            Packet(0x03, 0x12, 0x02, (uint)buf.ReadableBytes, buf);
            _ProcessStep++;
            CommandReady();

        }
    }
}
