using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Door.AutoLockedSetting
{
    /// <summary>
    /// 定时锁定门_结果
    /// </summary>
    public class ReadAutoLockedSetting_Result : WriteAutoLockedSetting_Parameter, INCommandResult
    {
    }
}
