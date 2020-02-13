using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.SystemStatus
{
    /// <summary>
    /// 获取设备运行信息
    /// </summary>
    public class ReadSystemStatus : Protocol.Door.FC8800.SystemParameter.SystemStatus.ReadSystemStatus
    {
        /// <summary>
        /// 获取设备运行信息 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadSystemStatus(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
            CmdIndex = 0x09;
            CheckResponseCmdType = 0x21;
        }
    }
}