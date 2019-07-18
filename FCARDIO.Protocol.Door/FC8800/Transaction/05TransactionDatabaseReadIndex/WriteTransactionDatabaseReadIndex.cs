﻿using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.TransactionDatabaseReadIndex
{
    /// <summary>
    /// 更新记录指针
    /// </summary>
    public class WriteTransactionDatabaseReadIndex : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public WriteTransactionDatabaseReadIndex(INCommandDetail cd, WriteTransactionDatabaseReadIndex_Parameter parameter) : base(cd, parameter) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteTransactionDatabaseReadIndex_Parameter model = value as WriteTransactionDatabaseReadIndex_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            WriteTransactionDatabaseReadIndex_Parameter model = _Parameter as WriteTransactionDatabaseReadIndex_Parameter;
            Packet(0x08, 0x03, 0x00, 0x06, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }

    }
}
