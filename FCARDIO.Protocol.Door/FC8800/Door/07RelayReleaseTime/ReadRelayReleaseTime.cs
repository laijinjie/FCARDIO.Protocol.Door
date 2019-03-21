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
    public class ReadRelayReleaseTime
         : FC8800Command
    {
        public ReadRelayReleaseTime(INCommandDetail cd, ReadRelayReleaseTime_Parameter parameter) : base(cd, parameter) { }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadRelayReleaseTime_Parameter model = value as ReadRelayReleaseTime_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x01))
            {
                var buf = oPck.CmdData;
                RelayReleaseTime_Result rst = new RelayReleaseTime_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

        protected override void CommandReSend()
        {
        }

        protected override void CreatePacket0()
        {
            Packet(0x03, 0x08, 0x00, 0x01, GetCmdData());
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
