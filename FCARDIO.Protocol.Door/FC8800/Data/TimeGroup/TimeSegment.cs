using DotNetty.Buffers;
using FCARDIO.Protocol.Door.FC8800.SystemParameter;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Data.TimeGroup
{
    public class TimeSegment
    {
        protected DateTime mBeginTime;
        protected DateTime mEndTime;

        public TimeSegment()
        {

        }

        /**
         * 设置开始时间
         *
         * @param iHour 开始时间的小时部分 取值范围 0-23
         * @param Minute 开始时间的分钟部分 取值范围 0-59
         */
        public void SetBeginTime(int Hour, int Minute)
        {
            CheckTime(Hour, Minute);
            DateTime n = DateTime.Now;
            mBeginTime = new DateTime(n.Year, n.Month, n.Day, Hour, Minute, 0);
        }

        /**
         * 获取开始时间
         *
         * @param time 开始时间返回的数组，传入时需要数组保持有2个元素 即 new short[2]
         */
        public DateTime GetBeginTime()
        {
            return mBeginTime;
        }

        /**
         * 设置结束时间
         *
         * @param iHour 开始时间的小时部分 取值范围 0-23
         * @param Minute 开始时间的分钟部分 取值范围 0-59
         */
        public void SetEndTime(int Hour, int Minute)
        {
            CheckTime(Hour, Minute);
            DateTime n = DateTime.Now;
            mEndTime = new DateTime(n.Year, n.Month, n.Day, Hour, Minute, 0);
        }

        /**
         * 获取开始时间
         *
         * @param time 开始时间返回的数组，传入时需要数组保持有2个元素 即 new short[2]
         */
        public DateTime GetEndTime()
        {
            return mEndTime;
        }


        public String toString()
        {
            StringBuilder buf = new StringBuilder(15);
            buf.Append(mBeginTime.ToString("HH:mm"));
            buf.Append(" - ");
            buf.Append(mEndTime.ToString("HH:mm"));
            return buf.ToString();
        }

        private void CheckTime(int Hour, int Minute)
        {
            if (Hour < 0 || Hour > 23)
            {
                throw new ArgumentException("Hour -- 0-23");
            }

            if (Minute < 0 || Minute > 59)
            {
                throw new ArgumentException("Minute -- 0-59");
            }
        }

        /**
         * 将对象写入到字节缓冲区
         *
         * @param bBuf
         */
        public virtual void GetBytes(IByteBuffer bBuf)
        {

            bBuf.WriteByte(ByteUtil.ByteToBCD((byte)mBeginTime.Hour));
            bBuf.WriteByte(ByteUtil.ByteToBCD((byte)mBeginTime.Minute));
            bBuf.WriteByte(ByteUtil.ByteToBCD((byte)mEndTime.Hour));
            bBuf.WriteByte(ByteUtil.ByteToBCD((byte)mEndTime.Minute));
        }

        /**
         * 从字节缓冲区中生成一个对象
         *
         * @param bBuf
         */
        public virtual void SetBytes(IByteBuffer bBuf)
        {
            DateTime n = DateTime.Now;
            mBeginTime = new DateTime(n.Year, n.Month, n.Day, ByteUtil.BCDToByte(bBuf.ReadByte()), ByteUtil.BCDToByte(bBuf.ReadByte()), 0);
            mEndTime = new DateTime(n.Year, n.Month, n.Day, ByteUtil.BCDToByte(bBuf.ReadByte()), ByteUtil.BCDToByte(bBuf.ReadByte()), 0);
        }
    }
}
