using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;
using FCARDIO.Protocol.Door.FC8800.TimeGroup;
using FCARDIO.Protocol.OnlineAccess;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Elevator.FC8864.TimeGroup
{
    /// <summary>
    /// 读取所有开门时段
    /// </summary>
    public class ReadTimeGroup : Protocol.Door.FC8800.TimeGroup.ReadTimeGroup
    {

        
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ReadTimeGroup(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x46;
            CmdIndex = 0x02;
            CheckResponseCmdType = 0x26;
        }

    }
}
