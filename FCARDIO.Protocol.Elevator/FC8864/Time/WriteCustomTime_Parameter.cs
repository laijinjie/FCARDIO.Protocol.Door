using DotNetty.Buffers;
using FCARDIO.Protocol.Util;
using System;

namespace FCARDIO.Protocol.Elevator.FC8864.Time
{
    /// <summary>
    /// 设置控制器的日期时间
    /// </summary>
    public class WriteCustomTime_Parameter : AbstractParameter
    {
        /// <summary>
        /// 控制器的日期时间
        /// </summary>
        public DateTime ControllerDate;

        /// <summary>
        /// 提供给 ReadTime_Result 使用
        /// </summary>
        public WriteCustomTime_Parameter()
        {

        }
        /// <summary>
        /// 控制器的日期时间参数初始化实例
        /// </summary>
        /// <param name="_ControllerDate">控制器的日期时间参数</param>
        public WriteCustomTime_Parameter(DateTime _ControllerDate)
        {
            ControllerDate = _ControllerDate;
           
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (ControllerDate.Year < 2000 || ControllerDate.Year > 2099)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
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
            byte[] btData = new byte[7];
            databuf.ReadBytes(btData, 0, 7);
            ControllerDate = TimeUtil.BCDTimeToDate_ssmmHHddMMWWyy(btData);
        }
    }
}