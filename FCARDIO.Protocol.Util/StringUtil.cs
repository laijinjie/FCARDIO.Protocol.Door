using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Util
{
    /// <summary>
    /// 字符串实用工具
    /// </summary>
    public class StringUtil
    {
        /// <summary>
        /// 十六进制转字节的转换表
        /// </summary>
        private static byte[] HexToByte_Digit;
        /// <summary>
        /// 字符串转数组的值表
        /// </summary>
        private static byte[] NumDigit;

        /// <summary>
        /// 初始化 HexToByte 的转换表
        /// </summary>
        static StringUtil()
        {
            int i;
            byte[] digits = new byte[256];
            byte[] tmp;
            tmp = System.Text.Encoding.ASCII.GetBytes("0123456789abcdef");
            for (i = 0; i < tmp.Length; i++)
            {
                digits[tmp[i]] = (byte)i;
            }
            tmp = Encoding.ASCII.GetBytes("ABCDEF");
            for (i = 0; i < tmp.Length; i++)
            {
                digits[tmp[i]] = (byte)(i + 10);
            }
            HexToByte_Digit = digits;

            digits = new byte[256];
            tmp = Encoding.ASCII.GetBytes("0123456789"); 
            for (i = 0; i < tmp.Length; i++)
            {
                digits[tmp[i]] = (byte)i;
            }

            NumDigit = digits;
        }


        /**
         * 十六进制字符串转字节数组
         *
         * @param hexString 需要转换的十六进制字符串
         * @return 转换后的字节数组 如果字符串为null则返回null
         */
        public static byte[] HexToByte(String hexString)
        {
            int i, iIndex = 0, iData;
            if (string.IsNullOrEmpty(hexString))
            {
                return null;
            }
            if (!IsHex(hexString))
            {
                return null;
            }

            //确定字符串长度必须是2的倍数
            if ((hexString.Length % 2) == 1)
            {
                hexString = "0" + hexString;
            }
            //生成转换值列表
            byte[] digits = HexToByte_Digit;

            //生成缓存
            byte[] buf = new byte[hexString.Length / 2];
            byte[] sbuf = System.Text.Encoding.Default.GetBytes(hexString);

            int ilen = sbuf.Length;
            //开始转换
            for (i = 0; i < ilen; i++)
            {
                iData = digits[sbuf[i++] & 0x000000ff] * 16;
                iData = iData + digits[sbuf[i]];

                buf[iIndex] = (byte)iData;
                iIndex++;
            }

            return buf;
        }

        /// <summary>
        ///  将十六进制字符串添加到ByteBuf中
        /// </summary>
        /// <param name="hexString">需要转换的十六进制字符串</param>
        /// <param name="buf">保存这些数据的缓冲区</param>
        public static void HextoByteBuf(String hexString, IByteBuffer buf)
        {
            int i, iIndex = 0, iData;
            if (string.IsNullOrEmpty(hexString))
            {
                return;
            }

            //确定字符串长度必须是2的倍数
            if ((hexString.Length % 2) == 1)
            {
                hexString = "0" + hexString;
            }
            //生成转换值列表
            byte[] digits = HexToByte_Digit;

            //生成缓存
            byte[] sbuf = System.Text.Encoding.Default.GetBytes(hexString);
            int ilen = sbuf.Length;
            //开始转换
            for (i = 0; i < ilen; i++)
            {
                //判断是否为十六进制字符串
                if (digits[sbuf[i]] == 0 && sbuf[i] != 0x30)
                {
                    return ;
                }

                iData = digits[sbuf[i++]] * 16;
                iData = iData + digits[sbuf[i]];

                buf.WriteByte(iData);
                iIndex++;
            }
        }

        /// <summary>
        /// 检查是否为十六进制字符串
        /// </summary>
        /// <param name="hexString">需要检查的字符串</param>
        /// <returns>表示是十六进制，false 表示包含非十六进制字符</returns>
        public static bool IsHex(String hexString)
        {
            if (string.IsNullOrEmpty(hexString))
            {
                return false;
            }
            byte[] sbuf = System.Text.Encoding.Default.GetBytes(hexString);
            byte[] digits = HexToByte_Digit;
            int i, ilen = sbuf.Length;
            //开始转换
            for (i = 0; i < ilen; i++)
            {
                if (digits[sbuf[i]] == 0 && sbuf[i] != 0x30)
                {
                    return false;
                }
            }
            return true;
        }

        /**
         * 检查是否有纯数字组成
         *
         * @param numString 需要检查的字符串
         * @return true 表示是纯数字字符串；false 包含非法字符
         */
        public static bool IsNum(String numString)
        {
            if (IsNullOrEmpty(numString))
            {
                return false;
            }
            byte[] sbuf = System.Text.Encoding.Default.GetBytes(numString);
            byte[] digits = NumDigit;
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

        /**
         * 检查字符串是否为空字符串或者为null
         *
         * @param str 需要检查的字符串
         * @return true表示为空或null
         */
        public static bool IsNullOrEmpty(String str)
        {
            if (str == null)
            {
                return true;
            }
            if (str.Length == 0)
            {
                return true;
            }
            return false;
        }

        /**
         * 检查字符串是否为Ascii字符
         *
         * @param asciiString 待检查的字符串
         * @return true 表示都是ascii组成，false 表示有包含不是ascii的字符串
         */
        public static bool IsAscii(String asciiString)
        {
            if (IsNullOrEmpty(asciiString))
            {
                return false;
            }
            byte[] buf = System.Text.Encoding.Default.GetBytes(asciiString);
            int ilen = buf.Length;
            for (int i = 0; i < ilen; i++)
            {
                if (buf[i] < 32 || buf[i] > 125)
                {
                    return false;
                }
            }
            return true;
        }

        /**
         * 填充字符串，并返回一个指定长度的字符串，原始字符串长度大于指定长度时将被阶段，小于指定长度时，使用 fillstr 参数填充内容
         * 填充的字符串在右边
         *
         * @param str
         * @param iLen
         * @param fillstr
         * @return
         */
        public static String FillString(String str, int iLen, String fillstr)
        {
            return FillString(str, iLen, fillstr, true);
        }

        /**
         * 填充字符串，并返回一个指定长度的字符串，原始字符串长度大于指定长度时将被阶段，小于指定长度时，使用 fillstr 参数填充内容
         *
         * @param str
         * @param iLen
         * @param fillstr
         * @param fill_right 填充值在右边
         * @return
         */
        public static String FillString(String str, int iLen, String fillstr, bool fill_right)
        {
            int iStrLen = 0;

            if (!IsNullOrEmpty(str))
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
                sbuf.Length.Equals(iAddCount);
            }
            if (!IsNullOrEmpty(str))
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

        /**
         * 检查并填充十六进制字符串
         *
         * @param str 需要检查的字符串
         * @param iLen 需要返回的字符串长度
         * @param fillstr 占位字符
         * @param fill_right 填充在结尾还是开头？ True 表示填充在结尾。
         * @return
         */
        public static String FillHexString(String str, int iLen, String fillstr, bool fill_right)
        {
            StringBuilder sbuf = new StringBuilder(iLen);

            if (StringUtil.IsNullOrEmpty(str))
            {
                for (int i = 0; i < iLen; i++)
                {
                    sbuf.Append(fillstr);
                }
                sbuf.Length.Equals(iLen);
                return sbuf.ToString();
            }
            if (!StringUtil.IsHex(str))
            {
                for (int i = 0; i < iLen; i++)
                {
                    sbuf.Append(fillstr);
                }
                sbuf.Length.Equals(iLen);
                return sbuf.ToString();
            }

            return FillString(str, iLen, fillstr, fill_right);

        }


    }
}
