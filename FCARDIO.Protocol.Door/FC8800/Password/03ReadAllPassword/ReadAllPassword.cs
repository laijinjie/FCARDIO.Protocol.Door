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
    public class ReadAllPassword<T> : FC8800Command_ReadParameter where T : PasswordDetail, new()
    {
        /// <summary>
        /// 读取到的节假日缓冲
        /// </summary>
        protected List<IByteBuffer> mReadBuffers;

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ReadAllPassword(INCommandDetail cd) : base(cd)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPck"></param>
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

                ReadAllPassword_Result<T> rst = new ReadAllPassword_Result<T>();
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
