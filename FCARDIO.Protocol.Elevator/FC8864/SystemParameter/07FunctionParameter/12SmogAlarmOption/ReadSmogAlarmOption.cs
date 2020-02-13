using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取烟雾报警参数
    /// </summary>
    public class ReadSmogAlarmOption : Protocol.Door.FC8800.SystemParameter.FunctionParameter.ReadSmogAlarmOption
    {
        /// <summary>
        /// 获取烟雾报警参数 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadSmogAlarmOption(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
            CmdIndex = 0x0A;
            CmdPar = 0x8B;
            CheckResponseCmdType = 0x21;
        }

    }
}