using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Fingerprint.SystemParameter.Password
{
    /// <summary>
    /// 读取密码 返回结果
    /// </summary>
    public class ReadPassword_Result : WritePassword_Parameter, INCommandResult
    {
    }
}
