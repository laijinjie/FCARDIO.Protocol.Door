using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.Deadline
{
    /// <summary>
    /// 获取设备有效期_结果
    /// </summary>
    public class ReadDeadline_Result : INCommandResult
    {
        public int Deadline;

        public ReadDeadline_Result(int _Deadline)
        {
            Deadline = _Deadline;
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