using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter
{
    public class TimeUtil
    {
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
    }
}