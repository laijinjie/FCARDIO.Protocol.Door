using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.TransactionDatabaseDetail
{
    /// <summary>
    /// 读取控制器中的卡片数据库信息
    /// </summary>
    public class ReadTransactionDatabaseDetail_Result : INCommandResult
    {
        /**
         * 记录数据库的详情
         */
        public FC8800.Data.TransactionDatabaseDetail DatabaseDetail { get; set; }


        public ReadTransactionDatabaseDetail_Result() {
            DatabaseDetail = new Data.TransactionDatabaseDetail();
        }

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="databaseDetail">数据库详情</param>
        //public ReadTransactionDatabaseDetail_Result(FC8800.Data.TransactionDatabaseDetail databaseDetail)
        //{
        //    DatabaseDetail = databaseDetail;
        //}

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            DatabaseDetail = null;
        }

        internal void SetBytes(IByteBuffer buf)
        {
            DatabaseDetail.SetBytes(buf);
        }
    }
}
