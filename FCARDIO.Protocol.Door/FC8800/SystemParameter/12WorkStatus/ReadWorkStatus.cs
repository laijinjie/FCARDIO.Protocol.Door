using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.WorkStatus
{
    /// <summary>
    /// 获取控制器各端口工作状态信息
    /// </summary>
    public class ReadWorkStatus : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 获取控制器各端口工作状态信息 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadWorkStatus(INCommandDetail cd) : base(cd) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x01, 0x0E, 0x00);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x34))
            {
                var buf = oPck.CmdData;
                ReadWorkStatus_Result rst = new ReadWorkStatus_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}