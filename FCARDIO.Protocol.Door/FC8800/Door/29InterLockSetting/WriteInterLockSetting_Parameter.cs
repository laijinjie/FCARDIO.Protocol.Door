using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.InterLockSetting
{
    /// <summary>
    /// 
    /// </summary>
    public class WriteInterLockSetting_Parameter : AbstractParameter
    {
        /// <summary>
        /// 门号
        /// 门端口在控制板中的索引号，取值：1-4
        /// </summary>
        public int DoorNum { get; set; }

        /// <summary>
        /// 功能开启 (1)
        /// </summary>
        public bool Use { get; set; }

        /// <summary>
        /// 从属类别 (1)
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        /// 主机SN  (16)
        /// </summary>
        public int AreaCode { get; set; }

        /// <summary>
        /// 从机序号
        /// </summary>
        public byte Num { get; set; }
        /// <summary>
        /// 主机IP端口 (4)
        /// </summary>
        public byte[] IP { get; set; }

        /// <summary>
        /// 主机IP端口 (2)
        /// </summary>
        public ushort Port { get; set; }

        public WriteInterLockSetting_Parameter()
        {
            IP = new byte[] { (byte)255, (byte)255, (byte)255, (byte)255 };
        }

        public WriteInterLockSetting_Parameter(byte door, bool use, bool type, int areacode,byte num, byte[] ip, ushort port)
        {
            DoorNum = door;
            Use = use;
            Type = type;
            AreaCode = areacode;
            Num = num;
            IP = ip;
            Port = port;
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
            if (databuf.WritableBytes != 25)
            {
                throw new ArgumentException("door Error!");
            }
            databuf.WriteByte(DoorNum);
            databuf.WriteBoolean(Use);
            databuf.WriteBoolean(Type);

            databuf.WriteInt(AreaCode);
            databuf.WriteByte(Num);
            databuf.WriteBytes(IP);
            databuf.WriteShort(Port);
            byte[] b = new byte[11];
            for (int i = 0; i < b.Length; i++)
            {
                b[i] = 0;
            }
            databuf.WriteBytes(b);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 25;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            DoorNum = databuf.ReadByte();
            Use = databuf.ReadBoolean();
            Type = databuf.ReadBoolean();
            AreaCode = databuf.ReadInt();
            Num = databuf.ReadByte();
            databuf.ReadBytes(IP, 0, 4);
            Port = databuf.ReadUnsignedShort();
        }
    }
}
