using DotNetty.Buffers;
using DoNetDrive.Protocol.Transaction;
using DoNetDrive.Protocol.Util;
using System;

namespace DoNetDrive.Protocol.POS.Data
{
    /// <summary>
    /// 读卡记录
    /// </summary>
    public class CardTransaction : AbstractTransaction
    {
       
        /// <summary>
        /// 卡号
        /// </summary>
        public ulong CardData;

        /// <summary>
        /// 机器类型（消费模式）
        /// 1 - 标准收费
        /// 2 - 定额收费
        /// 3 - 菜单收费
        /// 4 - 订餐机
        /// 5 - 补贴机
        /// 6 - 子账收费
        /// 7 - 子账补贴
        /// </summary>
        public int POSWorkMode;

        /// <summary>
        /// 卡类
        /// IC卡的卡类型 255-表示计次卡
        /// </summary>
        public byte CardType;

        /// <summary>
        /// 消费次序
        /// </summary>
        public ushort POSSerialNumber;

        /// <summary>
        /// 折扣
        /// </summary>
        public byte Discount;

        /// <summary>
        /// 现金账户现金账户(3B)
        /// </summary>
        public int POSMoney;

        /// <summary>
        /// 补贴账户现金账户(3B)
        /// </summary>
        public int SubsidyMoney;

        /// <summary>
        /// 现金余额(3B)
        /// </summary>
        public int POSMoneyTotal;

        /// <summary>
        /// 补贴余额(3B)
        /// </summary>
        public int POSSubsidyMoneyTotal;

        /// <summary>
        /// 消费时段
        /// 定额消费时使用的消费时段，取值1-8
        /// </summary>
        public byte POSTimeGroup;

        /// <summary>
        /// 特殊标记
        /// 0-无特殊情况
        /// 1-灰记录
        /// 2-已退款
        /// </summary>
        public byte SpecialMarking;

        /// <summary>
        /// 获取读卡记录格式长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 35;
        }

        public CardTransaction()
        {
            _TransactionType = 1;
        }
        /// <summary>
        /// 从buf中读取记录数据
        /// </summary>
        /// <param name="dtBuf"></param>
        public override void SetBytes(IByteBuffer dtBuf)
        {
            try
            {
                CardData = (ulong)dtBuf.ReadLong();
                POSWorkMode = dtBuf.ReadByte();
                CardType = dtBuf.ReadByte();
                _TransactionCode = dtBuf.ReadByte();

                _TransactionDate = TimeUtil.BCDTimeToDate_yyMMddhhmmssByDex(dtBuf);
                POSSerialNumber = dtBuf.ReadUnsignedShort();
                Discount = dtBuf.ReadByte();
                POSMoney = dtBuf.ReadUnsignedMedium();
                SubsidyMoney = dtBuf.ReadUnsignedMedium();
                POSMoneyTotal = dtBuf.ReadUnsignedMedium();
                SubsidyMoney = dtBuf.ReadUnsignedMedium();
                POSTimeGroup = dtBuf.ReadByte();
                SpecialMarking = dtBuf.ReadByte();
            }
            catch (Exception e)
            {
            }

            return;

        }
    }
}
