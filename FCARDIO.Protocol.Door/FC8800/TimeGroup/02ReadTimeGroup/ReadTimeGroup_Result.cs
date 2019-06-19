using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.TimeGroup
{
    public class ReadTimeGroup_Result : INCommandResult
    {
        public int Count { get; set; }
        public List<WeekTimeGroup> ListWeekTimeGroup { get; set; }

        public ReadTimeGroup_Result()
        {
            ListWeekTimeGroup = new List<WeekTimeGroup>();
        }
        public void Dispose()
        {
            ListWeekTimeGroup.Clear();
            ListWeekTimeGroup = null;
        }

        public void SetBytes(int iTotal, List<IByteBuffer> databufs)
        {
            ListWeekTimeGroup.Clear();
            ListWeekTimeGroup.Capacity = iTotal + 10;
            foreach (IByteBuffer buf in databufs)
            {
                int iCount = databufs.Count;
                for (int i = 0; i < iCount; i++)
                {
                    WeekTimeGroup wtg = new WeekTimeGroup(8);
                    wtg.SetBytes(buf);
                    ListWeekTimeGroup.Add(wtg);
                    break;
                }
                break;
            }
            Count = ListWeekTimeGroup.Count;
        }
    }
}
