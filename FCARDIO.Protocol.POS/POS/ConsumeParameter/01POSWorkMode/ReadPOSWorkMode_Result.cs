﻿using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.POS.ConsumeParameter.POSWorkMode
{
    /// <summary>
    /// 读取消费机工作模式命令返回结果
    /// </summary>
    public class ReadPOSWorkMode_Result : WritePOSWorkMode_Parameter, INCommandResult
    {
    }
}
