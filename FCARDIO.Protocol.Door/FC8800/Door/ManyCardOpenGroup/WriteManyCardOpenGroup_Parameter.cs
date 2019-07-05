using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ManyCardOpenGroup
{
    public class WriteManyCardOpenGroup_Parameter : AbstractParameter
    {
        /// <summary>
        /// 卡集合 (9*N)
        /// </summary>
        public List<string> ListCardData { get; set; }

        /// <summary>
        /// 组类别 (1)
        /// </summary>
        public byte GroupType { get; set; }

        /// <summary>
        /// 组号(1)
        /// </summary>
        public byte GroupNum { get; set; }


        public WriteManyCardOpenGroup_Parameter()
        {

        }

        public WriteManyCardOpenGroup_Parameter(List<string> list, byte grouptype, byte groupnum)
        {
            ListCardData = list;
            GroupType = grouptype;
            GroupNum = groupnum;
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
            databuf.WriteByte(GroupType);
            databuf.WriteByte(GroupNum);
            databuf.WriteByte(ListCardData.Count);
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
            GroupType = databuf.ReadByte();
            GroupNum = databuf.ReadByte();
        }
    }
}
