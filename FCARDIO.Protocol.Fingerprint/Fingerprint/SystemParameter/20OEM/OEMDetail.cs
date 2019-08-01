using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Data;

namespace FCARDIO.Protocol.Fingerprint.SystemParameter.OEM
{
    public class OEMDetail : INData
    {
        public static Encoding StringEncoding = Encoding.BigEndianUnicode;

        /// <summary>
        /// 制造商名称
        /// </summary>
        public string Manufacturer;
        /// <summary>
        /// 网址
        /// </summary>
        public string WebAddr;

        /// <summary>
        /// 出厂日期
        /// </summary>
        public DateTime DeliveryDate;

        /// <summary>
        /// 获取OEM数据结构的字节长度
        /// </summary>
        /// <returns></returns>
        public int GetDataLen()
        {
            return 127;
        }

        /// <summary>
        /// 获取一个 ByteBuf 此 缓冲中包含了此数据结构的所有数据
        /// </summary>
        /// <returns></returns>
        public IByteBuffer GetBytes()
        {
            return GetBytes(DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(127));
        }

        /// <summary>
        ///  将数据序列化到指定的 ByteBuf 中
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes < 127)
            {
                throw new ArgumentException("databuf Size < 127");
            }
            WriteString(databuf, Manufacturer, 60, StringEncoding);
            WriteString(databuf, WebAddr, 60, StringEncoding);
            FCARDIO.Protocol.Util.StringUtil.HextoByteBuf(DeliveryDate.ToString("yyyyMMddhhmmss"), databuf);
            return databuf;
        }

        /// <summary>
        /// 将字符串写入到固定长度的缓冲区，不足补0，超过就截取
        /// </summary>
        /// <param name="databuf">缓冲区</param>
        /// <param name="sValue">需要写入的字符串</param>
        /// <param name="iLen">缓冲区固定长度</param>
        /// <returns></returns>
        private IByteBuffer WriteString(IByteBuffer databuf, string sValue, int iLen, Encoding uc)
        {
            int iCount = 0, iNullCount = iLen;
            if (!string.IsNullOrEmpty(sValue))
            {
                iCount = uc.GetByteCount(sValue);
                if (iCount <= iLen)
                {
                    databuf.WriteString(sValue, uc);
                }
                else
                {
                    iCount = iLen;
                    var buf = uc.GetBytes(sValue);
                    databuf.WriteBytes(buf, 0, iCount);
                }
                iNullCount = iNullCount - iCount;
            }
            if (iNullCount > 0)
            {
                for (int i = 0; i < iNullCount; i++)
                {
                    databuf.WriteByte(0);
                }
            }
            return databuf;
        }


        /// <summary>
        /// 从缓冲区中读取一个定长的字符串
        /// </summary>
        /// <param name="databuf"></param>
        /// <param name="sValue"></param>
        /// <param name="uc"></param>
        /// <returns></returns>
        private string GetString(IByteBuffer databuf, int iLen, Encoding uc)
        {
            string sValue = string.Empty;

            sValue = databuf.ReadString(iLen, uc);
            if (sValue.EndsWith("\0"))
            {
                sValue = sValue.TrimEnd('\0');
            }
            return sValue;
        }


        /// <summary>
        /// 将buf中数据转换为实体
        /// </summary>
        /// <param name="databuf"></param>
        public void SetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes < 127)
            {
                throw new ArgumentException("databuf Size < 127");
            }
            Manufacturer = GetString(databuf, 60, StringEncoding);
            WebAddr = GetString(databuf, 60, StringEncoding);
            DeliveryDate = Util.TimeUtil.BCDTimeToDate_yyyyMMddhhmmss(databuf);
        }


    }
}
