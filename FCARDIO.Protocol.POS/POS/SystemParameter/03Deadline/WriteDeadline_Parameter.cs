using DotNetty.Buffers;
using System;
using FCARD.Common.Extensions;

namespace FCARDIO.Protocol.POS.SystemParameter.Deadline
{
    /// <summary>
    /// 设置设备有效期_参数
    /// </summary>
    public class WriteDeadline_Parameter : AbstractParameter
    {
        /// <summary>
        /// 设备有效期
        /// </summary>
        public DateTime Deadline;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public WriteDeadline_Parameter() { }

        /// <summary>
        /// 使用设备有效期初始化实例
        /// </summary>
        /// <param name="_Deadline">设备有效期</param>
        public WriteDeadline_Parameter(DateTime _Deadline)
        {
            Deadline = _Deadline;
            if (!checkedParameter())
            {
                throw new ArgumentException("Deadline Error");
            }
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            //DateTime dt = DateTime.Now;
            //if (DateTime.TryParse(Deadline,out dt))
            //{
            //    throw new ArgumentException("Deadline Error");
            //}

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
        /// 对有效期参数进行编码
        /// </summary>
        /// <param name="databuf">需要填充参数结构的字节缓冲区</param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes < GetDataLen())
            {
                throw new ArgumentException("databuf len error");
            }
            
            return databuf.WriteBytes(Deadline.ToBCDYYMMDD());
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x08;
        }

        /// <summary>
        /// 对有效期参数进行解码
        /// </summary>
        /// <param name="databuf">包含参数结构的缓冲区</param>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != GetDataLen())
            {
                throw new ArgumentException("databuf Error");
            }
            byte[] list = new byte[8];
            databuf.ReadBytes(list);
            //Deadline = list.to
        }
    }
}
