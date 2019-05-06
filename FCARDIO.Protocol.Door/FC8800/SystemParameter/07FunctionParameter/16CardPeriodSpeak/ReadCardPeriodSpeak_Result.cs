using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取卡片到期提示_结果
    /// </summary>
    public class ReadCardPeriodSpeak_Result : WriteCardPeriodSpeak_Parameter, INCommandResult
    {
    }
}