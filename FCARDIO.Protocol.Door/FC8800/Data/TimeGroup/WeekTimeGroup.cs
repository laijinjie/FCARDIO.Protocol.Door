using DotNetty.Buffers;
using FCARDIO.Protocol.Door.FC8800.SystemParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FCARDIO.Protocol.Door.FC8800.Data.TimeGroup
{
    public class WeekTimeGroup
    {

        protected DayTimeGroup[] mDay;
        private int iDaySegmentCount;
        protected int mIndex;
        protected int DaySegmentCount;

        public bool HasValue { get; private set; }

        /**
         * 初始化一个周时段
         *
         * @param iDaySegmentCount 一天中的时段数量
         */
        public WeekTimeGroup(int iDaySegmentCount)
        {

            DaySegmentCount = iDaySegmentCount;
            CreateDayTimeGroup();
        }

        /**
         * 创建一周中的天时段
         */
        protected virtual void CreateDayTimeGroup()
        {
            mDay = new DayTimeGroup[7];
            for (int i = 0; i < 7; i++)
            {
                mDay[i] = new DayTimeGroup(DaySegmentCount);
            }
        }

        public WeekTimeGroup(int iDaySegmentCount, int index)
        {
            this.iDaySegmentCount = iDaySegmentCount;
            mIndex = index;
        }

        /**
         * 获取在周时段列表中的索引号
         *
         * @return 索引号 1-64
         */
        public int GetIndex()
        {
            return mIndex;
        }

        /**
         * 设定在周时段列表中的索引号
         *
         * @param index 索引号 1-64
         */
        public void SetIndex(int index)
        {
            mIndex = index;
        }

        public DayTimeGroup GetItem(E_WeekDay week)
        {
            return mDay[(int)week];
        }

        public DayTimeGroup GetItem(int index)
        {
            return mDay[index];
        }


        public virtual int GetDataLen()
        {
            return 7 * DaySegmentCount * 4;
        }


        public virtual void SetBytes(IByteBuffer data)
        {
            data.ReadByte();
            data.ReadByte();
            SetBytes(E_WeekDay.Monday, data);
        }

        public virtual void ReadDoorWorkSetBytes(IByteBuffer data)
        {
            SetBytes(E_WeekDay.Monday, data);
        }

        /**
         * 从缓冲区中获取值并初始化周时段
         *
         * @param FistWeek 一周的第一天
         * @param data
         */
        public virtual void SetBytes(E_WeekDay FistWeek, IByteBuffer data)
        {
            HasValue = true;
            int [] WeekList = new int[7];
            GetWeekList(FistWeek, WeekList);
            for (int i = 0; i < 7; i++)
            {
                mDay[WeekList[i]].SetBytes(data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FistWeek"></param>
        /// <param name="WeekList"></param>
        protected void GetWeekList(E_WeekDay FistWeek, int[] WeekList)
        {
            int lBeginIndex = (int)FistWeek;
            int iEndIndex = 6 - lBeginIndex;
            for (int i = 0; i <= iEndIndex; i++)
            {
                WeekList[i] = lBeginIndex + i;
            }
            if (lBeginIndex != 0)
            {
                iEndIndex += 1;
                lBeginIndex = 0;
                for (int i = iEndIndex; i <= 6; i++)
                {
                    WeekList[i] = lBeginIndex;
                    lBeginIndex += 1;
                }
            }
        }



        /**
         * 使用从周一为一周的第一天进行排序的缓冲区获取时段信息
         * @param data 
         */
        public virtual void GetBytes(IByteBuffer data)
        {
            GetBytes(E_WeekDay.Monday, data);
        }

        public virtual void GetBytes(E_WeekDay FistWeek, IByteBuffer data)
        {
            int [] WeekList = new int[7];
            GetWeekList(FistWeek, WeekList);
            for (int i = 0; i < 7; i++)
            {
                mDay[WeekList[i]].GetBytes(data);
            }
        }

        /**
         * 克隆一个周时段
         *
         * @return
         */
        public virtual WeekTimeGroup Clone()
        {
            WeekTimeGroup w = new WeekTimeGroup(DaySegmentCount);
            IByteBuffer bBuf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(DaySegmentCount * 4);
            for (int i = 0; i < 10; i++)
            {
                mDay[i].GetBytes(bBuf);   
                w.mDay[i].SetBytes(bBuf);
                bBuf.Clear();
            }
            return w;
        }

        public void SetNowTime()
        {
            for (int y = 0; y < 7; y++)
            {
                DayTimeGroup dayTimeGroup = GetItem(y);
                //每天时段
                for (int i = 0; i < 8; i++)
                {
                    DateTime dt = DateTime.Now;
                    //dt = dt.AddMinutes(-1);
                    TimeSegment segment = dayTimeGroup.GetItem(i);
                    dt = dt.AddMinutes(i + 1);
                    segment.SetBeginTime(dt.Hour, dt.Minute);
                    dt = dt.AddMinutes(i + 1);
                    segment.SetEndTime(dt.Hour, dt.Minute);
                }
            }
            HasValue = true;
        }

        public void InitTimeGroup()
        {
            for (int y = 0; y < 7; y++)
            {
                DayTimeGroup dayTimeGroup = GetItem(y);
                //每天时段
                for (int i = 0; i < 8; i++)
                {
                    TimeSegment segment = dayTimeGroup.GetItem(i);
                    segment.SetBeginTime(0, 0);
                    segment.SetEndTime(0, 0);
                }
            }
            HasValue = true;
        }
    }
}
