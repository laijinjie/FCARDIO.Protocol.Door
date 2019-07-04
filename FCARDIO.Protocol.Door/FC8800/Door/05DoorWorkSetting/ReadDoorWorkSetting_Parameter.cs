﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;

namespace FCARDIO.Protocol.Door.FC8800.Door.DoorWorkSetting
{
    /// <summary>
    /// 门工作方式_参数
    /// </summary>
    public class ReadDoorWorkSetting_Parameter : AbstractParameter
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
        /// 开门方式
        /// </summary>
        public byte OpenDoorWay;

        /// <summary>
        /// 门常开触发模式
        /// </summary>
        public byte DoorTriggerMode;

        /// <summary>
        /// 保留值
        /// </summary>
        public byte RetainValue;

        /// <summary>
        /// 门工作方式时段
        /// </summary>
        public WeekTimeGroup weekTimeGroup;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public ReadDoorWorkSetting_Parameter() {
            weekTimeGroup = new WeekTimeGroup(8);
        }

        /// <summary>
        /// 门工作方式参数初始化实例
        /// </summary>
        /// <param name="door">门</param>
        /// <param name="use">功能是否启用</param>
        /// <param name="openDoorWay">开门方式</param>
        /// <param name="doorTriggerMode">门常开触发模式</param>
        /// <param name="retainValue">保留值</param>
        /// <param name="tg">门工作方式时段</param>
        public ReadDoorWorkSetting_Parameter(byte door, byte use, byte openDoorWay, byte doorTriggerMode, byte retainValue, WeekTimeGroup tg)
        {
            Door = door;
            Use = use;
            OpenDoorWay = openDoorWay;
            DoorTriggerMode = doorTriggerMode;
            RetainValue = retainValue;
            weekTimeGroup = tg;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (weekTimeGroup == null)
                throw new ArgumentException("doorWorkSetting Is Null!");
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
        /// 对门工作方式参数进行编码
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
            databuf.WriteByte(OpenDoorWay);
            databuf.WriteByte(DoorTriggerMode);
            databuf.WriteByte(RetainValue);
            weekTimeGroup.GetBytes(databuf);

            return databuf;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0xE5;
        }

        /// <summary>
        /// 对门工作方式参数进行解码
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
            OpenDoorWay = databuf.ReadByte();
            DoorTriggerMode = databuf.ReadByte();
            RetainValue = databuf.ReadByte();
            weekTimeGroup.ReadDoorWorkSetBytes(databuf);
        }
    }
}
