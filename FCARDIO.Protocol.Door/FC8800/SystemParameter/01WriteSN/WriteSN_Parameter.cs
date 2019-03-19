using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.WriteSN
{
    /// <summary>
    /// 写入控制器SN_参数
    /// </summary>
    public class WriteSN_Parameter : INCommandParameter
    {
        /// <summary>
        /// 控制器SN
        /// </summary>
        public string SN;

        public WriteSN_Parameter(string _SN)
        {
            SN = _SN;
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