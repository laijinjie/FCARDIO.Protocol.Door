﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.RelayOption
{
    public class RelayOption_Parameter : AbstractParameter
    {
        private const int _DataLength = 0x04;
        private byte[] _Relay = null;
        public RelayOption_Parameter() { }
        public RelayOption_Parameter(byte[] relay)
        {
            _Relay = relay;
        }
        public override bool checkedParameter()
        {
            if (_Relay == null)
                throw new ArgumentException("relay Is Null!");
            if (_Relay.Length != _DataLength)
                throw new ArgumentException("relay Length Error!");
            return true;
        }

        public override void Dispose()
        {
            _Relay = null;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error!");
            }
            return databuf.WriteBytes(_Relay);
        }

        public override int GetDataLen()
        {
            return _DataLength;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            if (_Relay == null)
            {
                _Relay = new byte[_DataLength];
            }
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(_Relay);
        }
    }
}
