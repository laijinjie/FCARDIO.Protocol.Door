﻿using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Elevator.FC8864.Holiday
{
    /// <summary>
    /// 读取控制板中已存储的所有节假日
    /// 读取成功返回 ReadAllHoliday_Result
    /// </summary>
    public class ReadAllHoliday : Read_Command
    {
        /// <summary>
        /// 读取到的节假日缓冲
        /// </summary>
        private List<IByteBuffer> mReadBuffers;

        /// <summary>
        /// 构造命令，无需其他参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadAllHoliday(INCommandDetail cd) : base(cd, null)
        {

        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x44, 3);
            mReadBuffers = new List<IByteBuffer>();
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            //应答：
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                buf.Retain();
                mReadBuffers.Add(buf);
                CommandWaitResponse();
            }
            //应答：传输结束
            if (CheckResponse(oPck, 0x54, 3, 0xff, 4))
            {
                var buf = oPck.CmdData;
                int iTotal = buf.ReadInt();
                _ProcessMax = iTotal;
                ReadAllHoliday_Result rst = new ReadAllHoliday_Result();
                _Result = rst;
                rst.SetBytes(iTotal,mReadBuffers);
                _ProcessStep = iTotal;
                ClearBuf();
                CommandCompleted();
            }

        }

        /// <summary>
        /// 命令重发时，对命令中一些缓冲做清空或参数重置<br/>
        /// 此命令一般情况下不需要实现！
        /// </summary>
        protected override void CommandReSend()
        {

            ClearBuf();


        }
        /// <summary>
        /// 清空缓冲区
        /// </summary>
        protected void ClearBuf()
        {
            foreach (IByteBuffer buf in mReadBuffers)
            {
                buf.Release();
            }
            mReadBuffers.Clear();
            mReadBuffers = null;
        }
    }
}