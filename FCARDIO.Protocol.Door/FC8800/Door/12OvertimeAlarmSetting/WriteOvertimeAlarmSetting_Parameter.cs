using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.OvertimeAlarmSetting
{
    public class WriteOvertimeAlarmSetting_Parameter
        : AbstractParameter
    {
        /// <summary>
        /// 门号
        /// 门端口在控制板中的索引号，取值：1-4
        /// </summary>
        public int DoorNum;

        /// <summary>
        /// 是否启用胁迫报警功能
        /// </summary>
        public bool Use;

        /// <summary>
        /// 超时时间，指门磁一直打开的时间。
        /// 取值范围：0-65535,0表示关闭；单位秒；
        /// </summary>
        public int Overtime;

        /// <summary>
        /// 在开门超时后，是否报警输出
        /// </summary>
        public bool Alarm;
        
        public WriteOvertimeAlarmSetting_Parameter()
        {
        }
        
        /// <summary>
        /// 创建结构,并传入门号和是否开启此功能
        /// </summary>
        /// <param name="door">门号</param>
        /// <param name="use">是否启动此功能</param>
        /// <param name="Overtime">超出时间</param>
        /// <param name="Alarm">超出后,是否开启此功能</param>
        public WriteOvertimeAlarmSetting_Parameter(byte door, bool use, byte overtime, bool alarm)
        {
            DoorNum = door;
            Use = use;
            Overtime = overtime;
            Alarm = alarm;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (DoorNum > 4)
                throw new ArithmeticException("OvertimeAlarmSetting Is Max");
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
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != 2)
            {
                throw new ArgumentException("databuf Error!");
            }
            databuf.WriteByte(DoorNum);
            databuf.WriteBoolean(Use);
            databuf.WriteByte(Overtime);
            databuf.WriteBoolean(Alarm);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 4;
        }
    
        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            DoorNum = databuf.ReadByte();
            Use = databuf.ReadBoolean();
            Overtime = databuf.ReadByte();
            Alarm = databuf.ReadBoolean();
        }
    }
}
