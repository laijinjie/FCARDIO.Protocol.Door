using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.CacheContent
{
    /// <summary>
    /// 缓存区内容操作
    /// </summary>
    public class CacheContent_Parameter : AbstractParameter
    {
        /// <summary>
        /// 缓存区内容
        /// </summary>
        public string CacheContent;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public CacheContent_Parameter() { }

        /// <summary>
        /// 使用缓存区内容初始化实例
        /// </summary>
        /// <param name="_CacheContent">缓存区内容</param>
        public CacheContent_Parameter(string _CacheContent)
        {
            CacheContent = _CacheContent;
            if (!checkedParameter())
            {
                throw new ArgumentException("CacheContent Error");
            }
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (CacheContent == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x1E;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
        /// 对缓存区内容进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteString(Utility.StringUtility.FillString(CacheContent, Convert.ToInt32(GetDataLen()), "F", true), System.Text.Encoding.ASCII);
            return databuf;
        }

        /// <summary>
        /// 对缓存区内容进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            CacheContent = databuf.ReadString(GetDataLen(), System.Text.Encoding.ASCII).Replace("F","");
        }
    }
}
