using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.Alarm
{
    /// <summary>
    /// 解除报警_参数
    /// </summary>
    public class CloseAlarm_Parameter : AbstractParameter
    {
        /// <summary>
        /// 需要解除报警的门，取值范围：1-4
        /// </summary>
        public byte Door;

        /// <summary>
        /// 需要解除的报警类型（0 - 非法卡报警、1 - 门磁报警、2 - 胁迫报警、3 - 开门超时报警、4 - 黑名单报警、5 - 匪警报警、6 - 防盗主机报警、7 - 消防报警、8 - 烟雾报警、9 - 关闭电锁出错报警、10 - 防拆报警、11 - 强制关锁报警、12 - 强制开锁报警）   注：9-12 为一体锁或一体机报警类型
        /// </summary>
        public ushort Alarm;

        public CloseAlarm_Parameter(byte _Door, ushort _Alarm)
        {
            Door = _Door;
            Alarm = _Alarm;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if ((Door != 1 && Door != 2 && Door != 3 && Door != 4 && Door != 255) || (Alarm < 0 || Alarm > 12))
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
            databuf.WriteByte(Door);
            databuf.WriteUnsignedShort(Alarm);

            return databuf;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x03;
        }

        /// <summary>
        /// 解码参数
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            Door = databuf.ReadByte();
            Alarm = databuf.ReadUnsignedShort();
        }
    }
}