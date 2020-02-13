using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.POS.SystemParameter.Buzzer
{
    /// <summary>
    /// 获取主板蜂鸣器
    /// </summary>
    public class ReadBuzzer : Door.FC8800.SystemParameter.FunctionParameter.ReadBuzzer
    {
        /// <summary>
        /// 获取主板蜂鸣器 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadBuzzer(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x01;
            CmdIndex = 0x12;
            CmdPar = 0x01;
        }

    }
}
