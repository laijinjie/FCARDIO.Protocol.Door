﻿using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取防潜回模式_结果
    /// </summary>
    public class ReadCheckInOut_Result : WriteCheckInOut_Parameter, INCommandResult
    {
    }
}