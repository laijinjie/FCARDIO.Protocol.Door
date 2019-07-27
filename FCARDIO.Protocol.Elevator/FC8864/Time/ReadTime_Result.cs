using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Time
{
    /// <summary>
    /// 从控制器中读取控制器时间_结果
    /// </summary>
    public class ReadTime_Result : WriteCustomTime_Parameter, INCommandResult
    {
    }
}