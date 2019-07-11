using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.ReadTransactionDatabaseByIndex
{
    /// <summary>
    /// 读取控制器中的卡片数据库信息返回值
    /// </summary>
    public class ReadTransactionDatabaseByIndex_Parameter
        : AbstractParameter
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
        public int DatabaseType;

        /// <summary>
        /// 读索引号
        /// </summary>
        public int ReadIndex;

        /// <summary>
        /// 读取数量
        /// </summary>
        public int Quantity;
        
        public ReadTransactionDatabaseByIndex_Parameter() { }

        /// <summary>
        ///创建结构
        /// </summary>
        /// <param name="_DatabaseType">记录数据库类型 取值1-6</param>
        /// <param name="_ReadIndex">读索引号</param>
        /// <param name="_Quantity">读取数量</param>
        /// <param name="_TransactionList">记录列表</param>
        public ReadTransactionDatabaseByIndex_Parameter(int _DatabaseType, int _ReadIndex, int _Quantity)
        {
            DatabaseType = _DatabaseType;
            ReadIndex = _ReadIndex;
            Quantity = _Quantity;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (DatabaseType < 1 || DatabaseType > 6)
                throw new ArgumentException("DatabaseType Error!");
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
            if (databuf.WritableBytes != 9)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteByte(DatabaseType);
            databuf.WriteInt(ReadIndex);
            databuf.WriteInt(Quantity);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 9;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            ReadIndex = databuf.ReadByte();
            Quantity = databuf.ReadByte();
        }
    }
}
