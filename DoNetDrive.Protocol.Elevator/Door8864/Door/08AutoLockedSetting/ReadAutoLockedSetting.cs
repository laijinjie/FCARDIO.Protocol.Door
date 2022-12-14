using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.Elevator.FC8864.Door.AutoLockedSetting
{
    /// <summary>
    /// 读取定时锁定门参数
    /// </summary>
    public class ReadAutoLockedSetting : Read_Command
    {
        /// <summary>
        /// 读取定时锁定门参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含门端口</param>
        public ReadAutoLockedSetting(INCommandDetail cd, DoorPort_Parameter par) : base(cd, par) { }

      

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0xE2))
            {
                var buf = oPck.CmdData;
                ReadAutoLockedSetting_Result rst = new ReadAutoLockedSetting_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            DoorPort_Parameter model = _Parameter as DoorPort_Parameter;
            Packet(0x43, 0x05, 0x01, 0x01, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }

    }
}
