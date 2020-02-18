using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Time;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.Time
{
    /// <summary>
    /// 从控制器中读取时间
    /// </summary>
    public class ReadTime : Protocol.Door.FC8800.Time.ReadTime
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
            Packet(0x42, 0x01);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x07))
            {
                var buf = oPck.CmdData;
                var rst = new ReadTime_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}