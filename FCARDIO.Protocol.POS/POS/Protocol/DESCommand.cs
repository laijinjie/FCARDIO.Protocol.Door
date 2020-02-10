using FCARDIO.Core.Command;
using FCARDIO.Core.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.POS.Protocol
{
    public class DESCommand : AbstractCommand
    {
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            throw new NotImplementedException();
        }

        protected override void CommandNext(INPacket readPacket)
        {
            throw new NotImplementedException();
        }

        protected override void CommandReSend()
        {
            throw new NotImplementedException();
        }

        protected override void CreatePacket()
        {
            throw new NotImplementedException();
        }

        protected override void Release0()
        {
            throw new NotImplementedException();
        }
    }
}
