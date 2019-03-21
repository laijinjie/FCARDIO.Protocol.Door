using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.AutoLockedSetting
{
    class WriteAutoLockedSetting
        : FC8800Command
    {
        public WriteAutoLockedSetting(INCommandDetail cd, AutoLockedSetting_Parameter parameter) : base(cd, parameter)
        { }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            AutoLockedSetting_Parameter model = value as AutoLockedSetting_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
        }

        protected override void CommandReSend()
        {

        }
        protected IByteBuffer GetCmdData()
        {
            AutoLockedSetting_Parameter model = _Parameter as AutoLockedSetting_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x07, 0x00, 0xE2, GetCmdData());
        }

        protected override void Release1()
        { }
    }
}
