using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Door.GateMagneticAlarm
{
    /// <summary>
    /// 读取门磁报警参数
    /// </summary>
    public class ReadGateMagneticAlarm_Result : WriteGateMagneticAlarm_Parameter , INCommandResult
    {
    }
}
