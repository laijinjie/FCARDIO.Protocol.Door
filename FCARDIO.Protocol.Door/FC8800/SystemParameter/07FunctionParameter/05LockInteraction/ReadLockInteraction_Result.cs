﻿using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取互锁参数_结果
    /// </summary>
    public class ReadLockInteraction_Result : WriteLockInteraction_Parameter, INCommandResult
    {
    }
}