using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FireAlarm
{
    /// <summary>
    /// 读取消防报警状态
    /// </summary>
    public class ReadFireAlarmState : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 消防报警状态（0 - 未开启报警、1 - 已开启报警）
        /// </summary>
        public byte FireAlarmState;

        /// <summary>
        /// 读取消防报警状态 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadFireAlarmState(INCommandDetail cd) : base(cd) {
            CmdType = 0x01;
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(CmdType, 0x0C, 0x02);
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
                FireAlarmState = buf.ReadByte();
                CommandCompleted();
            }
        }
    }
}