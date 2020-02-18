using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.POS.Subsidy
{
    /// <summary>
    /// 清空所有补贴
    /// </summary>
    public class ClearSubsidy : Read_Command
    {
        /// <summary>
        /// 构造命令，无需其他参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ClearSubsidy(INCommandDetail cd) : base(cd, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x07, 2);
        }
    }
}
