﻿using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.BalcklistAlarmOption
{
    /// <summary>
    /// 获取黑名单报警功能开关_结果
    /// </summary>
    public class ReadBalcklistAlarmOption_Result : WriteBalcklistAlarmOption_Parameter, INCommandResult
    {
    }
}