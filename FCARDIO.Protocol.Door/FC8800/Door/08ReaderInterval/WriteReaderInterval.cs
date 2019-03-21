using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ReaderInterval
{
   public class WriteReaderInterval
        :FC8800Command
    {
        public WriteReaderInterval(INCommandDetail cd, WriteReaderInterval_Parameter parameter) : base(cd, parameter) { }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteReaderInterval_Parameter model = value as WriteReaderInterval_Parameter;
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
            Packet(0x03, 0x09, 0x01, 0x03, GetCmdData());
        }

        private IByteBuffer GetCmdData()
        {
            WriteReaderInterval_Parameter model = _Parameter as WriteReaderInterval_Parameter;
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
