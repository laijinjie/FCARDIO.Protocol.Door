using DoNetDrive.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.USB.OfflinePatrol.OperatedDevice.TriggerBuzzer
{
    /// <summary>
    /// 触发蜂鸣器
    /// </summary>
    public class TriggerBuzzer : Write_Command
    {
        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public TriggerBuzzer(INCommandDetail cd) : base(cd, null)
        {

        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            return true;
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x05, 0x01, 0, null);
        }
    }
}
