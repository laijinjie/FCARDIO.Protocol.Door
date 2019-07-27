using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Door.Relay
{
    /// <summary>
    /// 继电器 返回结果
    /// </summary>
    public class ReadRelay_Result : WriteRelay_Parameter, INCommandResult
    {
    }
}
