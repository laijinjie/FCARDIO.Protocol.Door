using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.DoorWorkSetting
{
    public class WriteDoorWorkSetting:FC8800Command
    {
        //0x03	0x06	0x01	0xE5

        public WriteDoorWorkSetting(INCommandDetail cd, WriteDoorWorkSetting_Parameter value) : base(cd, value)
        {
        }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteDoorWorkSetting_Parameter model = value as WriteDoorWorkSetting_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            Packet(0x03, 0x06, 0x01, 0xE5, GetCmdData());
        }
        protected IByteBuffer GetCmdData()
        {
            WriteDoorWorkSetting_Parameter model = _Parameter as WriteDoorWorkSetting_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(18);
            model.GetBytes(buf);
            return buf;
        }
        protected override void CommandReSend()
        {
            return;
        }

        protected override void CreatePacket0()
        {
            return;
        }

        protected override void Release1()
        {
            return;
        }

    }
}
