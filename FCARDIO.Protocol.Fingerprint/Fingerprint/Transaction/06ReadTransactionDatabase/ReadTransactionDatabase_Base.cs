using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800;
using FCARDIO.Protocol.Fingerprint.Data;
using FCARDIO.Protocol.OnlineAccess;
using FCARDIO.Protocol.Transaction;
using System.Collections.Generic;
using System.Linq;

namespace FCARDIO.Protocol.Fingerprint.Transaction
{
    /// <summary>
    ///  读取新记录
    ///  读指定类型的记录数据库最新记录，并读取指定数量。
    ///  成功返回结果参考 link ReadTransactionDatabase_Result 
    /// </summary>
    public abstract class ReadTransactionDatabase_Base : 
        FCARDIO.Protocol.Door.FC8800.Transaction.ReadTransactionDatabase_Base
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
        protected override Protocol.Door.FC8800.Data.TransactionDetail GetTransactionDetail(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, CheckResponseCmdType, 0x01, 0x00, 0x0D * 3))
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
