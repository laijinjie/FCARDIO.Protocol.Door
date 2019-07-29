using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Door.FC8800.Door.AntiPassback
{
    /// <summary>
    /// 防潜返 返回结果
    /// </summary>
    public class AntiPassback_Result
        :WriteAntiPassback_Parameter,INCommandResult
    {
    }
}
