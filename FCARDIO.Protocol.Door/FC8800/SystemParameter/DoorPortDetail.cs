using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter
{
    /// <summary>
    /// 互锁参数信息
    /// </summary>
    public class DoorPortDetail
    {
        /// <summary>
        /// 最大门数
        /// </summary>
        public ushort DoorMax;

        /// <summary>
        /// 门的端口
        /// </summary>
        public byte[] DoorPort;

        /// <summary>
        /// 设置几个门的端口
        /// </summary>
        /// <param name="_DoorMax">最大门数</param>
        public DoorPortDetail(ushort _DoorMax)
        {
            DoorMax = _DoorMax;
            DoorPort = new byte[_DoorMax];
        }
    }
}