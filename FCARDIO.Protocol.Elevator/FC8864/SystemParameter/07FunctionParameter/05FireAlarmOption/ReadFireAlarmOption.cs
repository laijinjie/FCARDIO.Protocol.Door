using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取消防报警参数
    /// </summary>
    public class ReadFireAlarmOption : Protocol.Door.FC8800.SystemParameter.FunctionParameter.ReadFireAlarmOption
    {
        /// <summary>
        /// 获取设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadFireAlarmOption(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
            CmdIndex = 0x0A;
            CmdPar = 0x84;
            CheckResponseCmdType = 0x21;
        }

        
    }
}