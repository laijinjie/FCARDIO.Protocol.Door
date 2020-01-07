using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.POS.SystemParameter.SN
{
    /// <summary>
    /// 读取SN 返回结果
    /// </summary>
    public class SN_Result : Result_Base
    {
        /// <summary>
        /// SN的字节数组
        /// </summary>
        public byte[] SNBuf;

        /// <summary>
        /// 对SN参数进行解码
        /// </summary>
        /// <param name="buf"></param>
        internal void SetBytes(IByteBuffer databuf)
        {
            if (SNBuf == null)
            {
                SNBuf = new byte[10];
            }

            databuf.ReadBytes(SNBuf);
        }
    }




}
