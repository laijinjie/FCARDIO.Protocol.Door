﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Door.FC8800.Door.OvertimeAlarmSetting
{
    /// <summary>
    /// 开门超时报警 返回结果
    /// </summary>
    public class OvertimeAlarmSetting_Result
        :WriteOvertimeAlarmSetting_Parameter,INCommandResult
    {

    }
}
