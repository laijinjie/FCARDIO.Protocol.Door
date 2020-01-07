using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.POS.Reservation.DatabaseDetail
{
    /// <summary>
    /// 控制器中的卡片数据库信息
    /// </summary>
    public class ReadDatabaseDetail_Result : INCommandResult
    {
        /// <summary>
        /// 最大容量
        /// </summary>
        public long SortDataBaseSize;

        /// <summary>
        /// 最大容量
        /// </summary>
        public long SortSize;


        /// <summary>
        /// 创建结构
        /// </summary>
        public ReadDatabaseDetail_Result()
        {
        }

        public void SetBytes(IByteBuffer buf)
        {
            SortDataBaseSize = buf.ReadByte();
            SortSize = buf.ReadByte();

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
