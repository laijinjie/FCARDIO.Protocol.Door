﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.TCP485LineConnection
{
    /// <summary>
    /// 读取 TCP、485线路桥接
    /// </summary>
    public class Read485LineConnection : Protocol.Door.FC8800.SystemParameter.Check485Line.ReadCheck485Line
    {

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        public Read485LineConnection(INCommandDetail cd) : base(cd)
        {
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x41, 0x80, 0x01);
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x01))
            {
                var buf = oPck.CmdData;
                var rst = new Protocol.Door.FC8800.SystemParameter.Check485Line.ReadCheck485Line_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}
