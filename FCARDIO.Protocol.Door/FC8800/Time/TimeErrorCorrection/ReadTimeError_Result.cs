using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Time.TimeErrorCorrection
{
    /// <summary>
    /// 读取读取误差自修正参数_结果
    /// </summary>
    public class ReadTimeError_Result : WriteTimeError_Parameter, INCommandResult
    {
    }
}