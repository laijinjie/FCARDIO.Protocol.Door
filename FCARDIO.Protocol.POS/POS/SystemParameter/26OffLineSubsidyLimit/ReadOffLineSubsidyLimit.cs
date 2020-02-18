﻿using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.POS.SystemParameter.OffLineSubsidyLimit
{
    /// <summary>
    /// 读取离线补贴充值，卡内补贴余额上限命令
    /// </summary>
    public class ReadOffLineSubsidyLimit : Read_Command
    {
        /// <summary>
        /// 获取设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadOffLineSubsidyLimit(INCommandDetail cd) : base(cd) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x01, 0x17, 0x01);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 1))
            {
                var buf = oPck.CmdData;
                ReadOffLineSubsidyLimit_Result rst = new ReadOffLineSubsidyLimit_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}
