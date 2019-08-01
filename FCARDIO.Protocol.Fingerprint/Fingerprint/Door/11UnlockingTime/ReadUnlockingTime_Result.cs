using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Fingerprint.Door.RelayReleaseTime
{
    /// <summary>
    /// 开锁时输出时长_结果
    /// </summary>
    public class ReadUnlockingTime_Result : WriteUnlockingTime_Parameter, INCommandResult
    {
    }
}
