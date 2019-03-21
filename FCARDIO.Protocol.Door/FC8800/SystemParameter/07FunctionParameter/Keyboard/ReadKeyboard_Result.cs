﻿using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取读卡器密码键盘启用功能开关_结果
    /// </summary>
    public class ReadKeyboard_Result : INCommandResult
    {
        public byte Keyboard;

        public ReadKeyboard_Result(byte _Keyboard)
        {
            Keyboard = _Keyboard;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            return;
        }
    }
}