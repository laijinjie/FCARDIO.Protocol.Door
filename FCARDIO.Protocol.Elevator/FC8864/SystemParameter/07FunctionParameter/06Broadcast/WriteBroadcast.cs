using FCARDIO.Core.Command;
using System;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置语音播报语音段开关
    /// </summary>
    public class WriteBroadcast : Protocol.Door.FC8800.SystemParameter.FunctionParameter.WriteBroadcast
    {
        /// <summary>
        /// 设置语音播报语音段开关 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par"></param>
        public WriteBroadcast(INCommandDetail cd, WriteBroadcast_Parameter par) : base(cd, par) {
            CmdType = 0x41;
            CmdIndex = 0x0A;
            CmdPar = 0x05;
        }
    }
}