using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ReaderInterval
{
    /// <summary>
    /// 重复读卡间隔_参数
    /// </summary>
    public class WriteReaderInterval_Parameter : AbstractParameter
    {
        /// <summary>
        /// 门
        /// </summary>
        public byte Door;

        /// <summary>
        /// 是否有效
        /// </summary>
        public byte Use;

        /// <summary>
        /// 检测模式
        /// </summary>
        public byte DetectionMode;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public WriteReaderInterval_Parameter() { }

        /// <summary>
        /// 重复读卡间隔参数初始化实例
        /// </summary>
        /// <param name="door">门端口</param>
        /// <param name="use">是否有效</param>
        /// <param name="detectionMode">检测模式</param>
        public WriteReaderInterval_Parameter(byte door, byte use, byte detectionMode)
        {
            Door = door;
            Use = use;
            DetectionMode = detectionMode;
            checkedParameter();
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Use < 0 || Use > 1 || DetectionMode < 1 || DetectionMode > 3)
                throw new ArgumentException("Use or DetectionMode Error!");
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
        /// 对重复读卡间隔参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != GetDataLen())
            {
                throw new ArgumentException("databuf Error!");
            }
            databuf.WriteByte(Door);
            databuf.WriteByte(Use);
            databuf.WriteByte(DetectionMode);
            return databuf;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x03;
        }

        /// <summary>
        /// 对重复读卡间隔参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != GetDataLen())
            {
                throw new ArgumentException("databuf Error");
            }
            Door = databuf.ReadByte();
            Use = databuf.ReadByte();
            DetectionMode = databuf.ReadByte();
        }
    }
}
