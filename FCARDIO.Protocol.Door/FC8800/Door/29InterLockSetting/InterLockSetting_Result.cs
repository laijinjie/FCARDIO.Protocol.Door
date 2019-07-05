using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.InterLockSetting
{
    /// <summary>
    /// 区域互锁 参数
    /// </summary>
    public class InterLockSetting_Result : WriteInterLockSetting_Parameter, INCommandResult
    {
    }
}
