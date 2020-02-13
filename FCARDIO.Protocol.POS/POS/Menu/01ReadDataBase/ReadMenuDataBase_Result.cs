using DotNetty.Buffers;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.POS.Menu
{
    public class ReadMenuDataBase_Result : INCommandResult
    {
        /// <summary>
        /// 菜单容量
        /// </summary>
        public ushort SortSize;

        /// <summary>
        /// 已存数量
        /// </summary>
        public ushort UseSize;


        /// <summary>
        /// 创建结构
        /// </summary>
        public ReadMenuDataBase_Result()
        {
        }

        public void SetBytes(IByteBuffer buf)
        {
            SortSize = buf.ReadUnsignedShort();
            UseSize = buf.ReadUnsignedShort();

        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            return;
        }
    }
}
