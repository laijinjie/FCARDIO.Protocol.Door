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
    public class ReadTimeGroup : Door.FC8800.TimeGroup.ReadTimeGroup
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ReadTimeGroup(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x06;
            CmdIndex = 0x02;
        }

    }
}
