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
    /// 获取设备有效期
    /// </summary>
    public class ReadDeadline : Protocol.Door.FC8800.SystemParameter.Deadline.ReadDeadline
    {
        /// <summary>
        /// 获取设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadDeadline(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
            CmdIndex = 0x07;
            CheckResponseCmdType = 0x21;
        }

    }
}