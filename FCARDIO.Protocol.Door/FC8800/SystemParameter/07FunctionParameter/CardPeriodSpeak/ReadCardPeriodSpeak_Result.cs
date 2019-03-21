using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取卡片到期提示_结果
    /// </summary>
    public class ReadCardPeriodSpeak_Result : INCommandResult
    {
        public byte Use;

        public ReadCardPeriodSpeak_Result(byte _Use)
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