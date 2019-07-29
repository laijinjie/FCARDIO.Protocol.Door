using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取定时读卡播报语音消息参数_结果
    /// </summary>
    public class ReadReadCardSpeak_Result : WriteReadCardSpeak_Parameter, INCommandResult
    {
    }
}