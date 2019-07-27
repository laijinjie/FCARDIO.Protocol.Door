using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取读卡器数据校验_结果
    /// </summary>
    public class ReadReaderCheckMode_Result : WriteReaderCheckMode_Parameter, INCommandResult
    {
    }
}