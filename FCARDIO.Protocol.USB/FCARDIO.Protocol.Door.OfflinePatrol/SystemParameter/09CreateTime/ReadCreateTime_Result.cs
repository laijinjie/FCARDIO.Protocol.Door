using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.OfflinePatrol.SystemParameter.CreateTime
{
    /// <summary>
    /// 读取生成日期 返回结果
    /// </summary>
    public class ReadCreateTime_Result : WriteCreateTime_Parameter, INCommandResult
    {
    }
}
