using DotNetty.Buffers;
using DoNetDrive.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoNetDrive.Protocol.POS.TemplateMethod;

namespace DoNetDrive.Protocol.POS.Data
{
    public class ReservationDetail : TemplateData_Base
    {
        /// <summary>
        /// 卡号
        /// 4字节
        /// </summary>
        public int CardData { get; set; }

        /// <summary>
        /// 订餐日期
        /// 3字节
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// 餐段权限
        /// 8位表示8个餐段
        /// </summary>
        public byte TimeGroup;

        public override void SetBytes(IByteBuffer data)
        {
            CardData = data.ReadInt();
            ReservationDate = TimeUtil.BCDTimeToDate_yyMMdd(data);
            TimeGroup = data.ReadByte();
        }

        public override IByteBuffer GetBytes(IByteBuffer data)
        {
            data.WriteInt(CardData);
            TimeUtil.DateToBCD_yyMMdd(data, ReservationDate);
            data.WriteByte(TimeGroup);
            return data;
        }

        /// <summary>
        /// 获取每个添加卡类长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 64;
        }

        public override IByteBuffer GetDeleteBytes(IByteBuffer data)
        {
            throw new NotImplementedException();
        }

        public override int GetDeleteDataLen()
        {
            throw new NotImplementedException();
        }

        public override void SetFailBytes(IByteBuffer databuf)
        {

        }
    }
}