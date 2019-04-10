using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.ClearTransactionDatabase
{
    /// <summary>
    /// 清空指定类型的记录数据库
    /// </summary>
    public class ClearTransactionDatabase_Parameter:AbstractParameter
    {
        /// <summary>
        /// 记录数据库类型
        /// 1 &emsp; 读卡记录 
        /// 2 &emsp; 出门开关记录
        /// 3 &emsp; 门磁记录  
        /// 4 &emsp; 软件操作记录
        /// 5 &emsp; 报警记录 
        /// 6 &emsp; 系统记录  
        /// </summary>
        public e_TransactionDatabaseType DatabaseType;
        
        /// <summary>
        /// 清空指定类型的记录数据库
        /// </summary>
        /// <param name="type">取值范围 1-6</param>
        public ClearTransactionDatabase_Parameter(e_TransactionDatabaseType type)
        {
            DatabaseType = type;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if(databuf.ReadableBytes != 1)
                throw new NotImplementedException();
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 1;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            
        }
    }
}
