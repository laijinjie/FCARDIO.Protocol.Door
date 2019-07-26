﻿using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取读卡器密码键盘启用功能开关_结果
    /// </summary>
    public class ReadKeyboard_Result : WriteKeyboard_Parameter, INCommandResult
    {
    }
}