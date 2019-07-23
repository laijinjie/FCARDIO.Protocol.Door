﻿using DotNetty.Buffers;
using System;

namespace FCARDIO.Protocol.USB.OfflinePatrol.SystemParameter.RecordStorageMode
{
    /// <summary>
    /// 写入记录存储方式 参数
    /// </summary>
    public class WriteRecordStorageMode_Parameter : AbstractParameter
    {
        /// <summary>
        /// 记录存储方式
        /// 00表示记录满循环，01表示记录满不循环
        /// </summary>
        public byte Mode;

        /// <summary>
        /// 提供给继承类
        /// </summary>
        public WriteRecordStorageMode_Parameter()
        {

        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="mode"></param>
        public WriteRecordStorageMode_Parameter(byte mode)
        {
            Mode = mode;
        }

        /// <summary>
        /// 检查参数 0或1
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Mode != 0 && Mode != 1)
                throw new ArgumentException("Mode Error!");
            return true;
        }



        /// <summary>
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteByte(Mode);
            return databuf;
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 1;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            Mode = databuf.ReadByte();
        }
    }
}
