using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Card.CardDetail
{
    /// <summary>
    /// FC89H 读取单个卡片在控制器中的信息
    /// </summary>
    public class ReadCardDetail_Parameter:FC8800.Card.CardDetail.ReadCardDetail_Parameter
    {
        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="carddata">需要读取卡片详情的卡号</param>
        public ReadCardDetail_Parameter(UInt64 carddata):base(carddata)
        {

        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (CardData == 0)
            {
                throw new ArgumentException("CardData Error!");
            }
            return true;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 9;
        }

        /// <summary>
        /// 将结构编码为 字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes < 9)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteByte(0);
            databuf.WriteLong((long)CardData);
            return databuf;
        }
    }
}
