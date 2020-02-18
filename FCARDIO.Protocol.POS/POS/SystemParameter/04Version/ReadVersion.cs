using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.SystemParameter.Version
{
    /// <summary>
    /// 获取设备版本号
    /// </summary>
    public class ReadVersion : Door.FC8800.SystemParameter.Version.ReadVersion
    {
        /// <summary>
        /// 获取设备版本号 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadVersion(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x01;
            CmdIndex = 0x04;
        }
    }
}
