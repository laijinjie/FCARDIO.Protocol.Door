using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.POS.CardType.DatabaseDetail
{
    /// <summary>
    /// 控制器中的订餐数据库信息
    /// </summary>
    public class ReadDatabaseDetail_Result : INCommandResult
    {
        /// <summary>
        /// 最大容量
        /// </summary>
        public ushort SortDataBaseSize;

        /// <summary>
        /// 最大容量
        /// </summary>
        public ushort SortSize;


        /// <summary>
        /// 创建结构
        /// </summary>
        public ReadDatabaseDetail_Result()
        {
        }

        public void SetBytes(IByteBuffer buf)
        {
            SortDataBaseSize = buf.ReadUnsignedShort();
            SortSize = buf.ReadUnsignedShort();

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
