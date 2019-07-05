using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ManageKeyboardSetting
{
    /// <summary>
    /// 键盘管理功能 返回结果
    /// </summary>
    public class ManageKeyboardSetting_Result : WriteManageKeyboardSetting_Parameter, INCommandResult
    {
    }
}
