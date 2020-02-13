using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Time
{
    /// <summary>
    /// 将电脑的最新时间写入到控制器中
    /// </summary>
    public class WriteTime : Protocol.Door.FC8800.Time.WriteTime
    {
        /// <summary>
        /// 将电脑的最新时间写入到控制器中
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public WriteTime(INCommandDetail cd) : base(cd) {
            CmdType = 0x42;
        }

    }
}