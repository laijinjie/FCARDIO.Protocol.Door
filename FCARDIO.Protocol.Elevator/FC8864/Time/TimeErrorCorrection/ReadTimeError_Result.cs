using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Time.TimeErrorCorrection
{
    /// <summary>
    /// 读取读取误差自修正参数_结果
    /// </summary>
    public class ReadTimeError_Result : WriteTimeError_Parameter, INCommandResult
    {
    }
}