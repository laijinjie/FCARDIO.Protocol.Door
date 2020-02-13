using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FireAlarm
{
    /// <summary>
    /// 解除消防报警
    /// </summary>
    public class CloseFireAlarm : Protocol.Door.FC8800.SystemParameter.FireAlarm.CloseFireAlarm
    {
        /// <summary>
        /// 解除消防报警 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public CloseFireAlarm(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
        }

    }
}