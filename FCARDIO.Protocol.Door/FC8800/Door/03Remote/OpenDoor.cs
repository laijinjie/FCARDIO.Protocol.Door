using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Door.Remote
{
    public class OpenDoor
        : FC8800Command
    {
        public OpenDoor(INCommandDetail cd,Remote_Parameter parameter) : base(cd, parameter)
        {

        }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            Remote_Parameter model = value as Remote_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
        }

        protected override void CommandReSend()
        {
            return;
        }
        protected IByteBuffer GetCmdData()
        {
            Remote_Parameter model = _Parameter as Remote_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x03, 0x00, 0x04, GetCmdData());
        }

        protected override void Release1()
        {
        }
    }
}
