﻿using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.OfflinePatrol.Time
{
    /// <summary>
    /// 从设备中读取控制器时间_结果
    /// </summary>
    public class ReadTime_Result : WriteCustomTime_Parameter, INCommandResult
    {
    }
}
