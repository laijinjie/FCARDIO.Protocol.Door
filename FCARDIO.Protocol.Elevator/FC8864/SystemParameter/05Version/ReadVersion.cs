using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.Version
{
    /// <summary>
    /// 获取设备版本号
    /// </summary>
    public class ReadVersion : Protocol.Door.FC8800.SystemParameter.Version.ReadVersion
    {
        /// <summary>
        /// 获取设备版本号 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadVersion(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
            CmdIndex = 0x08;
            CheckResponseCmdType = 0x21;
        }

    }
}