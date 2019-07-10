﻿using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.MultiCard
{
    /// <summary>
    /// 
    /// </summary>
    public class MultiCard_Result : INCommandResult
    {
        /// <summary>
        /// 刷卡模式 (1)
        /// </summary>
        public byte Door { get; private set; }

        /// <summary>
        /// 卡集合 (9*N)
        /// </summary>
        public Dictionary<int, List<string>> AListCardData { get; set; }
        public Dictionary<int, List<string>> BListCardData { get; set; }

        /// <summary>
        /// 刷卡模式 (1)
        /// </summary>
        public byte Mode { get; private set; }

        /// <summary>
        /// 防潜回检测 (1)
        /// </summary>
        public byte AntiPassback { get; private set; }

        /// <summary>
        /// 开门验证方式 (1)
        /// </summary>
        public byte VerifyType { get; private set; }

        /// <summary>
        /// A组刷卡数量 (1)
        /// </summary>
        public byte AGroupCount { get; private set; }

        /// <summary>
        /// B组刷卡数量 (1)
        /// </summary>
        public byte BGroupCount { get; private set; }

        public MultiCard_Result()
        {
            AListCardData = new Dictionary<int, List<string>>();
            BListCardData = new Dictionary<int, List<string>>();
        }
        public void Dispose()
        {

        }

        public void GetOpenModeBytes(IByteBuffer databuf)
        {
            Mode = databuf.ReadByte();
            AntiPassback = databuf.ReadByte();
        }

        public void GetOpenVerifyBytes(IByteBuffer databuf)
        {
            VerifyType = databuf.ReadByte();
            AGroupCount = databuf.ReadByte();
            BGroupCount = databuf.ReadByte();
        }

        public void GetListCardDataBytes(byte grouptype, byte groupnum,IByteBuffer databuf)
        {
            var l = databuf.Capacity - databuf.ReaderIndex;
            if (grouptype == 0)
            {
                var index = databuf.ReadByte();
                while (databuf.ReaderIndex < databuf.Capacity)
                {
                    byte[] array = new byte[9];
                    string carddata = FCARDIO.Protocol.Util.StringUtil.ByteBufToHex(databuf, 9);
                    carddata = Convert.ToUInt64(carddata, 16).ToString();
                    if (AListCardData.ContainsKey(groupnum))
                    {
                        AListCardData[groupnum].Add(carddata);
                    }
                    else
                    {
                        List<string> list = new List<string>() { carddata };
                        AListCardData.Add(groupnum, list);
                    }
                    //AListCardData.Add(carddata);
                }
            }
            else
            {
                var index = databuf.ReadByte();
                while (databuf.ReaderIndex < databuf.Capacity)
                {
                    byte[] array = new byte[9];
                    string carddata = FCARDIO.Protocol.Util.StringUtil.ByteBufToHex(databuf, 9);
                    carddata = Convert.ToInt32(carddata, 16).ToString();
                    if (BListCardData.ContainsKey(groupnum))
                    {
                        BListCardData[groupnum].Add(carddata);
                    }
                    else
                    {
                        List<string> list = new List<string>() { carddata };
                        BListCardData.Add(groupnum, list);
                    }
                    //BListCardData.Add(carddata);
                }
            }
        }

    }
}
