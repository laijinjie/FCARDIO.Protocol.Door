using DoNetDrive.Protocol.POS.Protocol;
using DotNetty.Buffers;
using System.Collections.Generic;
using System.Text;

namespace DoNetDrive.Protocol.POS.SystemParameter.ReceiptPrint
{
    public class ReadReceiptPrint : Read_Command
    {

        /// <summary>
        /// 获取设备有效期 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadReceiptPrint(DESDriveCommandDetail cd) : base(cd) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x01, 0x0D, 0x01);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(DESPacket oPck)
        {
            if (CheckResponse(oPck, 2))
            {
                var databuf = oPck.CommandPacket.CmdData;
                ReadReceiptPrint_Result rst = new ReadReceiptPrint_Result();
                _Result = rst;
                rst.IsOpen = databuf.ReadByte();
                rst.PrintCount = databuf.ReadByte();
                CommandCompleted();
            }

        }

    }
}
