using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.ReaderWorkSetting
{
    /// <summary>
    /// 门读卡认证方式
    /// </summary>
    public class WriteReaderWorkSetting_Parameter : AbstractParameter
    {
        /// <summary>
        /// 数据长度
        /// </summary>
        private const int DataLength = 119;
        /// <summary>
        /// 门读卡认证方式
        /// </summary>
        private byte[] ReaderWorkSetting = null;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public WriteReaderWorkSetting_Parameter() { }

        /// <summary>
        /// 门读卡认证方式参数初始化实例
        /// </summary>
        /// <param name="readerWorkSetting">门读卡认证方式</param>
        public WriteReaderWorkSetting_Parameter(byte[] readerWorkSetting)
        {
            ReaderWorkSetting = readerWorkSetting;
            checkedParameter();
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (ReaderWorkSetting == null)
                throw new ArgumentException("readerWorkSetting Is Null!");
            if (ReaderWorkSetting.Length != DataLength)
                throw new ArgumentException("readerWorkSetting Length Error!");
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            ReaderWorkSetting = null;
        }

        /// <summary>
        /// 对门认证方式参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != DataLength)
            {
                throw new ArgumentException("databuf Error!");
            }
            return databuf.WriteBytes(ReaderWorkSetting);
        }

        /// <summary>
        /// 获得数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return DataLength;
        }

        /// <summary>
        /// 对门认证方式参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (ReaderWorkSetting == null)
            {
                ReaderWorkSetting = new byte[DataLength];
            }
            if (databuf.ReadableBytes != DataLength)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(ReaderWorkSetting);
        }
    }
}
