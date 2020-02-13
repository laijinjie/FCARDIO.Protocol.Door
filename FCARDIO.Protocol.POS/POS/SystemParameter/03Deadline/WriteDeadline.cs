using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.POS.SystemParameter.Deadline
{
    /// <summary>
    /// 设置设备有效期
    /// </summary>
    public class WriteDeadline : Door.FC8800.SystemParameter.Deadline.WriteDeadline
    {
        /// <summary>
        /// 设置设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含设备有效期</param>
        public WriteDeadline(INCommandDetail cd, WriteDeadline_Parameter par) : base(cd, par)
        {
            CmdType = 0x01;
            CmdIndex = 0x03;
        }


    }
}
