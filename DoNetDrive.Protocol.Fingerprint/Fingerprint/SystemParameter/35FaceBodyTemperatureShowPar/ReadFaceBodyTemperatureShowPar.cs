using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.Fingerprint.SystemParameter
{
    /// <summary>
    /// 读取人脸机体温数值显示开关
    /// </summary>
    public class ReadFaceBodyTemperatureShowPar : Door8800Command_ReadParameter
    {
        /// <summary>
        /// 创建读取人脸机体温数值显示开关的命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadFaceBodyTemperatureShowPar(INCommandDetail cd) : base(cd) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x01, 0x2E, 0x01);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x01))
            {
                var buf = oPck.CmdData;
                ReadFaceBodyTemperatureShowPar_Result rst = new ReadFaceBodyTemperatureShowPar_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}
