using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardListBySort
{
    /// <summary>
    /// 将卡片列表写入到控制器排序区
    /// </summary>
    public class WriteCardListBySort_Parameter : AbstractParameter
    {
        /// <summary>
        /// 需要上传的卡片列表
        /// </summary>
        public List<FC8800.Data.CardDetail> CardList;

        public WriteCardListBySort_Parameter() { }

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="cardList">需要上传的卡片列表</param>
        public WriteCardListBySort_Parameter(List<FC8800.Data.CardDetail> cardList)
        {
            CardList = cardList;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
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
            uint iLen = (10 * 0x21) + 8;
            if (databuf.WritableBytes != iLen)
            {
                throw new ArgumentException("Crad Error");
            }
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            int iLen = (10 * 0x21) + 8;
            return iLen;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {

        }
    }
}
