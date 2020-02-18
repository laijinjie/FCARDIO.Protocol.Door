using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.SystemParameter.ConnectPassword
{
    /// <summary>
    /// 设置控制器通讯密码
    /// </summary>
    public class WriteConnectPassword : Door.FC8800.SystemParameter.ConnectPassword.WriteConnectPassword
    {
        /// <summary>
        /// 设置控制器通讯密码 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含命令所需新的通讯密码</param>
        public WriteConnectPassword(INCommandDetail cd, Password_Parameter par) : base(cd, par)
        {
            CmdType = 0x01;
            CmdIndex = 0x03;
        }
    }
}
