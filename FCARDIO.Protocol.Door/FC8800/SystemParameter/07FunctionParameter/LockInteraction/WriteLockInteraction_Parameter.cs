using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置互锁参数_参数
    /// </summary>
    public class WriteLockInteraction_Parameter : AbstractParameter
    {
        public DoorPortDetail DoorPort;

        public WriteLockInteraction_Parameter(DoorPortDetail _DoorPort)
        {
            DoorPort = _DoorPort;
        }

        public override bool checkedParameter()
        {
            if (DoorPort == null)
            {
                return false;
            }

            return true;
        }

        public override void Dispose()
        {
            DoorPort = null;

            return;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf.WriteBytes(DoorPort.DoorPort);
        }

        public override int GetDataLen()
        {
            return 0x04;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            DoorPort = new DoorPortDetail(databuf.ReadShort());
        }
    }
}