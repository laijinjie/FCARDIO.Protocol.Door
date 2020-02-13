using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Door.DoorWorkSetting
{
    /// <summary>
    /// 设置门工作方式
    /// </summary>
    public class WriteDoorWorkSetting : Protocol.Door.FC8800.Door.DoorWorkSetting.WriteDoorWorkSetting
    {
        /// <summary>
        /// 设置门工作方式
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par"></param>
        public WriteDoorWorkSetting(INCommandDetail cd, WriteDoorWorkSetting_Parameter par) : base(cd, par) {
            CmdType = 0x43;
            CmdIndex = 0x04;
        }

    }
}
