using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Data.TimeGroup
{
    public class DayTimeGroup_ReaderWork : DayTimeGroup
    {
        public DayTimeGroup_ReaderWork(int SegmentCount) : base(SegmentCount)
        {
        }

        public override void SetSegmentCount(int SegmentCount)
        {
            mSegment = new TimeSegment_ReaderWork[SegmentCount];
            for (int i = 0; i < SegmentCount; i++)
            {
                mSegment[i] = new TimeSegment_ReaderWork();
            }
        }
    }
}
