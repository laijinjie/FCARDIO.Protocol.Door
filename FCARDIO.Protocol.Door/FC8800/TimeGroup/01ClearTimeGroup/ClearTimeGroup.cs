﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.TimeGroup
{
    /// <summary>
    /// 清空所有开门时段
    /// </summary>
    public class ClearTimeGroup : FC8800Command_ReadParameter
    {
        public ClearTimeGroup(INCommandDetail cd) : base(cd, null)
        {

        }
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x06, 0x01);
        }
    }
}
