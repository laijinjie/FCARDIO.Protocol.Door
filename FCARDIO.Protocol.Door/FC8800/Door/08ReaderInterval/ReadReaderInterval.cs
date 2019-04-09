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
    public class ReadReaderInterval
         : FC8800Command
    {
        public ReadReaderInterval(INCommandDetail cd, ReadReaderInterval_Parameter parameter) : base(cd, parameter) { }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadReaderInterval_Parameter model = value as ReadReaderInterval_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x01))
            {
                var buf = oPck.CmdData;
                ReaderInterval_Result rst = new ReaderInterval_Result();
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
            Packet(0x03, 0x09, 0x00, 0x01, GetCmdData());
        }

        private IByteBuffer GetCmdData()
        {
            ReadReaderInterval_Parameter model = _Parameter as ReadReaderInterval_Parameter;
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
