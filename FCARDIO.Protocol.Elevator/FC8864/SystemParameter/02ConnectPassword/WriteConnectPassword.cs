using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.ConnectPassword
{
    /// <summary>
    /// 设置控制器通讯密码
    /// </summary>
    public class WriteConnectPassword : Protocol.Door.FC8800.SystemParameter.ConnectPassword.WriteConnectPassword
    {
        /// <summary>
        /// 设置控制器通讯密码 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含命令所需新的通讯密码</param>
        public WriteConnectPassword(INCommandDetail cd, Password_Parameter par) : base(cd, par) {
            CmdType = 0x41;
            CmdIndex = 0x03;
        }

       
    }
}