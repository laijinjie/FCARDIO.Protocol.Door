using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Door.FC8800.Door.SensorAlarmSetting
{
    public class SensorAlarmSetting_Result
        :WriteSensorAlarmSetting_Parameter, INCommandResult
    {
    }
}
