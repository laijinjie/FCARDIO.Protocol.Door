using DotNetty.Buffers;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.USB.OfflinePatrol.Data
{
    /// <summary>
    /// 巡更人员信息
    /// </summary>
    public class PatrolEmpl
    {
        /// <summary>
        /// 工号
        /// </summary>
        public ushort PCode;

        /// <summary>
        /// 卡号
        /// 1 - 16777215
        /// </summary>
        public uint CardData;

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name;

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="buf"></param>
        public void SetBytes(IByteBuffer buf)
        {
            PCode = buf.ReadUnsignedShort();
            byte[] bCard = new byte[3];
            buf.ReadBytes(bCard);

            CardData = FCARD.Common.NumUtil.ByteToInt32(bCard, 0, 3);

            byte[] bName = new byte[10];
            buf.ReadBytes(bName);
            Name = Convert.ToString(System.Text.Encoding.Default.GetString(bName));

            //Name = StringUtil.ByteBufToHex(buf, 10);
        }

        /// <summary>
        /// 写入添加的巡更人员 到字节缓冲
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public IByteBuffer GetBytes(IByteBuffer buf)
        {
            buf.WriteUnsignedShort(PCode);

            byte[] b = FCARD.Common.NumUtil.Int24ToByte((int)CardData);
            buf.WriteBytes(b);

            byte[] bName = new byte[10];
            bName = System.Text.Encoding.ASCII.GetBytes(Name.PadRight(10, '0'));
            buf.WriteBytes(bName);
            return buf;
        }

    }
}
