﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Holiday
{
    /// <summary>
    /// 读取控制板中已存储的所有节假日<br/>
    /// 读取成功返回 ReadAllHoliday_Result
    /// </summary>
    public class ReadAllHoliday : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 读取到的节假日缓冲
        /// </summary>
        protected List<IByteBuffer> mReadBuffers;

        /// <summary>
        /// 构造命令，无需其他参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadAllHoliday(INCommandDetail cd) : base(cd, null)
        {
            CmdType = 0x04;
            CheckResponseCmdType = 0x04;
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(CmdType, 3);
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
            if (CheckResponse(oPck, CheckResponseCmdType, 3, 0xff, 4))
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
