using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Time
{
    /// <summary>
    /// 从控制器中读取控制器时间_结果
    /// </summary>
    public class ReadTime_Result : INCommandResult
    {
        /// <summary>
        /// 控制器的日期时间
        /// </summary>
        public DateTime ControllerDate;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            return;
        }

        /// <summary>
        /// 对控制器的日期时间参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public void SetBytes(IByteBuffer databuf)
        {
            //控制器的日期时间
            byte[] btData = new byte[7];
            databuf.ReadBytes(btData, 0, 7);
            ControllerDate = TimeUtil.BCDTimeToDate_ssmmHHddMMWWyy(btData);
        }
    }
}