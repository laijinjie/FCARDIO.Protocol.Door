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
    /// 设置主板蜂鸣器
    /// </summary>
    public class WriteBuzzer : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 设置主板蜂鸣器 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含主板蜂鸣器</param>
        public WriteBuzzer(INCommandDetail cd, WriteBuzzer_Parameter par) : base(cd, par) {
            CmdType = 0x01;
            CmdIndex = 0x0A;
            CmdPar = 0x0A;
        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteBuzzer_Parameter model = value as WriteBuzzer_Parameter;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            WriteBuzzer_Parameter model = _Parameter as WriteBuzzer_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(model.GetDataLen());

            Packet(CmdType, CmdIndex, CmdPar, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
        }
    }
}