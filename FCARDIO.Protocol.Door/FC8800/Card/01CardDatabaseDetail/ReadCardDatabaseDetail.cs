﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardDatabaseDetail
{
    /// <summary>
    /// 读取卡片存储详情
    /// </summary>
    public class ReadCardDatabaseDetail : FC8800Command
    {
        public ReadCardDatabaseDetail(INCommandDetail detail) : base(detail, null) { }

        /// <summary>
        /// 检查参数
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
            Packet(0x07, 0x01, 0x00, 0x00, null);
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x10))
            {
                var buf = oPck.CmdData;
                ReadCardDatabaseDetail_Result rst = new ReadCardDatabaseDetail_Result();
                rst.SetBytes(rst,buf);
                _Result = rst;
               
                CommandCompleted();
            }
        }

        /// <summary>
        /// 命令重发时需要处理的函数
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }

        /// <summary>
        /// 命令释放时需要处理的函数
        /// </summary>
        protected override void Release1()
        {
            return;
        }
    }
}
