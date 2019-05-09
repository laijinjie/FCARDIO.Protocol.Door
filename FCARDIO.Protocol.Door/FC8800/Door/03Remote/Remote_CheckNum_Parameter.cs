﻿using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.Remote
{
    /// <summary>
    /// 远程开门_带验证码
    /// </summary>
    public class Remote_CheckNum_Parameter : AbstractParameter
    {
        /// <summary>
        /// 数据长度
        /// </summary>
        private readonly int DataLength = 0x05;
        /// <summary>
        /// 门
        /// </summary>
        private DoorDetail<bool> Door = null;
        /// <summary>
        /// 验证码
        /// </summary>
        private byte CheckNum;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public Remote_CheckNum_Parameter() { }

        /// <summary>
        /// 远程开关门参数初始化实例
        /// </summary>
        /// <param name="_Door">门</param>
        /// <param name="_CheckNum">验证码</param>
        public Remote_CheckNum_Parameter(DoorDetail<bool> _Door, byte _CheckNum)
        {
            Door = _Door;
            CheckNum = _CheckNum;
            checkedParameter();
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Door == null)
                throw new ArgumentException("door Is Null!");

            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            Door = null;
        }

        /// <summary>
        /// 对远程开关门参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != DataLength)
            {
                throw new ArgumentException("databuf Error!");
            }
            databuf.WriteByte(CheckNum);
            for (int i = 1; i <= 4; i++)
            {
                databuf.WriteBoolean(Door.GetDoor(i));
            }
            return databuf;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return DataLength;
        }

        /// <summary>
        /// 对远程开关门参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            CheckNum = databuf.ReadByte();
            if (Door == null)
            {
                Door = new DoorDetail<bool> ();
            }
            if (databuf.ReadableBytes != DataLength)
            {
                throw new ArgumentException("databuf Error");
            }
            for (int i = 1; i <= 4; i++)
            {
                Door.SetDoor(i, databuf.ReadBoolean());
            }
        }
    }
}
