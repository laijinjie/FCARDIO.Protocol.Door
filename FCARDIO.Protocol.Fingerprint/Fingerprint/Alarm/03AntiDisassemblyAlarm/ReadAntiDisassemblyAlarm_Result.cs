﻿using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Fingerprint.Alarm.AntiDisassemblyAlarm
{
    /// <summary>
    /// 读取 防拆报警功能 返回结果
    /// </summary>
    public class ReadAntiDisassemblyAlarm_Result : WriteAntiDisassemblyAlarm_Parameter, INCommandResult
    {
    }
}
