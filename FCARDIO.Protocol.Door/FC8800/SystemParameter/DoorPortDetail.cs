using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter
{
    /// <summary>
    /// 门端口参数详情_模型
    /// </summary>
    public class DoorPortDetail
    {
        public ushort DoorMax { get; set; }

        public byte[] DoorPort { get; set; }

        public DoorPortDetail(ushort _DoorMax)
        {
            DoorMax = _DoorMax;
            DoorPort = new byte[_DoorMax];
        }
    }
}