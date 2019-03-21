using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 门内人数限制_模型
    /// </summary>
    public class DoorLimit
    {
        public UInt64 GlobalLimit { get; set; }

        public long[] DoorLimitArray { get; set; }

        public long GlobalEnter { get; set; }

        public long[] DoorEnterArray { get; set; }

        public DoorLimit()
        {
            DoorLimitArray = new long[4];
            DoorEnterArray = new long[4];
        }
    }
}