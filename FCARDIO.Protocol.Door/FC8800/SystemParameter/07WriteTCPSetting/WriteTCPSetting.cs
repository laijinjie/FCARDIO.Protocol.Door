using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.WriteTCPSetting
{
    public class WriteTCPSetting : FC8800Command
    {
        public WriteTCPSetting(INCommandDetail cd, INCommandParameter par) : base(cd, par) { }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteTCPSetting_Parameter model = value as WriteTCPSetting_Parameter;
            if (model == null)
            {
                return false;
            }

            return true;
        }

        protected override void CreatePacket0()
        {
            WriteTCPSetting_Parameter model = _Parameter as WriteTCPSetting_Parameter;

            Packet(0x01, 0x06, 0x01, Convert.ToUInt32(model.TCP.GetDataLen()), model.TCP.GetBytes());
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }

        protected override void CommandReSend()
        {
            return;
        }

        protected override void Release1()
        {
            return;
        }
    }
}