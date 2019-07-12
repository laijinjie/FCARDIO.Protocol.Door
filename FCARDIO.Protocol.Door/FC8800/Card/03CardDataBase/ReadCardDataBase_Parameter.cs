using DotNetty.Buffers;
using System;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardDataBase
{
    /// <summary>
    /// 读取卡片数据库中的所有卡数据
    /// </summary>
    public class ReadCardDataBase_Parameter
        : AbstractParameter
    {
        /// <summary>
        /// 带读取的卡片数据类型
        /// 1--排序卡区域   ；
        /// 2--非排序卡区域 ；
        /// 3--所有区域     ；
        /// </summary>
        public int CardType = 3;

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="cardType">带读取的卡片数据类型</param>
        public ReadCardDataBase_Parameter(int cardType)
        {
            CardType = cardType;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if(CardType<1 || CardType > 3)
            {
                throw new ArgumentException("CardType Error!");
            }
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != 1)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteByte(CardType);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 1;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            CardType = databuf.ReadByte();
        }
    }
}
