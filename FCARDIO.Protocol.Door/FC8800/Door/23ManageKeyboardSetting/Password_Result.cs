using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ManageKeyboardSetting
{
    /// <summary>
    /// Password 返回结果
    /// </summary>
    public class Password_Result : WritePassword_Parameter, INCommandResult
    {
    }
}
