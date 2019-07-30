namespace FCARDIO.Protocol.Elevator.FC8864.Data.TimeGroup
{
    /// <summary>
    /// 表示一个完整时段，一个时段里包含7天
    /// </summary>
    public class WeekTimeGroup : Protocol.Door.FC8800.Data.TimeGroup.WeekTimeGroup
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="iDaySegmentCount">一天中的时段数量</param>
        public WeekTimeGroup(int iDaySegmentCount) : base(iDaySegmentCount)
        {

        }
    }
}
