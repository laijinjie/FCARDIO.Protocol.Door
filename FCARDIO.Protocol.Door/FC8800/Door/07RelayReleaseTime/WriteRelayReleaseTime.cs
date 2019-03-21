using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.RelayReleaseTime
{
    public class WriteRelayReleaseTime
        : FC8800Command
    {
        public WriteRelayReleaseTime(INCommandDetail cd, WriteRelayReleaseTime_Parameter parameter) : base(cd, parameter) { }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadRelayReleaseTime_Parameter model = value as ReadRelayReleaseTime_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
        }

        protected override void CommandReSend()
        {
        }

        protected override void CreatePacket0()
        {
            Packet(0x03, 0x08, 0x01, 0x03, GetCmdData());
        }

        private IByteBuffer GetCmdData()
        {
            ReadRelayReleaseTime_Parameter model = _Parameter as ReadRelayReleaseTime_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }

        protected override void Release1()
        {
            return;
        }
    }
}
