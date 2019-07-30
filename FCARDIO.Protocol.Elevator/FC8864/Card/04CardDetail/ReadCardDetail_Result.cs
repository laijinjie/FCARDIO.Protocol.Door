﻿using FCARDIO.Core.Command;


namespace FCARDIO.Protocol.Elevator.FC8864.Card.CardDetail
{
    /// <summary>
    ///  读取单个卡片在控制器中的信息，命令成功后的返回值
    /// </summary>
    public class ReadCardDetail_Result :INCommandResult
    {
        /// <summary>
        /// 卡片是否存在
        /// </summary>
        public bool IsReady;

        /// <summary>
        /// 卡片的详情
        /// </summary>
        public Data.CardDetail Card;


        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="isReady">卡片是否存在</param>
        /// <param name="card">CardDetail 保存卡详情的实体</param>
        public ReadCardDetail_Result(bool isReady, Data.CardDetail card)
        {
            IsReady = isReady;
            Card = card;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Card = null;
        }
    }
}
