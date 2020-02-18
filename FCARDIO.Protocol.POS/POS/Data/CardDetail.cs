using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.POS.Data
{
    /// <summary>
    /// 卡号名单
    /// </summary>
    public class CardDetail : Door.FC8800.TemplateMethod.TemplateData_Base
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public int CardData;

        /// <summary>
        /// 卡类型
        /// </summary>
        public byte CardType;

        /// <summary>
        /// 占位
        /// </summary>
        public int Standby;

        public override void SetBytes(IByteBuffer databuf)
        {
            CardData = databuf.ReadInt();
            CardType = databuf.ReadByte();
            Standby = databuf.ReadMedium();
        }


        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteInt(CardData);
            databuf.WriteByte(CardType);
            databuf.WriteMedium(Standby);
            return databuf;
        }

        /// <summary>
        /// 获取每个添加卡类长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 8;
        }

        public override IByteBuffer GetDeleteBytes(IByteBuffer data)
        {
            throw new NotImplementedException();
        }

        public override int GetDeleteDataLen()
        {
            throw new NotImplementedException();
        }

        public override void SetFailBytes(IByteBuffer databuf)
        {
            CardData = databuf.ReadInt();
        }
    }
}
