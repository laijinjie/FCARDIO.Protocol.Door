using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800.SystemParameter.Version;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.Elevator.FC8864.SystemParameter.Version
{
    /// <summary>
    /// 获取设备版本号
    /// </summary>
    public class ReadVersion : Protocol.Door.Door8800.SystemParameter.Version.ReadVersion
    {
        /// <summary>
        /// 获取设备版本号 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadVersion(INCommandDetail cd) : base(cd) {
           
        }
        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x41, 0x08);
        }

        /// <summary>
        /// 检查指令返回值
        /// </summary>
        /// <param name="oPck"></param>
        /// <param name="dl">参数长度</param>
        /// <returns></returns>
        protected override bool CheckResponse(OnlineAccessPacket oPck, int dl)
        {
            return (oPck.DataLen == dl);

        }

        /// <summary>
        /// 检查指令返回值
        /// </summary>
        /// <param name="oPck"></param>
        /// <param name="CmdType">命令类型</param>
        /// <param name="CmdIndex">命令索引</param>
        /// <param name="CmdPar">命令参数</param>
        /// <param name="dl">参数长度</param>
        /// <returns></returns>
        protected override bool CheckResponse(OnlineAccessPacket oPck, byte CmdType, byte CmdIndex, byte CmdPar, int dl)
        {
            return (oPck.CmdType == CmdType &&
                oPck.CmdIndex == CmdIndex &&
                oPck.CmdPar == CmdPar &&
                oPck.DataLen == dl);

        }
    }
}