using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.Remote
{
    /// <summary>
    /// 远程开关门
    /// </summary>
    public class Remote_Parameter : AbstractParameter
    {
        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength = 0x04;
        /// <summary>
        /// 门字节数组
        /// </summary>
        public byte[] Door = null;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public Remote_Parameter() { }

        /// <summary>
        /// 远程开关门参数初始化实例
        /// </summary>
        /// <param name="_Door">门字节数组</param>
        public Remote_Parameter(byte[] _Door)
        {
            Door = _Door;
            checkedParameter();
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Door == null)
                throw new ArgumentException("door Is Null!");
            if (Door.Length != DataLength)
                throw new ArgumentException("door Length Error!");
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            Door = null;
        }

        /// <summary>
        /// 对远程开关门参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != DataLength)
            {
                throw new ArgumentException("databuf Error!");
            }
            return databuf.WriteBytes(Door);
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
        /// 对远程开关门参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (Door == null)
            {
                Door = new byte[DataLength];
            }
            if (databuf.ReadableBytes != DataLength)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(Door);
        }
    }
}

