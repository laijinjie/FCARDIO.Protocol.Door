﻿using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.Fingerprint.Transaction
{
    /// <summary>
    /// 读取设备中的记录存储信息
    /// </summary>
    public class ReadTransactionDatabaseDetail : Door8800Command_ReadParameter
    {
        /// <summary>
        /// 创建 读取设备中的记录存储信息命令
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
            if (CheckResponse(oPck))
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
