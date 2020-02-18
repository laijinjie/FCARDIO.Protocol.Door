﻿using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800.SystemParameter.Deadline;
using DoNetDrive.Protocol.Door8800;
using DoNetDrive.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Elevator.FC8864.SystemParameter.Deadline
{
    /// <summary>
    /// 获取设备有效期
    /// </summary>
    public class ReadDeadline : Protocol.Door.Door8800.SystemParameter.Deadline.ReadDeadline
    {
        /// <summary>
        /// 获取设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadDeadline(INCommandDetail cd) : base(cd) {

        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x41, 0x07);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 2))
            {
                var buf = oPck.CmdData;
                var rst = new ReadDeadline_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}