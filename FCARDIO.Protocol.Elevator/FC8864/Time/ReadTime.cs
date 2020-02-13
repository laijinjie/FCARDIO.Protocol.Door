using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.Time
{
    /// <summary>
    /// 从控制器中读取时间
    /// </summary>
    public class ReadTime : Protocol.Door.FC8800.Time.ReadTime
    {
        /// <summary>
        /// 获取设备运行信息 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadTime(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x42;
            CheckResponseCmdType = 0x22;
        }
        
    }
}