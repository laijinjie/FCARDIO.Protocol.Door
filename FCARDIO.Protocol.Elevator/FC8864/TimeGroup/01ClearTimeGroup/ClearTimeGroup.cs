using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.TimeGroup
{
    /// <summary>
    /// 清空所有开门时段
    /// </summary>
    public class ClearTimeGroup : Protocol.Door.FC8800.TimeGroup.ClearTimeGroup
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ClearTimeGroup(INCommandDetail cd) : base(cd)
        {
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x46, 0x01);
        }
    }
}
