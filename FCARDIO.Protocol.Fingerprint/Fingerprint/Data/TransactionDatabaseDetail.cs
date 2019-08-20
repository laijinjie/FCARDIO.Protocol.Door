using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Fingerprint.Data
{
    /// <summary>
    /// 记录数据库的详情
    /// 读卡记录,  门磁,  系统记录
    /// </summary>
    public class TransactionDatabaseDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public TransactionDetail[] ListTransaction { get; set; }
        /// <summary>
        /// 读卡相关记录
        /// </summary>
        public TransactionDetail CardTransactionDetail;
        /// <summary>
        /// 门磁相关记录
        /// </summary>
        public TransactionDetail DoorSensorTransactionDetail;
        
        /// <summary>
        /// 系统相关记录
        /// </summary>
        public TransactionDetail SystemTransactionDetail;

        /// <summary>
        /// 初始化参数
        /// </summary>
        public TransactionDatabaseDetail()
        {
            CardTransactionDetail = new TransactionDetail();
            DoorSensorTransactionDetail = new TransactionDetail();
            SystemTransactionDetail = new TransactionDetail();
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <returns></returns>
        public int GetDataLen()
        {
            return 0x0D * 3;
        }

        /// <summary>
        /// 进行解码
        /// </summary>
        /// <param name="data"></param>
        public void SetBytes(IByteBuffer data)
        {
            ListTransaction = new TransactionDetail[]{CardTransactionDetail, DoorSensorTransactionDetail, SystemTransactionDetail};
            for (int i = 0; i < ListTransaction.Length; i++)
            {
                ListTransaction[i].SetBytes(data);
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IByteBuffer GetBytes()
        {
            return null;
        }
    }
}
