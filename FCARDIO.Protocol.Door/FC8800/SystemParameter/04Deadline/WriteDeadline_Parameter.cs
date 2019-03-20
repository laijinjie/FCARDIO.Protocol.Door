using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.Deadline
{
    /// <summary>
    /// 设置设备有效期_参数
    /// </summary>
    public class WriteDeadline_Parameter : AbstractParameter
    {
        public int Deadline;

        public WriteDeadline_Parameter(int _Deadline)
        {
            Deadline = _Deadline;
        }

        public override bool checkedParameter()
        {
            if (Deadline < 0 || Deadline > 65535)
            {
                Deadline = 0;
            }

            return true;
        }

        public override void Dispose()
        {
            return;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf.WriteInt(Deadline);
        }

        public override int GetDataLen()
        {
            return 0x02;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            Deadline = databuf.ReadInt();
        }
    }
}