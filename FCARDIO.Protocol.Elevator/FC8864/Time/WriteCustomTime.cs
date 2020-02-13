using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Time
{
    /// <summary>
    /// 将自定义时间写入到控制器中
    /// </summary>
    public class WriteCustomTime : Protocol.Door.FC8800.Time.WriteCustomTime
    {
        /// <summary>
        /// 将自定义时间写入到控制器中
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含自定义时间</param>
        public WriteCustomTime(INCommandDetail cd, WriteCustomTime_Parameter par) : base(cd, par) {
            CmdType = 0x42;
        }

    }
}