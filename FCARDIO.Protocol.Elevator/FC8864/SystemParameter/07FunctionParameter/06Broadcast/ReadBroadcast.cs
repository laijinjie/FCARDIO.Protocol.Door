using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取语音播报语音段开关
    /// </summary>
    public class ReadBroadcast : Protocol.Door.FC8800.SystemParameter.FunctionParameter.ReadBroadcast
    {
        /// <summary>
        /// 获取设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadBroadcast(INCommandDetail cd) : base(cd) {
        }
        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x41, 0x0A, 0x85);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x0A))
            {
                var buf = oPck.CmdData;
                var rst = new ReadBroadcast_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}