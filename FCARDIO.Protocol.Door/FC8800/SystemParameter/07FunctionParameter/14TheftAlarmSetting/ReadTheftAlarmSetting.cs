﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取智能防盗主机参数
    /// </summary>
    public class ReadTheftAlarmSetting : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 获取智能防盗主机参数 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadTheftAlarmSetting(INCommandDetail cd) : base(cd) {
            CmdType = 0x01;
            CmdIndex = 0x0A;
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(CmdType, CmdIndex, 0x8E);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x0D))
            {
                var buf = oPck.CmdData;
                ReadTheftAlarmSetting_Result rst = new ReadTheftAlarmSetting_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}