using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800
{
    /// <summary>
    /// 针对命令中的写参数命令进行抽象封装
    /// </summary>
    public abstract class FC8800Command_WriteParameter : FC8800Command
    {
        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含命令所需要的其他参数</param>
        public FC8800Command_WriteParameter(INCommandDetail cd, INCommandParameter par) : base(cd, par)
        {

        }

        /// <summary>
        /// 命令返回值的判断<br/>
        /// 【应答：OK】 => 父类已处理
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccess.OnlineAccessPacket oPck)
        {
            return;
        }

        /// <summary>
        /// 命令重发时，对命令中一些缓冲做清空或参数重置<br/>
        /// 此命令一般情况下不需要实现！
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }

        /// <summary>
        /// 释放命令占用的内存<br/>
        /// 此命令一般情况下不需要实现！
        /// </summary>
        protected override void Release1()
        {
            return;
        }
    }
}
