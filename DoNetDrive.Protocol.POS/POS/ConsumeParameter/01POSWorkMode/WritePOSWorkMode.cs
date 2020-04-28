﻿using DoNetDrive.Core.Command;
using System;

namespace DoNetDrive.Protocol.POS.ConsumeParameter.POSWorkMode
{
    /// <summary>
    /// 设置消费机工作模式命令
    /// </summary>
    public class WritePOSWorkMode : Write_Command
    {
        /// <summary>
        /// 设置设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含设备有效期</param>
        public WritePOSWorkMode(Protocol.DESDriveCommandDetail cd, WritePOSWorkMode_Parameter par) : base(cd, par) { }


        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WritePOSWorkMode_Parameter model = value as WritePOSWorkMode_Parameter;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            WritePOSWorkMode_Parameter model = _Parameter as WritePOSWorkMode_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(model.GetDataLen());

            Packet(0x03, 0x01, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
        }
    }
}
