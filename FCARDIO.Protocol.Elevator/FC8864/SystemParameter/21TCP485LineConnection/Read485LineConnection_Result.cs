using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.TCP485LineConnection
{
    /// <summary>
    /// 读取 TCP、485线路桥接 返回结果
    /// </summary>
    public class Read485LineConnection_Result : Write485LineConnection_Parameter, INCommandResult
    {
    }
}
