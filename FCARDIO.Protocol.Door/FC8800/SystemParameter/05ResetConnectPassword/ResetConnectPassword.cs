using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.ResetConnectPassword
{
    /// <summary>
    /// 重置控制器通讯密码
    /// </summary>
    public class ResetConnectPassword : FC8800Command
    {
        private static readonly byte[] DataStrt = new byte[] { 0x46, 0x43, 0x61, 0x72, 0x64, 0x59, 0x7A };

        public ResetConnectPassword(INCommandDetail cd) : base(cd, null) { }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            return true;
        }

        protected override void CreatePacket0()
        {
            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(7);
            buf.WriteBytes(DataStrt);

            Packet(0x01, 0x05, 0x00, 0x07, buf);
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