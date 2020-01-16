using DotNetty.Buffers;
using FCARDIO.Protocol.Door.FC8800;
using System;

namespace FCARDIO.Protocol.Fingerprint.Transaction
{
    /// <summary>
    /// 更新记录指针
    /// </summary>
    public class WriteTransactionDatabaseReadIndex_Parameter : AbstractParameter
    {

       /// <summary>
       ///  记录数据库类型
       ///  1  读卡记录
       ///  2  门磁记录
       ///  3  系统记录
       /// </summary>
        public e_TransactionDatabaseType DatabaseType;
        /// <summary>
        /// 数据库中的索引号
        /// </summary>
        public int RecordIndex;

        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="_DatabaseType">记录数据库类型</param>
        /// <param name="_ReadIndex">数据库中的读索引号</param>
        /// <param name="_IsCircle">循环标记</param>
        public WriteTransactionDatabaseReadIndex_Parameter(e_TransactionDatabaseType _DatabaseType, int _ReadIndex)
        {
            DatabaseType = _DatabaseType;
            RecordIndex = _ReadIndex;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            
            if (RecordIndex < 0)
            {
                throw new ArgumentException("ReadIndex Error!");
            }
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
            if (databuf.WritableBytes != 5)
            {
                throw new ArgumentException("Buffer Error");
            }
            databuf.WriteByte((byte)DatabaseType);
            databuf.WriteInt(RecordIndex);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 5;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            RecordIndex = databuf.ReadByte();
        }
    }
}
