using DotNetty.Buffers;
using FCARDIO.Protocol.Door.FC8800.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Data
{
    /// <summary>
    /// FC89H 适用于此型号的卡详情
    /// </summary>
    public class CardDetail : CardDetailBase
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public CardDetail() { }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="sur">授权卡信息 抽象类</param>
        public CardDetail(CardDetailBase sur) : base(sur) { }

        /// <summary>
        /// 获取一个卡详情实例，序列化到buf中的字节占比
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x25;//37字节
        }

        /// <summary>
        /// 将卡号序列化并写入buf中
        /// </summary>
        /// <param name="data"></param>
        public override void WriteCardData(IByteBuffer data)
        {
            data.WriteByte(0);
            data.WriteLong((long)CardData);
        }

        /// <summary>
        /// 从buf中读取卡号
        /// </summary>
        /// <param name="data"></param>
        public override void ReadCardData(IByteBuffer data)
        {
            data.ReadByte();

            CardData = (UInt64)data.ReadLong();
        }

    }
}
