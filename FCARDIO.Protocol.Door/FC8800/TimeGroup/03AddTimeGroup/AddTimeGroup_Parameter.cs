using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;

namespace FCARDIO.Protocol.Door.FC8800.TimeGroup
{
    public class AddTimeGroup_Parameter : AbstractParameter
    {
        private int writeIndex = 0;
        /// <summary>
        /// 
        /// </summary>
        public List<WeekTimeGroup> ListWeekTimeGroup { get; private set; }
        public AddTimeGroup_Parameter(List<WeekTimeGroup> list)
        {
            ListWeekTimeGroup = list;
        }
        public override bool checkedParameter()
        {
            if (ListWeekTimeGroup == null || ListWeekTimeGroup.Count == 0)
            {
                return false;
            }
            return true;
        }

        public override void Dispose()
        {
            ListWeekTimeGroup = null;
        }

        public void SetWriteIndex(int index)
        {
            if (index < ListWeekTimeGroup.Count)
            {
                writeIndex = index;
            }
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteByte(writeIndex + 1);
            //foreach (WeekTimeGroup tg in ListWeekTimeGroup)
            //{
            //    tg.GetBytes(databuf);
            //}
            ListWeekTimeGroup[writeIndex].GetBytes(databuf);
            return databuf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 225;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
           

        }
    }
}
