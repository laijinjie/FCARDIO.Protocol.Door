using FCARDIO.Core.Command;
using System;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置记录存储方式
    /// </summary>
    public class WriteRecordMode : Protocol.Door.FC8800.SystemParameter.FunctionParameter.WriteRecordMode
    {
        /// <summary>
        /// 设置记录存储方式 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含记录存储方式</param>
        public WriteRecordMode(INCommandDetail cd, WriteRecordMode_Parameter par) : base(cd, par) {
            CmdType = 0x41;
            CmdIndex = 0x0A;
        }

    }
}