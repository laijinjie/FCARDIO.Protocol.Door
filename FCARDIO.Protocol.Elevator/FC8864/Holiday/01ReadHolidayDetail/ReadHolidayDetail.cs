using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Holiday
{
    /// <summary>
    /// 从控制板中读取节假日存储详情，命令成功后返回 ReadHolidayDetail_Result
    /// </summary> 
    public class ReadHolidayDetail : Protocol.Door.FC8800.Holiday.ReadHolidayDetail
    {
        /// <summary>
        /// 构造命令，无需其他参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadHolidayDetail(INCommandDetail cd) : base(cd)
        {
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x44, 1);
        }
    }
}
