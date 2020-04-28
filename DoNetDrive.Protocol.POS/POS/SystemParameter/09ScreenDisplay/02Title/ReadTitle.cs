﻿using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.POS.Protocol;

namespace DoNetDrive.Protocol.POS.SystemParameter.ScreenDisplay.Title
{
    /// <summary>
    /// 读取标题命令
    /// </summary>
    public class ReadTitle : Read_Command
    {
        /// <summary>
        /// 获取设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadTitle(DESDriveCommandDetail cd) : base(cd) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x01, 0x09, 0x03);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(DESPacket oPck)
        {
            if (CheckResponse(oPck, 0x1E))
            {
                var buf = oPck.CommandPacket.CmdData;
                ReadTitle_Result rst = new ReadTitle_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}
