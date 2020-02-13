﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FireAlarm
{
    /// <summary>
    /// 解除消防报警
    /// </summary>
    public class CloseFireAlarm : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 解除消防报警 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public CloseFireAlarm(INCommandDetail cd) : base(cd) {
            CmdType = 0x01;
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(CmdType, 0x0C, 0x01);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }
    }
}