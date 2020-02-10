using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Core.Connector;
using FCARDIO.Core.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.POS.Protocol
{
    public class DESRequestHandle : AbstractRequestHandle
    {
        public override void DisposeResponse(INConnector connector, IByteBuffer msg)
        {
            throw new NotImplementedException();
        }

        protected override void fireRequestEvent(INConnector connector, INPacket p)
        {
            throw new NotImplementedException();
        }
    }
}
