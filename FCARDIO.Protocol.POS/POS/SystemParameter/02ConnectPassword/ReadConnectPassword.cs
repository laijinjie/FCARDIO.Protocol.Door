using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.POS.SystemParameter.ConnectPassword
{
    /// <summary>
    /// 获取控制器通讯密码
    /// </summary>
    public class ReadConnectPassword : Door.FC8800.SystemParameter.ConnectPassword.ReadConnectPassword
    {
        
        /// <summary>
        /// 获取控制器通讯密码 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadConnectPassword(INCommandDetail cd) : base(cd) {
            CmdType = 0x01;
        }

    }
}
