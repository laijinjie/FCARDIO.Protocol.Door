using FCARDIO.Core.Command;
using FCARDIO.Core.Extension;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.WriteConnectPassword
{
    /// <summary>
    /// 设置控制器通讯密码
    /// </summary>
    public class WriteConnectPassword : FC8800Command
    {
        public WriteConnectPassword(INCommandDetail cd, INCommandParameter par) : base(cd, par) { }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteConnectPassword_Parameter model = value as WriteConnectPassword_Parameter;
            if (model == null)
            {
                return false;
            }

            return true;
        }

        protected override void CreatePacket0()
        {
            WriteConnectPassword_Parameter model = _Parameter as WriteConnectPassword_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(4);

            if (string.IsNullOrEmpty(model.PWD))
            {
                buf.WriteBytes(NULLPassword.HexToByte());
            }
            else
            {
                buf.WriteBytes(model.PWD.GetBytes());
            }

            Packet(0x01, 0x03, 0x00, 0x04, buf);
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