using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 将密码列表写入到控制器的返回值
    /// </summary>
    public class AddPassword_Result : INCommandResult
    {
        /// <summary>
        /// 无法写入的密码数量
        /// </summary>
        public readonly int FailTotal;

        /// <summary>
        /// 无法写入的密码列表
        /// </summary>
        public List<string> PasswordList;

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="failtotal">密码数量</param>
        /// <param name="passwordList">密码列表</param>
        public AddPassword_Result(int failtotal, List<string> passwordList)
        {
            FailTotal = failtotal;
            PasswordList = passwordList;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            PasswordList = null;
        }
    }
}
