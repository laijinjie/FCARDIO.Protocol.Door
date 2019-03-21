using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.TCPSetting
{
    /// <summary>
    /// 设置TCP参数_参数
    /// </summary>
    public class WriteTCPSetting_Parameter : AbstractParameter
    {
        public TCPDetail TCP;

        public WriteTCPSetting_Parameter(TCPDetail _TCP)
        {
            TCP = _TCP;

            if (!checkedParameter())
            {
                throw new ArgumentException("TCP Error");
            }
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (TCP == null)
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
            if (TCP != null)
            {
                TCP = null;
            }

            return;
        }

        /// <summary>
        /// 编码参数
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return TCP.GetBytes(databuf);
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return TCP.GetDataLen();
        }

        /// <summary>
        /// 解码参数
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            TCP.SetBytes(databuf);
        }
    }
}