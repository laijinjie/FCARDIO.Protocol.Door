using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Fingerprint.SystemParameter.ComparisonThreshold
{
    /// <summary>
    /// 读取 脸、指纹比对阈值 返回结果
    /// </summary>
    public class ReadComparisonThreshold_Result : WriteComparisonThreshold_Parameter, INCommandResult
    {
    }
}
