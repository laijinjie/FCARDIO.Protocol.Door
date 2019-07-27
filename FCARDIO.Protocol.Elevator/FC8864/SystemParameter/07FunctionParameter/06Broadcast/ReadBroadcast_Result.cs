using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取语音播报语音段开关_结果
    /// </summary>
    public class ReadBroadcast_Result : WriteBroadcast_Parameter, INCommandResult
    {
    }
}