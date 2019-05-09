using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Data.TimeGroup
{
    public class WeekTimeGroup_ReaderWork : WeekTimeGroup
    {
        DayTimeGroup_ReaderWork[] mDay;
        public WeekTimeGroup_ReaderWork(int iDaySegmentCount) : base(iDaySegmentCount)
        {
        }

        public WeekTimeGroup_ReaderWork(int iDaySegmentCount, int index) : base(iDaySegmentCount, index)
        {
        }

        protected override void CreateDayTimeGroup()
        {
            mDay = new DayTimeGroup_ReaderWork[7];
            for (int i = 0; i < 7; i++)
            {
                mDay[i] = new DayTimeGroup_ReaderWork(DaySegmentCount);
            }
        }

        public override int GetDataLen()
        {
            return 7 * DaySegmentCount * 5;
        }
    }
}
