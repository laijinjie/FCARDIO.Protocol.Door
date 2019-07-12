using DotNetty.Buffers;
using FCARDIO.Protocol.Door.FC8800.Password;
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
    public class AddPassword_Parameter : FC8800.Password.AddPassword_Parameter
    {
        /// <summary>
        /// 密码集合
        /// </summary>
        //public List<PasswordDetail> ListPassword { get; private set; }
        public AddPassword_Parameter(List<PasswordDetail> list) : base(list)
        {

            //ListPassword = list;
        }

        public override bool checkedParameter()
        {
            if (ListPassword == null || ListPassword.Count == 0)
            {
                return false;
            }
            int iOut = 0;
            foreach (var item in ListPassword)
            {
                if (item.Password.Length > 8)
                {
                    return false;
                }
                if (!int.TryParse(item.Password, out iOut))
                {
                    return false;
                }
                if (item.OpenTimes < 0 || item.OpenTimes > 65535)
                {
                    return false;
                }
                if (item.Expiry == DateTime.MinValue || item.Expiry >= DateTime.MaxValue)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetDataLen()
        {
            return 4 + (BatchCount * 12);
        }
    }
}
