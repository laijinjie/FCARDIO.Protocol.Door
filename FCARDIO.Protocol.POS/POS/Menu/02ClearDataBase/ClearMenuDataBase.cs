using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.POS.Menu
{
    /// <summary>
    /// 清空所有菜单命令
    /// </summary>
    public class ClearMenuDataBase : Read_Command
    {
        /// <summary>
        /// 构造命令，无需其他参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ClearMenuDataBase(INCommandDetail cd) : base(cd, null)
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
            Packet(0x06, 2);
        }
    }
}
