﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.ExploreLockMode
{
    /// <summary>
    /// 设置防探测功能开关_参数
    /// </summary>
    public class WriteExploreLockMode_Parameter : AbstractParameter
    {
        /// <summary>
        /// 防探测功能开关（0 - 关、1 - 开）
        /// </summary>
        public byte Use;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public WriteExploreLockMode_Parameter() { }

        /// <summary>
        /// 使用防探测功能开关参数初始化实例
        /// </summary>
        /// <param name="_Use">防探测功能开关参数</param>
        public WriteExploreLockMode_Parameter(byte _Use)
        {
            Use = _Use;
            if (!checkedParameter())
            {
                throw new ArgumentException("Use Error");
            }
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Use != 0 && Use != 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
        /// 对防探测功能开关参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf.WriteByte(Use);
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x01;
        }

        /// <summary>
        /// 对防探测功能开关参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            Use = databuf.ReadByte();
        }
    }
}