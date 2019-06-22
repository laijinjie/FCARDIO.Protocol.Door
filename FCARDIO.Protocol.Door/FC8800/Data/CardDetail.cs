using DotNetty.Buffers;
using FCARDIO.Core.Extension;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FCARDIO.Protocol.Door.FC8800.Data
{
    /// <summary>
    /// FC88A、MC58 卡片权限详情
    /// </summary>
    public class CardDetail : CardDetailBase
    {

        /// <summary>
        /// 获取一个卡详情实例，序列化到buf中的字节占比
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x21;//33字节
        }

        /// <summary>
        /// 将卡号序列化并写入buf中
        /// </summary>
        /// <param name="data"></param>
        public override void WriteCardData(IByteBuffer data)
        {
            data.WriteByte(0);
            data.WriteInt((int)CardData);
        }

        /// <summary>
        /// 从buf中读取卡号
        /// </summary>
        /// <param name="data"></param>
        public override void ReadCardData(IByteBuffer data)
        {
            data.ReadByte();

            CardData = data.ReadUnsignedInt();
        }
    }
}
