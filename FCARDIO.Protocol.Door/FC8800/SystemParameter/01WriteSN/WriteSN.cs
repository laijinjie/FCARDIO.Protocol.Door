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
    /// 写入控制器SN
    /// </summary>
    public class WriteSN : FC8800Command
    {
        private static readonly byte[] DataStrt = new byte[] { 0x03, 0xC5, 0x89, 0x12, 0x3E };
        private static readonly byte[] DataEnd = new byte[] { 0x90, 0x7F, 0x78 };

        public WriteSN(INCommandDetail cd, INCommandParameter par) : base(cd, par) { }

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

            Packet(0x01, 0x01, 0x00, 0x18, buf);
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