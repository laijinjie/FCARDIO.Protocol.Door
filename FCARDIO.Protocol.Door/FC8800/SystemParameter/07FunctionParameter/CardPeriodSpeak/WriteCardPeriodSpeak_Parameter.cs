﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置卡片到期提示_参数
    /// </summary>
    public class WriteCardPeriodSpeak_Parameter : AbstractParameter
    {
        /// <summary>
        /// 卡片到期提示（是否启用，0不启用，1启用）
        /// </summary>
        public byte Use;

        public WriteCardPeriodSpeak_Parameter(byte _Use)
        {
            Use = _Use;
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
        /// 编码参数
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
        /// 解码参数
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            Use = databuf.ReadByte();
        }
    }
}