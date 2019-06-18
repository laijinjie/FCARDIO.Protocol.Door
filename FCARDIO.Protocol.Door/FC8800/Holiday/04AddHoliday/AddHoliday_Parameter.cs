using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Holiday
{
    public class AddHoliday_Parameter : AbstractParameter
    {
        public AddHoliday_Parameter() { }

        /// <summary>
        /// 节假日集合
        /// </summary>
        public List<HolidayDetail> ListHoliday { get; private set; }
        public AddHoliday_Parameter(List<HolidayDetail> list) {
            ListHoliday = list;
            if (!checkedParameter())
            {
                throw new ArgumentException("ListHoliday Error");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (ListHoliday == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            ListHoliday = null;
        }

        /// <summary>
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            int iLen = 5 * ListHoliday.Count +4;
            if (databuf.WritableBytes != iLen)
            {
                //throw new ArgumentException("Crad Error");
            }
            databuf.WriteInt(ListHoliday.Count);
            foreach (var holiday in ListHoliday)
            {
                databuf = holiday.GetBytes(databuf);
            }
            //databuf.WriteByte(byte.Parse(ListHoliday.ToString()));
            return databuf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            int iLen = (5 * ListHoliday.Count) + 4;
            return iLen;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            int count = databuf.ReadByte();
        }
    }
}
