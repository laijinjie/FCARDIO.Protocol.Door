using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.Time
{
    /// <summary>
    /// 从控制器中读取控制器时间
    /// </summary>
    public class ReadTime : Door.Door8800.Time.ReadTime
    {
        /// <summary>
        /// 获取设备运行信息 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadTime(INCommandDetail cd) : base(cd)
        {
        }


        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x02, 0x01);
        }
    }
}
