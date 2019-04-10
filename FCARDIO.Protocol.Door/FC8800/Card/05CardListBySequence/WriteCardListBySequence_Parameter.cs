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
        /// <summary>
        /// 失败卡数量
        /// </summary>
        public int FailTotal;

        /// <summary>
        /// 失败的卡列表
        /// </summary>
        public List<FC8800.Data.CardDetail> CardList;

        public WriteCardListBySequence_Parameter() { }

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="isReady">卡片是否存在</param>
        /// <param name="Card">CardDetail类</param>
        /// <param name=""></param>
        public WriteCardListBySequence_Parameter(int failTotal, List<FC8800.Data.CardDetail> cardList)
        {
            FailTotal = failTotal;
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
            if (databuf.ReadableBytes != 2)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteByte(FailTotal);
            databuf.WriteByte(byte.Parse(CardList.ToString()));
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 2;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            FailTotal = databuf.ReadByte();
        }
    }
}
