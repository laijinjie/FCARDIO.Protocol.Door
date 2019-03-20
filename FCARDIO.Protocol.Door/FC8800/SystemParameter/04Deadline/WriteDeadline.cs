using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.Deadline
{
    /// <summary>
    /// 设置设备有效期
    /// </summary>
    public class WriteDeadline : FC8800Command
    {
        public WriteDeadline(INCommandDetail cd, INCommandParameter par) : base(cd, par) { }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteDeadline_Parameter model = value as WriteDeadline_Parameter;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }

        protected override void CreatePacket0()
        {
            WriteDeadline_Parameter model = _Parameter as WriteDeadline_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(model.GetDataLen());

            Packet(0x01, 0x07, 0x01, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
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