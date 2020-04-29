using DoNetDrive.Protocol.Door.Door8800.Data.TimeGroup;
using DotNetty.Buffers;
using System;

namespace DoNetDrive.Protocol.POS.TimeGroup
{
    public class AddTimeGroup_Parameter : AbstractParameter
    {
        /// <summary>
        /// 
        /// </summary>
        public byte Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public WeekTimeGroup WeekTimeGroup { get; set; }

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public AddTimeGroup_Parameter() { }

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="Index">消费时段号</param>
        public AddTimeGroup_Parameter(WeekTimeGroup WeekTimeGroup)
        {
            this.WeekTimeGroup = WeekTimeGroup;
            if (!checkedParameter())
            {
                throw new ArgumentException("Parameter Error");
            }
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (WeekTimeGroup == null)
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
            return;
        }

        /// <summary>
        /// 对有效期参数进行编码
        /// </summary>
        /// <param name="databuf">需要填充参数结构的字节缓冲区</param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != GetDataLen())
            {
                throw new ArgumentException("databuf len error");
            }
            WeekTimeGroup.SetBytes(databuf);
            return databuf;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x71;
        }

        /// <summary>
        /// 对有效期参数进行解码
        /// </summary>
        /// <param name="databuf">包含参数结构的缓冲区</param>
        public override void SetBytes(IByteBuffer databuf)
        {
            return;
        }
    }
}
