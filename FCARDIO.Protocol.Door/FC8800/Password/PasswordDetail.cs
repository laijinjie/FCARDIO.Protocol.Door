using DotNetty.Buffers;
using FCARDIO.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 表示一个密码表
    /// </summary>
    public class PasswordDetail : AbstractData
    {
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }

        public override int GetDataLen()
        {
            throw new NotImplementedException();
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }
    }
}
