using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardListBySequence
{
    /// <summary>
    /// 将卡片列表写入到控制器非排序区
    /// </summary>
    public class WriteCardListBySequence_Parameter : AbstractParameter
    {
        protected int mIndex = 0;//指示当前命令进行的步骤
        /// <summary>
        /// 数量
        /// </summary>
        public int Total;

        /// <summary>
        /// 卡列表
        /// </summary>
        public List<FC8800.Data.CardDetail> CardList;

        public WriteCardListBySequence_Parameter() { }

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="total"></param>
        /// <param name="cardList"></param>
        /// <param name=""></param>
        public WriteCardListBySequence_Parameter(int total, List<FC8800.Data.CardDetail> cardList)
        {
            Total = total;
            CardList = cardList;
        }
        
        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (CardList == null || CardList.Count == 0)
            {
                return false;
            }
            foreach (Data.CardDetail card in CardList)
            {
                if (card == null)
                {
                    return false;
                }
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
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            int iMaxSize = 5; //每个数据包最大5个卡
            int iSize = 0;
            int iIndex = 0;

            databuf.Clear();
            int iLen = GetDataLen();
            if (databuf.WritableBytes != iLen)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteInt(iMaxSize);
            for (int i = mIndex; i < CardList.Count; i++)
            {
                iIndex = i;
                iSize += 1;

                CardList[iIndex].GetBytes(databuf);
                if (iSize == iMaxSize)
                {
                    break;
                }
                //card.WriteCardData(databuf);
            }
            if (iSize != iMaxSize)
            {
                databuf.SetInt(0, iSize);
            }
            mIndex = iIndex + 1;
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            int iLen = (1 * 0x21) + 4;
            return iLen;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            Total = databuf.ReadByte();
        }
    }
}
