using FCARDIO.Core.Command;
using FCARDIO.Core.Extension;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.WriteSN
{
    /// <summary>
    /// 广播写入控制器SN
    /// </summary>
    public class WriteSN_Broadcast : FC8800Command
    {
        private static readonly byte[] DataStrt = new byte[] { 0xFC, 0x65, 0x65, 0x33, 0xFF };
        private static readonly byte[] DataEnd = new byte[] { 0xCF, 0x35, 0x92 };

        public WriteSN_Broadcast(INCommandDetail cd, INCommandParameter par) : base(cd, par) { }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteSN_Parameter model = value as WriteSN_Parameter;
            if (model == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(model.SN))
            {
                return false;
            }

            if (model.SN.Length != 16)
            {
                return false;
            }

            return true;
        }

        protected override void CreatePacket0()
        {
            WriteSN_Parameter model = _Parameter as WriteSN_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(18);
            buf.WriteBytes(DataStrt);
            buf.WriteBytes(model.SN.GetBytes());
            buf.WriteBytes(DataEnd);

            Packet(0xC1, 0xD1, 0xF7, 0x18, buf);
        }

        /// <summary>
        /// 【应答：OK】 => 父类已处理
        /// </summary>
        /// <param name="oPck"></param>
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