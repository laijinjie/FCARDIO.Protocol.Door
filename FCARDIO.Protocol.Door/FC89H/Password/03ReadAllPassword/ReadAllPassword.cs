using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Password.ReadAllPassword
{
    /// <summary>
    /// 读取所有密码
    /// </summary>
    public class ReadAllPassword : FC8800.Password.ReadAllPassword<ReadAllPassword_Result>
    {
        public ReadAllPassword(INCommandDetail cd) : base(cd)
        {

        }
    }
}
