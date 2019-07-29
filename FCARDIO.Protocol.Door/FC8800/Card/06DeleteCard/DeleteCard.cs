﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Card.DeleteCard
{
    /// <summary>
    /// FC88\MC58 将卡片列表从到控制器中删除
    /// </summary>
    public class DeleteCard : DeleteCardBase<Data.CardDetail>
    {


        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public DeleteCard(INCommandDetail cd, DeleteCard_Parameter parameter) : base(cd, parameter)
        {
            mPacketCardMax = 20;
            MaxBufSize = (mPacketCardMax * 4) + 4;
        }



        /// <summary>
        /// 将数据部分写入到缓冲区
        /// </summary>
        /// <param name="card">卡详情</param>
        /// <param name="buf">缓冲区</param>
        protected override void WriteCardBodyToBuf0(Data.CardDetail card, IByteBuffer buf)
        {
            buf.WriteByte(0);
            buf.WriteInt((int)card.CardData);
        }


    }
}
