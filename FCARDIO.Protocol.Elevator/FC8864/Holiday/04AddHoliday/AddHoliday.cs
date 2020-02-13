using FCARDIO.Core.Command;
using System;

namespace FCARDIO.Protocol.Elevator.FC8864.Holiday
{
    /// <summary>
    /// 添加节假日到控制版
    /// </summary>
    public class AddHoliday : Protocol.Door.FC8800.Holiday.AddHoliday
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public AddHoliday(INCommandDetail cd, AddHoliday_Parameter par) : base(cd, par){
            CmdType = 0x44;
            CmdIndex = 0x04;
        }
    }
}
