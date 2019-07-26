using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.USB.CardReader.SystemParameter.TTLOutput
{
    /// <summary>
    /// 读取TTL输出参数 返回结果
    /// </summary>
    public class ReadTTLOutput_Result : WriteTTLOutput_Parameter, INCommandResult
    {
    }
}
