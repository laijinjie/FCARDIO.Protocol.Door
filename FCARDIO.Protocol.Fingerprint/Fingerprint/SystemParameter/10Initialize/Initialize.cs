using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800;

namespace FCARDIO.Protocol.Fingerprint.SystemParameter.Initialize
{
    /// <summary>
    /// 初始化设备
    /// </summary>
    public class Initialize : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd"></param>
        public Initialize(INCommandDetail cd) : base(cd, null)
        {

        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x01, 0x0F, 0);
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

        
    }
}
