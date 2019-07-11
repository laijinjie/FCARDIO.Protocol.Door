using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Password.AddPassword
{
    public class AddPassword : FC8800.Password.AddPassword<PasswordDetail>
    {
        public AddPassword(INCommandDetail cd, AddPassword_Parameter par) : base(cd, par)
        {

        }


    }
}
