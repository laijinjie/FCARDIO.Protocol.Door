﻿using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.OfflinePatrol.SystemParameter.RecordStorageMode
{
    /// <summary>
    /// 读取记录存储方式 返回结果
    /// </summary>
    public class ReadRecordStorageMode_Result : WriteRecordStorageMode_Parameter, INCommandResult
    {
    }
}
