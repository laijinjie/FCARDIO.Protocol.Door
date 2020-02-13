using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Holiday
{
    /// <summary>
    /// 清空控制器中的所有节假日
    /// </summary>
    public class ClearHoliday : Protocol.Door.FC8800.Holiday.ClearHoliday
    {
        /// <summary>
        /// 构造命令，无需其他参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ClearHoliday(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x44;
        }

    }
}
