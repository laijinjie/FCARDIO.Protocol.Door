using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.POS.ConsumeParameter.ConsumptionLimits
{
    /// <summary>
    /// 设置消费限额参数
    /// </summary>
    public class WriteConsumptionLimits_Parameter : AbstractParameter
    {
        /// <summary>
        /// 单次限额
        /// </summary>
        public int LimitMoney;

        /// <summary>
        /// 单日限额
        /// </summary>
        public int DayLimitMoney;

        /// <summary>
        /// 单日限次
        /// </summary>
        public byte Limit;

        /// <summary>
        /// 月限额
        /// </summary>
        public int MonthLimitMoney;

        /// <summary>
        /// 月限次
        /// </summary>
        public byte MonthLimit;

        /// <summary>
        /// 卡内最低保留余额
        /// </summary>
        public int MinimumReservedBalance;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public WriteConsumptionLimits_Parameter() { }

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="LimitMoney">单次限额</param>
        /// <param name="DayLimitMoney">单日限额</param>
        /// <param name="Limit">单日限次</param>
        /// <param name="MonthLimitMoney">月限额</param>
        /// <param name="MonthLimit">月限次</param>
        /// <param name="MinimumReservedBalance">卡内最低保留余额</param>
        public WriteConsumptionLimits_Parameter(int LimitMoney, int DayLimitMoney, byte Limit, int MonthLimitMoney, byte MonthLimit, int MinimumReservedBalance)
        {
            this.LimitMoney = LimitMoney;
            this.DayLimitMoney = DayLimitMoney;
            this.Limit = Limit;
            this.MonthLimitMoney = MonthLimitMoney;
            this.MonthLimit = MonthLimit;
            this.MinimumReservedBalance = MinimumReservedBalance;
            if (!checkedParameter())
            {
                throw new ArgumentException("Parameter Error");
            }
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (LimitMoney < 0)
            {
                return false;
            }
            if (DayLimitMoney < 0)
            {
                return false;
            }
            if (MonthLimitMoney < 0)
            {
                return false;
            }
            if (MinimumReservedBalance < 0)
            {
                return false;
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
        /// 对有效期参数进行编码
        /// </summary>
        /// <param name="databuf">需要填充参数结构的字节缓冲区</param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != GetDataLen())
            {
                throw new ArgumentException("databuf len error");
            }
            databuf.WriteInt(LimitMoney);
            databuf.WriteInt(DayLimitMoney);
            databuf.WriteByte(Limit);
            databuf.WriteInt(MonthLimitMoney);
            databuf.WriteByte(MonthLimit);
            databuf.WriteInt(MinimumReservedBalance);
            return databuf;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 18;
        }

        /// <summary>
        /// 对有效期参数进行解码
        /// </summary>
        /// <param name="databuf">包含参数结构的缓冲区</param>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != GetDataLen())
            {
                throw new ArgumentException("databuf Error");
            }
            LimitMoney = databuf.ReadInt();
            DayLimitMoney = databuf.ReadInt();
            Limit = databuf.ReadByte();
            MonthLimitMoney = databuf.ReadInt();
            MonthLimit = databuf.ReadByte();
            MinimumReservedBalance = databuf.ReadInt();


        }
    }
}
