﻿using DotNetty.Buffers;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.TCP485LineConnection
{
    /// <summary>
    /// 设置 TCP、485线路桥接 参数
    /// </summary>
    public class Write485LineConnection_Parameter : Protocol.Door.FC8800.SystemParameter.FunctionParameter.WriteCardPeriodSpeak_Parameter
    {
        
        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public Write485LineConnection_Parameter() { }

        /// <summary>
        /// 使用主板蜂鸣器初始化实例
        /// </summary>
        /// <param name="isUse">是否启用</param>
        public Write485LineConnection_Parameter(byte _Use)
        {
            Use = _Use;
        }

    }
}
