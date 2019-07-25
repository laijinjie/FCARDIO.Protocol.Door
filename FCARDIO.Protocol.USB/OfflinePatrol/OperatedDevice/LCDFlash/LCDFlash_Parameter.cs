﻿using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.USB.OfflinePatrol.OperatedDevice.LCDFlash
{
    /// <summary>
    /// LCD屏幕刷屏 参数
    /// </summary>
    public class LCDFlash_Parameter : AbstractParameter
    {
        /// <summary>
        /// 是否开启
        /// 128 - 开启 ， 8 - 关闭
        /// </summary>
        public byte Code;

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="code">是否开启 128 - 开启 ， 8 - 关闭</param>
        public LCDFlash_Parameter(byte code)
        {
            Code = code;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Code != 128 && Code != 8)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteByte(Code);

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

        }
    }
}
