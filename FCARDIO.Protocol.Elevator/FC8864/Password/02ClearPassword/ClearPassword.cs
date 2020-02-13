using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.Password
{
    /// <summary>
    /// 清空所有密码
    /// </summary>
    public class ClearPassword : Protocol.Door.FC8800.Password.ClearPassword
    {
        /// <summary>
        /// 构造命令，无需其他参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ClearPassword(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x45;
        }
    }
}
