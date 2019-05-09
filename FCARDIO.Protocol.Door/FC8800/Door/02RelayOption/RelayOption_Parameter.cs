using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.RelayOption
{
    /// <summary>
    /// 控制器4个门的继电器参数
    /// </summary>
    public class RelayOption_Parameter : AbstractParameter
    {
        /// <summary>
        /// 数据长度
        /// </summary>
        private readonly int DataLength = 0x04;

        /// <summary>
        /// 门的继电器字节数组
        /// </summary>
        public byte[] Relay = null;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public RelayOption_Parameter() { }

        /// <summary>
        /// 控制器4个门的继电器参数初始化实例
        /// </summary>
        /// <param name="_Relay">门的继电器字节数组</param>
        public RelayOption_Parameter(byte[] _Relay)
        {
            Relay = _Relay;
            checkedParameter();
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Relay == null)
                throw new ArgumentException("relay Is Null!");
            if (Relay.Length != DataLength)
                throw new ArgumentException("relay Length Error!");
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            Relay = null;
        }

        /// <summary>
        /// 对控制器4个门的继电器参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != DataLength)
            {
                throw new ArgumentException("databuf Error!");
            }
            return databuf.WriteBytes(Relay);
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return DataLength;
        }

        /// <summary>
        /// 对控制器4个门的继电器参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (Relay == null)
            {
                Relay = new byte[DataLength];
            }
            if (databuf.ReadableBytes != DataLength)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(Relay);
        }
    }
}
