using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Door.FC8800.Door.InvalidCardAlarmOption
{
    /// <summary>
    /// 非法读卡报警_结果
    /// </summary>
    public class InvalidCardAlarmOption_Result : WriteInvalidCardAlarmOption_Parameter, INCommandResult
    {

    }
}
