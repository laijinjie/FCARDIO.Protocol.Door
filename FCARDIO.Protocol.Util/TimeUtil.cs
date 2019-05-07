using System;


namespace FCARDIO.Protocol.Util
{
   public class TimeUtil
    {
        public static DateTime BCDTimeToDate_ssmmhhddMMyy(byte[]  btTime)
        {
            btTime = ByteUtil.BCDToByte(btTime);
            int year = btTime[5];
            int month = btTime[4];
            int dayOfMonth = btTime[3];
            int hourOfDay = btTime[2];
            int minute = btTime[1];
            int second = btTime[0];


            if (year > 99)
            {
                return DateTime.Now;
            }
            if (month == 0 || month > 12)
            {
                return DateTime.Now;
            }
            if (dayOfMonth == 0 || dayOfMonth > 31)
            {
                return DateTime.Now;
            }
            if (hourOfDay > 23)
            {
                return DateTime.Now;
            }

            if (minute > 59)
            {
                return DateTime.Now;
            }

            if (second > 59)
            {
                return DateTime.Now;
            }

            DateTime dTime = new DateTime(2000 + year, month - 1, dayOfMonth, hourOfDay, minute, second);
            return dTime;
        }

        public static DateTime BCDTimeToDate_yyMMddhh(byte [] btTime)
        {
            btTime = ByteUtil.BCDToByte(btTime);
            int year = btTime[0];
            int month = btTime[1];
            int dayOfMonth = btTime[2];
            int hourOfDay = btTime[3];


            if (year > 99)
            {
                return DateTime.Now;
            }
            if (month == 0 || month > 12)
            {
                return DateTime.Now;
            }
            if (dayOfMonth == 0 || dayOfMonth > 31)
            {
                return DateTime.Now;
            }
            if (hourOfDay > 23)
            {
                return DateTime.Now;
            }

            DateTime dTime = new DateTime(2000 + year, month - 1, dayOfMonth, hourOfDay, 0, 0);
            return dTime;
        }

        public static void DateToBCD_yyMMddhh(byte[] btData, DateTime date)
        {
            if (date == null)
            {
                for (int i = 0; i < 4; i++)
                {
                    btData[i] = 0;
                }
            }
            else
            {
                btData[0] = (byte)(date.Year - 2000);
                btData[1] = (byte)(date.Month + 1);
                btData[2] = (byte)date.Day;
                btData[3] = (byte)date.Hour;
                btData = ByteUtil.ByteToBCD(btData);
            }
        }

        public static void DateToBCD_ssmmhhddMMwwyy(byte[] btData, DateTime date)
        {
            if (date == null)
            {
                for (int i = 0; i < 7; i++)
                {
                    btData[i] = 0;
                }
            }
            else
            {
                int MONTH = date.Day - 1;//这里获取到的周，周日表示1，周一表示2 ... 周六表示 7
                if (MONTH == 0)
                {
                    MONTH = 7;
                }
                btData[6] = (byte)(date.Year - 2000);
                btData[5] = (byte)MONTH;
                btData[4] = (byte)(date.Month + 1);
                btData[3] = (byte)date.Day;
                btData[2] = (byte)date.Hour;
                btData[1] = (byte)date.Minute;
                btData[0] = (byte)date.Second;
                btData = ByteUtil.ByteToBCD(btData);
            }
        }


        public static DateTime BCDTimeToDate_yyMMddhhmm(byte [] btTime)
        {
            btTime = ByteUtil.BCDToByte(btTime);
            int year = btTime[0];
            int month = btTime[1];
            int dayOfMonth = btTime[2];
            int hourOfDay = btTime[3];
            int minute = btTime[4];

            if (year > 99)
            {
                return DateTime.Now;
            }
            if (month == 0 || month > 12)
            {
                return DateTime.Now;
            }
            if (dayOfMonth == 0 || dayOfMonth > 31)
            {
                return DateTime.Now;
            }
            if (hourOfDay > 23)
            {
                return DateTime.Now;
            }

            if (minute > 59)
            {
                return DateTime.Now;
            }

            DateTime dTime = new DateTime(2000 + year, month - 1, dayOfMonth, hourOfDay, minute, 0);
            return dTime;
        }

