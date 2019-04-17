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
        protected short[] mBeginTime;
        protected short[] mEndTime;

        public TimeSegment()
        {
            mBeginTime = new short[2];
            mEndTime = new short[2];
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
            mBeginTime[0] = (short)Hour;
            mBeginTime[1] = (short)Minute;
        }

        /**
         * 获取开始时间
         *
         * @param time 开始时间返回的数组，传入时需要数组保持有2个元素 即 new short[2]
         */
        public void GetBeginTime(short[] time)
        {
            time[0] = mBeginTime[0];
            time[1] = mBeginTime[1];
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
            mEndTime[0] = (short)Hour;
            mEndTime[1] = (short)Minute;
        }

        /**
         * 获取开始时间
         *
         * @param time 开始时间返回的数组，传入时需要数组保持有2个元素 即 new short[2]
         */
        public void GetEndTime(short[] time)
        {
            time[0] = mEndTime[0];
            time[1] = mEndTime[1];
        }


        public String toString()
        {
            StringBuilder buf = new StringBuilder(15);
            buf.Append(String.Format("%02d", mBeginTime[0]));
            buf.Append(":");
            buf.Append(String.Format("%02d", mBeginTime[1]));
            buf.Append(" - ");
            buf.Append(String.Format("%02d", mEndTime[0]));
            buf.Append(":");
            buf.Append(String.Format("%02d", mEndTime[1]));
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
        public void GetBytes(IByteBuffer bBuf)
        {

            bBuf.WriteByte(ByteUtil.ByteToBCD((byte)mBeginTime[0]));
            bBuf.WriteByte(ByteUtil.ByteToBCD((byte)mBeginTime[1]));
            bBuf.WriteByte(ByteUtil.ByteToBCD((byte)mEndTime[0]));
            bBuf.WriteByte(ByteUtil.ByteToBCD((byte)mEndTime[1]));
        }

        /**
         * 从字节缓冲区中生成一个对象
         *
         * @param bBuf
         */
        public void SetBytes(IByteBuffer bBuf)
        {
            mBeginTime[0] = ByteUtil.BCDToByte(bBuf.ReadByte());
            mBeginTime[1] = ByteUtil.BCDToByte(bBuf.ReadByte());

            mEndTime[0] = ByteUtil.BCDToByte(bBuf.ReadByte());
            mEndTime[1] = ByteUtil.BCDToByte(bBuf.ReadByte());
        }
    }
}
