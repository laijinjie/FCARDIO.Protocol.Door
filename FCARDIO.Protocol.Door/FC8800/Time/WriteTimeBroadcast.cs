﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.Util;
using System;

namespace FCARDIO.Protocol.Door.FC8800.Time
{
    /// <summary>
    /// 校时广播
    /// </summary>
    public class WriteTimeBroadcast : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 校时广播
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public WriteTimeBroadcast(INCommandDetail cd) : base(cd, null) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            return true;
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(0x07);

            byte[] Datebuf = new byte[7];

            TimeUtil.DateToBCD_ssmmhhddMMwwyy(Datebuf, DateTime.Now);

            buf.WriteBytes(Datebuf);

            Packet(0x02, 0x02, 0x01, 0x07, buf);
        }
    }
}