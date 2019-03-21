using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.AutoLockedSetting
{
    public class AutoLockedSetting_Parameter
        : AbstractParameter
    {
        private const int _DataLength = 0x05;
        private byte[] _AutoLock = null;
        public AutoLockedSetting_Parameter() { }
        public AutoLockedSetting_Parameter(byte[] autoLock)
        {
            _AutoLock = autoLock;
        }
        public override bool checkedParameter()
        {
            if (_AutoLock == null)
                throw new ArgumentException("autoLock Is Null!");
            if (_AutoLock.Length != _DataLength)
                throw new ArgumentException("autoLock Length Error!");
            return true;
        }

        public override void Dispose()
        {
            _AutoLock = null;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error!");
            }
            return databuf.WriteBytes(_AutoLock);
        }

        public override int GetDataLen()
        {
            return _DataLength;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            if (_AutoLock == null)
            {
                _AutoLock = new byte[_DataLength];
            }
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(_AutoLock);
        }
    }
}
