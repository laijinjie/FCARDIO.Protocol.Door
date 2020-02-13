using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Elevator.FC8864.TimeGroup
{
    /// <summary>
    /// 添加开门时段
    /// </summary>
    public class AddTimeGroup : Protocol.Door.FC8800.TimeGroup.AddTimeGroup
    {
       

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd">命令详情</param>
        /// <param name="par">命令逻辑所需要的命令参数 </param>
        public AddTimeGroup(INCommandDetail cd, AddTimeGroup_Parameter par) : base(cd, par) {
            mPar = par;
            CmdType = 0x46;
            CmdIndex = 0x03;
        }

        
    }
}
