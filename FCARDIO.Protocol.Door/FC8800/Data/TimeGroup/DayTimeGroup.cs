﻿using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Data.TimeGroup
{
    public class DayTimeGroup
    {
        protected TimeSegment[] mSegment;

        /**
         * 初始化天时段
         * @param SegmentCount  一天中的时段数量
         */
        public DayTimeGroup(int SegmentCount)
        {
            SetSegmentCount(SegmentCount);
        }

        /**
         * 设置一天中包含的时段数量
         *
         * @param SegmentCount 时段数量
         */
        public virtual void SetSegmentCount(int SegmentCount)
        {
            mSegment = new TimeSegment[SegmentCount];
            for (int i = 0; i < SegmentCount; i++)
            {
                mSegment[i] = new TimeSegment();
            }
        }

        /**
         * 获取一天中包含的时段数量
         *
         * @return 时段数量
         */
        public int GetSegmentCount()
        {
            if (mSegment == null)
            {
                return 0;

            }
            return mSegment.Length;
        }

        /**
         * 获取一个时段，进行操作
         * @param iIndex 此时段在这一天当中的索引号，索引从0开始
         * @return 时间段
         */
        public TimeSegment GetItem(int iIndex)
        {
            if (iIndex < 0 || iIndex > GetSegmentCount())
            {
                throw new ArgumentException("iIndex<0 || iIndex > GetSegmentCount()");
            }
            return mSegment[iIndex];
        }

        /**
         * 将对象写入到字节缓冲区
         *
         * @param bBuf
         */
        public void GetBytes(IByteBuffer bBuf)
        {
            int iCount = GetSegmentCount();
            for (int i = 0; i < iCount; i++)
            {
                mSegment[i].GetBytes(bBuf);
            }

        }

        /**
         * 从字节缓冲区中生成一个对象
         *
         * @param bBuf
         */
        public void SetBytes(IByteBuffer bBuf)
        {
            int iCount = GetSegmentCount();
            for (int i = 0; i < iCount; i++)
            {
                mSegment[i].SetBytes(bBuf);
                if (bBuf.ReadableBytes == 0)
                {
                    return;
                }
            }
        }
    }
}
