﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.DoorWorkSetting
{
    public class WriteDoorWorkSetting_Parameter : AbstractParameter
    {
        private const int _DataLength = 1;
        private byte[] _DoorWorkSetting_ = null;
        public WriteDoorWorkSetting_Parameter() { }
        public WriteDoorWorkSetting_Parameter(byte[] doorWorkSetting)
        {
            _DoorWorkSetting_ = doorWorkSetting;
        }
        public override bool checkedParameter()
        {
            if (_DoorWorkSetting_ == null)
                throw new ArgumentException("doorWorkSetting Is Null!");
            if (_DoorWorkSetting_.Length != _DataLength)
                throw new ArgumentException("doorWorkSetting Length Error!");
            return true;
        }

        public override void Dispose()
        {
            _DoorWorkSetting_ = null;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error!");
            }
            return databuf.WriteBytes(_DoorWorkSetting_);
        }

        public override int GetDataLen()
        {
            return _DataLength;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            if (_DoorWorkSetting_ == null)
            {
                _DoorWorkSetting_ = new byte[_DataLength];
            }
            if (databuf.ReadableBytes != _DataLength)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(_DoorWorkSetting_);
        }
    }
}
