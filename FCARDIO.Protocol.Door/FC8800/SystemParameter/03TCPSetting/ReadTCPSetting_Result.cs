using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.TCPSetting
{
    /// <summary>
    /// 获取TCP参数_结果
    /// </summary>
    public class ReadTCPSetting_Result : INCommandResult
    {
        public TCPDetail TCP;

        public ReadTCPSetting_Result(TCPDetail _TCP)
        {
            TCP = _TCP;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            TCP = null;

            return;
        }
    }
}