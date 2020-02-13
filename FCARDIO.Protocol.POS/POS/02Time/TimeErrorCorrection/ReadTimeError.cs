using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.POS.Time.TimeErrorCorrection
{
    /// <summary>
    /// 读取误差自修正参数
    /// </summary>
    public class ReadTimeError : Door.FC8800.Time.TimeErrorCorrection.ReadTimeError
    {
        /// <summary>
        /// 读取误差自修正参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadTimeError(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x02;
            CmdIndex = 0x03;
        }
    }
}
