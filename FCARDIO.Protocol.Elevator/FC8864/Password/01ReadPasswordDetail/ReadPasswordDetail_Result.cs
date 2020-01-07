﻿using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.Password
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadPasswordDetail_Result : INCommandResult
    {
        /// <summary>
        /// 排序数据区容量上限
        /// </summary>
        public short DataSize;

        /// <summary>
        /// 排序数据区已使用数量
        /// </summary>
        public short PasswordSize;

        /// <summary>
        /// 初始化，构造一个空的 HolidayDBDetail 详情实例
        /// </summary>
        public ReadPasswordDetail_Result()
        {

        }

        /// <summary>
        /// 将字节缓冲区反序列化到实例
        /// </summary>
        /// <param name="buf"></param>
        public void SetBytes(IByteBuffer buf)
        {
            DataSize = buf.ReadShort();
            PasswordSize = buf.ReadShort();
        }

        /// <summary>
        /// 释放使用的资源
        /// </summary>
        public void Dispose()
        {
            return;
        }
    }
}