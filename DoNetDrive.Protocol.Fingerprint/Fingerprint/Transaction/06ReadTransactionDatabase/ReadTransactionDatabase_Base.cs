using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800;
using DoNetDrive.Protocol.Fingerprint.Data;
using DoNetDrive.Protocol.OnlineAccess;
using DoNetDrive.Protocol.Transaction;
using System.Collections.Generic;
using System.Linq;

namespace DoNetDrive.Protocol.Fingerprint.Transaction
{
    /// <summary>
    ///  读取新记录
    ///  读指定类型的记录数据库最新记录，并读取指定数量。
    ///  成功返回结果参考 link ReadTransactionDatabase_Result 
    /// </summary>
    public abstract class ReadTransactionDatabase_Base : 
        DoNetDrive.Protocol.Door.Door8800.Transaction.ReadTransactionDatabase_Base
    {
        


        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadTransactionDatabase_Base(INCommandDetail cd, ReadTransactionDatabase_Parameter parameter) 
            : base(cd, parameter)
        {
        }

        /// <summary>
        /// 获取指定类型的数据库详情信息
        /// </summary>
        /// <returns></returns>
        protected override Protocol.Door.Door8800.Data.TransactionDetail GetTransactionDetail(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, CheckResponseCmdType, 0x01, 0x00))
            {
                var buf = oPck.CmdData;
                ReadTransactionDatabaseDetail_Result rst = new ReadTransactionDatabaseDetail_Result();
                rst.SetBytes(buf);
                return rst.DatabaseDetail.ListTransaction[(int)mParameter.DatabaseType - 1];
            }

            return null;
        }

    }
}
