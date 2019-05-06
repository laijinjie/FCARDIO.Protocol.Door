﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Utility
{
    public class StringUtility
    {
        /// <summary>
        /// 填充字符串
        /// </summary>
        /// <param name="str">需要被填充的字符串</param>
        /// <param name="iLen">字符串长度</param>
        /// <param name="fillstr">填充的字符串</param>
        /// <returns></returns>
        public static string FillString(string str, int iLen, string fillstr)
        {
            return FillString(str, iLen, fillstr, true);
        }

        /// <summary>
        /// 填充字符串
        /// </summary>
        /// <param name="str">需要被填充的字符串</param>
        /// <param name="iLen">字符串长度</param>
        /// <param name="fillstr">填充的字符串</param>
        /// <param name="fill_right">是否右边填充</param>
        /// <returns></returns>
        public static string FillString(string str, int iLen, string fillstr, bool fill_right)
        {
            int iStrLen = 0;

            if (!string.IsNullOrEmpty(str))
            {
                iStrLen = str.Length;

                if (iStrLen == iLen)
                {
                    return str;
                }
            }

            if (iStrLen > iLen)
            {
                if (fill_right)
                {
                    return str.Substring(0, iLen);
                }
                else
                {
                    return str.Substring(iStrLen - iLen, iStrLen);
                }

            }
            StringBuilder sbuf = new StringBuilder(iLen);

            int iAddCount = iLen - iStrLen;
            for (int i = 0; i < iAddCount; i++)
            {
                sbuf.Append(fillstr);
                if (sbuf.Length > iAddCount)
                {
                    break;
                }
            }
            if (sbuf.Length > iAddCount)
            {
                sbuf.Length = iAddCount;
            }
            if (!string.IsNullOrEmpty(str))
            {
                if (fill_right)
                {
                    sbuf.Insert(0, str);
                }
                else
                {
                    sbuf.Append(str);
                }

            }
            return sbuf.ToString();
        }


        /// <summary>
        /// BCD格式日期时间转DateTime
        /// </summary>
        /// <param name="btTime">字节数组</param>
        /// <returns></returns>
        public static DateTime BCDTimeToDate_yyMMddhh(byte[] btTime)
        {
            btTime = BCDToByte(btTime);
            int year = uByte(btTime[0]);
            int month = uByte(btTime[1]);
            int dayOfMonth = uByte(btTime[2]);
            int hourOfDay = uByte(btTime[3]);

            if (year > 99)
            {
                return new DateTime();
            }
            if (month == 0 || month > 12)
            {
                return new DateTime();
            }
            if (dayOfMonth == 0 || dayOfMonth > 31)
            {
                return new DateTime();
            }
            if (hourOfDay > 23)
            {
                return new DateTime();
            }

            DateTime dTime = new DateTime(2000 + year, month, dayOfMonth, hourOfDay, 0, 0);
            return dTime;
        }

        /// <summary>
        /// BCD格式转字节数组
        /// </summary>
        /// <param name="iNum"></param>
        /// <returns></returns>
        public static byte[] BCDToByte(byte[] iNum)
        {
            int iLen = iNum.Length;
            for (int i = 0; i < iLen; i++)
            {
                iNum[i] = BCDToByte(iNum[i]);
            }
            return iNum;
        }

        /// <summary>
        /// BCD格式转字节
        /// </summary>
        /// <param name="iNum"></param>
        /// <returns></returns>
        public static byte BCDToByte(byte iNum)
        {
            int iValue = uByte(iNum);
            iValue = ((iValue / 16) * 10) + (iValue % 16);
            return (byte)iValue;
        }

        /// <summary>
        /// 字节转数值
        /// </summary>
        /// <param name="byte0"></param>
        /// <returns></returns>
        public static int uByte(byte byte0)
        {
            return byte0 & 0x000000ff;
        }

        /// <summary>
        /// DateTime转BCD格式日期时间
        /// </summary>
        /// <param name="btData">字节数组</param>
        /// <param name="date">DateTime</param>
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
                btData[1] = (byte)(date.Month);
                btData[2] = (byte)date.Day;
                btData[3] = (byte)date.Hour;
                btData = ByteToBCD(btData);
            }
        }

        /// <summary>
        /// 字节数组转BCD格式
        /// </summary>
        /// <param name="iNum">字节数组</param>
        /// <returns></returns>
        public static byte[] ByteToBCD(byte[] iNum)
        {
            int iLen = iNum.Length;
            for (int i = 0; i < iLen; i++)
            {
                iNum[i] = ByteToBCD(iNum[i]);
            }
            return iNum;
        }

        /// <summary>
        /// 字节转BCD格式
        /// </summary>
        /// <param name="iNum">字节</param>
        /// <returns></returns>
        public static byte ByteToBCD(byte iNum)
        {
            int iValue = uByte(iNum);
            iValue = (iValue / 10) * 16 + (iValue % 10);
            return (byte)iValue;
        }
    }
}
