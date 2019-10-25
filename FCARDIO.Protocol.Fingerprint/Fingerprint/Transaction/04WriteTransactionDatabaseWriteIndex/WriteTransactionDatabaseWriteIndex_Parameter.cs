using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Protocol.Door.FC8800;

namespace FCARDIO.Protocol.Fingerprint.Transaction.WriteTransactionDatabaseWriteIndex
{
    /// <summary>
    /// 修改指定记录数据库的写索引
    /// 记录尾地址
    /// </summary>
    public class WriteTransactionDatabaseWriteIndex_Parameter : TransactionDatabaseReadIndex.WriteTransactionDatabaseReadIndex_Parameter
    {

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="_DatabaseType">记录数据库类型</param>
        /// <param name="_WriteIndex">记录尾地址</param>
        public WriteTransactionDatabaseWriteIndex_Parameter
(e_TransactionDatabaseType _DatabaseType, int _WriteIndex) : base(_DatabaseType, _WriteIndex)
        {
        }
    }
}
