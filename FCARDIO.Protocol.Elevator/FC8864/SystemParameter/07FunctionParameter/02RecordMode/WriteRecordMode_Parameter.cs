﻿using DotNetty.Buffers;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置记录存储方式_参数
    /// </summary>
    public class WriteRecordMode_Parameter : Protocol.Door.FC8800.SystemParameter.FunctionParameter.WriteRecordMode_Parameter
    {
        public WriteRecordMode_Parameter()
        {

        }
        public WriteRecordMode_Parameter(byte _Mode)
        {
            Mode = _Mode;
        }
    }
}