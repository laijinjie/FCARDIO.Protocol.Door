using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.Transaction._02ClearTransactionDatabase
{
    public class ClearTransactionDatabase_StartIndex : ClearTransactionDatabase_Base
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ClearTransactionDatabase_StartIndex(INCommandDetail cd, ClearTransactionDatabase_Parameter parameter) : base(cd, parameter)
        {
            CmdPar = 0x03;
        }
    }
}
