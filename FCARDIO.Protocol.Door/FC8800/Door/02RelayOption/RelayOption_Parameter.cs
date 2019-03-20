using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.RelayOption
{
    public class RelayOption_Parameter : AbstractParameter
    {
        private byte[] _Relay = null;
        public RelayOption_Parameter() { }
        public RelayOption_Parameter(byte[] relay)
        {

        }
        public override bool checkedParameter()
        {
            if (_Relay == null)
                throw new ArgumentException("relay Is Null!");
            if (_Relay.Length != 4)
                throw new ArgumentException("relay Length Error!");
            return true;
        }

        public override void Dispose()
        {
            _Relay = null;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != 4)
            {
                throw new ArgumentException("databuf Error!");
            }
            return databuf.WriteBytes(_Relay);
        }

        public override int GetDataLen()
        {
            return 4;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            if (_Relay == null)
            {
                _Relay = new byte[4];
            }
            if (databuf.ReadableBytes != 4)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(_Relay);
        }
    }
}
