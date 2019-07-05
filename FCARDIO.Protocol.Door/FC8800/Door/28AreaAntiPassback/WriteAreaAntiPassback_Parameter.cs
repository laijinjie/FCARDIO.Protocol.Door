using DotNetty.Buffers;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.AreaAntiPassback
{
    /// <summary>
    /// 设置区域防潜回
    /// </summary>
    public class WriteAreaAntiPassback_Parameter : AbstractParameter
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
        public string SN { get; set; }

        /// <summary>
        /// 主机IP端口 (4)
        /// </summary>
        public byte[] IP { get; set; }

        /// <summary>
        /// 主机IP端口 (2)
        /// </summary>
        public short Port { get; set; }

        public WriteAreaAntiPassback_Parameter()
        {
            IP = new byte[] { (byte)255, (byte)255, (byte)255, (byte)255 };
        }

        public WriteAreaAntiPassback_Parameter(byte door, bool use,bool type, string sn, byte[] ip, short port)
        {
            DoorNum = door;
            Use = use;
            Type = type;
            SN = sn;
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
            SN = SN.PadLeft(16, '0');
            byte[] array = new byte[16];
            array = System.Text.Encoding.ASCII.GetBytes(SN);
            databuf.WriteBytes(array);
            //SN = StringUtil.FillHexString(SN, 32, "0", false);
            //StringUtil.HextoByteBuf(SN, databuf);
            databuf.WriteBytes(IP);
            databuf.WriteShort(Port);
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
            byte[] b = new byte[16];
            databuf.ReadBytes(b);
            SN = Convert.ToString(System.Text.Encoding.ASCII.GetString(b));
            //SN = StringUtil.ByteBufToHex(databuf, 16);
            databuf.ReadBytes(IP, 0, 4);
            Port = databuf.ReadShort();
        }
    }
}
