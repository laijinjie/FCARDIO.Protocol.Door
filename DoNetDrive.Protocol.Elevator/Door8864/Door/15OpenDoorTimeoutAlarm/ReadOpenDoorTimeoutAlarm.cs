using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.Elevator.FC8864.Door.OpenDoorTimeoutAlarm
{
    /// <summary>
    /// 读取 开门超时报警参数
    /// </summary>
    public class ReadOpenDoorTimeoutAlarm : Read_Command
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par"></param>
        public ReadOpenDoorTimeoutAlarm(INCommandDetail cd) : base(cd, null) { }


        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x43, 0x0A, 0x01);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0xC0))
            {
                var buf = oPck.CmdData;
                ReadOpenDoorTimeoutAlarm_Result rst = new ReadOpenDoorTimeoutAlarm_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}
