using DotNetty.Buffers;
using FCARDIO.Core.Extension;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FCARDIO.Protocol.Door.FC8800.Data
{
    /// <summary>
    /// 卡片权限详情
    /// </summary>
    //public abstract class CardDetailBase
    public class CardDetail : CardDetailBase
    {
        /// <summary>
        /// 
        /// </summary>
        public CardDetail()
        {
            CardData = 0;
            Password = null;
            Expiry = DateTime.Now;
            TimeGroup = new byte[4];
            Door = 0;
            Privilege = 0;
            CardStatus = 0;
            Holiday = new byte[] { (byte)255, (byte)255, (byte)255, (byte)255 };
            RecordTime = DateTime.Now;
            EnterStatus = 0;
            HolidayUse = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public override void WriteCardData(IByteBuffer data)
        {
            data.WriteByte(0);
            data.WriteInt((int)CardData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public override void ReadCardData(IByteBuffer data)
        {
            data.ReadByte();
            CardData = data.ReadUnsignedInt();
        }
    }
}
