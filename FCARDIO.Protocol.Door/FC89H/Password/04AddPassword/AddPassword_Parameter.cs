using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Password.AddPassword
{
    /// <summary>
    /// 添加密码参数
    /// </summary>
    public class AddPassword_Parameter<T> : FC8800.Password.AddPassword_Parameter<T> where T : PasswordDetail, new()
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="list"></param>
        public AddPassword_Parameter(List<T> list) : base(list)
        {

        }
        /// <summary>
        /// 检查每个密码
        /// </summary>
        /// <returns></returns>
        public bool checkedParameterItem(PasswordDetail password)
        {
            if (password.OpenTimes < 0 || password.OpenTimes > 65535)
            {
                throw new ArgumentException("Password.OpenTimes Error!");
            }
            if (password.Expiry == DateTime.MinValue || password.Expiry >= DateTime.MaxValue)
            {
                throw new ArgumentException("Password.Expiry Error!");
            }

            return true;
        }

    }
}
