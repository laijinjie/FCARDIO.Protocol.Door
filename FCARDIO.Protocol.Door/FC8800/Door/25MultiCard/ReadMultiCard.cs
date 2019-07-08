using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.MultiCard
{
    /// <summary>
    /// 读多卡参数
    /// </summary>
    public class ReadMultiCard : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 此命令对应的门端口号
        /// </summary>
        protected int mPort;

        /// <summary>
        /// 命令进度
        /// </summary>
        protected int mStep;

        /// <summary>
        /// 当前正在读取的组号
        /// </summary>
        protected int mGroupNum;

        /// <summary>
        /// 读单个门的多卡参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value"></param>
        public ReadMultiCard(INCommandDetail cd, DoorPort_Parameter value) : base(cd, value) { }

        /// <summary>
        /// 创建命令
        /// </summary>
        protected override void CreatePacket0()
        {
            DoorPort_Parameter model = _Parameter as DoorPort_Parameter;
            mPort = model.Door;
            //1、第一步，读取多卡开门检测模式参数
            Packet(0x03, 0x17, 0x01, 0x01, model.GetBytes(_Connector.GetByteBufAllocator().Buffer(1)));
            _ProcessMax = 27;
            _ProcessStep = 1;
            mStep = 1;
        }


        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            DoorPort_Parameter model = value as DoorPort_Parameter;
            if (model == null) return false;

            return model.checkedParameter();
        }

        /// <summary>
        /// 命令回应处理
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            IByteBuffer tmpBuf;
            int iDoor;
            switch (mStep)
            {
                case 1://读取 多卡开门检测模式参数
                    if (CheckResponse(oPck, 3))
                    {
                        tmpBuf = oPck.CmdData;
                        iDoor = tmpBuf.ReadByte();
                        if (iDoor != mPort)
                        {
                            return;
                        }


                        _ProcessStep++;




                        FCPacket.CmdIndex = 0x18;//修改命令为读取 多卡开门验证方式


                        CommandReady();//设定命令当前状态为准备就绪，等待发送
                        mStep = 2;//使命令进入下一个阶段
                    }

                    break;
                case 2: //读取 多卡开门验证方式 的返回
                    if (CheckResponse(oPck, 4))
                    {
                        tmpBuf = oPck.CmdData;
                        iDoor = tmpBuf.ReadByte();
                        if (iDoor != mPort)
                        {
                            return;
                        }


                        _ProcessStep++;



                        //开启读取多卡开门AB组内容
                        tmpBuf = _Connector.GetByteBufAllocator().Buffer(2);
                        tmpBuf.WriteByte(0).WriteByte(1);
                        mGroupNum = 1;
                        Packet(0x03, 0x18, 0x03, 2, tmpBuf);


                        CommandReady();//设定命令当前状态为准备就绪，等待发送
                        mStep = 3;//使命令进入下一个阶段
                    }

                    break;
                case 3://读取A组的数据
                    if (CheckResponse(oPck))
                    {
                        tmpBuf = oPck.CmdData;
                        var iGroupType = tmpBuf.ReadByte();//组类别：0--A组；
                        var iGroupNum = tmpBuf.ReadByte(); //组号：取值范围 1 - 5；
                        var iCount = tmpBuf.ReadByte();
                        if (iGroupType != 0)
                        {
                            return;
                        }
                        if (iGroupNum != mGroupNum)
                        {
                            return;
                        }

                        if (iCount > 0)
                        {
                            //将卡号取出，并序列号到实体
                        }

                        //读取下一个组
                        _ProcessStep++;

                        if (iGroupNum == 5)
                        {
                            //读取B组
                            tmpBuf.SetByte(0, 1);//组类别：1--B组
                            tmpBuf.SetByte(1, 1); //组号
                            iGroupNum = 1;
                            mStep = 4;//使命令进入下一个阶段
                        }
                        else
                        {
                            tmpBuf = FCPacket.CmdData;

                            iGroupNum++;
                            tmpBuf.SetByte(1, iGroupNum);
                        }

                        CommandReady();//设定命令当前状态为准备就绪，等待发送




                    }
                    if (CheckResponse(oPck, 0x03, 0x18, 0x53))
                    {

                    }

                    break;
                case 4://B组
                    if (CheckResponse(oPck))
                    {
                        tmpBuf = oPck.CmdData;
                        var iGroupType = tmpBuf.ReadByte();//组类别：0--A组；
                        var iGroupNum = tmpBuf.ReadByte(); //组号：取值范围 1 - 5；
                        var iCount = tmpBuf.ReadByte();
                        if (iGroupType != 0)
                        {
                            return;
                        }
                        if (iGroupNum != mGroupNum)
                        {
                            return;
                        }

                        if (iCount > 0)
                        {
                            //将卡号取出，并序列号到实体
                        }

                        //读取下一个组
                        _ProcessStep++;

                        if (iGroupNum == 20)
                        {
                            //读固定组合
                            mStep = 5;
                        }
                        else
                        {
                            tmpBuf = FCPacket.CmdData;

                            iGroupNum++;
                            tmpBuf.SetByte(1, iGroupNum);
                        }

                        CommandReady();//设定命令当前状态为准备就绪，等待发送




                    }

                    break;
                case 5://读取固定组合
                       //固定组合读完了
                       //命令全部发送完毕
                    CommandCompleted();

                    break;
                default:
                    break;
            }
            if (CheckResponse_OK(oPck))
            {

                //继续发下一包
                CommandNext1(oPck);
            }
            else if (CheckResponse(oPck, 0x07, 0x04, 0xFF, oPck.DataLen))
            {//检查是否不是错误返回值

                //缓存错误返回值
                if (mBufs == null)
                {
                    mBufs = new Queue<IByteBuffer>();
                }

                //mBufs.Enqueue(oPck.CmdData);

                //继续发下一包
                CommandNext1(oPck);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            throw new NotImplementedException();
        }



    }
}
