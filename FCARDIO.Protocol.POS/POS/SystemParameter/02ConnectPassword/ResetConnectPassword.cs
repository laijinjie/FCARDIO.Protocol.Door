using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.POS.SystemParameter.ConnectPassword
{
    /// <summary>
    /// 重置控制器通讯密码
    /// </summary>
    public class ResetConnectPassword : Write_Command
    {
        /// <summary>
        /// 命令数据部分
        /// </summary>
        private static readonly byte[] DataStrt = new byte[] { 0x64, 0xFE, 0xD8, 0xFB, 0xA3, 0x04, 0xDD, 0x72 };

        /// <summary>
        /// 重置控制器通讯密码 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ResetConnectPassword(INCommandDetail cd) : base(cd, null) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            return true;
        }

        /// <summary>
        /// 拼装命令
        /// </summary>
        protected override void CreatePacket0()
        {
            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(8);
            buf.WriteBytes(DataStrt);

            Packet(0x01, 0x02, 0x02, 0x08, buf);
        }
    }
}
