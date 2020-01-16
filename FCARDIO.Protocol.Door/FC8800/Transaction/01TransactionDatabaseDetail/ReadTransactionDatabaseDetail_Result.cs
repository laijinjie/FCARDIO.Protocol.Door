using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Transaction
{
    /// <summary>
    /// 读取控制器中的卡片数据库信息
    /// </summary>
    public class ReadTransactionDatabaseDetail_Result : INCommandResult
    {
        /// <summary>
        /// 记录数据库的详情
        /// </summary>
        public FC8800.Data.TransactionDatabaseDetail DatabaseDetail;

        /// <summary>
        /// 初始化参数
        /// </summary>
        public ReadTransactionDatabaseDetail_Result() {
            DatabaseDetail = new Data.TransactionDatabaseDetail();
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            DatabaseDetail = null;
        }

        /// <summary>
        /// 进行解码
        /// </summary>
        /// <param name="buf"></param>
        public void SetBytes(IByteBuffer buf)
        {
            DatabaseDetail.SetBytes(buf);
        }
    }
}
