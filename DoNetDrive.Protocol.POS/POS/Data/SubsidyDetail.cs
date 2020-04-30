﻿
using DoNetDrive.Protocol.POS.TemplateMethod;
using DoNetDrive.Protocol.Util;
using DotNetty.Buffers;
using System;

namespace DoNetDrive.Protocol.POS.Data
{
    /// <summary>
    /// 补贴
    /// </summary>
    public class SubsidyDetail : TemplateData_Base
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public int CardData { get; set; }

        /// <summary>
        /// 补贴状态
        /// </summary>
        public byte SubsidyState { get; set; }

        /// <summary>
        /// 补贴金
        /// </summary>
        public decimal SubsidyMoney { get; set; }

        /// <summary>
        /// 补贴实际发放金
        /// </summary>
        public decimal ActualSubsidyMoney { get; set; }

        /// <summary>
        /// 补贴截止时间
        /// </summary>
        public DateTime SubsidyDate { get; set; }

        /// <summary>
        /// 补贴类型
        /// </summary>
        public byte SubsidyType { get; set; }

        /// <summary>
        /// 自定义编号
        /// </summary>
        public byte CustomNumber { get; set; }

        public override void SetBytes(IByteBuffer data)
        {
            CardData = data.ReadInt();
            SubsidyState = data.ReadByte();
            SubsidyMoney = data.ReadUnsignedShort();
            SubsidyDate = TimeUtil.BCDTimeToDate_yyMMdd(data);
            ActualSubsidyMoney = data.ReadUnsignedShort();

            SubsidyType = data.ReadByte();
            CustomNumber = data.ReadByte();
        }

      
        public override IByteBuffer GetBytes(IByteBuffer data)
        {
            data.WriteInt(CardData);
            data.WriteByte(SubsidyState);


            data.WriteByte(SubsidyType);
            data.WriteByte(CustomNumber);
            return data;
        }

        /// <summary>
        /// 获取每个添加卡类长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 16;
        }

        public override IByteBuffer GetDeleteBytes(IByteBuffer data)
        {
            throw new NotImplementedException();
        }

        public override int GetDeleteDataLen()
        {
            throw new NotImplementedException();
        }

        public override void SetFailBytes(IByteBuffer buf)
        {
            throw new NotImplementedException();
        }
    }
}
