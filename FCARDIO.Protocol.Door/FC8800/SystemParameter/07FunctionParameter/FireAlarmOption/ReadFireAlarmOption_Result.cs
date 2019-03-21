using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取消防报警参数_结果
    /// </summary>
    public class ReadFireAlarmOption_Result : INCommandResult
    {
        public byte Option;

        public ReadFireAlarmOption_Result(byte _Option)
        {
            Option = _Option;
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