﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Transaction
{
    /// <summary>
    /// 读取控制器中的卡片数据库信息
    /// </summary>
    public class ReadTransactionDatabaseDetail
        : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="detail"></param>
        public ReadTransactionDatabaseDetail(INCommandDetail detail) : base(detail, null) { }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x08, 0x01, 0x00, 0x00, null);
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x0D * 6))
            {
                var buf = oPck.CmdData;
                ReadTransactionDatabaseDetail_Result rst = new ReadTransactionDatabaseDetail_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

    }
}
