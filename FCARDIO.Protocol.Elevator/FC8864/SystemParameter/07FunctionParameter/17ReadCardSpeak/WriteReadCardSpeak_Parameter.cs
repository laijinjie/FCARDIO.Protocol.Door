using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Util;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置定时读卡播报语音消息参数_参数
    /// </summary>
    public class WriteReadCardSpeak_Parameter : AbstractParameter
    {
        /// <summary>
        /// 定时读卡播报语音消息参数
        /// </summary>
        public ReadCardSpeak SpeakSetting;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public WriteReadCardSpeak_Parameter() { }

        /// <summary>
        /// 使用定时读卡播报语音消息参数初始化实例
        /// </summary>
        /// <param name="_SpeakSetting">定时读卡播报语音消息参数</param>
        public WriteReadCardSpeak_Parameter(ReadCardSpeak _SpeakSetting)
        {
            SpeakSetting = _SpeakSetting;
            if (!checkedParameter())
            {
                throw new ArgumentException("SpeakSetting Error");
            }
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (SpeakSetting == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            SpeakSetting = null;
            return;
        }

        /// <summary>
        /// 对定时读卡播报语音消息参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteBoolean(SpeakSetting.Use);
            databuf.WriteByte(SpeakSetting.MsgIndex);
            byte[] btData = new byte[4];

            Utility.StringUtility.DateToBCD_yyMMddhh(btData, SpeakSetting.BeginDate);
            databuf.WriteBytes(btData);

            Utility.StringUtility.DateToBCD_yyMMddhh(btData, SpeakSetting.EndDate);
            databuf.WriteBytes(btData);
            return databuf;
        }

        ///// <summary>
        ///// DateTime转BCD格式日期时间
        ///// </summary>
        ///// <param name="btData">字节数组</param>
        ///// <param name="date">DateTime</param>
        //public static void DateToBCD_yyMMddhh(byte[] btData, DateTime date)
        //{
        //    if (date == null)
        //    {
        //        for (int i = 0; i < 4; i++)
        //        {
        //            btData[i] = 0;
        //        }
        //    }
        //    else
        //    {
        //        btData[0] = (byte)(date.Year - 2000);
        //        btData[1] = (byte)(date.Month);
        //        btData[2] = (byte)date.Day;
        //        btData[3] = (byte)date.Hour;
        //        btData = ByteToBCD(btData);
        //    }
        //}

        ///// <summary>
        ///// 字节转BCD格式
        ///// </summary>
        ///// <param name="iNum">字节</param>
        ///// <returns></returns>
        //public static byte ByteToBCD(byte iNum)
        //{
        //    int iValue = uByte(iNum);
        //    iValue = (iValue / 10) * 16 + (iValue % 10);
        //    return (byte)iValue;
        //}

        ///// <summary>
        ///// 字节转数值
        ///// </summary>
        ///// <param name="byte0"></param>
        ///// <returns></returns>
        //public static int uByte(byte byte0)
        //{
        //    return byte0 & 0x000000ff;
        //}

        ///// <summary>
        ///// 字节数组转BCD格式
        ///// </summary>
        ///// <param name="iNum">字节数组</param>
        ///// <returns></returns>
        //public static byte[] ByteToBCD(byte[] iNum)
        //{
        //    int iLen = iNum.Length;
        //    for (int i = 0; i < iLen; i++)
        //    {
        //        iNum[i] = ByteToBCD(iNum[i]);
        //    }
        //    return iNum;
        //}

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x0A;
        }

        /// <summary>
        /// 对定时读卡播报语音消息参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (SpeakSetting == null)
            {
                SpeakSetting = new ReadCardSpeak();
            }
            SpeakSetting.Use = databuf.ReadBoolean();
            SpeakSetting.MsgIndex = databuf.ReadByte();

            byte[] btData = new byte[4];
            databuf.ReadBytes(btData, 0, 4);
            SpeakSetting.BeginDate = Utility.StringUtility.BCDTimeToDate_yyMMddhh(btData);

            databuf.ReadBytes(btData, 0, 4);
            SpeakSetting.EndDate = Utility.StringUtility.BCDTimeToDate_yyMMddhh(btData);
            return;
        }

        ///// <summary>
        ///// BCD格式日期时间转DateTime
        ///// </summary>
        ///// <param name="btTime">字节数组</param>
        ///// <returns></returns>
        //public static DateTime BCDTimeToDate_yyMMddhh(byte[] btTime)
        //{
        //    btTime = BCDToByte(btTime);
        //    int year = uByte(btTime[0]);
        //    int month = uByte(btTime[1]);
        //    int dayOfMonth = uByte(btTime[2]);
        //    int hourOfDay = uByte(btTime[3]);

        //    if (year > 99)
        //    {
        //        return new DateTime();
        //    }
        //    if (month == 0 || month > 12)
        //    {
        //        return new DateTime();
        //    }
        //    if (dayOfMonth == 0 || dayOfMonth > 31)
        //    {
        //        return new DateTime();
        //    }
        //    if (hourOfDay > 23)
        //    {
        //        return new DateTime();
        //    }

        //    DateTime dTime = new DateTime(2000 + year, month, dayOfMonth, hourOfDay, 0, 0);
        //    return dTime;
        //}

        ///// <summary>
        ///// BCD格式转字节
        ///// </summary>
        ///// <param name="iNum"></param>
        ///// <returns></returns>
        //public static byte BCDToByte(byte iNum)
        //{
        //    int iValue = uByte(iNum);
        //    iValue = ((iValue / 16) * 10) + (iValue % 16);
        //    return (byte)iValue;
        //}

        ///// <summary>
        ///// BCD格式转字节数组
        ///// </summary>
        ///// <param name="iNum"></param>
        ///// <returns></returns>
        //public static byte[] BCDToByte(byte[] iNum)
        //{
        //    int iLen = iNum.Length;
        //    for (int i = 0; i < iLen; i++)
        //    {
        //        iNum[i] = BCDToByte(iNum[i]);
        //    }
        //    return iNum;
        //}
    }
}