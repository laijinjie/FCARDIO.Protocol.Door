using DotNetty.Buffers;
using FCARDIO.Protocol.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.OfflinePatrol.Data.Transaction
{
    /// <summary>
    /// 系统记录
    /// </summary>
    public class SystemTransaction : AbstractTransaction
    {
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
