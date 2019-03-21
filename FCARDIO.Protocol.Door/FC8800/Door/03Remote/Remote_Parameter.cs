using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.Remote
{
    public class Remote_Parameter
        : AbstractParameter
    {
        private const int _DataLength = 0x04;
        private byte[] _Door = null;
        public Remote_Parameter() { }
        public Remote_Parameter(byte[] door)
        {
            _Door = door;
        }
        public override bool checkedParameter()
        {
            if (_Door == null)
                throw new ArgumentException("door Is Null!");
            if (_Door.Length != _DataLength)
                throw new ArgumentException("door Length Error!");
            return true;
        }

        public override void Dispose()
        {
            _Door = null;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error!");
            }
            return databuf.WriteBytes(_Door);
        }

        public override int GetDataLen()
        {
            return _DataLength;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            if (_Door == null)
            {
                _Door = new byte[_DataLength];
            }
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(_Door);
        }
    }
}
