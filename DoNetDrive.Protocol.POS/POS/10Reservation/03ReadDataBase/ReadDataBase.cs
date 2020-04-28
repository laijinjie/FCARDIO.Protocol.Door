using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.POS.Protocol;
using DoNetDrive.Protocol.POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.POS.Reservation.ReadDataBase
{
    public class ReadDataBase : Read_Command
    {
        /// <summary>
        /// 读取到的密码缓冲
        /// </summary>
        protected List<IByteBuffer> mReadBuffers;

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="detail"></param>
        public ReadDataBase(DESDriveCommandDetail detail) : base(detail, null) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            return true;
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x0A, 0x03, 0x00, 0x00, null);
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(DESPacket oPck)
        {
            if (CheckResponse(oPck, 0x03))
            {
                var buf = oPck.CmdData;
                buf.Retain();
                mReadBuffers.Add(buf);
                CommandWaitResponse();
            }

            if (CheckResponse(oPck,0x03, 0xff,2))
            {
                var buf = oPck.CmdData;
                int iTotal = buf.ReadInt();
                _ProcessMax = iTotal;
                List<ReservationDetail> reservationDetailList = new List<ReservationDetail>(iTotal);
                foreach (IByteBuffer tmpbuf in mReadBuffers)
                {
                    int iCount = tmpbuf.ReadInt();
                    for (int i = 0; i < iCount; i++)
                    {
                        ReservationDetail dtl = new ReservationDetail();
                        dtl.SetBytes(tmpbuf);
                        reservationDetailList.Add(dtl);
                    }
                    _ProcessStep += iCount;
                    fireCommandProcessEvent();
                }

                ReadDataBase_Result rst = new ReadDataBase_Result(reservationDetailList);
                _Result = rst;

                ClearBuf();
                CommandCompleted();
            }
        }

        /// <summary>
        /// 命令重发时需要处理的函数
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }

        /// <summary>
        /// 命令释放时需要处理的函数
        /// </summary>
        protected override void Release1()
        {
            return;
        }

        /// <summary>
        /// 清空缓冲区
        /// </summary>
        protected void ClearBuf()
        {
            foreach (IByteBuffer buf in mReadBuffers)
            {
                buf.Release();
            }
            mReadBuffers.Clear();
            mReadBuffers = null;
        }
    }
}
