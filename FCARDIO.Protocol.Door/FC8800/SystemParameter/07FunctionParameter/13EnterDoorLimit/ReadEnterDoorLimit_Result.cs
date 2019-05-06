using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取门内人数限制_结果
    /// </summary>
    public class ReadEnterDoorLimit_Result : WriteEnterDoorLimit_Parameter, INCommandResult
    {
    }
}