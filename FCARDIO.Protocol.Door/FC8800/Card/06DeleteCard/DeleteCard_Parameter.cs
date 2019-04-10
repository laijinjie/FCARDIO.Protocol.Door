using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Card.DeleteCard
{
    /// <summary>
    /// 删除卡片
    /// </summary>
    public class DeleteCard_Parameter:AbstractParameter
    {
        /// <summary>
        /// 需要删除的卡片列表
        /// </summary>
        public long[] CardList;

        public DeleteCard_Parameter() { }

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="cardList">需要删除的卡片列表</param>
        public DeleteCard_Parameter(long[] cardList)
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
            if (databuf.ReadableBytes != 1)
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
            return 1;
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
