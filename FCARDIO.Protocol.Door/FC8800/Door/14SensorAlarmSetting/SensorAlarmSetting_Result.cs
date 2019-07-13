using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Door.FC8800.Door.SensorAlarmSetting
{
    /// <summary>
    /// 门磁报警功能 返回结果
    /// </summary>
    public class SensorAlarmSetting_Result
        :WriteSensorAlarmSetting_Parameter, INCommandResult
    {
    }
}
