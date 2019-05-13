﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;

namespace FCARDIO.Protocol.Door.FC8800.Door.AutoLockedSetting
{
    /// <summary>
    /// 定时锁定门_参数
    /// </summary>
    public class AutoLockedSetting_Parameter : AbstractParameter
    {
        /// <summary>
        /// 门
        /// </summary>
        public byte Door;

        /// <summary>
        /// 功能是否启用
        /// </summary>
        public byte Use;

        /// <summary>
        /// 定时锁定门时段
        /// </summary>
        public WeekTimeGroup weekTimeGroup;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public AutoLockedSetting_Parameter()
        {
            weekTimeGroup = new WeekTimeGroup(8);
        }

        /// <summary>
        /// 定时锁定门参数初始化实例
        /// </summary>
        /// <param name="door">门</param>
        /// <param name="use">功能是否启用</param>
        /// <param name="tg">定时锁定门时段</param>
        public AutoLockedSetting_Parameter(byte door, byte use, WeekTimeGroup tg)
        {
            Door = door;
            Use = use;
            weekTimeGroup = tg;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (weekTimeGroup == null)
                throw new ArgumentException("autoLockedSetting Is Null!");
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            weekTimeGroup = null;
        }

        /// <summary>
        /// 对定时锁定门参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != GetDataLen())
            {
                throw new ArgumentException("databuf Error!");
            }
            databuf.WriteByte(Door);
            databuf.WriteByte(Use);
            weekTimeGroup.GetBytes(databuf);
            return databuf;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0xE2;
        }

        /// <summary>
        /// 对定时锁定门参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (weekTimeGroup == null)
            {
                weekTimeGroup = new WeekTimeGroup(8);
            }
            if (databuf.ReadableBytes != GetDataLen())
            {
                throw new ArgumentException("databuf Error");
            }
            Door = databuf.ReadByte();
            Use = databuf.ReadByte();
            weekTimeGroup.SetBytes(databuf);
        }
    }
}