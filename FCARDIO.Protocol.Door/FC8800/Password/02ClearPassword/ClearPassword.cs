using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 清空所有密码
    /// </summary>
    public class ClearPassword : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 构造命令，无需其他参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ClearPassword(INCommandDetail cd) : base(cd, null)
        {
            CmdType = 0x05;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(CmdType, 2);
        }
    }
}
