using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.TimeGroup
{
    /// <summary>
    /// 清空所有开门时段
    /// </summary>
    public class ClearTimeGroup : Read_Command
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ClearTimeGroup(INCommandDetail cd) : base(cd, null)
        {

        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x46, 0x01);
        }

        /// <summary>
        /// 处理返回通知
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }

       
    }
}
