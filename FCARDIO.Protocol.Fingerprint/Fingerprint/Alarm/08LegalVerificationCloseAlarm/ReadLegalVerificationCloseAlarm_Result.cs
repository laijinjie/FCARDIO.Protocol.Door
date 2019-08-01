using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Fingerprint.Alarm.LegalVerificationCloseAlarm
{
    /// <summary>
    /// 读取 合法验证解除报警开关 返回结果
    /// </summary>
    public class ReadLegalVerificationCloseAlarm_Result : WriteLegalVerificationCloseAlarm_Parameter, INCommandResult
    {
    }
}
