using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.ReadTransactionDatabase
{
    /// <summary>
    /// 读取新记录
    /// </summary>
    public class ReadTransactionDatabase_Parameter
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
        public e_TransactionDatabaseType DatabaseType;

        /// <summary>
        /// 读取数量 0-160000,0表示都取所有新记录
        /// </summary>
        public int Quantity;

        /// <summary>
        ///  每次读取数量 1-300
        /// </summary>
        public int PacketSize;

       /// <summary>
       ///  创建结构
       /// </summary>
       /// <param name="detail"> 包含命令的执行时的一些必要信息，命令执行的连接器通道，命令身份验证信息，用户附加数据，超时重试参数</param>
       /// <param name="type"> 取值范围 1-6</param>
        public ReadTransactionDatabase_Parameter(e_TransactionDatabaseType type,int _PacketSize, int _Quantity)
        {
            DatabaseType = type;
            PacketSize = _PacketSize;
            Quantity = _Quantity;
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
            if (databuf.WritableBytes != 0)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteByte(PacketSize);
            databuf.WriteByte(Quantity);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            PacketSize = databuf.ReadByte();
            Quantity = databuf.ReadByte();
        }
    }
}
