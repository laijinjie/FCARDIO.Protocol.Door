using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Door.FirstCardOpen
{
    /// <summary>
    /// 首卡开门参数 返回结果
    /// </summary>
    public class ReadFirstCardOpen_Result : WriteFirstCardOpen_Parameter, INCommandResult
    {
    }
}
