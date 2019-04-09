using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardDetail
{
    /// <summary>
    /// 读取单个卡片在控制器中的信息
    /// </summary>
    public class ReadCardDetail_Parameter
         : AbstractParameter
    {
        /// <summary>
        /// 要读取详情的授权卡卡号
        /// 取值：1-0xFFFFFFFF
        /// </summary>
        public long CardData;

        public ReadCardDetail_Parameter() { }

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="cardType">带读取的卡片数据类型</param>
        public ReadCardDetail_Parameter(int carddata)
        {
            CardData = carddata;
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
            return;
        }

        /// <summary>
        /// 将结构编码为 字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != 1)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteLong(CardData);
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
            CardData = databuf.ReadLong();
        }

    }
}
