using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 门端口参数_模型
    /// </summary>
    public class DoorPortDetail
    {
        public short DoorMax { get; set; }

        public byte[] DoorPort { get; set; }

        public DoorPortDetail(short _DoorMax)
        {
            DoorMax = _DoorMax;
            DoorPort = new byte[_DoorMax];
        }
    }
}