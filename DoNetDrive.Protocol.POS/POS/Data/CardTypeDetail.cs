using DotNetty.Buffers;
using DoNetDrive.Core.Data;
using System;
using DoNetDrive.Protocol.POS.TemplateMethod;

namespace DoNetDrive.Protocol.POS.Data
{
    /// <summary>
    /// 卡类
    /// </summary>
    public class CardTypeDetail : TemplateData_Base
    {
        /// <summary>
        /// 卡类类型 
        /// 1字节
        /// </summary>
        public byte CardType { get; set; }

        /// <summary>
        /// 消费折扣
        /// 1字节
        /// </summary>
        public byte Discount { get; set; }

        /// <summary>
        /// 月补贴
        /// 4字节
        /// </summary>
        public int SubsidyMoney { get; set; }

        /// <summary>
        /// 餐段权限
        /// 1字节
        /// </summary>
        public byte TimeGroup { get; set; }

        /// <summary>
        /// 积分倍率
        /// 1字节
        /// </summary>
        public byte Integral { get; set; }

        /// <summary>
        /// 获取指定餐段是否有权限
        /// </summary>
        /// <param name="iDoor">餐段权限，取值范围：1-8</param>
        /// <returns>true 有权限，false 无权限</returns>
        public bool GetTimeGroup(int iTimeGroup)
        {
            if (iTimeGroup < 0 || iTimeGroup > 8)
            {

                throw new ArgumentException("TimeGroup 1-8");
            }
            iTimeGroup -= 1;

            int iBitIndex = iTimeGroup % 8;
            int iMaskValue = (int)Math.Pow(2, iBitIndex);
            int iByteValue = TimeGroup & iMaskValue;
            if (iBitIndex > 0)
            {
                iByteValue = iByteValue >> (iBitIndex);
            }
            return iByteValue == 1;
        }

        public override void SetBytes(IByteBuffer data)
        {
            CardType = data.ReadByte();
            Discount = data.ReadByte();
            SubsidyMoney = data.ReadInt();
            TimeGroup = data.ReadByte();
            Integral = data.ReadByte();

        }

        /// <summary>
        /// 写入 要删除的密码信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IByteBuffer GetDeleteBytes(IByteBuffer data)
        {
            data.WriteByte(CardType);
            return data;
        }

        public override IByteBuffer GetBytes(IByteBuffer data)
        {
            data.WriteByte(CardType);
            data.WriteByte(Discount);
            data.WriteInt(SubsidyMoney);
            data.WriteByte(TimeGroup);
            data.WriteByte(Integral);
            return data;
        }

        /// <summary>
        /// 获取每个添加卡类长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 8;
        }

        /// <summary>
        /// 获取每个删除卡类长度
        /// </summary>
        /// <returns></returns>
        public override int GetDeleteDataLen()
        {
            return 1;
        }

        public override void SetFailBytes(IByteBuffer databuf)
        {
            CardType = databuf.ReadByte();
        }
    }
}
