using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.POS.ConsumeParameter.ConsumptionLimits
{
    /// <summary>
    /// 读取消费机消费限额返回结果
    /// </summary>
    public class ReadConsumptionLimits_Result : WriteConsumptionLimits_Parameter, INCommandResult
    {
    }
}
