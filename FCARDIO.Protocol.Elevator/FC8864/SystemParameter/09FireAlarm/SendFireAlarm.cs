using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FireAlarm
{
    /// <summary>
    /// 通知设备触发消防报警
    /// </summary>
    public class SendFireAlarm : Protocol.Door.FC8800.SystemParameter.FireAlarm.SendFireAlarm
    {
        /// <summary>
        /// 通知设备触发消防报警 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public SendFireAlarm(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
            
        }

    }
}