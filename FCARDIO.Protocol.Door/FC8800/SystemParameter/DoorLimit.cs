using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter
{
    /// <summary>
    /// 门内人数限制_模型
    /// </summary>
    public class DoorLimit
    {
        public uint GlobalLimit { get; set; }

        public uint[] DoorLimitArray { get; set; }

        public uint GlobalEnter { get; set; }

        public uint[] DoorEnterArray { get; set; }

        public DoorLimit()
        {
            DoorLimitArray = new uint[4];
            DoorEnterArray = new uint[4];
        }
    }
}