using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.POS.SystemParameter.SN
{
    /// <summary>
    /// 获取控制器SN
    /// </summary>
    public class ReadSN : Door.FC8800.SystemParameter.SN.ReadSNf
    {
        /// <summary>
        /// 获取控制器SN 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadSN(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x01;
        }

    }
}
