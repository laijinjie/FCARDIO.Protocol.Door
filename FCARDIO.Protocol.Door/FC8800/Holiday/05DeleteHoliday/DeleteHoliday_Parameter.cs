using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Holiday
{
    public class DeleteHoliday_Parameter : AbstractParameter
    {
        public List<HolidayDetail> ListHoliday { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public DeleteHoliday_Parameter(List<HolidayDetail> list)
        {
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
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteInt(ListHoliday.Count);
            foreach (var holiday in ListHoliday)
            {
                databuf.WriteByte(holiday.Index);
            }
            return databuf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            int iLen = (1 * ListHoliday.Count) + 4;
            return iLen;
        }

        public override void SetBytes(IByteBuffer databuf)
        {

        }
    }
}
