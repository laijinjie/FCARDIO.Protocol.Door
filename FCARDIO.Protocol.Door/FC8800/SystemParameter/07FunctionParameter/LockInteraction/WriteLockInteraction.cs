using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置互锁参数
    /// </summary>
    public class WriteLockInteraction : FC8800Command
    {
        public WriteLockInteraction(INCommandDetail cd, INCommandParameter par) : base(cd, par) { }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteLockInteraction_Parameter model = value as WriteLockInteraction_Parameter;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }

        protected override void CreatePacket0()
        {
            WriteLockInteraction_Parameter model = _Parameter as WriteLockInteraction_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(model.GetDataLen());

            Packet(0x01, 0x0A, 0x04, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
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