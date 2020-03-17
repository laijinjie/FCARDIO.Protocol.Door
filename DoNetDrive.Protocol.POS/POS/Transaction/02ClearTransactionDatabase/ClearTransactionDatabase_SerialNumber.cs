using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.Transaction._02ClearTransactionDatabase
{
    /// <summary>
    /// 清空指定类型的记录序号
    /// </summary>
    public class ClearTransactionDatabase_SerialNumber : ClearTransactionDatabase_Base
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ClearTransactionDatabase_SerialNumber(INCommandDetail cd, ClearTransactionDatabase_Parameter parameter) : base(cd, parameter)
        {
            CmdPar = 0x02;
        }
    }
}
