using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Door.UnlockingTime
{
    /// <summary>
    /// 读取 开锁时输出时长 返回参数
    /// </summary>
    public class ReadUnlockingTime_Result : WriteUnlockingTime_Parameter, INCommandResult
    {
    }
}
