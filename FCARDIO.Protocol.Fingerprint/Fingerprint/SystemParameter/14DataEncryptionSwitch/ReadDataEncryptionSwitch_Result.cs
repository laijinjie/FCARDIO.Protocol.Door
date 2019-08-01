using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Fingerprint.SystemParameter.DataEncryptionSwitch
{
    /// <summary>
    /// 读取 数据包加密开关 返回结果
    /// </summary>
    public class ReadDataEncryptionSwitch_Result : WriteDataEncryptionSwitch_Parameter, INCommandResult
    {
    }
}
