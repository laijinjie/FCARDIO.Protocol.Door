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
        /// 1--校验成功
        //0--校验失败
        //2--特征码无法识别
        //3--人员照片不可识别
        //255-文件未准备就绪
        /// </summary>
        public byte Success;

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
