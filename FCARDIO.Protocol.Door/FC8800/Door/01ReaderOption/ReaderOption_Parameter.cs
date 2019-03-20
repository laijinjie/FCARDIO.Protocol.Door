using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.ReaderOption
{
    /// <summary>
    /// 控制器4个门的读卡器字节数
    /// </summary>
    public class ReaderOption_Parameter : AbstractParameter
    {
        private byte[] _Door = null;

        public ReaderOption_Parameter()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="door"></param>
        public ReaderOption_Parameter(byte[] door)
        {
            _Door = door;
            checkedParameter();
        }
        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (_Door == null)
                throw new ArgumentException("door Is Null!");
            if (_Door.Length != 4)
                throw new ArgumentException("door Length Error!");
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            _Door = null;
        }

        /// <summary>
        /// 对SN参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if(databuf.ReadableBytes != 4)
            {
                throw new ArgumentException("databuf Error!");
            }
           return databuf.WriteBytes(_Door);
        }

        public override int GetDataLen()
        {
            return 4;
        }

        /// <summary>
        /// 对SN参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (_Door == null)
            {
                _Door = new byte[4];
            }
            if (databuf.ReadableBytes != 4)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(_Door);
        }
    }
}
