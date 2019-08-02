using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.ManageCard
{
    /// <summary>
    /// 读取 管理卡功能 返回结果
    /// </summary>
    public class ReadManageCard_Result : WriteManageCard_Parameter, INCommandResult
    {
    }
}
