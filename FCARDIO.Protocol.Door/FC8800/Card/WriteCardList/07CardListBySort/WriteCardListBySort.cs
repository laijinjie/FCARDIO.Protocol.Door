﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardListBySort
{
    /// <summary>
    /// FC88\MC58 将卡片列表写入到控制器排序区 
    /// </summary>
    public class WriteCardListBySort : WriteCardListBySortBase<Data.CardDetail>
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="perameter"></param>
        public WriteCardListBySort(INCommandDetail cd, WriteCardListBySort_Parameter perameter) : base(cd, perameter)
        {
            mPacketCardMax = 10;
            MaxBufSize = (mPacketCardMax * 0x21) + 8;
        }


        /// <summary>
        /// 从错误卡列表中读取一个错误卡号，加入到cardlist中
        /// </summary>
        /// <param name="CardList"></param>
        /// <param name="buf"></param>
        protected override void ReadCardByFailBuf(List<ulong> CardList, IByteBuffer buf)
        {
            CardList.Add((UInt64)buf.ReadInt());
        }
    }
}
