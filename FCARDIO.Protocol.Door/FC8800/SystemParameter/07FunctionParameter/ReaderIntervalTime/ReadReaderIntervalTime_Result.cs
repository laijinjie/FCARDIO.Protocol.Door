using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取读卡间隔时间_结果
    /// </summary>
    public class ReadReaderIntervalTime_Result : INCommandResult
    {
        public ushort IntervalTime;

        public ReadReaderIntervalTime_Result(ushort _IntervalTime)
        {
            IntervalTime = _IntervalTime;
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