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

        /// <summary>
        /// 从缓冲区中获取值并初始化周时段
        /// </summary>
        /// <param name="FistWeek">一周的第一天</param>
        /// <param name="data"></param>
        public override void SetBytes(E_WeekDay FistWeek, IByteBuffer data)
        {
            int[] WeekList = new int[7];
            GetWeekList(FistWeek, WeekList);
            for (int i = 0; i < 7; i++)
            {
                mDay[WeekList[i]].SetBytes(data);
            }
        }

        public override void SetBytes(IByteBuffer data)
        {
            SetBytes(E_WeekDay.Monday, data);
        }

        public override void GetBytes(IByteBuffer data)
        {
            GetBytes(E_WeekDay.Monday, data);
        }

        public override void GetBytes(E_WeekDay FistWeek, IByteBuffer data)
        {
            int[] WeekList = new int[7];
            GetWeekList(FistWeek, WeekList);
            for (int i = 0; i < 7; i++)
            {
                mDay[WeekList[i]].GetBytes(data);
            }
        }
    }
}
