using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter
{
    public class ByteUtil
    {
        public static byte ByteToBCD(byte iNum)
        {
            int iValue = uByte(iNum);
            iValue = (iValue / 10) * 16 + (iValue % 10);
            return (byte)iValue;
        }

        public static byte[] ByteToBCD(byte[] iNum)
        {
            int iLen = iNum.Length;
            for (int i = 0; i < iLen; i++)
            {
                iNum[i] = ByteToBCD(iNum[i]);
            }
            return iNum;
        }

        public static int uByte(byte byte0)
        {
            return byte0 & 0x000000ff;
        }

        // BCD 转 字节
      
        public static byte BCDToByte(byte iNum)
        {
            int iValue = uByte(iNum);
            iValue = ((iValue / 16) * 10) + (iValue % 16);
            return (byte)iValue;
        }

        //BCD 转 字节
        public static byte[] BCDToByte(byte [] iNum)
        {
            int iLen = iNum.Length;
            for (int i = 0; i < iLen; i++)
            {
                iNum[i] = BCDToByte(iNum[i]);
            }
            return iNum;
        }
      
    }
}