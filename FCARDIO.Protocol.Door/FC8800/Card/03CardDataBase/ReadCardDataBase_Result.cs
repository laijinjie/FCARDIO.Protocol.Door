﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System.Collections;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardDataBase
{
    /// <summary>
    /// 读取卡片数据库中的所有卡数据
    /// </summary>
    public class ReadCardDataBase_Result
        : INCommandResult
    {
        /// <summary>
        /// 读取到的卡片列表
        /// </summary>
        public List<FC8800.Data.CardDetail> CardList;

        /// <summary>
        /// 读取到的卡片数量
        /// </summary>
        public int DataBaseSize;

        /// <summary>
        /// 带读取的卡片数据类型
        /// 1 &emsp; 排序卡区域 
        /// 2 &emsp; 非排序卡区域 
        /// 3 &emsp; 所有区域 
        /// </summary>
        public int CardType;

        public ReadCardDataBase_Result()
        {
        }

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="sortDataBaseSize">排序数据区容量上限</param>
        /// <param name="sortCardSize">排序数据区已使用数量</param>
        /// <param name="sequenceDataBaseSize">顺序存储区容量上限</param>
        /// <param name="sequenceCardSize">顺序存储区已使用数量</param>
        public ReadCardDataBase_Result(List<FC8800.Data.CardDetail> cardList, int dataBaseSize,int cardType)
        {
            CardList = cardList;
            DataBaseSize = dataBaseSize;
            CardType = cardType;
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
            return;
        }
    }
}