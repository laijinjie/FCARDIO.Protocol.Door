using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800
{
    /// <summary>
    /// 针对命令中的写参数命令进行抽象封装
    /// </summary>
    public abstract class FC8800CommandEx : FC8800Command

    {

        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含命令所需要的其他参数</param>
        public FC8800CommandEx(INCommandDetail cd, INCommandParameter par) : base(cd, par)
        {

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
        /// 检查指令返回值
        /// </summary>
        /// <param name="oPck"></param>
        /// <param name="CmdType">命令类型</param>
        /// <param name="CmdIndex">命令索引</param>
        /// <param name="CmdPar">命令参数</param>
        /// <returns></returns>
        protected virtual bool CheckResponse(OnlineAccessPacket oPck, byte CmdType, byte CmdIndex, byte CmdPar)
        {
            return (oPck.CmdType == CmdType + 0x30 &&
                oPck.CmdIndex == CmdIndex &&
                oPck.CmdPar == CmdPar);

        }


        /// <summary>
        /// 获取一个指定大小的Buf
        /// </summary>
        /// <param name="iSize">大小</param>
        /// <returns></returns>
        protected IByteBuffer GetNewCmdDataBuf(int iSize)
        {
            var acl = _Connector.GetByteBufAllocator();
            IByteBuffer buf = acl.Buffer(iSize);
            return buf;
        }

        /// <summary>
        /// 重置命令内容
        /// </summary>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        /// <param name="dl">数据长度</param>
        protected void RewritePacket(byte ct, byte ci, byte cp, uint dl)
        {
            FCPacket.CmdType = ct;
            FCPacket.CmdIndex = ci;
            FCPacket.CmdPar = cp;
            FCPacket.DataLen = dl;

        }


        /// <summary>
        /// 重置命令内容
        /// </summary>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        /// <param name="dl">数据长度</param>
        protected void RewritePacket(byte ci, byte cp, uint dl)
        {
            FCPacket.CmdIndex = ci;
            FCPacket.CmdPar = cp;
            FCPacket.DataLen = dl;

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

    }
}
