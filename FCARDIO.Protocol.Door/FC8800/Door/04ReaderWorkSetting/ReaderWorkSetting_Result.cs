using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;
using FCARDIO.Core.Data;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.ReaderWorkSetting
{
    /// <summary>
    /// 门认证方式_结果
    /// </summary>
    public class ReaderWorkSetting_Result : WriteReaderWorkSetting_Parameter, INCommandResult
    {
    }
}
