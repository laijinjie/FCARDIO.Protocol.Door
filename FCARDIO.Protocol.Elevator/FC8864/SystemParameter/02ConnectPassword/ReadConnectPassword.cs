using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.ConnectPassword
{
    /// <summary>
    /// 获取控制器通讯密码
    /// </summary>
    public class ReadConnectPassword : Protocol.Door.FC8800.SystemParameter.ConnectPassword.ReadConnectPassword
    {
        /// <summary>
        
        /// <summary>
        /// 获取控制器通讯密码 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadConnectPassword(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
            CmdIndex = 0x04;
        }


    }
}