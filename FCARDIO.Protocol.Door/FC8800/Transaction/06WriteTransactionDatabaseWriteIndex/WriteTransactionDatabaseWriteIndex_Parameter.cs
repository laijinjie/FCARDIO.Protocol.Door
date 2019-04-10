using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.WriteTransactionDatabaseWriteIndex
{
    /// <summary>
    /// 修改指定记录数据库的写索引
    /// 记录尾地址
    /// </summary>
    public class WriteTransactionDatabaseWriteIndex_Parameter : AbstractParameter
    {
        /// <summary>
        ///  记录数据库类型
        ///  1 &emsp; 读卡记录
        ///  2 &emsp; 出门开关记录
        ///  3 &emsp; 门磁记录
        ///  4 &emsp; 软件操作记录
        ///  5 &emsp; 报警记录
        ///  6 &emsp; 系统记录
        /// </summary>
        public e_TransactionDatabaseType DatabaseType;
       /// <summary>
       /// 数据库中的写索引号
       /// 记录尾地址
        /// </summary>
        public int WriteIndex;

        public WriteTransactionDatabaseWriteIndex_Parameter(){}

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="_DatabaseType">记录数据库类型</param>
        /// <param name="_WriteIndex">记录尾地址</param>
        public WriteTransactionDatabaseWriteIndex_Parameter
(e_TransactionDatabaseType _DatabaseType, int _WriteIndex)
        {
            DatabaseType = _DatabaseType;
            WriteIndex = _WriteIndex;
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
            if (databuf.ReadableBytes != 2)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteByte(WriteIndex);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 2;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            WriteIndex = databuf.ReadByte();
        }
    }
}
