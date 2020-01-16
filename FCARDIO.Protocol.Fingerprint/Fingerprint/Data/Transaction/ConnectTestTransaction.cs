using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Fingerprint.Data.Transaction
{
    /// <summary>
    /// 连接测试消息
    /// </summary>
    public class ConnectTestTransaction:FCARDIO.Protocol.Door.FC8800.Data.Transaction.ConnectMessageTransaction
    {
        /// <summary>
        /// 创建一个连接测试消息
        /// </summary>
        public ConnectTestTransaction()
        {
            _TransactionType = 0xA0;//连接测试消息
        }
    }
}
