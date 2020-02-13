using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.SN
{
    /// <summary>
    /// 获取控制器SN
    /// </summary>
    public class ReadSN : Protocol.Door.FC8800.SystemParameter.SN.ReadSN
    {
        /// <summary>
        /// 获取控制器SN 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadSN(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x41;
            CheckResponseCmdType = 0x21;
        }

    }
}
