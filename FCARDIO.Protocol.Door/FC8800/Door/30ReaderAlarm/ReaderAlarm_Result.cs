using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ReaderAlarm
{
    /// <summary>
    /// 读卡器防拆报警 返回结果
    /// </summary>
    public class ReaderAlarm_Result : WriteReaderAlarm_Parameter, INCommandResult
    {
    }
}
