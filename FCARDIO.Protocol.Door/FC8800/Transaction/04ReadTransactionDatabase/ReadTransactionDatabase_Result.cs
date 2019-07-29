using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Data;
using FCARDIO.Protocol.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.ReadTransactionDatabase
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadTransactionDatabase_Result : INCommandResult
    {
        /// <summary>
        /// 记录数据库类型
        /// 读卡记录
        /// 出门开关记录
        /// 门磁记录
        /// 软件操作记录
        /// 报警记录
        /// 系统记录
        /// </summary>
        public e_TransactionDatabaseType DatabaseType;

        /// <summary>
        /// 读取数量
        /// </summary>
        public int Quantity;

        /// <summary>
        /// 剩余新记录数量
        /// </summary>
        public int readable;

        /// <summary>
        /// 记录列表
        /// </summary>
        public List<AbstractTransaction> TransactionList;

        /// <summary>
        /// 初始化参数
        /// </summary>
        public ReadTransactionDatabase_Result() { }

      
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            TransactionList?.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf"></param>
        internal void SetBytes(IByteBuffer buf)
        {
            
        }
    }
}
