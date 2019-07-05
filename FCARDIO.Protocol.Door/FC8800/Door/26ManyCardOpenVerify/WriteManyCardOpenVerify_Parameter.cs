using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ManyCardOpenVerify
{
    public class WriteManyCardOpenVerify_Parameter : AbstractParameter
    {
        /// <summary>
        /// 门号
        /// 门端口在控制板中的索引号，取值：1-4
        /// </summary>
        public int DoorNum { get; set; }

        /// <summary>
        /// 开门验证方式 (1)
        /// </summary>
        public byte VerifyType { get; set; }

        /// <summary>
        /// A组刷卡数量 (1)
        /// </summary>
        public byte AGroupCount { get; set; }

        /// <summary>
        /// B组刷卡数量 (1)
        /// </summary>
        public byte BGroupCount { get; set; }

        public WriteManyCardOpenVerify_Parameter()
        {

        }

        public WriteManyCardOpenVerify_Parameter(byte door, byte verifytype, byte agroupcount, byte bgroupcount)
        {
            DoorNum = door;
            VerifyType = verifytype;
            AGroupCount = agroupcount;
            BGroupCount = bgroupcount;
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
            if (databuf.WritableBytes != 4)
            {
                throw new ArgumentException("door Error!");
            }
            databuf.WriteByte(DoorNum);
            databuf.WriteByte(VerifyType);
            databuf.WriteByte(AGroupCount);
            databuf.WriteByte(BGroupCount);
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
            VerifyType = databuf.ReadByte();
            AGroupCount = databuf.ReadByte();
            BGroupCount = databuf.ReadByte();
        }
    }
}