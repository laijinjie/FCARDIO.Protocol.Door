﻿using DotNetty.Buffers;
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
    public class ReadDoorWorkSetting : FC8800Command
    {
        //0x03	0x06	0x00	0x01

        public ReadDoorWorkSetting(INCommandDetail cd, ReadDoorWorkSetting_Parameter value) : base(cd, value)
        {
        }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadDoorWorkSetting_Parameter model = value as ReadDoorWorkSetting_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0xE5))
            {
                var buf = oPck.CmdData;
                DoorWorkSetting_Result rst = new DoorWorkSetting_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
        protected IByteBuffer GetCmdData()
        {
            ReadDoorWorkSetting_Parameter model = _Parameter as ReadDoorWorkSetting_Parameter;
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
            Packet(0x03, 0x05, 0x01, 0x1, GetCmdData());
        }

        protected override void Release1()
        {
            return;
        }
    }
}
