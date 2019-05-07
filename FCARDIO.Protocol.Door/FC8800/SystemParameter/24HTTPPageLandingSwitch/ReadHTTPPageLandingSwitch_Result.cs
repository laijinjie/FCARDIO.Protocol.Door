using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.HTTPPageLandingSwitch
{
    /// <summary>
    /// 读取HTTP网页登陆开关_结果
    /// </summary>
    public class ReadHTTPPageLandingSwitch_Result : WriteHTTPPageLandingSwitch_Parameter, INCommandResult
    {
    }
}