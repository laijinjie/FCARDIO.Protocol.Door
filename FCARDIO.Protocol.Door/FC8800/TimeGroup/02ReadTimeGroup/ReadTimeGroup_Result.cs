using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;
using FCARDIO.Protocol.Door.FC8800.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.TimeGroup
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
