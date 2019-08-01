using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Fingerprint.Door.ExemptionVerificationOpen
{
    /// <summary>
    /// 免验证开门 返回结果
    /// </summary>
    public class ReadExemptionVerificationOpen_Result : WriteExemptionVerificationOpen_Parameter, INCommandResult
    {
    }
}
