﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.POS.ConsumeParameter.TemporaryChangeFixedFee
{
    /// <summary>
    /// 读取临时变更定额、定次消费额度命令
    /// </summary>
    public class ReadTemporaryChangeFixedFee : Read_Command
    {
        /// <summary>
        /// 获取设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadTemporaryChangeFixedFee(INCommandDetail cd) : base(cd) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x05, 0x01);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 2))
            {
                var buf = oPck.CmdData;
                ReadTemporaryChangeFixedFee_Result rst = new ReadTemporaryChangeFixedFee_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}
