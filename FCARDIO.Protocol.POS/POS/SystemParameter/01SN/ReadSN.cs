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
    public class ReadSN : Door.Door8800.SystemParameter.SN.ReadSN
    {
        /// <summary>
        /// 获取控制器SN 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadSN(INCommandDetail cd) : base(cd)
        {
        }
        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(1, 2);
        }
    }
}
