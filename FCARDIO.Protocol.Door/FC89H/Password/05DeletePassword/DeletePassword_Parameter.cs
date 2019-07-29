using FCARDIO.Protocol.Door.FC8800.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Password
{
    /// <summary>
    /// 写入密码 参数
    /// </summary>
    public class DeletePassword_Parameter : Password_Parameter_Base<PasswordDetail>
    {

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="list">要添加的密码集合</param>
        public DeletePassword_Parameter(List<PasswordDetail> list) : base(list) { }

    }
}
