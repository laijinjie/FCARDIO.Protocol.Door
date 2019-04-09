using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardDatabaseDetail
{
    /// <summary>
    /// 控制器中的卡片数据库信息
    /// </summary>
    public class ReadCardDatabaseDetail_Result : INCommandResult
    {
        /// <summary>
        /// 排序数据区容量上限
        /// </summary>
        public long SortDataBaseSize;

        /// <summary>
        /// 排序数据区已使用数量
        /// </summary>
        public long SortCardSize;


        /// <summary>
        /// 顺序存储区容量上限
        /// </summary>
        public long SequenceDataBaseSize;

        /// <summary>
        /// 顺序存储区已使用数量
        /// </summary>
        public long SequenceCardSize;

        public ReadCardDatabaseDetail_Result()
        {
        }

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="sortDataBaseSize">排序数据区容量上限</param>
        /// <param name="sortCardSize">排序数据区已使用数量</param>
        /// <param name="sequenceDataBaseSize">顺序存储区容量上限</param>
        /// <param name="sequenceCardSize">顺序存储区已使用数量</param>
        public ReadCardDatabaseDetail_Result(long sortDataBaseSize, long sortCardSize, long sequenceDataBaseSize,long sequenceCardSize)
        {
            SortDataBaseSize = sortDataBaseSize;
            SortCardSize = sortCardSize;
            SequenceDataBaseSize = sequenceDataBaseSize;
            SequenceCardSize = sequenceCardSize;
        }


        internal void SetBytes(IByteBuffer buf)
        {
            return;
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
