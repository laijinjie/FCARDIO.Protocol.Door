﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.ReaderOption
{
    /// <summary>
    /// 控制器4个门的读卡器字节数
    /// </summary>
    public class ReaderOption_Parameter : AbstractParameter
    {
        /// <summary>
        /// 数据长度
        /// </summary>
        private readonly int DataLength = 0x04;
        /// <summary>
        /// 门字节数组
        /// </summary>
        public byte[] Door = null;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public ReaderOption_Parameter() { }

        /// <summary>
        /// 控制器4个门的读卡器字节数初始化实例
        /// </summary>
        /// <param name="_Door">门字节数组</param>
        public ReaderOption_Parameter(byte[] _Door)
        {
            Door = _Door;
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
            if (Door.Length != DataLength)
                throw new ArgumentException("door Length Error!");
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
        /// 对控制器4个门的读卡器字节数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if(databuf.WritableBytes != DataLength)
            {
                throw new ArgumentException("databuf Error!");
            }
           return databuf.WriteBytes(Door);
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
        /// 对控制器4个门的读卡器字节数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (Door == null)
            {
                Door = new byte[DataLength];
            }
            if (databuf.ReadableBytes != DataLength)
            {
                throw new ArgumentException("databuf Error");
            }
            databuf.ReadBytes(Door);
        }
    }
}
