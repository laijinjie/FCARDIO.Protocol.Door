using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Data;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardDataBase
{
    /// <summary>
    /// FC88/MC58 读取卡片数据库中的所有卡数据
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
