using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace DoNetDrive.Protocol.Door.Door8800.SystemParameter.HTTPPageLandingSwitch
{
    /// <summary>
    /// 设置HTTP网页登陆开关_参数
    /// </summary>
    public class WriteHTTPPageLandingSwitch_Parameter : AbstractParameter
    {
        /// <summary>
        /// HTTP网页登陆开关（取值范围：0--关闭功能，1--开启功能 默认值：1）
        /// </summary>
        public byte Use;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public WriteHTTPPageLandingSwitch_Parameter() { }

        /// <summary>
        /// HTTP网页登陆开关参数初始化实例
        /// </summary>
        /// <param name="_Use">HTTP网页登陆开关参数</param>
        public WriteHTTPPageLandingSwitch_Parameter(byte _Use)
        {
            Use = _Use;
            if (!checkedParameter())
            {
                throw new ArgumentException("Use Error");
            }
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Use != 0 && Use != 1)
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
        /// 对HTTP网页登陆开关参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf.WriteByte(Use);
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x01;
        }

        /// <summary>
        /// 对HTTP网页登陆开关参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            Use = databuf.ReadByte();
        }
    }
}