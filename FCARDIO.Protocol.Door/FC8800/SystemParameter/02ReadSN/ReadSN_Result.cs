using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.ReadSN
{
    /// <summary>
    /// 获取控制器SN_结果
    /// </summary>
    public class ReadSN_Result : INCommandResult
    {
        /// <summary>
        /// 控制器SN
        /// </summary>
        public string SN;

        /// <summary>
        /// SN的字节数组
        /// </summary>
        public byte[] SNBuf;

        /// <summary>
        /// 初始化类结构，导入返回值结果
        /// </summary>
        /// <param name="sn">控制器的身份SN</param>
        /// <param name="buf">SN的字节数组</param>
        public ReadSN_Result(string sn, byte[] buf)
        {
            this.SN = sn;
            SNBuf = buf;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            SNBuf = null;
        }
    }
}
