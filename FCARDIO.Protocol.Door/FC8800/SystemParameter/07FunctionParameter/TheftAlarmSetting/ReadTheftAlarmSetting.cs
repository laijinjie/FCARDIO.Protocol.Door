using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取智能防盗主机参数
    /// </summary>
    public class ReadTheftAlarmSetting : FC8800Command
    {
        public ReadTheftAlarmSetting(INCommandDetail cd) : base(cd, null) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            return true;
        }

        /// <summary>
        /// 拼装命令
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            Packet(0x01, 0x0A, 0x8E);
        }

        protected override void CommandReSend()
        {
            return;
        }

        protected override void CreatePacket0()
        {
            return;
        }

        protected override void Release1()
        {
            return;
        }
    }
}