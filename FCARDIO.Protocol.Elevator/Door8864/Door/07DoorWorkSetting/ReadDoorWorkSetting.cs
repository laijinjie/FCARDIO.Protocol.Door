using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.Elevator.FC8864.Door.DoorWorkSetting
{
    /// <summary>
    /// 读取门工作方式
    /// </summary>
    public class ReadDoorWorkSetting : Protocol.Door.Door8800.Door.ReaderWorkSetting.ReadReaderWorkSetting_Base<DoorPort_Parameter>
    {
        /// <summary>
        /// 读取门工作方式
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含门端口</param>
        public ReadDoorWorkSetting(INCommandDetail cd, DoorPort_Parameter par) : base(cd, par) {
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            DoorPort_Parameter model = _Parameter as DoorPort_Parameter;
            Packet(0x43, 0x04, 0x00, 0x01, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }
    }
}
