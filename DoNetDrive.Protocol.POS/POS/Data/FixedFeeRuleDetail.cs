using DotNetty.Buffers;
using DoNetDrive.Core.Data;
using System.Text;

namespace DoNetDrive.Protocol.POS.Data
{
    /// <summary>
    /// 定额扣费规则
    /// </summary>
    public class FixedFeeRuleDetail : AbstractData
    {
        /// <summary>
        /// 序号
        /// </summary>
        public byte SerialNumber;

        /// <summary>
        /// 开始时间
        /// </summary>
        public ushort BeginTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        public ushort EndTime;

        /// <summary>
        /// 定额值
        /// </summary>
        public int FixedFee;

        /// <summary>
        /// 消费限额
        /// </summary>
        public int ConsumptionLimits;

        /// <summary>
        /// 限次
        /// </summary>
        public byte Limite;


        /// <summary>
        /// 计次卡扣次
        /// </summary>
        public byte CountingCardsDeductionCount;

        /// <summary>
        /// 计次卡限次
        /// </summary>
        public byte CountingCardsLimitsCount;

        /// <summary>
        /// 是否订餐
        /// </summary>
        public byte IsReservation;

        /// <summary>
        /// 餐段名称
        /// </summary>
        public string MealTimeName;

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteByte(SerialNumber);
            databuf.WriteUnsignedShort(BeginTime);
            databuf.WriteUnsignedShort(EndTime);
            databuf.WriteMedium(FixedFee);
            databuf.WriteMedium(ConsumptionLimits);
            databuf.WriteByte(Limite);
            databuf.WriteByte(CountingCardsDeductionCount);
            databuf.WriteByte(CountingCardsLimitsCount);
            databuf.WriteByte(IsReservation);
            Util.StringUtil.WriteString(databuf, MealTimeName, 10, Encoding.BigEndianUnicode);
            return databuf;
        }

        public override int GetDataLen()
        {
            return 25;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            SerialNumber = databuf.ReadByte();
            BeginTime = databuf.ReadUnsignedShort();
            EndTime = databuf.ReadUnsignedShort();
            FixedFee = databuf.ReadUnsignedMedium();
            ConsumptionLimits = databuf.ReadUnsignedMedium();
            Limite = databuf.ReadByte();
            CountingCardsDeductionCount = databuf.ReadByte();
            CountingCardsLimitsCount = databuf.ReadByte();
            IsReservation = databuf.ReadByte();
            MealTimeName = Util.StringUtil.GetString(databuf, 10, Encoding.BigEndianUnicode);
        }
    }
}
