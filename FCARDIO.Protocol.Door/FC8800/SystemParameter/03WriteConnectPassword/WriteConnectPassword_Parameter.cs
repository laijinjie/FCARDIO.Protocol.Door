using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.WriteConnectPassword
{
    /// <summary>
    /// 设置控制器通讯密码_参数
    /// </summary>
    public class WriteConnectPassword_Parameter : INCommandParameter
    {
        /// <summary>
        /// 控制器通讯密码
        /// </summary>
        public string PWD;

        public WriteConnectPassword_Parameter(string _PWD)
        {
            PWD = _PWD;
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