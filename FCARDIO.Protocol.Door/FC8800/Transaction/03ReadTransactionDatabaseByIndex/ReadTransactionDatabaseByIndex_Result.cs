using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using FCARDIO.Core.Extension;
using FCARDIO.Protocol.Door.FC8800.Data;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.ReadTransactionDatabaseByIndex
{
    /// <summary>
    /// 读取控制器中的卡片数据库信息返回值
    /// </summary>
    public class ReadTransactionDatabaseByIndex_Result : INCommandResult
    {
        /// <summary>
        ///  记录数据库类型
        ///  1 读卡记录
        ///  2 出门开关记录
        ///  3 门磁记录
        ///  4 软件操作记录
        ///  5 报警记录
        ///  6 系统记录
        /// </summary>
        public e_TransactionDatabaseType DatabaseType;

        /// <summary>
        /// 读索引号
        /// </summary>
        public int ReadIndex;

        /// <summary>
        /// 读取数量
        /// </summary>
        public int Quantity;

        /// <summary>
        /// 记录列表
        /// </summary>
        public List<AbstractTransaction> TransactionList;

        /// <summary>
        /// 初始化参数
        /// </summary>
        public ReadTransactionDatabaseByIndex_Result(){}

        /// <summary>
        ///创建结构
        /// </summary>
        /// <param name="_DatabaseType">记录数据库类型 取值1-6</param>
        /// <param name="_ReadIndex">读索引号</param>
        /// <param name="_Quantity">读取数量</param>
        /// <param name="_TransactionList">记录列表</param>
        public ReadTransactionDatabaseByIndex_Result(e_TransactionDatabaseType _DatabaseType, int _ReadIndex, int _Quantity,List<AbstractTransaction> _TransactionList)
        {
            DatabaseType = _DatabaseType;
            ReadIndex = _ReadIndex;
            Quantity = _Quantity;
            TransactionList = _TransactionList;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

       
    }
}
