using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Core.Packet;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Door.MultiCard
{
    /// <summary>
    /// 写多卡参数
    /// </summary>
    public class WriteMultiCard : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 多卡参数
        /// </summary>
        private WriteMultiCard_Parameter mMultiCardPar ;

        /// <summary>
        /// 当前命令步骤
        /// </summary>
        protected int Step { get; set; }


        /// <summary>
        /// 多卡A组类型
        /// </summary>
        public const int GroupTypeA = 0;
        /// <summary>
        /// 多卡B组类型
        /// </summary>
        public const int GroupTypeB = 1;

        /// <summary>
        /// 当前正在写入的组合类型
        /// </summary>
        protected int mGroupType;

        /// <summary>
        /// 当前正在写入的组号
        /// </summary>
        protected int mGroupNum;


        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value"></param>
        public WriteMultiCard(INCommandDetail cd, WriteMultiCard_Parameter value) : base(cd, value) {
            
            mMultiCardPar = value;
        }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteMultiCard_Parameter model = value as WriteMultiCard_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建第一个数据包
        /// </summary>
        protected override void CreatePacket0()
        {
            //发送 设置多卡开门检测模式参数
            Packet(0x03, 0x17, 0x00, 3, mMultiCardPar.CheckMode_GetBytes( GetCmdDataBuf(3)));
            Step = 1;
            if (mMultiCardPar.mProtocolType.Contains("MC58"))
            {
                _ProcessMax = 2;
            }
            else
            {
                switch (mMultiCardPar.VerifyType)
                {
                    case 1:
                        _ProcessMax = 27;
                        break;
                    case 2:
                        _ProcessMax = 12;
                        break;
                    default:
                        _ProcessMax = 2;
                        break;
                }

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
                    }
                   
                    break;
                case 2:
                    //检查需要发送的内容
                    switch (mMultiCardPar.VerifyType)
                    {
                        case 1://多卡AB组
                            //开始写A组
                            mGroupType = GroupTypeA;
                            mGroupNum = 1;
                            WriteMultiCard_GroupAB();

                            Step = 3;
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
                    mGroupNum++;
                    if( mGroupType == GroupTypeA &&  mGroupNum>5)
                    {
                        mGroupType = GroupTypeB;
                        mGroupNum = 1;
                    }

                    if(mGroupType == GroupTypeB && mGroupNum > 20)
                    {
                        CommandCompleted();
                        return;
                    }

                    WriteMultiCard_GroupAB();



                    break;
                case 4://继续写固定组
                    mGroupNum++;
                    if ( mGroupNum > 10)
                    {
                        CommandCompleted();
                        return;
                    }
                    WriteMultiCard_GroupFix();
                    break;
                default:
                    break;
            }


        }



        /// <summary>
        /// 二十七 多卡开门A组设置
        /// </summary>
        protected virtual void WriteMultiCard_GroupAB()
        {
            //先找到对应的组
            List<UInt64> group=null;
            int iMax = 50;
            int iCount = 0;

            if (mGroupType == GroupTypeA) group = mMultiCardPar.GroupA[mGroupNum-1];
            if (mGroupType == GroupTypeB)
            {
                iMax = 100;
                group = mMultiCardPar.GroupB[mGroupNum - 1];
            }
                    
            var buf = GetCmdDataBuf(403);
            iCount = group.Count;
            if (iCount > iMax) iCount = iMax;

            buf.WriteByte(mGroupType);
            buf.WriteByte(mGroupNum);
            buf.WriteByte(iCount);

            for (int i = 0; i < iCount; i++)
            {
                buf.WriteInt((int)group[i]);
            }

            Packet(0x03, 0x18, 0x02, (uint)buf.ReadableBytes, buf);
            _ProcessStep++;
            CommandReady();

        }


        /// <summary>
        /// 写入固定多卡组
        /// </summary>
        protected virtual void WriteMultiCard_GroupFix()
        {
            //先找到对应的组
            MultiCard_GroupFix Fix;
            List<UInt64> group = null;
            int iMax = 8;
            int iCount = 0;

            Fix = mMultiCardPar.GroupFix[mGroupNum - 1];
            group = Fix.CardList;

            var buf = GetCmdDataBuf(36);
            iCount = group.Count;
            if (iCount > iMax) iCount = iMax;

            buf.WriteByte(mMultiCardPar.DoorNum);
            buf.WriteByte(mGroupNum);
            buf.WriteByte(iCount);
            buf.WriteByte(Fix.GroupType);

            for (int i = 0; i < iCount; i++)
            {
                buf.WriteInt((int)group[i]);
            }


            Packet(0x03, 0x12, 0x02, (uint)buf.ReadableBytes, buf);
            _ProcessStep++;
            CommandReady();

        }





        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            
        }

        /// <summary>
        /// 获取一个指定大小的Buf
        /// </summary>
        /// <param name="iSize"></param>
        /// <returns></returns>
        protected IByteBuffer GetCmdDataBuf(int iSize)
        {
            var buf = FCPacket?.CmdData;
            var acl = _Connector.GetByteBufAllocator();
            
            if(buf==null)
            {
                
                buf = acl.Buffer(iSize);

            }
            buf.Clear();
            if (buf.WritableBytes < iSize )
            {
                buf = acl.Buffer(iSize);
            }
            else
            {
                buf = acl.Buffer(iSize);
            }
           
            return buf;
        }
    }
}
