using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.KeepAliveInterval
{
    /// <summary>
    /// 设置控制器作为客户端时，和服务器的保活间隔时间_参数
    /// </summary>
    public class WriteKeepAliveInterval_Parameter : AbstractParameter
    {
        /// <summary>
        /// 保活间隔时间（取值范围：0-3600,0--关闭功能）
        /// </summary>
        public ushort IntervalTime;

        public WriteKeepAliveInterval_Parameter(ushort _IntervalTime)
        {
            IntervalTime = _IntervalTime;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (IntervalTime < 0 || IntervalTime > 3600)
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
        /// 编码参数
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf.WriteUnsignedShort(IntervalTime);
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x02;
        }

        /// <summary>
        /// 解码参数
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            IntervalTime = databuf.ReadUnsignedShort();
        }
    }
}