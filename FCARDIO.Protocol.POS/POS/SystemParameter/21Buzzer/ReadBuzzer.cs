using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.SystemParameter.Buzzer
{
    /// <summary>
    /// 获取主板蜂鸣器
    /// </summary>
    public class ReadBuzzer : Door.Door8800.SystemParameter.FunctionParameter.ReadBuzzer
    {
        /// <summary>
        /// 获取主板蜂鸣器 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadBuzzer(INCommandDetail cd) : base(cd)
        {
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x01, 0x12, 0x01);
        }
    }
}
