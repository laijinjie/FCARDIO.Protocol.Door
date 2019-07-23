using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.USB.OfflinePatrol.Transaction.ClearTransactionDatabase
{
    /// <summary>
    /// 清空所有类型的记录数据库
    /// </summary>
    public class TransactionDatabaseEmpty : Write_Command
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        public TransactionDatabaseEmpty(INCommandDetail cd) : base(cd, null) { }

        /// <summary>
        /// 检查 参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            return true;
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x04, 0x02, 0x00, null);
        }
    }
}
