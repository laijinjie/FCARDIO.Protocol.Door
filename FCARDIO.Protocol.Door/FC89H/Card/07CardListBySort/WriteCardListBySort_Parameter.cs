using FCARDIO.Protocol.Door.FC8800.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Card.CardListBySort
{
    /// <summary>
    /// FC89H 将卡片列表写入到控制器排序区
    /// </summary>
    public class WriteCardListBySort_Parameter : WriteCardList_Parameter_Base<Data.CardDetail>
    {
        /// <summary>
        /// 创建 将卡片列表写入到控制器排序区 指令的参数
        /// </summary>
        /// <param name="cardList">需要上传的卡片列表</param>
        public WriteCardListBySort_Parameter(List<Data.CardDetail> cardList) : base(cardList)
        {
            cardList.Sort();
        }
    }
}
