﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.USBDrive;

namespace FCARDIO.Protocol.USB.CardReader.SystemParameter.CreateTime
{
    /// <summary>
    /// 读取生产日期
    /// </summary>
    public class ReadCreateTime : Read_Command
    {
        /// <summary>
        /// 获取控制器SN 初始化命令
        /// </summary>
        /// <param name="cd"></param>
        public ReadCreateTime(INCommandDetail cd) : base(cd, null)
        {
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(USBDrivePacket oPck)
        {
            if (CheckResponse(oPck,1,0x84, 3))
            {
                var buf = oPck.CmdData;
                ReadCreateTime_Result rst = new ReadCreateTime_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(1, 0x84);
        }
    }
}
