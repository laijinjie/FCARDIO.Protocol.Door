using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Fingerprint.Alarm.BlacklistAlarm
{
    /// <summary>
    /// 读取 黑名单报警 返回结果
    /// </summary>
    public class ReadBlacklistAlarm_Result : WriteBlacklistAlarm_Parameter, INCommandResult
    {
    }
}
