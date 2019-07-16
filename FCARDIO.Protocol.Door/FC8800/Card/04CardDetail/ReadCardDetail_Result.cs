using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System.Collections;
using FCARDIO.Protocol.Door.FC8800.Data;


namespace FCARDIO.Protocol.Door.FC8800.Card.CardDetail
{
    /// <summary>
    /// FC88/MC58 读取单个卡片在控制器中的信息，命令成功后的返回值
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
        public FC8800.Data.CardDetail Card;


        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="isReady">卡片是否存在</param>
        /// <param name="card">CardDetail 保存卡详情的实体</param>
        public ReadCardDetail_Result(bool isReady, FC8800.Data.CardDetail card)
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
