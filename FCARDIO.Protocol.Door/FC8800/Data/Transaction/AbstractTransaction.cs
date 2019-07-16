using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Data
{
    /// <summary>
    /// 所有事务、记录的抽象类
    /// </summary>
    public abstract class AbstractTransaction
    {
        /// <summary>
        /// 事务序列号
        /// </summary>
        public int SerialNumber;

        /// <summary>
        /// 事务时间
        /// </summary>
        public DateTime TransactionDate;

        /// <summary>
        /// 事务类型
        /// </summary>
        public short TransactionType;

        /// <summary>
        /// 事务代码
        /// </summary>
        public short TransactionCode;

        /// <summary>
        /// 记录是否为空记录
        /// </summary>
        protected bool IsNull;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public abstract void SetBytes(IByteBuffer data);
    }
}
