using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.ConnectPassword
{
    /// <summary>
    /// 获取控制器通讯密码_结果
    /// </summary>
    public class ReadConnectPassword_Result : INCommandResult
    {
        public string Password;

        public ReadConnectPassword_Result(string _Password)
        {
            Password = _Password;
        }

        public void Dispose()
        {
            return;
        }
    }
}