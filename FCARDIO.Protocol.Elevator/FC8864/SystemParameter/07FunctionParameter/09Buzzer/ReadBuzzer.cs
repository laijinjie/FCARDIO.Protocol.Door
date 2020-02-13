using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取主板蜂鸣器
    /// </summary>
    public class ReadBuzzer : Protocol.Door.FC8800.SystemParameter.FunctionParameter.ReadBuzzer
    {
        /// <summary>
        /// 获取主板蜂鸣器 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadBuzzer(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
            CmdIndex = 0x0A;
            CmdPar = 0x88;
            CheckResponseCmdType = 0x21;
        }

    }
}