using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.Test.Model
{
    public class DoorUI
    {
        public bool Selected { get; set; }
        public int Index { get; set; }
        public string OutputFormat { get; set; }

        public int Time { get; set; }
    }
}
