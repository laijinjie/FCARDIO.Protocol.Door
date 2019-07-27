﻿using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Door.AlarmPassword
{
    /// <summary>
    /// 写入胁迫报警功能
    /// 功能开启后，在密码键盘读卡器上输入特定密码后就会报警；
    /// </summary>
    public class WriteAlarmPassword
        : Write_Command
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public WriteAlarmPassword(INCommandDetail cd, WriteAlarmPassword_parameter parameter) : base(cd, parameter) { }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            WriteAlarmPassword_parameter model = _Parameter as WriteAlarmPassword_parameter;
            Packet(0x41, 0x0A, 0x0C, 0x06, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }


        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteAlarmPassword_parameter model = value as WriteAlarmPassword_parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        
    }
}
