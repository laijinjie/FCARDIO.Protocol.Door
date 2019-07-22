using FCARDIO.Core.Command;
using FCARDIO.Protocol.USBDrive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.OfflinePatrol
{
    public class CommandEx : USBDriveCommand
    {
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            throw new NotImplementedException();
        }

        protected override void CommandNext1(USBDrivePacket oPck)
        {
            throw new NotImplementedException();
        }

        protected override void CommandReSend()
        {
            throw new NotImplementedException();
        }

        protected override void CreatePacket0()
        {
            throw new NotImplementedException();
        }

        protected override void Release1()
        {
            throw new NotImplementedException();
        }
    }
}
