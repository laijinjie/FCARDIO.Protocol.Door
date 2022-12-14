using System;
using System.Collections.Generic;
using System.Text;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.Fingerprint.Elevator
{
    /// <summary>
    /// 读取电梯继电器板的继电器输出类型的命令
    /// </summary>
    public class ReadRelayType : Door8800Command_ReadParameter
    {
        /// <summary>
        /// 创建读取电梯继电器板的继电器输出类型的命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadRelayType(INCommandDetail cd) : base(cd) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x22);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 64))
            {
                var buf = oPck.CmdData;
                ReadRelayType_Result rst = new ReadRelayType_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}
