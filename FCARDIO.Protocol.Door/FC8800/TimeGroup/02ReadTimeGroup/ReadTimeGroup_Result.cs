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
        public int Count { get; set; }

        /// <summary>
        /// 开门时段集合
        /// </summary>
        public List<WeekTimeGroup> ListWeekTimeGroup;

        public ReadTimeGroup_Result()
        {
            ListWeekTimeGroup = new List<WeekTimeGroup>();
        }
        public void Dispose()
        {
            ListWeekTimeGroup.Clear();
            ListWeekTimeGroup = null;
        }

        /// <summary>
        ///  将 字节流  转换为 开门时段
        /// </summary>
        /// <param name="iTotal"></param>
        /// <param name="databufs"></param>
        public void SetBytes(int iTotal, List<IByteBuffer> databufs)
        {
            ListWeekTimeGroup.Clear();
            ListWeekTimeGroup.Capacity = iTotal + 10;
            //64个IByteBuffer，每个包含组 号2byte+224byte(7*8*4(时分-时分))
            foreach (IByteBuffer buf in databufs)
            {
                //StringUtility.WriteByteBuffer(buf);
                //continue;
                WeekTimeGroup wtg = new WeekTimeGroup(8);
                wtg.SetBytes(buf);
                ListWeekTimeGroup.Add(wtg);

            }
            Count = ListWeekTimeGroup.Count;
        }
    }
}
