using FCARDIO.Protocol.Door.FC89H.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Password
{
    /// <summary>
    /// 写入密码到控制器参数
    /// </summary>
    public class AddPassword_Parameter : FC8800.Password.Password_Parameter_Base<PasswordDetail>
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="list">要添加的密码集合</param>
        public AddPassword_Parameter(List<PasswordDetail> list) : base(list) { }

        /// <summary>
        /// 检查每个密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        protected override bool checkedParameterItem(PasswordDetail password)
        {
            if (password.OpenTimes < 0 || password.OpenTimes > 65535)
            {
                throw new ArgumentException("Password.OpenTimes Error!");
            }
            if (password.Expiry == DateTime.MinValue)
            {
                throw new ArgumentException("Password.Expiry Error!");
            }

            return true;
        }
    }
}
