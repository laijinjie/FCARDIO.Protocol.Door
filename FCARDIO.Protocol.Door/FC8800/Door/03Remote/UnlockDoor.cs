using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.Remote
{
    public class UnlockDoor
        : OpenDoor
    {
        public UnlockDoor(INCommandDetail cd, Remote_Parameter parameter) : base(cd, parameter)
        { }
   
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x04, 0x01, 0x04, GetCmdData());
        }
    }
}
