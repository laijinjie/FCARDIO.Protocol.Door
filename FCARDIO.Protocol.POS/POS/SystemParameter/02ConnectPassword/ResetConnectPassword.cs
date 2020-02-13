using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.POS.SystemParameter.ConnectPassword
{
    /// <summary>
    /// 重置控制器通讯密码
    /// </summary>
    public class ResetConnectPassword : Door.FC8800.SystemParameter.ConnectPassword.ResetConnectPassword
    {
        /// <summary>
        /// 重置控制器通讯密码 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ResetConnectPassword(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x01;
            CmdIndex = 0x05;
        }
    }
}