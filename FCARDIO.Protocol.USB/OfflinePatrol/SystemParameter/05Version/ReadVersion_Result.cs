using DotNetty.Buffers;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.USB.OfflinePatrol.SystemParameter.Version
{
    /// <summary>
    /// 获取设备运行信息 返回结果
    /// </summary>
    public class ReadVersion_Result : INCommandResult
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public ushort VerNum;

        /// <summary>
        /// 修正号
        /// </summary>
        public ushort Revise;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
           
        }


        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public void SetBytes(IByteBuffer databuf)
        {
            VerNum = databuf.ReadUnsignedShort();
            Revise = databuf.ReadUnsignedShort();
        }

        /// <summary>
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteUnsignedShort(VerNum);
            databuf.WriteUnsignedShort(Revise);
            return databuf;
        }
    }
}
