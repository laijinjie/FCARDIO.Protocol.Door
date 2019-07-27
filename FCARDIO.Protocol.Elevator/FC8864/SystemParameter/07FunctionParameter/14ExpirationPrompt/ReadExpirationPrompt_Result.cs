using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter.ExpirationPrompt
{
    /// <summary>
    /// 读取 卡片到期提示 返回结果
    /// </summary>
    public class ReadExpirationPrompt_Result : WriteExpirationPrompt_Parameter, INCommandResult
    {
    }
}
