using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;
using FCARDIO.Protocol.Door.FC8800.TimeGroup;
using FCARDIO.Protocol.OnlineAccess;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Elevator.FC8864.TimeGroup
{
    /// <summary>
    /// 读取所有开门时段
    /// </summary>
    public class ReadTimeGroup : Protocol.Door.FC8800.TimeGroup.ReadTimeGroup
    {

        
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ReadTimeGroup(INCommandDetail cd) : base(cd)
        {
        }
        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x46, 2);
            _ProcessMax = 64;
            mReadBuffers = new List<IByteBuffer>();
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                buf.Retain();
                mReadBuffers.Add(buf);
                _ProcessStep++;
                fireCommandProcessEvent();
                CommandWaitResponse();
            }

            if (CheckResponse(oPck, 0x56, 0x02, 0xff, 4))
            {
                var buf = oPck.CmdData;
                int iTotal = buf.ReadInt();

                ReadTimeGroup_Result rtgr = new ReadTimeGroup_Result();
                _Result = rtgr;

                SetBytes(rtgr, mReadBuffers);
                ClearBuf();
                CommandCompleted();
            }
        }
    }
}
