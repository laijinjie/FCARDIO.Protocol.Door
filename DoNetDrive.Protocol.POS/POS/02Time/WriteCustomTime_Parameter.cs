using DoNetDrive.Protocol.Util;
using DotNetty.Buffers;
using System;

namespace DoNetDrive.Protocol.POS.Time
{
    /// <summary>
    /// 设置控制器的日期时间
    /// </summary>
    public class WriteCustomTime_Parameter : AbstractParameter
    {
        /// <summary>
        /// 设备有效期
        /// </summary>
        public DateTime ControllerDate;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public WriteCustomTime_Parameter() { }

        /// <summary>
        /// 使用设备有效期初始化实例
        /// </summary>
        /// <param name="ControllerDate">电脑时间</param>
        public WriteCustomTime_Parameter(DateTime ControllerDate)
        {
            this.ControllerDate = ControllerDate;
            if (!checkedParameter())
            {
                throw new ArgumentException("Deadline Error");
            }
        }

        public override bool checkedParameter()
        {
            //if (Deadline < 0 || Deadline > 65535)
            //{
            //    throw new ArgumentException("Deadline Error");
            //}

            return true;
        }

        public override void Dispose()
        {

        }/// <summary>
         /// 对控制器的日期时间参数进行编码
         /// </summary>
         /// <param name="databuf"></param>
         /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            byte[] Datebuf = new byte[7];
            TimeUtil.DateToBCD_ssmmhhddMMwwyy(Datebuf, ControllerDate);
            databuf.WriteBytes(Datebuf);
            return databuf;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x07;
        }

        /// <summary>
        /// 对控制器的日期时间参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            //控制器的日期时间
            ControllerDate = TimeUtil.BCDTimeToDate_ssmmHHddMMWWyy(databuf);
        }
    }
}

