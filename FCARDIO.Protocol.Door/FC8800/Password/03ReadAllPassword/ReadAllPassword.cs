using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 读取所有密码
    /// </summary>
    public class ReadAllPassword : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 读取到的节假日缓冲
        /// </summary>
        protected List<IByteBuffer> mReadBuffers;
        public ReadAllPassword(INCommandDetail cd) : base(cd)
        {

        }
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                buf.Retain();
                mReadBuffers.Add(buf);
                CommandWaitResponse();
            }

            if (CheckResponse(oPck, 5, 3, 0xff, 4))
            {
                var buf = oPck.CmdData;
                int iTotal = buf.ReadInt();

                ReadAllPassword_Result rst = new ReadAllPassword_Result();
                _Result = rst;


                rst.SetBytes(iTotal, mReadBuffers);
                ClearBuf();
                CommandCompleted();
            }
        }

        protected override void CreatePacket0()
        {
            Packet(5, 3);
            mReadBuffers = new List<IByteBuffer>();
        }

        /// <summary>
        /// 命令重发时，对命令中一些缓冲做清空或参数重置<br/>
        /// 此命令一般情况下不需要实现！
        /// </summary>
        protected override void CommandReSend()
        {

            ClearBuf();


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
        }
    }
}
