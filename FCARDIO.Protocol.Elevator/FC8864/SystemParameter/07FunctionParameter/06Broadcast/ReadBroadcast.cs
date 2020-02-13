using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取语音播报语音段开关
    /// </summary>
    public class ReadBroadcast : Protocol.Door.FC8800.SystemParameter.FunctionParameter.ReadBroadcast
    {
        /// <summary>
        /// 获取设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadBroadcast(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
            CmdIndex = 0x0A;
            CmdPar = 0x85;
            CheckResponseCmdType = 0x21;
        }

    }
}