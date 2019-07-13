using FCARDIO.Protocol.Door.FC8800.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Password.DeletePassword
{
    /// <summary>
    /// 删除密码
    /// </summary>
    public class DeletePassword_Parameter<T> : FC8800.Password.DeletePassword_Parameter<T> where T : FC8800.Password.PasswordDetail, new ()
    {
        public DeletePassword_Parameter(List<T> list) : base(list)
        {

        }

        public override int GetDataLen()
        {
            return 4 + (BatchCount * 4);
        }
    }
}
