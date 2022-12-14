using DotNetty.Buffers;
using System;

namespace DoNetDrive.Protocol.USB.OfflinePatrol.SystemParameter.LEDOpenHoldTime
{
    /// <summary>
    /// 设置开机保持时间 参数
    /// </summary>
    public class WriteLEDOpenHoldTime_Parameter : AbstractParameter
    {
        /// <summary>
        /// 时间 时间最小为1秒，最大为255秒
        /// </summary>
        public int Time = 10;


        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Time < 1 || Time > 255)
                throw new ArgumentException("Time Error!");
            return true;
        }



        /// <summary>
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteByte(Time);
            return databuf;
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 1;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            Time = databuf.ReadByte();
        }
    }
}