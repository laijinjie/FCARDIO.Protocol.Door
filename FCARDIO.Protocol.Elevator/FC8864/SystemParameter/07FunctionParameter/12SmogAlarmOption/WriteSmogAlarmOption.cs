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
    /// 设置烟雾报警参数
    /// </summary>
    public class WriteSmogAlarmOption : Protocol.Door.FC8800.SystemParameter.FunctionParameter.WriteSmogAlarmOption
    {
        /// <summary>
        /// 设置烟雾报警参数 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含烟雾报警参数</param>
        public WriteSmogAlarmOption(INCommandDetail cd, WriteSmogAlarmOption_Parameter par) : base(cd, par) {
            CmdType = 0x41;
            CmdIndex = 0x0A;
            CmdPar = 0x0B;
        }

    }
}