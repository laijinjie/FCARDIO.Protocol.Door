using DotNetty.Buffers;
using FCARDIO.Protocol.Transaction;
using FCARDIO.Protocol.Util;

namespace FCARDIO.Protocol.Fingerprint.Data.Transaction
{
    public class CardTransaction : AbstractTransaction
    {
        public uint Number { get; private set; }
        public uint UserCode { get; private set; }

        public byte Photo { get; private set; }

        /// <summary>
        /// 读卡器号
        /// 1--表示主机；2--表示子机
        /// </summary>
        public byte Reader { get; private set; }

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
            Number = dtBuf.ReadUnsignedInt();
            UserCode = dtBuf.ReadUnsignedInt();
            byte[] time = new byte[6];
            
            dtBuf.ReadBytes(time, 0, 6);
            _TransactionDate = TimeUtil.BCDTimeToDate_ssmmhhddMMyy(time);
            _TransactionDate = _TransactionDate.AddMonths(1);
            Reader = dtBuf.ReadByte();
            _TransactionCode = dtBuf.ReadByte();
            Photo = dtBuf.ReadByte();
        }
    }
}
