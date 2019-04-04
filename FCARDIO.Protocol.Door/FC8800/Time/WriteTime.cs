using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.Time
{
    /// <summary>
    /// 将电脑的最新时间写入到控制器中
    /// </summary>
    public class WriteTime : FC8800Command
    {
        public WriteTime(INCommandDetail cd) : base(cd, null) { }

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

            var buf = acl.Buffer(0x07);

            byte[] Datebuf = new byte[7];

            TimeUtil.DateToBCD_ssmmhhddMMwwyy(Datebuf, DateTime.Now);

            buf.WriteBytes(Datebuf);

            Packet(0x02, 0x02, 0x00, 0x07, buf);
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