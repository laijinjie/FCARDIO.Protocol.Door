using DotNetty.Buffers;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Fingerprint.AdditionalData.WriteFeatureCode
{
    /// <summary>
    /// 准备写文件返回结果
    /// </summary>
    public class WriteFeatureCode_Result : INCommandResult
    {
        /// <summary>
        /// 文件句柄
        /// </summary>
        public int FileHandle;

        /// <summary>
        /// 写入结果
        /// </summary>
        public bool Success;

        public void Dispose()
        {

        }

        /// <summary>
        /// 读取ByteBuffer内容
        /// </summary>
        /// <param name="buf"></param>
        public void SetBytes(IByteBuffer buf)
        {
            FileHandle = buf.ReadInt();
        }
    }
}
