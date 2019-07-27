﻿using FCARDIO.Core.Command;
using System;

namespace FCARDIO.Protocol.Elevator.FC8864.Time.TimeErrorCorrection
{
    /// <summary>
    /// 设置误差自修正参数
    /// </summary>
    public class WriteTimeError : Write_Command
    {
        /// <summary>
        /// 设置误差自修正参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含误差自修正参数</param>
        public WriteTimeError(INCommandDetail cd, WriteTimeError_Parameter par) : base(cd, par) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteTimeError_Parameter model = value as WriteTimeError_Parameter;
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
            WriteTimeError_Parameter model = _Parameter as WriteTimeError_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(model.GetDataLen());

            Packet(0x42, 0x03, 0x01, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
        }
    }
}