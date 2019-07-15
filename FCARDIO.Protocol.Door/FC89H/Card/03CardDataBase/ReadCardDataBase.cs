using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Card.CardDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Card.CardDataBase
{
    /// <summary>
    /// FC89H 读所有卡
    /// </summary>
    public class ReadCardDataBase : ReadCardDataBase_Base<FC89H.Data.CardDetail>
    {
        /// <summary>
        /// 初始化命令结构 
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadCardDataBase(INCommandDetail cd, ReadCardDataBase_Parameter parameter) : base(cd, parameter) { }

        /// <summary>
        /// 创建返回值
        /// </summary>
        /// <param name="cardList"></param>
        /// <param name="dataBaseSize"></param>
        /// <param name="cardType"></param>
        protected override ReadCardDataBase_Base_Result<Data.CardDetail> CreateResult(List<Data.CardDetail> cardList, int dataBaseSize, int cardType)
        {
            ReadCardDataBase_Result result = new ReadCardDataBase_Result(cardList, dataBaseSize, cardType);
            return result;
        }

    }
}
