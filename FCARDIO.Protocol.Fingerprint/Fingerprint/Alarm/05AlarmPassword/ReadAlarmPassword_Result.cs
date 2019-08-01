using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Fingerprint.Alarm.AlarmPassword
{
    /// <summary>
    /// 读取 胁迫报警密码 返回结果
    /// </summary>
    public class ReadAlarmPassword_Result : WriteAlarmPassword_Parameter, INCommandResult
    {
    }
}
