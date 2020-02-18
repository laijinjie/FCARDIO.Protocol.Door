using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.SystemParameter.Buzzer
{
    /// <summary>
    /// 设置主板蜂鸣器
    /// </summary>
    public class WriteBuzzer : Door.FC8800.SystemParameter.FunctionParameter.WriteBuzzer
    {
        /// <summary>
        /// 设置主板蜂鸣器 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par"></param>
        public WriteBuzzer(INCommandDetail cd, WriteBuzzer_Parameter par) : base(cd, par)
        {
            CmdType = 0x01;
            CmdIndex = 0x12;
            CmdPar = 0x01;
        }

    }
}
