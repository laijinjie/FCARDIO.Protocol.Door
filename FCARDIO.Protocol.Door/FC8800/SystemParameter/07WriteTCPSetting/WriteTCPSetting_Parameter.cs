using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.ReadTCPSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.WriteTCPSetting
{
    /// <summary>
    /// 写入TCP参数
    /// </summary>
    public class WriteTCPSetting_Parameter : INCommandParameter
    {
        /// <summary>
        /// 控制器TCP信息
        /// </summary>
        public TCPDetail TCP;

        public WriteTCPSetting_Parameter(TCPDetail _TCP)
        {
            TCP = _TCP;
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