﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.TransactionDatabaseReadIndex
{
    /// <summary>
    /// 更新记录指针
    /// </summary>
    public class WriteTransactionDatabaseReadIndex_Parameter : AbstractParameter
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
        /// 数据库中的读索引号
        /// </summary>
        public int ReadIndex;
        /// <summary>
        /// 循环标记
        /// </summary>
        public bool IsCircle;

        public WriteTransactionDatabaseReadIndex_Parameter(){}
        /// <summary>
        /// 创建结构
        /// </summary>
        /// <param name="_DatabaseType">记录数据库类型</param>
        /// <param name="_ReadIndex">数据库中的读索引号</param>
        /// <param name="_IsCircle">循环标记</param>
        public WriteTransactionDatabaseReadIndex_Parameter(e_TransactionDatabaseType _DatabaseType, int _ReadIndex,bool _IsCircle)
        {
            DatabaseType = _DatabaseType;
            ReadIndex = _ReadIndex;
            IsCircle = _IsCircle;
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
            if (databuf.ReadableBytes != 3)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteByte(ReadIndex);
            databuf.WriteBoolean(IsCircle);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 3;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            ReadIndex = databuf.ReadByte();
            IsCircle = databuf.ReadBoolean();
        }
    }
}
