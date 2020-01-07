using DotNetty.Buffers;
using FCARDIO.Protocol.Transaction;
using FCARDIO.Protocol.Util;

namespace FCARDIO.Protocol.Fingerprint.Data.Transaction
{
    public class CardTransaction : AbstractTransaction
    {
        /// <summary>
        /// 记录唯一序号
        /// </summary>
        public uint RecordSerialNumber { get; private set; }
        /// <summary>
        /// 用户号
        /// </summary>
        public uint UserCode { get; private set; }
        /// <summary>
        /// 是否包含照片
        /// </summary>
        public byte Photo { get; private set; }

        /// <summary>
        /// 出入类型：1--表示进门；2--表示出门
        /// </summary>
        public byte Accesstype { get; private set; }

        /// <summary>
        /// 初始化参数
        /// </summary>
        public CardTransaction()
        {
            _TransactionType = 1;
        }

        /// <summary>
        /// 获取读卡记录格式长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 13;
        }

        /// <summary>
        /// 从buf中读取记录数据
        /// </summary>
        /// <param name="dtBuf"></param>
        public override void SetBytes(IByteBuffer dtBuf)
        {
            RecordSerialNumber = dtBuf.ReadUnsignedInt();
            UserCode = dtBuf.ReadUnsignedInt();
            byte[] time = new byte[6];
            dtBuf.ReadBytes(time, 0, 6);
            _TransactionDate = TimeUtil.BCDTimeToDate_ssmmhhddMMyy(time);
            _TransactionDate = _TransactionDate.AddMonths(1);
            Accesstype = dtBuf.ReadByte();
            _TransactionCode = dtBuf.ReadByte();
            Photo = dtBuf.ReadByte();
        }
    }
}
