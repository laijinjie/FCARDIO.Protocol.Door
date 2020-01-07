using DotNetty.Buffers;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.POS.Data
{
    public class ReservationDetail
    {
        /// <summary>
        /// 卡号
        /// 4字节
        /// </summary>
        public int CardData;

        /// <summary>
        /// 订餐日期
        /// 3字节
        /// </summary>
        public DateTime ReservationDate;

        /// <summary>
        /// 餐段权限
        /// 8位表示8个餐段
        /// </summary>
        public byte TimeGroup;

        public void SetBytes(IByteBuffer data)
        {
            CardData = data.ReadInt();
            ReservationDate = TimeUtil.BCDTimeToDate_yyMMdd(data);
            TimeGroup = data.ReadByte();
        }

        public virtual void GetBytes(IByteBuffer data)
        {
            data.WriteInt(CardData);
            TimeUtil.DateToBCD_yyMMdd(data, ReservationDate);
            data.WriteByte(TimeGroup);
        }
    }
}