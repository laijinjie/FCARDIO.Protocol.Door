using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.POS.SystemParameter.ConnectPassword
{
    /// <summary>
    /// 获取控制器通讯密码
    /// </summary>
    public class ReadConnectPassword : Door.Door8800.SystemParameter.ConnectPassword.ReadConnectPassword
    {
        /// <summary>
        /// 命令数据部分
        /// </summary>
        private static readonly byte[] DataStrt = new byte[] { 0x46, 0x43, 0x61, 0x72, 0x64, 0x59, 0x7A };

        /// <summary>
        /// 获取控制器通讯密码 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadConnectPassword(INCommandDetail cd) : base(cd) {
        }
        /// 拼装命令
        /// </summary>
        protected override void CreatePacket0()
        {
            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(7);
            buf.WriteBytes(DataStrt);

            //Packet(0x01, 0x02, 0x00, 0x08, buf);
            Packet(0x01, 0x04, 0x00, 0x07, buf);
        }
    }
}
