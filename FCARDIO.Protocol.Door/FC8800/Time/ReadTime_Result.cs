using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Time
{
    /// <summary>
    /// 从控制器中读取控制器时间_结果
    /// </summary>
    public class ReadTime_Result : WriteCustomTime_Parameter, INCommandResult
    {
    }
}