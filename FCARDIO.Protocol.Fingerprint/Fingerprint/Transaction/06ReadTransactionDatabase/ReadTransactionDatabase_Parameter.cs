using DotNetty.Buffers;
using FCARDIO.Protocol.Door.FC8800;
using System;

namespace FCARDIO.Protocol.Fingerprint.Transaction
{
    /// <summary>
    /// 读取新记录
    /// </summary>
    public class ReadTransactionDatabase_Parameter
        : FCARDIO.Protocol.Door.FC8800.Transaction.ReadTransactionDatabase_Parameter
    {

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="type">取值范围 1-3</param>
        /// <param name="_Quantity">读取数量</param>
        public ReadTransactionDatabase_Parameter(int type, int _Quantity) :
            base((Protocol.Door.FC8800.Transaction.e_TransactionDatabaseType)type, _Quantity)
        {
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (PacketSize < 1 || PacketSize > 200)
            {
                throw new ArgumentException("PacketSize Error!");
            }
            if (Quantity < 0 || Quantity > 1000000)
            {
                throw new ArgumentException("Quantity Error!");
            }
            return true;
        }
    }
}
