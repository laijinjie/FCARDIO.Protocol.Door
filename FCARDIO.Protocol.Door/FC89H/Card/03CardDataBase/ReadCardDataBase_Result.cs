﻿using FCARDIO.Protocol.Door.FC8800.Card.CardDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Card.CardDataBase
{
    /// <summary>
    /// FC89H 读取卡片数据库中的所有卡数据
    /// </summary>
    public class ReadCardDataBase_Result : ReadCardDataBase_Base_Result<Data.CardDetail>
    {

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="cardList">卡列表</param>
        /// <param name="dataBaseSize">读取到的卡数量</param>
        /// <param name="cardType">卡数据库类型</param>
        public ReadCardDataBase_Result(List<Data.CardDetail> cardList, int dataBaseSize, int cardType)
            : base(cardList, dataBaseSize, cardType)
        {
        }
    }
}
