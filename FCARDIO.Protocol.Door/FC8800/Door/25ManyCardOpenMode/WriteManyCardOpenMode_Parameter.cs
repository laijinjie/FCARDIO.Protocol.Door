using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ManyCardOpenMode
{
    /// <summary>
    /// 多卡开门检测模式参数
    /// </summary>
    public class WriteManyCardOpenMode_Parameter : AbstractParameter
    {
        /// <summary>
        /// 门号
        /// 门端口在控制板中的索引号，取值：1-4
        /// </summary>
        public int DoorNum { get; set; }

        /// <summary>
        /// 刷卡模式 (1)
        /// </summary>
        public byte Mode { get; set; }

        /// <summary>
        /// 防潜回检测 (1)
        /// </summary>
        public byte AntiPassback { get; set; }

        public WriteManyCardOpenMode_Parameter()
        {
            
        }

        public WriteManyCardOpenMode_Parameter(byte door, byte mode, byte antiPassback)
        {
            DoorNum = door;
            Mode = mode;
            AntiPassback = antiPassback;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
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
            if (databuf.WritableBytes != 3)
            {
                throw new ArgumentException("door Error!");
            }
            databuf.WriteByte(DoorNum);
            databuf.WriteByte(Mode);
            databuf.WriteByte(AntiPassback);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 3;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            DoorNum = databuf.ReadByte();
            Mode = databuf.ReadByte();
            AntiPassback = databuf.ReadByte();
        }
    }
}
