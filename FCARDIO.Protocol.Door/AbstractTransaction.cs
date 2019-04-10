using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door
{
    public abstract class AbstractTransaction
    {
        /// <summary>
        /// 事务序列号
        /// </summary>
        public int SerialNumber;
        /// <summary>
        /// 事务时间
        /// </summary>
        protected DateTime _TransactionDate; 
        /// <summary>
        /// 事务类型
        /// </summary>
        protected short _TransactionType; 
        /// <summary>
        /// 事务代码
        /// </summary>
        protected short _TransactionCode; 
        /// <summary>
        ///  记录是否为空记录
        /// </summary>
        protected bool _IsNull;

        /// <summary>
        /// 事务时间
        /// </summary>
        /// <returns>事务时间</returns>
        public DateTime TransactionDate()
        {
            return _TransactionDate;
        }

        /// <summary>
        /// 记录是否为空记录
        /// </summary>
        /// <returns>记录是否为空记录</returns>
        public bool IsNull()
        {
            return _IsNull;
        }

        /// <summary>
        /// 事务类型
        /// </summary>
        /// <returns>事务类型</returns>
        public short TransactionType()
        {
            return _TransactionType;
        }

        /// <summary>
        /// 事务代码
        /// </summary>
        /// <returns>事务代码</returns>
        public short TransactionCode()
        {
            return _TransactionCode;
        }
    }
}
