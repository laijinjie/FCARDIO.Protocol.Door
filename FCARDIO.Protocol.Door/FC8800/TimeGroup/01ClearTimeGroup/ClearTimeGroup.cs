using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.TimeGroup
{
    /// <summary>
    /// 
    /// </summary>
    public class ClearTimeGroup : FC8800Command_ReadParameter
    {
        public ClearTimeGroup(INCommandDetail cd) : base(cd, null)
        {

        }
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }

        protected override void CreatePacket0()
        {
            Packet(0x06, 0x01);
        }
    }
}
