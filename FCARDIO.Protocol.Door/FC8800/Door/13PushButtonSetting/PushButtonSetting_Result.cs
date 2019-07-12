using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Door.FC8800.Door.PushButtonSetting
{
    /// <summary>
    /// 出门开关
    /// </summary>
    public class PushButtonSetting_Result
        :WritePushButtonSetting_Parameter, INCommandResult
    {

    }
}
