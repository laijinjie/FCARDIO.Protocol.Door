using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Core.Data;
using FCARDIO.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.SN
{
    /// <summary>
    /// 写入控制器SN_参数
    /// </summary>
    public class SN_Parameter : AbstractParameter

    {
        /// <summary>
        /// SN的字节数组
        /// </summary>
        public byte[] SNBuf;

        public SN_Parameter() { }

        public SN_Parameter(byte[] _SN)
        {
            SNBuf = _SN;
            if (!checkedParameter())
            {
                throw new ArgumentException("SN Error");
            }
        }

        public SN_Parameter(string _SN)
            : this(_SN.GetBytes())
        {
        }

        public override bool checkedParameter()
        {
            if (SNBuf == null)
            {
                return false;
            }
            if (SNBuf.Length != 16)
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
            SNBuf = null;
            return;
        }

        /// <summary>
        /// 对SN参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteBytes(SNBuf);
            return databuf;
        }

        public override int GetDataLen()
        {
            return 16;
        }

        /// <summary>
        /// 对SN参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (SNBuf == null)
            {
                SNBuf = new byte[15];
            }
            if (databuf.ReadableBytes != 16)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(SNBuf);
        }
    }
}