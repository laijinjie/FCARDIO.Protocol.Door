using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.SystemParameter.Version
{
    /// <summary>
    /// 获取设备版本号
    /// </summary>
    public class ReadVersion : Door.Door8800.SystemParameter.Version.ReadVersion
    {
        /// <summary>
        /// 获取设备版本号 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadVersion(INCommandDetail cd) : base(cd)
        {
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x01, 0x04);
        }
    }
}
