﻿using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardListBySort
{
    /// <summary>
    /// 将卡片列表写入到控制器排序区 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class WriteCardListBySortBase<T> : WriteCardListBase<T>
        where T : Data.CardDetailBase
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="perameter"></param>
        public WriteCardListBySortBase(INCommandDetail cd, WriteCardList_Parameter_Base<T> perameter) : base(cd, perameter) { }


        /// <summary>
        /// 创建一个通讯指令,准备开始写排序区
        /// </summary>
        protected override void CreatePacket0()
        {
            mStep = 1;
            Packet(0x07, 0x07, 0x00);

            _ProcessMax = _CardPar.CardList.Count + 2;
            _ProcessStep = 1;
        }

        /// <summary>
        /// 重写父类对处理返回值的定义
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            switch (mStep)
            {
                case 1://处理开始写入指令返回
                    if (CheckResponse_OK(oPck))
                    {
                        _ProcessStep++;
                        //硬件已准备就绪，开始写入卡

                        //创建一个通讯缓冲区
                        var buf = GetNewCmdDataBuf(MaxBufSize);
                        WriteCardDetailToBuf(buf);
                        Packet(0x07, 0x07, 0x01, (uint)buf.ReadableBytes, buf);
                        CommandReady();//设定命令当前状态为准备就绪，等待发送
                        mStep = 2;//使命令进入下一个阶段
                        return;
                    }
                    break;
                case 2:
                    if (CheckResponse_OK(oPck))
                    {
                        //继续发下一包
                        CommandNext1(oPck);
                    }
                    else if (CheckResponse(oPck, 0x07, 0x07, 0xFF, oPck.DataLen))
                    {//检查是否不是错误返回值

                        //缓存错误返回值
                        if (mBufs == null)
                        {
                            mBufs = new Queue<IByteBuffer>();
                        }
                        oPck.CmdData.Retain();
                        mBufs.Enqueue(oPck.CmdData);

                        //继续发下一包
                        CommandNext1(oPck);
                    }
                    break;
                case 3:
                    if (CheckResponse_OK(oPck))
                    {
                        Create_Result();

                        //命令全部发送完毕
                        CommandCompleted();
                    }
                    break;
                default:
                    break;
            }

        }


        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (IsWriteOver())
            {
                //使命令进入下一个阶段
                Packet(0x07, 0x07, 0x2);
                CommandReady();//设定命令当前状态为准备就绪，等待发送
                mStep = 3;//使命令进入下一个阶段
                _ProcessStep++;
            }
            else
            {
                //未发送完毕，继续发送
                var buf = GetCmdBuf();
                WriteCardDetailToBuf(buf);
                FCPacket.DataLen = (UInt32)buf.ReadableBytes;
                CommandReady();//设定命令当前状态为准备就绪，等待发送
            }
        }

        /// <summary>
        /// 写入数据头
        /// </summary>
        /// <param name="buf">数据缓冲区</param>
        /// <param name="iPacketCardCount">本次需要写入的卡号数量</param>
        protected override void WritePacketHeadToBuf(IByteBuffer buf, int iPacketCardCount)
        {
            buf.WriteInt(mWriteCardIndex + 1);//指示此次写入的卡起始序号
            buf.WriteInt(iPacketCardCount);//指示此包包含的卡数量
        }
    }
}
