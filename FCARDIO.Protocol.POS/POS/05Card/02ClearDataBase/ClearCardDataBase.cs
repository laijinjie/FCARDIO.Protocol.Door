﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.POS.Card
{
    /// <summary>
    /// 清空所有名单命令
    /// </summary>
    public class ClearCardDataBase : Read_Command
    {
        /// <summary>
        /// 构造命令，无需其他参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ClearCardDataBase(INCommandDetail cd) : base(cd, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x05, 2);
        }
    }
}
