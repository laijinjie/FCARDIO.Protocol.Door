using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置读卡器数据校验
    /// </summary>
    public class WriteReaderCheckMode : Protocol.Door.FC8800.SystemParameter.FunctionParameter.WriteReaderCheckMode
    {
        /// <summary>
        /// 设置读卡器数据校验 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par"></param>
        public WriteReaderCheckMode(INCommandDetail cd, WriteReaderCheckMode_Parameter par) : base(cd, par) {
            CmdType = 0x41;
            CmdIndex = 0x0A;
            CmdPar = 0x07;
        }

    }
}