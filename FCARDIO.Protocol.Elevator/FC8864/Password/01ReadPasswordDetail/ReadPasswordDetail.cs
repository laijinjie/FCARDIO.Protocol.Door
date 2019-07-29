using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Elevator.FC8864.Password
{
    /// <summary>
    /// 读取密码容量信息
    /// </summary>
    public class ReadPasswordDetail : Read_Command
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ReadPasswordDetail(INCommandDetail cd) : base(cd, null)
        {

        }

        /// <summary>
        /// 处理返回通知
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x04))
            {
                var buf = oPck.CmdData;
                ReadPasswordDetail_Result rst = new ReadPasswordDetail_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x45, 1);
        }
    }
}
