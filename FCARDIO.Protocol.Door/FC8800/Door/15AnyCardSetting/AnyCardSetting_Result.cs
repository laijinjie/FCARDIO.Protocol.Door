using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Door.FC8800.Door.AnyCardSetting
{
    /// <summary>
    /// 全卡开门功能
    /// </summary>
    public class AnyCardSetting_Result
        :WriteAnyCardSetting_Parameter, INCommandResult
    {

    }
}
