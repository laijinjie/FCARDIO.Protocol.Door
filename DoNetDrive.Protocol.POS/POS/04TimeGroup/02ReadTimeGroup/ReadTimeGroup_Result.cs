using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800.Data.TimeGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.POS.TimeGroup
{
    public class ReadTimeGroup_Result : Door.Door8800.TimeGroup.ReadTimeGroup_Result
    {
        public WeekTimeGroup WeekTimeGroup { get; set; }

        public byte Index { get; set; }
    }
}
