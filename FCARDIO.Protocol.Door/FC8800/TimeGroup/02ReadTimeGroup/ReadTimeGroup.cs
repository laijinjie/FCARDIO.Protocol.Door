using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Door.FC8800.TimeGroup
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadTimeGroup : FC8800Command_ReadParameter
    {

        /// <summary>
        /// 读取到的开门时段缓冲
        /// </summary>
        private List<IByteBuffer> mReadBuffers;

        public ReadTimeGroup(INCommandDetail cd) : base(cd, null)
        {

        }
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                buf.Retain();
                Console.WriteLine(oPck.DataLen);
                Console.WriteLine(ByteBufferUtil.HexDump(buf));
                mReadBuffers.Add(buf);
                CommandWaitResponse();
            }

            if (CheckResponse(oPck, 0x06, 0x02, 0xff, 4))
            {
                var buf = oPck.CmdData;
                int iTotal = buf.ReadInt();

                ReadTimeGroup_Result rtgr = new ReadTimeGroup_Result();
                _Result = rtgr;


                rtgr.SetBytes(iTotal, mReadBuffers);
                ClearBuf();
                CommandCompleted();
            }
        }

        protected override void CreatePacket0()
        {
            Packet(6, 2);
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
