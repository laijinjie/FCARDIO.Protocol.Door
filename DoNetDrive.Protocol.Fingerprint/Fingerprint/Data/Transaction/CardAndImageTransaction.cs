using DotNetty.Buffers;
using DoNetDrive.Protocol.Transaction;
using DoNetDrive.Protocol.Util;
using System;

namespace DoNetDrive.Protocol.Fingerprint.Data.Transaction
{
    public class CardAndImageTransaction : AbstractTransaction
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
        public byte Photo { get; set; }

        /// <summary>
        /// 出入类型：1--表示进门；2--表示出门
        /// </summary>
        public byte Accesstype { get; private set; }

        /// <summary>
        /// 体温
        /// </summary>
        public int Temperature { get; private set; }

        /// <summary>
        /// 初始化参数
        /// </summary>
        public CardAndImageTransaction()
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
            _SerialNumber = (int)RecordSerialNumber;
            UserCode = dtBuf.ReadUnsignedInt();
            byte[] time = new byte[6];
            dtBuf.ReadBytes(time, 0, 6);
            _TransactionDate = TimeUtil.BCDTimeToDate_ssmmhhddMMyy(time);
            Accesstype = dtBuf.ReadByte();
            _TransactionCode = dtBuf.ReadByte();
            Photo = dtBuf.ReadByte();
        }

        /// <summary>
        /// 使用缓冲区构造一个事务实例
        /// </summary>
        /// <param name="data">缓冲区</param>
        public void SetBodyTemperatureBytes(IByteBuffer data)
        {
            try
            {
                _IsNull = CheckNull(data, 2);

                if (_IsNull)
                {
                    ReadNullRecord(data);
                    return;
                }

                data.ReadInt();
                Temperature = data.ReadUnsignedShort();
            }
            catch (Exception e)
            {
            }

            return;
        }
    }
}
