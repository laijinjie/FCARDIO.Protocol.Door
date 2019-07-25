using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.USB.CardReader.SystemParameter.ICCardCustomNum
{
    /// <summary>
    /// 读取卡号参数 返回结果
    /// </summary>
    public class ReadICCardCustomNum_Result : WriteICCardCustomNum_Parameter, INCommandResult
    {
    }
}
