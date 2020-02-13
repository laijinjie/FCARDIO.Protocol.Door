using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.POS.Time.TimeErrorCorrection
{
    /// <summary>
    /// 设置误差自修正参数
    /// </summary>
    public class WriteTimeError : Door.FC8800.Time.TimeErrorCorrection.WriteTimeError
    {
        /// <summary>
        /// 设置误差自修正参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含误差自修正参数</param>
        public WriteTimeError(INCommandDetail cd, WriteTimeError_Parameter par) : base(cd, par)
        {
            CmdType = 0x02;
            CmdIndex = 0x03;
            CmdPar = 0x01;
        }
    }
}
