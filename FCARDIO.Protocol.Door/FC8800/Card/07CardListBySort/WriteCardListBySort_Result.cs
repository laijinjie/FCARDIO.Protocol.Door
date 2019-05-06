﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System.Collections;
using FCARDIO.Protocol.Door.FC8800.Data;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardListBySort
{
    public class WriteCardListBySort_Result:INCommandResult
    {
        /// <summary>
        /// 失败卡数量
        /// </summary>
        public int FailTotal;

        /// <summary>
        /// 失败的卡列表
        /// </summary>
        public List<FC8800.Data.CardDetail> CardList;

        public WriteCardListBySort_Result() { }

        /// <summary>
        /// 创建结构 
        /// </summary>
        /// <param name="failtotal">失败卡数量</param>
        /// <param name="cardList">失败的卡列表</param>
        public WriteCardListBySort_Result(int failtotal, List<FC8800.Data.CardDetail> cardList)
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

    }
}