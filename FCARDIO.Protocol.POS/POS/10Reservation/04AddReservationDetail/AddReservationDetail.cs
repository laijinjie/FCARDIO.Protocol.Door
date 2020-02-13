using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.POS.Reservation.AddReservationDetail
{
    /// <summary>
    /// 添加订餐信息
    /// </summary>
    public class AddReservationDetail : Write_Command
    {
        /// <summary>
        /// 1个写入参数长度
        /// </summary>
        protected int mParDataLen;

        /// <summary>
        /// 每次上传数量
        /// </summary>
        protected int mBatchCount = 5;

        /// <summary>
        /// 已上传数量
        /// </summary>
        protected int mIndex;

        /// <summary>
        /// 默认的缓冲区大小
        /// </summary>
        protected int MaxBufSize = 350;

        /// <summary>
        /// 需要写入数
        /// </summary>
        protected int maxCount = 0;

        /// <summary>
        /// 当前命令进度
        /// </summary>
        protected int mStep = 0;

        /// <summary>
        /// 指示当前命令进行的步骤
        /// </summary>
        protected int mWriteCardIndex = 0;

        /// <summary>
        /// 保存待上传订餐列表的参数
        /// </summary>
        protected AddReservationDetail_Parameter _ReservationPar = null;

        public AddReservationDetail(INCommandDetail cd, AddReservationDetail_Parameter par) : base(cd,par)
        {

        }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            _ReservationPar = value as AddReservationDetail_Parameter;
            if (_ReservationPar == null) return false;
            return _ReservationPar.checkedParameter();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            mStep = 1;
            Packet(0x0A, 0x04, 0x00);

            //var buf = GetNewCmdDataBuf(MaxBufSize);
            //WriteReservationToBuf(buf);
            //Packet(0x5, 0x4, 0x00, (uint)buf.ReadableBytes, buf);
        }

        /// <summary>
        /// 命令回应处理
        /// </summary>
        /// <param name="readPacket"></param>
        protected override void CommandNext(Core.Packet.INPacket readPacket)
        {
            OnlineAccessPacket oPck = readPacket as OnlineAccessPacket;
            if (oPck == null) return;
            if (oPck.Code != FCPacket.Code) return;//信息代码不一致，不是此命令的后续
            if (CheckResponse_PasswordErr(oPck))
            {
                base.CommandNext(readPacket);
                return;
            }
            if (CheckResponse_CheckSumErr(oPck))
            {
                base.CommandNext(readPacket);
                return;
            }

            //继续检查响应是否为命令的下一步骤
            CommandNext0(oPck);

        }

        /// <summary>
        /// 重写父类对处理返回值的定义
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            switch (mStep)
            {
                case 1://处理开始写入指令返回
                    if (CheckResponse_OK(oPck))
                    {
                        _ProcessStep++;
                        //硬件已准备就绪，开始写入卡

                        //创建一个通讯缓冲区
                        var buf = GetNewCmdDataBuf(MaxBufSize);
                        WriteReservationToBuf(buf);
                        Packet(0x0A, 0x04, 0x01, (uint)buf.ReadableBytes, buf);
                        CommandReady();//设定命令当前状态为准备就绪，等待发送
                        mStep = 2;//使命令进入下一个阶段
                        return;
                    }
                    break;
                case 2:
                    if (CheckResponse_OK(oPck))
                    {
                        //继续发下一包
                        CommandNext1(oPck);
                    }
                    break;
                
                default:
                    break;
            }

        }


        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (IsWriteOver())
            {
                CommandCompleted();
            }
            else
            {
                //未发送完毕，继续发送
                var buf = GetCmdBuf();
                WriteReservationToBuf(buf);
                FCPacket.DataLen = buf.ReadableBytes;
                CommandReady();//设定命令当前状态为准备就绪，等待发送
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public IByteBuffer WriteReservationToBuf(IByteBuffer databuf)
        {

            var lst = _ReservationPar.ReservationDetailList;
            int iCount = lst.Count;//获取列表总长度
            iCount = iCount - mIndex;//计算未上传总数

            int iLen = iCount;
            if (iLen > mBatchCount)
            {
                iLen = mBatchCount;
            }

            databuf.Clear();

            databuf.WriteInt(iLen);
            for (int i = 0; i < iLen; i++)
            {
                //WritePasswordBodyToBuf(databuf, lst[mIndex + i]);
                lst[mIndex + i].GetBytes(databuf);
            }

            mIndex += iLen;
            _ProcessStep += iLen;
            return databuf;
        }

        protected bool IsWriteOver()
        {
            int iCount = _ReservationPar.ReservationDetailList.Count;//获取列表总长度

            return (iCount - mWriteCardIndex) == 0;
        }
    }
}
