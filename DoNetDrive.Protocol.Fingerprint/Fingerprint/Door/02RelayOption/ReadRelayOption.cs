using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.Fingerprint.Door.RelayOption
{
    /// <summary>
    /// 读取继电器参数
    /// </summary>
    public class ReadRelayOption : Door8800Command_ReadParameter
    {
        /// <summary>
        /// 读取控制器4个门的读卡器字节数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadRelayOption(INCommandDetail cd) : base(cd) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x02);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 1))
            {
                var buf = oPck.CmdData;
                RelayOption_Result rst = new RelayOption_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}
