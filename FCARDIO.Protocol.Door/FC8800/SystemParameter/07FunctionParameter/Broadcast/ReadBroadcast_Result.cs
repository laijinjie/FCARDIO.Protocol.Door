using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取语音播报语音段开关_结果
    /// </summary>
    public class ReadBroadcast_Result : INCommandResult
    {
        public BroadcastDetail Broadcast;

        public ReadBroadcast_Result(byte[] data)
        {
            Broadcast = new BroadcastDetail(data);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Broadcast = null;

            return;
        }
    }
}