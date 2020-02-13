using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置消防报警参数
    /// </summary>
    public class WriteFireAlarmOption : Protocol.Door.FC8800.SystemParameter.FunctionParameter.WriteFireAlarmOption
    {
        /// <summary>
        /// 设置消防报警参数 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含消防报警参数</param>
        public WriteFireAlarmOption(INCommandDetail cd, WriteFireAlarmOption_Parameter par) : base(cd, par) {
            CmdType = 0x41;
            CmdIndex = 0x0A;
            CmdPar = 0x04;
        }

    }
}