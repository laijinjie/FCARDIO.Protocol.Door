using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Elevator.FC8864.Holiday
{
    /// <summary>
    /// 读取控制板中已存储的所有节假日
    /// 读取成功返回 ReadAllHoliday_Result
    /// </summary>
    public class ReadAllHoliday : Protocol.Door.FC8800.Holiday.ReadAllHoliday
    {
        

        /// <summary>
        /// 构造命令，无需其他参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadAllHoliday(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x44;
            CheckResponseCmdType = 0x24;
        }

    }
}
