using DoNetDrive.Core.Command;
using System;

namespace DoNetDrive.Protocol.POS.Transaction
{
    /// <summary>
    /// 更新记录尾号命令
    /// </summary>
    public class WriteTransactionDatabaseWriteIndex : WriteTransactionDatabaseIndex
    {
       
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public WriteTransactionDatabaseWriteIndex(INCommandDetail cd, WriteTransactionDatabaseIndex_Parameter parameter) : base(cd, parameter)
        {
            CmdPar = 0x02;
        }
    }
}
