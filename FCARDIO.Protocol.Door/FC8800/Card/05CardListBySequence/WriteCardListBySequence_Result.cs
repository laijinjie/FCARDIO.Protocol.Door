﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System.Collections;
using FCARDIO.Protocol.Door.FC8800.Data;


namespace FCARDIO.Protocol.Door.FC8800.Card.CardListBySequence
{
    /// <summary>
    /// 将卡片列表写入到控制器非排序区
    /// </summary>
    public class WriteCardListBySequence_Result: INCommandResult
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int FailTotal;

        /// <summary>
        /// 卡列表
        /// </summary>
        public List<FC8800.Data.CardDetail> CardList;

        public WriteCardListBySequence_Result() { }

        /// <summary>
        /// 创建结构 
        /// </summary>
        /// <param name="failtotal">卡数量</param>
        /// <param name="cardList">卡列表</param>
        public WriteCardListBySequence_Result(int failtotal, List<FC8800.Data.CardDetail> cardList)
        {
            FailTotal = failtotal;
            CardList = cardList;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            CardList = null;
        }

        internal void SetBytes(IByteBuffer buf)
        {
            throw new NotImplementedException();
        }
    }
}
