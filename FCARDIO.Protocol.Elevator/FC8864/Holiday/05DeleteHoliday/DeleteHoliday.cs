using FCARDIO.Core.Command;
using System;

namespace FCARDIO.Protocol.Elevator.FC8864.Holiday
{
    /// <summary>
    /// 从控制器删除节假日
    /// </summary>
    public class DeleteHoliday : FCARDIO.Protocol.Door.FC8800.Holiday.DeleteHoliday
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public DeleteHoliday(INCommandDetail cd, DeleteHoliday_Parameter par) : base(cd, par)
        {
            CmdType = 0x44;
            CmdIndex = 0x04;
        }

    }
}
