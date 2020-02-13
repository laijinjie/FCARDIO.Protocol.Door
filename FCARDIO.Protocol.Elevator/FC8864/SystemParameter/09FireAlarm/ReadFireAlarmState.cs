using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FireAlarm
{
    /// <summary>
    /// 读取消防报警状态
    /// </summary>
    public class ReadFireAlarmState : Protocol.Door.FC8800.SystemParameter.FireAlarm.ReadFireAlarmState
    {
       
        /// <summary>
        /// 读取消防报警状态 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadFireAlarmState(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
        }

    }
}