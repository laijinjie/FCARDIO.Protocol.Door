using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ReaderOption
{
    public class ReadReaderOption : FC8800Command
    {
        public ReadReaderOption(INCommandDetail cd) : base(cd, null)
        {

        }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            return true;
        }

        /// <summary>
        /// 命令在此进行拼装
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x01);
        }



        /// <summary>
        /// 【应答：OK】 => 父类已处理
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 4))
            {
                var buf = oPck.CmdData;
                ReaderOption_Result rst = new ReaderOption_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
            return;
        }
        /// <summary>
        /// 准备重新发送命令，可能子类需要清空一些标志或缓冲区，则再此函数中执行
        /// 本命令不需要
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void Release1()
        {
            return;
        }
    }
}
