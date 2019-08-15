﻿using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.USB.CardReader.ICCard.SearchCard
{
    /// <summary>
    /// 寻卡 返回结果
    /// </summary>
    public class SearchCard_Result : INCommandResult
    {
        /// <summary>
        /// 寻卡是否成功
        /// </summary>
        public bool IsSuccess;

        /// <summary>
        /// 卡片类型
        /// 1 - MF1 IC卡 S50
        /// 2 - NFC标签卡
        /// 3 - NFC手机
        /// 4 - 身份证
        /// 5 - CPU IC卡 S50
        /// 6 - CPU卡
        /// 7 - MF1 IC卡 S70
        /// 8 - CPU IC卡 S70
        /// 9 - ID卡
        /// </summary>
        public int Type;

        /// <summary>
        /// 卡号长度
        /// </summary>
        public int Length;

        /// <summary>
        /// 卡号
        /// </summary>
        public UInt64 CardData;

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public void SetBytes(IByteBuffer databuf)
        {
            IsSuccess = databuf.ReadBoolean();
            if (IsSuccess)
            {
                Type = databuf.ReadByte();
                Length = databuf.ReadByte();
                switch (Length)
                {
                    case 1:
                        CardData = databuf.ReadByte();
                        break;
                    case 2:
                        CardData = databuf.ReadUnsignedShort();
                        break;
                    case 3:
                        CardData = (UInt64)databuf.ReadUnsignedMedium();
                        break;
                    case 4:
                        CardData = databuf.ReadUnsignedInt();
                        break;
                    case 8:
                        CardData = (UInt64)databuf.ReadLong();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {

        }
    }
}
