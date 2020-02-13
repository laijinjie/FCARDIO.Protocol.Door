using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.Door.DoorWorkSetting
{
    /// <summary>
    /// 读取门工作方式
    /// </summary>
    public class ReadDoorWorkSetting : Protocol.Door.FC8800.Door.ReaderWorkSetting.ReadReaderWorkSetting_Base<DoorPort_Parameter>
    {
        /// <summary>
        /// 读取门工作方式
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含门端口</param>
        public ReadDoorWorkSetting(INCommandDetail cd, DoorPort_Parameter par) : base(cd, par) {
            CmdType = 0x43;
            CmdIndex = 0x04;
            CheckResponseCmdType = 0x23;
        }

    }
}