        public static void DateToBCD_yyMMddhhmm(byte[] btData, DateTime date)
        {
            if (date == null)
            {
                for (int i = 0; i < 5; i++)
                {
                    btData[i] = 0;
                }
            }
            else
            {
                btData[0] = (byte)(date.Year - 2000);
                btData[1] = (byte)(date.Month + 1);
                btData[2] = (byte)date.Day;
                btData[3] = (byte)date.Hour;
                btData[4] = (byte)date.Minute;
                btData = ByteUtil.ByteToBCD(btData);
            }
        }

        public static DateTime BCDTimeToDate_yyMMddhhmmss(byte [] btTime)
        {
            btTime = ByteUtil.BCDToByte(btTime);
            int year = btTime[0];
            int month = btTime[1];
            int dayOfMonth = btTime[2];
            int hourOfDay = btTime[3];
            int minute = btTime[4];
            int second = btTime[5];

            if (year > 99)
            {
                return DateTime.Now;
            }
            if (month == 0 || month > 12)
            {
                return DateTime.Now;
            }
            if (dayOfMonth == 0 || dayOfMonth > 31)
            {
                return DateTime.Now;
            }
            if (hourOfDay > 23)
            {
                return DateTime.Now;
            }

            if (minute > 59)
            {
                return DateTime.Now;
            }
            if (second > 59)
            {
                return DateTime.Now;
            }

            DateTime dTime = new DateTime(2000 + year, month - 1, dayOfMonth, hourOfDay, minute, second);
            return dTime;
        }

        public static void DateToBCD_yyMMddhhmmss(byte[] btData, DateTime date)
        {
            if (date == null)
            {
                for (int i = 0; i < 6; i++)
                {
                    btData[i] = 0;
                }
            }
            else
            {
                btData[0] = (byte)(date.Year - 2000);
                btData[1] = (byte)(date.Month + 1);
                btData[2] = (byte)date.Day;
                btData[3] = (byte)date.Hour;
                btData[4] = (byte)date.Minute;
                btData = ByteUtil.ByteToBCD(btData);
            }
        }

        /**
         * 将时间类型格式化为 yyyy-MM-dd HH:mm:ss
         *
         * @param date 需要格式化的时间
         * @return 时间字符串
         */
        public static String FormatTime(DateTime date)
        {
            if (date == null)
            {
                return "";
            }
            string format = date.ToString("yyyy-MM-dd HH:mm:ss");
            return format;
        }

        /**
         * 将时间类型格式化为 yyyy-MM-dd HH:mm
         *
         * @param date 需要格式化的时间
         * @return 时间字符串
         */
        public static String FormatTimeHHmm(DateTime date)
        {
            if (date == null)
            {
                return "";
            }
            string format = date.ToString("yyyy-MM-dd HH:mm");
            return format;
        }

        public static String FormatTimeMillisecond(DateTime date)
        {
            if (date == null)
            {
                return "";
            }
            string format = date.ToString("HH:mm:ss.sss");
            return format;
        }

        /// <summary>
        /// BCD格式日期字节数组转换为日期类型
        /// </summary>
        /// <param name="btTime"></param>
        /// <returns></returns>
        public static DateTime BCDTimeToDate_ssmmHHddMMWWyy(byte[] btTime)
        {
            btTime = ByteUtil.BCDToByte(btTime);
            int year = btTime[6];
            int month = btTime[4];
            int dayOfMonth = btTime[3];
            int hourOfDay = btTime[2];
            int minute = btTime[1];
            int second = btTime[0];


            if (year > 99)
            {
                return DateTime.Now;
            }
            if (month == 0 || month > 12)
            {
                return DateTime.Now;
            }
            if (dayOfMonth == 0 || dayOfMonth > 31)
            {
                return DateTime.Now;
            }
            if (hourOfDay > 23)
            {
                return DateTime.Now;
            }

            if (minute > 59)
            {
                return DateTime.Now;
            }

            if (second > 59)
            {
                return DateTime.Now;
            }

            DateTime dTime = new DateTime(2000 + year, month, dayOfMonth, hourOfDay, minute, second);
            return dTime;
        }
    }
}
