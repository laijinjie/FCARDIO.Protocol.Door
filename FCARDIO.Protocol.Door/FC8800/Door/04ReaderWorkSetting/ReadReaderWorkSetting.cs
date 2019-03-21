using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ReaderWorkSetting
{
    public class ReadReaderWorkSetting : FC8800Command
    {
        //0x03	0x05	0x00	0x119
        public ReadReaderWorkSetting(INCommandDetail cd, ReadReaderWorkSetting_Parameter value) : base(cd, value)
        {
        }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteReaderWorkSetting_Parameter model = value as WriteReaderWorkSetting_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x119))
            {
                var buf = oPck.CmdData;
                ReaderWorkSetting_Result rst = new ReaderWorkSetting_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
        protected IByteBuffer GetCmdData()
        {
            WriteReaderWorkSetting_Parameter model = _Parameter as WriteReaderWorkSetting_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }
        protected override void CommandReSend()
        {
            return;
        }

        protected override void CreatePacket0()
        {
            Packet(0x03, 0x05, 0x00, 0x01, GetCmdData());
        }

        protected override void Release1()
        {
            return;
        }
    }

}
