using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.EquptOnNetNum
{
    /// <summary>
    /// 设置网络标记_参数
    /// </summary>
    public class SearchEquptOnNetNum_Parameter : AbstractParameter
    {
        /// <summary>
        /// 网络标记 取值范围：1-65535
        /// </summary>
        public ushort NetNum;

        public SearchEquptOnNetNum_Parameter(ushort _NetNum)
        {
            NetNum = _NetNum;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (NetNum < 1 || NetNum > 65535)
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
            return databuf.WriteUnsignedShort(NetNum);
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
            NetNum = databuf.ReadUnsignedShort();
        }
    }
}