using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadPasswordDetail_Result : INCommandResult
    {
        /// <summary>
        /// 控制器中存储的密码详情
        /// </summary>
        public PasswordDetail Detail;

        /// <summary>
        /// 初始化，构造一个空的 HolidayDBDetail 详情实例
        /// </summary>
        public ReadPasswordDetail_Result()
        {
            Detail = new PasswordDetail();
        }

        /// <summary>
        /// 将字节缓冲区反序列化到实例
        /// </summary>
        /// <param name="databuf"></param>
        public void SetBytes(IByteBuffer databuf)
        {
            Detail.SetBytes(databuf);
        }

        /// <summary>
        /// 释放使用的资源
        /// </summary>
        public void Dispose()
        {
            Detail = null;
        }
    }
}
