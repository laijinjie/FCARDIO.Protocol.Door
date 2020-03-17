using DotNetty.Buffers;
using DoNetDrive.Protocol.Door.Door8800.TemplateMethod;

namespace DoNetDrive.Protocol.POS.Data
{
    /// <summary>
    /// 卡号名单
    /// </summary>
    public class CardDetail : TemplateData_Base
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

        public override IByteBuffer GetDeleteBytes(IByteBuffer databuf)
        {
            databuf.WriteByte(CardType);
            return databuf;
        }

        public override int GetDeleteDataLen()
        {
            return 1;
        }

        public override void SetFailBytes(IByteBuffer databuf)
        {
            CardData = databuf.ReadInt();
        }
    }
}
