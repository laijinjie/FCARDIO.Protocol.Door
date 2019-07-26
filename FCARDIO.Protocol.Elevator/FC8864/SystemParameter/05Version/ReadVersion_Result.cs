using DotNetty.Buffers;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.Version
{
    /// <summary>
    /// 获取设备版本号_结果
    /// </summary>
    public class ReadVersion_Result : INCommandResult
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version;
        /// <summary>
        /// 修正号
        /// </summary>
        public string CorrectionNumber;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Version = null;
            CorrectionNumber = null;
            return;
        }

        /// <summary>
        /// 对设备版本号参数进行解码
        /// </summary>
        /// <param name="databuf">包含参数结构的缓冲区</param>
        public void SetBytes(IByteBuffer databuf)
        {
            Version = databuf.ReadString(2, System.Text.Encoding.ASCII);
            CorrectionNumber = databuf.ReadString(2, System.Text.Encoding.ASCII);
            //版本号与修正号拼接
            Version = $"{Version}.{CorrectionNumber}";
        }
    }
}