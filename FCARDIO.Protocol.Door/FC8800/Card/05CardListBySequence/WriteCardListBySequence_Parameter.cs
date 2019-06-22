using System;
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
    public class WriteCardListBySequence_Parameter : AbstractParameter
    {
        /// <summary>
        /// 需要写入的卡列表
        /// </summary>
        public List<FC8800.Data.CardDetail> CardList;


        /// <summary>
        /// 创建 将卡片列表写入到控制器非排序区 指令的参数
        /// </summary>
        /// <param name="cardList">需要写入的卡列表</param>
        /// <param name=""></param>
        public WriteCardListBySequence_Parameter( List<FC8800.Data.CardDetail> cardList)
        {
            CardList = cardList;
            if (!checkedParameter())
            {
                throw new ArgumentException("cardList Error");
            }
        }
        
        /// <summary>
        /// 检查卡片列表参数，任何情况下都不能为空，元素数不能为0,列表元素不能为空
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if(CardList==null) return false;
            

            if (CardList.Count == 0)  return false;
            

            foreach (var c in CardList)
            {
                if (c == null) return false;
            }

            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            CardList = null;
        }
        
        /// <summary>
        /// 不实现此功能
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf;
        }

        /// <summary>
        /// 不实现此功能
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0;
        }

        /// <summary>
        /// 不实现此功能
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            return;
        }
    }
}
