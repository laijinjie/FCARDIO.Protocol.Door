﻿using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Fingerprint.Door.Remote
{
    /// <summary>
    /// 锁定门
    /// </summary>
    public class LockDoor : OpenDoor
    {
        /// <summary>
        /// 锁定门
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含锁定门参数</param>
        public LockDoor(INCommandDetail cd, Remote_Parameter par) : base(cd, par) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x04, 0x00);
        }
    }
}
