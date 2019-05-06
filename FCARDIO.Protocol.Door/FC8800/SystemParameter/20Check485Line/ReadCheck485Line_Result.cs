using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.Check485Line
{
    /// <summary>
    /// 读取485线路反接检测开关_结果
    /// </summary>
    public class ReadCheck485Line_Result : WriteCheck485Line_Parameter, INCommandResult
    {
    }
}