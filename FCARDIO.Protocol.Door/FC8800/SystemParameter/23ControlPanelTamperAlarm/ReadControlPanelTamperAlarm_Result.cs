﻿using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.ControlPanelTamperAlarm
{
    /// <summary>
    /// 读取控制板防拆报警功能开关_结果
    /// </summary>
    public class ReadControlPanelTamperAlarm_Result : WriteControlPanelTamperAlarm_Parameter, INCommandResult
    {
    }
}