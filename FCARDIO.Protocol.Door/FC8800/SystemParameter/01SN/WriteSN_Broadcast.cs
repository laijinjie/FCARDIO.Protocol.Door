using FCARDIO.Core.Command;
using FCARDIO.Core.Extension;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.SN
{
    /// <summary>
    /// 广播写入控制器SN
    /// </summary>
    public class WriteSN_Broadcast : WriteSN
    {
        protected override byte[] DataStrt { get { return new byte[] { 0xFC, 0x65, 0x65, 0x33, 0xFF }; } }
        protected override byte[] DataEnd { get { return new byte[] { 0xCF, 0x35, 0x92 }; } }

        public WriteSN_Broadcast(INCommandDetail cd, INCommandParameter par) : base(cd, par) { }

        /// <summary>
        /// 创建指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0xC1, 0xD1, 0xF7, 0x18, GetCmdData());
        }
    }
}