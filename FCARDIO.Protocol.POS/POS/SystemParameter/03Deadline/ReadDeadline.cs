using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.SystemParameter.Deadline
{
    /// <summary>
    /// 获取设备有效期
    /// </summary>
    public class ReadDeadline : Door.FC8800.SystemParameter.Deadline.ReadDeadline
    {
        /// <summary>
        /// 获取设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadDeadline(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x01;
            CmdIndex = 0x03;
        }
    }
}
