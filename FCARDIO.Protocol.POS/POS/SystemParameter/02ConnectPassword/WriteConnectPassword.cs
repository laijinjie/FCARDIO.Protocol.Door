﻿using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.SystemParameter.ConnectPassword
{
    /// <summary>
    /// 设置控制器通讯密码
    /// </summary>
    public class WriteConnectPassword : Door.Door8800.SystemParameter.ConnectPassword.WriteConnectPassword
    {
        /// <summary>
        /// 设置控制器通讯密码 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含命令所需新的通讯密码</param>
        public WriteConnectPassword(INCommandDetail cd, Password_Parameter par) : base(cd, par)
        {
        }
        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Password_Parameter model = _Parameter as Password_Parameter;
            Packet(0x01, 0x03, 0x00, 4, model.GetBytes(_Connector.GetByteBufAllocator().Buffer(4)));
        }
    }
}
