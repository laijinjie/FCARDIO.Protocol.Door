using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.POS.Data
{
    /// <summary>
    /// 卡类
    /// </summary>
    public class CardTypeDetail
    {
        /// <summary>
        /// 卡类类型 
        /// 1字节
        /// </summary>
        public byte CardType;

        /// <summary>
        /// 消费折扣
        /// 1字节
        /// </summary>
        public byte Discount;

        /// <summary>
        /// 月补贴
        /// 4字节
        /// </summary>
        public int SubsidyMoney;

        /// <summary>
        /// 餐段权限
        /// 1字节
        /// </summary>
        public byte TimeGroup;

        /// <summary>
        /// 积分倍率
        /// 1字节
        /// </summary>
        public byte Integral;

        public void SetBytes(IByteBuffer data)
        {
            CardType = data.ReadByte();
            Discount = data.ReadByte();
            SubsidyMoney = data.ReadInt();
            TimeGroup = data.ReadByte();
            Integral = data.ReadByte();

        }

        public virtual void GetBytes(IByteBuffer data)
        {
            data.WriteByte(CardType);
            data.WriteByte(Discount);
            data.WriteInt(SubsidyMoney);
            data.WriteByte(TimeGroup);
            data.WriteByte(Integral);
        }
    }
}
