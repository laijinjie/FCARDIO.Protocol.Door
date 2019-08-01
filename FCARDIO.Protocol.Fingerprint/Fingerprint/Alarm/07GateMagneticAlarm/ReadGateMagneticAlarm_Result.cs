using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Fingerprint.Alarm.GateMagneticAlarm
{
    /// <summary>
    /// 读取门磁报警参数
    /// </summary>
    public class ReadGateMagneticAlarm_Result : WriteGateMagneticAlarm_Parameter , INCommandResult
    {
    }
}
