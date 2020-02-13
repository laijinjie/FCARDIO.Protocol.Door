using FCARDIO.Core.Command;
using System;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置主板蜂鸣器
    /// </summary>
    public class WriteBuzzer : Protocol.Door.FC8800.SystemParameter.FunctionParameter.WriteBuzzer
    {
        /// <summary>
        /// 设置主板蜂鸣器 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par"></param>
        public WriteBuzzer(INCommandDetail cd, WriteBuzzer_Parameter par) : base(cd, par) {
            CmdType = 0x41;
            CmdIndex = 0x0A;
            CmdPar = 0x08;
        }

    }
}