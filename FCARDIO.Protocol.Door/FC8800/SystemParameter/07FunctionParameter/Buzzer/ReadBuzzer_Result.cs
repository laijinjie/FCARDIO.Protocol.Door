using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取主板蜂鸣器_结果
    /// </summary>
    public class ReadBuzzer_Result : INCommandResult
    {
        public byte Buzzer;

        public ReadBuzzer_Result(byte _Buzzer)
        {
            Buzzer = _Buzzer;
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