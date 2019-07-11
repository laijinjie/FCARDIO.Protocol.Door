using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Door;
using FCARDIO.Protocol.Door.FC8800.Door.MultiCard;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Door.MultiCard
{
    public class ReadMultiCard : FCARDIO.Protocol.Door.FC8800.Door.MultiCard.ReadMultiCard
    {
        private int mCardCount { get; set; }
        private int mGroupType { get; set; }
        private int mGroupNum = 1;

        /// <summary>
        /// 组号
        /// </summary>
        private byte iGroupNum = 1;
        /// <summary>
        /// 读单个门的多卡参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value"></param>
        public ReadMultiCard(INCommandDetail cd, DoorPort_Parameter value) : base(cd, value) { }

        /// <summary>
        /// 读取AB组的卡数据
        /// </summary>
        /// <param name="group"></param>
        /// <param name="tmpBuf"></param>
        protected override void ReadGroupABCard(List<UInt64> group,IByteBuffer tmpBuf)
        {
            var index = tmpBuf.ReadByte();
            while (tmpBuf.ReadableBytes >= 8)
            {
                tmpBuf.ReadByte();
                UInt64 card = (ulong)tmpBuf.ReadLong();
                group.Add(card);
            }
        }

        /// <summary>
        /// 命令回应处理
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            IByteBuffer tmpBuf;

            switch (Step)
            {
                case 1://读取 多卡开门检测模式参数
                    if (CheckResponse(oPck, 3))
                    {
                        ReadCheckModeCallblack(oPck.CmdData);
                    }

                    break;
                case 2: //读取 多卡开门验证方式 的返回
                    if (CheckResponse(oPck, 4))
                    {
                        ReadVerifyType(oPck.CmdData);
                    }

                    break;
                case 3://读取AB组的数据
                    if (CheckResponse(oPck))
                    {
                        tmpBuf = oPck.CmdData;
                        mGroupType = tmpBuf.ReadByte();//组类别：0--A组；
                        iGroupNum = tmpBuf.ReadByte(); //组号：取值范围 1 - 5；
                        mCardCount = tmpBuf.ReadByte();
                        if (mCardCount == 0)
                        {
                            tmpBuf = _Connector.GetByteBufAllocator().Buffer(2);
                            MoveNextGroup(tmpBuf);
                        }
                    }
                    if (CheckResponse(oPck, 0x03, 0x18, 0x53))
                    {
                        //if (iGroupType != mGroupType)
                        //{
                        //    return;
                        //}
                        if (iGroupNum != mGroupNum)
                        {
                            return;
                        }
                        ReadGroupAB(oPck.CmdData);
                    }
                    break;
                case 4://固定组数据
                    if (CheckResponse(oPck))
                    {
                        ReadGroupFix(oPck.CmdData);
                    }
                    break;
                default:
                    break;
            }

        }

        protected override void ReadGroupFix(IByteBuffer tmpBuf)
        {
            var iDoorPort = tmpBuf.ReadByte();//端口号
            if (mPort != iDoorPort)
            {
                return;
            }
            if (mResult.GroupFix == null)
            {
                mResult.GroupFix = new List<MultiCard_GroupFix>();
            }
            int iCount;
            int iIndex = 1;

            MultiCard_GroupFix group = new MultiCard_GroupFix();
            var groupnum = tmpBuf.ReadByte();
            iCount = tmpBuf.ReadByte();
            group.GroupType = tmpBuf.ReadByte();
            var cardList = new List<UInt64>();
            group.CardList = cardList;
            if (iCount > 0)
            {
                for (int j = 0; j < iCount; j++)
                {
                    tmpBuf.ReadByte();
                    cardList.Add((ulong)tmpBuf.ReadLong());
                }
            }


            mResult.GroupFix.Add(group);

            //iIndex += 34;
            //if (tmpBuf.Capacity < iIndex)
            //{
            //    tmpBuf.SetReaderIndex(iIndex);
            //}

            //for (int i = 1; i <= 10; i++)
            //{

            //}
            if (mResult.GroupFix.Count == 10)
            {
                CommandCompleted();
            }
            else
            {
                CommandWaitResponse();
            }
        }

        private void MoveNextGroup(IByteBuffer tmpBuf)
        {
            mGroupNum++;
            _ProcessStep++;
            if (mGroupNum == 21)
            {
                tmpBuf = null;
                CommandCompleted();
                return;
            }
            if (mGroupType == WriteMultiCard.GroupTypeA && mGroupNum == 6)
            {
                mGroupNum = 1;
                mGroupType = 1;
                //mStep = 4;//使命令进入下一个阶段
            }

            tmpBuf.WriteByte(mGroupType).WriteByte(mGroupNum);
            Packet(0x03, 0x18, 0x03, 2, tmpBuf);
            CommandReady();
        }

        /// <summary>
        /// 读取AB组的数据
        /// </summary>
        /// <param name="tmpBuf"></param>
        private void ReadGroupAB(IByteBuffer tmpBuf)
        {
            if (mResult.GroupA == null)
            {
                mResult.GroupA = new List<List<ulong>>();
                for (int i = 0; i < 5; i++)
                {
                    mResult.GroupA.Add(new List<ulong>());
                }
            }
            if (mResult.GroupB == null)
            {
                mResult.GroupB = new List<List<ulong>>();
                for (int i = 0; i < 20; i++)
                {
                    mResult.GroupB.Add(new List<ulong>());
                }
            }

            List<UInt64> group = null;
            if (mGroupType == WriteMultiCard.GroupTypeA)
            {
                
                group = mResult.GroupA[mGroupNum - 1];
            }
            if (mGroupType == WriteMultiCard.GroupTypeB)
            {
                group = mResult.GroupB[mGroupNum - 1];
            }


            if (mCardCount > 0)
            {
                ReadGroupABCard(group, tmpBuf);
            }

            if (mGroupType == WriteMultiCard.GroupTypeA && mResult.GroupA[mGroupNum - 1].Count < mCardCount)
            {
                CommandWaitResponse();
            }
            else if (mGroupType == WriteMultiCard.GroupTypeB && mResult.GroupB[mGroupNum - 1].Count < mCardCount)
            {
                CommandWaitResponse();
            }
            else
            { //读下一个组
                //var cmdBuf = FCPacket.CmdData;
                tmpBuf = _Connector.GetByteBufAllocator().Buffer(2);
                mGroupNum++;
                if (mGroupType == WriteMultiCard.GroupTypeA && mGroupNum > 5)
                {
                    mGroupType = WriteMultiCard.GroupTypeB;
                    mGroupNum = 1;
                }

                if (mGroupType == WriteMultiCard.GroupTypeB && mGroupNum > 20)
                {
                    tmpBuf = null;
                    CommandCompleted();
                    return;
                }

                tmpBuf.SetByte(mGroupType, mGroupNum);//改变下一包要读的组号
                CommandReady();
                //
            }
           
        }

    }
}
