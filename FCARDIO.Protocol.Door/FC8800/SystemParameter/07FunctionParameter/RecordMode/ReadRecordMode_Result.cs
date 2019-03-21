﻿using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取记录存储方式_结果
    /// </summary>
    public class ReadRecordMode_Result : INCommandResult
    {
        public byte Mode;

        public ReadRecordMode_Result(byte _Mode)
        {
            Mode = _Mode;
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