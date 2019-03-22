using FCARDIO.Core.Command;
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
    public class ReadBalcklistAlarmOption_Result : INCommandResult
    {
        public byte Use;

        public ReadBalcklistAlarmOption_Result(byte _Use)
        {
            Use = _Use;
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