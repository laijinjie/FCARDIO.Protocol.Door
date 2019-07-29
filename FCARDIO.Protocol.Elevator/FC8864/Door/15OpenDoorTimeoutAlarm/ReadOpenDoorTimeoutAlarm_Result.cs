using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Door.OpenDoorTimeoutAlarm
{
    /// <summary>
    /// 读取 开门超时报警参数 返回结果
    /// </summary>
    public class ReadOpenDoorTimeoutAlarm_Result : WriteOpenDoorTimeoutAlarm_Parameter, INCommandResult
    {
    }
}
