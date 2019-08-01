﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Fingerprint.Alarm.AntiDisassemblyAlarm
{
    /// <summary>
    /// 读取 防拆报警功能
    /// </summary>
    public class ReadAntiDisassemblyAlarm : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadAntiDisassemblyAlarm(INCommandDetail cd) : base(cd, null) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x04, 0x03, 0x01);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x01))
            {
                var buf = oPck.CmdData;
                ReadAntiDisassemblyAlarm_Result rst = new ReadAntiDisassemblyAlarm_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}
