using DotNetty.Buffers;
using System;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.SN
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
                SNBuf = new byte[16];
            }
            if (databuf.ReadableBytes != 10)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(SNBuf);
        }
    }
}
