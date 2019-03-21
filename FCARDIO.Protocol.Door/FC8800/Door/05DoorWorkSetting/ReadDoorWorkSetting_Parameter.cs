using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.DoorWorkSetting
{
    public class ReadDoorWorkSetting_Parameter : AbstractParameter
    {
        private const int _DataLength = 0xE5;
        private byte[] _DoorWorkSetting = null;
        public ReadDoorWorkSetting_Parameter() { }
        public ReadDoorWorkSetting_Parameter(byte[] doorWorkSetting)
        {
            _DoorWorkSetting = doorWorkSetting;
        }
        public override bool checkedParameter()
        {
            if (_DoorWorkSetting == null)
                throw new ArgumentException("doorWorkSetting Is Null!");
            if (_DoorWorkSetting.Length != _DataLength)
                throw new ArgumentException("doorWorkSetting Length Error!");
            return true;
        }

        public override void Dispose()
        {
            _DoorWorkSetting = null;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error!");
            }
            return databuf.WriteBytes(_DoorWorkSetting);
        }

        public override int GetDataLen()
        {
            return _DataLength;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            if (_DoorWorkSetting == null)
            {
                _DoorWorkSetting = new byte[_DataLength];
            }
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(_DoorWorkSetting);
        }
    }
}
