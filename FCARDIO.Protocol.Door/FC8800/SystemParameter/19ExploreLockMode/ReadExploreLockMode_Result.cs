using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.ExploreLockMode
{
    /// <summary>
    /// 获取防探测功能开关_结果
    /// </summary>
    public class ReadExploreLockMode_Result : INCommandResult
    {
        public byte Use;

        public ReadExploreLockMode_Result(byte _Use)
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