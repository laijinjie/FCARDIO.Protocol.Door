using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.ClearTransactionDatabase
{
    /// <summary>
    /// 清空所有类型的记录数据库
    /// </summary>
    public class TransactionDatabaseEmpty : FC8800Command_WriteParameter
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
            Packet(0x08, 0x02, 0x00, 0x00, null);
        }
    }
}
