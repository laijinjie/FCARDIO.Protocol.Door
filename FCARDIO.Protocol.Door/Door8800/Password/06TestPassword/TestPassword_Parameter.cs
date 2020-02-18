using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Password.TestPassword
{
    /// <summary>
    /// 测试密码参数
    /// </summary>
    public class TestPassword_Parameter : AbstractParameter
    {

        public string Password;

        public override bool checkedParameter()
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {

        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }

        public override int GetDataLen()
        {
            return 13;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }
    }
}
