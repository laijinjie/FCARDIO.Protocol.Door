using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Door.FC8800.Password.TestPassword
{
    /// <summary>
    /// 测试密码
    /// </summary>
    public class TestPassword : FC8800Command_WriteParameter
    {
        public TestPassword(INCommandDetail cd, TestPassword_Parameter par) : base(cd, par)  {

        }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            AddPassword_Parameter model = value as AddPassword_Parameter;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }

        protected override void CreatePacket0()
        {

        }
    }
}
