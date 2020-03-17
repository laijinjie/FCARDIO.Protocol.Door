using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.POS.TimeGroup
{
    /// <summary>
    /// 读取所有时段
    /// </summary>
    public class ReadTimeGroup : Door.Door8800.TimeGroup.ReadTimeGroup
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ReadTimeGroup(INCommandDetail cd) : base(cd)
        {
        }
        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x06, 0x02);
        }
    }
}
