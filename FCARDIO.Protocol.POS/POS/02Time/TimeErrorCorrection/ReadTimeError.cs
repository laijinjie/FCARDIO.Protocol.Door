using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.Time.TimeErrorCorrection
{
    /// <summary>
    /// 读取误差自修正参数
    /// </summary>
    public class ReadTimeError : Door.Door8800.Time.TimeErrorCorrection.ReadTimeError
    {
        /// <summary>
        /// 读取误差自修正参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadTimeError(INCommandDetail cd) : base(cd)
        {
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x02, 0x03);
        }
    }
}
