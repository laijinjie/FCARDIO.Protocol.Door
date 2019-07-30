using DotNetty.Buffers;
using System;

namespace FCARDIO.Protocol.Elevator.FC8864.Data.TimeGroup
{
    /// <summary>
    /// 表示一天的时段 ,一天可以包含多个时段
    /// </summary>
    public class DayTimeGroup : FCARDIO.Protocol.Door.FC8800.Data.TimeGroup.DayTimeGroup
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="SegmentCount">一天中的时段数量</param>
        public DayTimeGroup(int SegmentCount) : base(SegmentCount)
        {

        }
    }
}
