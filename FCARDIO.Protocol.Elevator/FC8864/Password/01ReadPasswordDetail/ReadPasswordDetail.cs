using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.Password
{
    /// <summary>
    /// 读取密码容量信息
    /// </summary>
    public class ReadPasswordDetail : Protocol.Door.FC8800.Password.ReadPasswordDetail
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ReadPasswordDetail(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x45;
            CheckResponseCmdType = 0x25;
        }

    }
}
