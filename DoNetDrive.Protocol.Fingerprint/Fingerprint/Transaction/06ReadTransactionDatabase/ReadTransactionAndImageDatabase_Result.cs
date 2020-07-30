using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Fingerprint.Data.Transaction;
using DoNetDrive.Protocol.Transaction;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint.Transaction
{
    /// <summary>
    /// 读取认证记录和图片的返回值
    /// </summary>
    public class ReadTransactionAndImageDatabase_Result : INCommandResult
    {
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
        public List<CardAndImageTransaction> TransactionList;

        /// <summary>
        /// 初始化参数
        /// </summary>
        public ReadTransactionAndImageDatabase_Result() { }


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
