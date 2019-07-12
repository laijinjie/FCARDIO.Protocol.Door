﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardListBySequence
{
    /// <summary>
    ///FC88\MC58 将卡片列表写入到控制器非排序区
    /// </summary>
    public class WriteCardListBySequence_Parameter : WriteCardList_Parameter_Base<Data.CardDetail>
    {
        /// <summary>
        /// 创建 将卡片列表写入到控制器非排序区 指令的参数
        /// </summary>
        /// <param name="cardList">需要写入的卡列表</param>
        /// <param name=""></param>
        public WriteCardListBySequence_Parameter( List<Data.CardDetail> cardList):base(cardList)
        {
        }
        
    }
}