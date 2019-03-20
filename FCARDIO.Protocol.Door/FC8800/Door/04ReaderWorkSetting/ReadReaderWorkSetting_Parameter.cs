using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.ReaderWorkSetting
{
    public class ReadReaderWorkSetting_Parameter : AbstractParameter
    {
        private const int _DataLength = 1;
        private byte[] _ReaderWorkSetting = null;
        public ReadReaderWorkSetting_Parameter() { }
        public ReadReaderWorkSetting_Parameter(byte[] readerWorkSetting)
        {
            _ReaderWorkSetting = readerWorkSetting;
        }
        public override bool checkedParameter()
        {
            if (_ReaderWorkSetting == null)
                throw new ArgumentException("readerWorkSetting Is Null!");
            if (_ReaderWorkSetting.Length != _DataLength)
                throw new ArgumentException("readerWorkSetting Length Error!");
            return true;
        }

        public override void Dispose()
        {
            _ReaderWorkSetting = null;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error!");
            }
            return databuf.WriteBytes(_ReaderWorkSetting);
        }

        public override int GetDataLen()
        {
            return _DataLength;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            if (_ReaderWorkSetting == null)
            {
                _ReaderWorkSetting = new byte[_DataLength];
            }
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(_ReaderWorkSetting);
        }
    }
}
