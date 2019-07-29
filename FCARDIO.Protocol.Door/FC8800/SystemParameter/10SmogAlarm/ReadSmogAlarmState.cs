using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.SmogAlarm
{
    /// <summary>
    /// 获取烟雾报警状态
    /// </summary>
    public class ReadSmogAlarmState : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 烟雾报警状态（0 - 未开启报警、1 - 已开启报警）
        /// </summary>
        public byte SmogAlarmState;

        /// <summary>
        /// 获取烟雾报警状态 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadSmogAlarmState(INCommandDetail cd) : base(cd) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x01, 0x0C, 0x12);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x01))
            {
                var buf = oPck.CmdData;
                SmogAlarmState = buf.ReadByte();
                CommandCompleted();
            }
        }
    }
}