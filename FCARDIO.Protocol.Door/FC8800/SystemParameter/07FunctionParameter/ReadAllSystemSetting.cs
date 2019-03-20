﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取所有系统参数
    /// </summary>
    public class ReadAllSystemSetting : FC8800Command
    {
        public ReadAllSystemSetting(INCommandDetail cd) : base(cd, null) { }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            return true;
        }

        protected override void CreatePacket0()
        {
            Packet(0x01, 0x0A, 0xFF);
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }

        protected override void CommandReSend()
        {
            return;
        }

        protected override void Release1()
        {
            return;
        }
    }
}