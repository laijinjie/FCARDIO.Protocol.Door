using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace DoNetDrive.Protocol.POS.Data
{
    public class SystemTransactionDetail : TransactionDetailBase
    {
        public override void SetBytes(IByteBuffer data)
        {
            throw new NotImplementedException();
        }
    }
}
