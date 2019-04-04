using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.Time
{
    /// <summary>
    /// 从控制器中读取控制器时间_结果
    /// </summary>
    public class ReadTime_Result : INCommandResult
    {
        /// <summary>
        /// 控制器的日期时间
        /// </summary>
        public DateTime ControllerDate;

        public ReadTime_Result(DateTime _ControllerDate)
        {
            ControllerDate = _ControllerDate;
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