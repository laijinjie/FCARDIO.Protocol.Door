using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取记录存储方式
    /// </summary>
    public class ReadRecordMode : Protocol.Door.FC8800.SystemParameter.FunctionParameter.ReadRecordMode
    {
        /// <summary>
        /// 获取设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadRecordMode(INCommandDetail cd) : base(cd) {
            CmdType = 0x41;
            CmdIndex = 0x0A;
            CheckResponseCmdType = 0x21;
        }

    }
}