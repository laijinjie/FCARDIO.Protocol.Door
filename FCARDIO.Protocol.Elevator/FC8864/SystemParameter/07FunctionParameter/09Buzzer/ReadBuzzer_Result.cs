using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取主板蜂鸣器_结果
    /// </summary>
    public class ReadBuzzer_Result : WriteBuzzer_Parameter, INCommandResult
    {
    }
}