﻿using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.USBDrive;

namespace FCARDIO.Protocol.USB.CardReader
{
    public abstract class CommandEx : USBDriveCommand
    {
        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含命令所需要的其他参数</param>
        public CommandEx(INCommandDetail cd, INCommandParameter par) : base(cd, par)
        {

        }

        /// <summary>
        /// 检查指令返回值
        /// </summary>
        /// <param name="oPck"></param>
        /// <param name="CmdType">命令类型</param>
        /// <param name="CmdIndex">命令索引</param>
        /// <returns></returns>
        protected virtual bool CheckResponse(USBDrivePacket oPck, byte CmdType, byte CmdIndex)
        {
            return (oPck.CmdType == CmdType + 0x30 &&
                oPck.CmdIndex == CmdIndex);

        }

        /// <summary>
        /// 命令重发时，对命令中一些缓冲做清空或参数重置<br/>
        /// 此命令一般情况下不需要实现！
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }


        /// <summary>
        /// 释放命令占用的内存<br/>
        /// 此命令一般情况下不需要实现！
        /// </summary>
        protected override void Release1()
        {
            return;
        }

        /// <summary>
        /// 获取当前命令所使用的缓冲区
        /// </summary>
        /// <returns></returns>
        protected IByteBuffer GetCmdBuf()
        {
            var buf = FCPacket.CmdData;
            buf?.Clear();
            return buf;
        }

        /// <summary>
        /// 重置命令内容
        /// </summary>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        /// <param name="dl">数据长度</param>
        protected void RewritePacket(byte ci, int dl)
        {
            FCPacket.CmdIndex = ci;
            FCPacket.DataLen = dl;

        }
    }
}
