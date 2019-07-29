using FCARDIO.Core.Command;
using FCARDIO.Protocol.Elevator.FC8864.Data.TimeGroup;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Elevator.FC8864.TimeGroup
{
    /// <summary>
    /// 读取所有开门时段结果
    /// </summary>
    public class ReadTimeGroup_Result : INCommandResult
    {
        /// <summary>
        /// 返回的总数量
        /// </summary>
        public int Count;

        /// <summary>
        /// 开门时段集合
        /// </summary>
        public List<WeekTimeGroup> ListWeekTimeGroup;

        /// <summary>
        /// 初始化参数
        /// </summary>
        public ReadTimeGroup_Result()
        {
            ListWeekTimeGroup = new List<WeekTimeGroup>();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            ListWeekTimeGroup.Clear();
            ListWeekTimeGroup = null;
        }

        
    }
}
