using DotNetty.Buffers;
using FCARDIO.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置智能防盗主机参数_参数
    /// </summary>
    public class WriteTheftAlarmSetting_Parameter : AbstractParameter
    {
        public TheftAlarmSetting Setting;

        public WriteTheftAlarmSetting_Parameter(TheftAlarmSetting _Setting)
        {
            Setting = _Setting;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Setting == null)
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
            Setting = null;

            return;
        }

        /// <summary>
        /// 编码参数
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteBoolean(Setting.Use);
            databuf.WriteByte(Setting.InTime);
            databuf.WriteByte(Setting.OutTime);
            Setting.BeginPassword = FillHexString(Setting.BeginPassword, 8, "F", false);
            Setting.ClosePassword = FillHexString(Setting.ClosePassword, 8, "F", false);
            int pwd = Convert.ToInt32(Setting.BeginPassword, 16);
            databuf.WriteInt(pwd);
            pwd = Convert.ToInt32(Setting.ClosePassword, 16);
            databuf.WriteInt((int)pwd);
            databuf.WriteUnsignedShort(Setting.AlarmTime);
            return databuf;
        }

        public static string FillHexString(string str, int iLen, string fillstr, bool fill_right)
        {
            StringBuilder sbuf = new StringBuilder(iLen);

            if (string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < iLen; i++)
                {
                    sbuf.Append(fillstr);
                }
                sbuf.Length = iLen;
                return sbuf.ToString();
            }
            if (!IsHex(str))
            {
                for (int i = 0; i < iLen; i++)
                {
                    sbuf.Append(fillstr);
                }
                sbuf.Length = iLen;
                return sbuf.ToString();
            }

            return FillString(str, iLen, fillstr, fill_right);
        }

        public static bool IsHex(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
            {
                return false;
            }
            byte[] sbuf = hexString.GetBytes();
            byte[] digits = GetHexToByte_Digit();
            int i, ilen = sbuf.Length;
            //开始转换
            for (i = 0; i < ilen; i++)
            {
                if (digits[sbuf[i] & 0x000000ff] == 0 && sbuf[i] != 0x30)
                {
                    return false;
                }
            }
            return true;
        }

        public static byte[] GetHexToByte_Digit()
        {
            int i;
            byte[] digits = new byte[256];
            byte[] tmp;
            tmp = "0123456789abcdef".GetBytes();
            for (i = 0; i < tmp.Length; i++)
            {
                digits[tmp[i]] = (byte)i;
            }
            tmp = "ABCDEF".GetBytes();
            for (i = 0; i < tmp.Length; i++)
            {
                digits[tmp[i]] = (byte)(i + 10);
            }

            return digits;
        }

        public static string FillString(string str, int iLen, string fillstr)
        {
            return FillString(str, iLen, fillstr, true);
        }

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
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x0D;
        }

        /// <summary>
        /// 解码参数
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            Setting.Use = databuf.ReadBoolean();
            Setting.InTime = databuf.ReadInt();
            Setting.OutTime = databuf.ReadInt();
            byte[] btPwd = new byte[4];
            databuf.ReadBytes(btPwd, 0, 4);
            Setting.BeginPassword = ByteToHex(btPwd);

            databuf.ReadBytes(btPwd, 0, 4);
            Setting.ClosePassword = ByteToHex(btPwd);

            Setting.AlarmTime = databuf.ReadUnsignedShort();
            return;
        }

        public static string ByteToHex(byte[] b)
        {
            if (b == null)
            {
                return null;
            }
            if (b.Length == 0)
            {
                return null;
            }

            int ilen = b.Length;
            char[] sHexbuf = new char[ilen * 2];
            int lIndex = 0;
            int iData;
            char[] digits = ByteToHex_Digit;
            try
            {
                for (int i = 0; i < ilen; i++)
                {
                    iData = uByte(b[i]);//取字节的无符号整形数值

                    sHexbuf[lIndex++] = digits[iData / 16];
                    sHexbuf[lIndex++] = digits[iData % 16];
                }
                return sHexbuf.ToString().ToUpper();
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static char[] ByteToHex_Digit = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        public static int uByte(byte byte0)
        {
            return byte0 & 0x000000ff;
        }
    }
}