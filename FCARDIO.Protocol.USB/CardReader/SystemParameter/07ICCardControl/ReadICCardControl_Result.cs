﻿using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.USB.CardReader.SystemParameter.ICCardControl
{
    /// <summary>
    /// 读取扇区验证 返回结果
    /// </summary>
    public class ReadICCardControl_Result : WriteICCardControl_Parameter, INCommandResult
    {
    }
}
