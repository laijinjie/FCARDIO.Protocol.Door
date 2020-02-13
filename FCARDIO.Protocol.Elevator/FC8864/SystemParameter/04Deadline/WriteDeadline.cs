using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.Deadline
{
    /// <summary>
    /// 设置设备有效期
    /// </summary>
    public class WriteDeadline : Protocol.Door.FC8800.SystemParameter.Deadline.WriteDeadline
    {
        /// <summary>
        /// 设置设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含设备有效期</param>
        public WriteDeadline(INCommandDetail cd, WriteDeadline_Parameter par) : base(cd, par) {
            CmdType = 0x41;
            CmdIndex = 0x07;
        }

        
    }
}