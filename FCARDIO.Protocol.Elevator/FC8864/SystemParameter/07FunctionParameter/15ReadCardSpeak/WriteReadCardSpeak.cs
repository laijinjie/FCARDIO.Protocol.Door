﻿using FCARDIO.Core.Command;
using System;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置定时读卡播报语音消息参数
    /// </summary>
    public class WriteReadCardSpeak : Write_Command
    {
        /// <summary>
        /// 设置定时读卡播报语音消息参数 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par"></param>
        public WriteReadCardSpeak(INCommandDetail cd, WriteReadCardSpeak_Parameter par) : base(cd, par) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteReadCardSpeak_Parameter model = value as WriteReadCardSpeak_Parameter;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }

        /// <summary>
        /// 拼装命令
        /// </summary>
        protected override void CreatePacket0()
        {
            WriteReadCardSpeak_Parameter model = _Parameter as WriteReadCardSpeak_Parameter;
            Packet(0x41, 0x0A, 0x0E, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }
    }
}