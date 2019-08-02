using DotNetty.Buffers;
using FCARDIO.Protocol.Transaction;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.USB.OfflinePatrol.Data.Transaction
{
    /// <summary>
    /// 读卡记录
    /// </summary>
    public class CardTransaction : AbstractTransaction
    {
        /// <summary>
        /// 工号
        /// </summary>
        public ushort PCode;

        /// <summary>
        /// 卡号
        /// </summary>
        public uint CardData;

        /// <summary>
        /// 状态
        /// 0  普通卡
        /// 1  巡更人员卡
        /// </summary>
        public byte State;

        /// <summary>
        /// 获取读卡记录格式长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 12;
        }

        public CardTransaction()
        {
            _TransactionType = 1;
        }
        /// <summary>
        /// 从buf中读取记录数据
        /// </summary>
        /// <param name="dtBuf"></param>
        public override void SetBytes(IByteBuffer dtBuf)
        {
            try
            {
                PCode = dtBuf.ReadUnsignedShort();
                if (PCode == ushort.MaxValue)
                {
                    _IsNull = true;
                    PCode = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        dtBuf.ReadByte();
                    }
                    
                    return;
                }
                _TransactionDate = TimeUtil.BCDTimeToDate_yyMMddhhmmss(dtBuf);

                State = dtBuf.ReadByte();


                CardData = (uint)dtBuf.ReadUnsignedMedium();
               
            }
#pragma warning disable CS0168 // 声明了变量“e”，但从未使用过
            catch (Exception e)
#pragma warning restore CS0168 // 声明了变量“e”，但从未使用过
            {
            }

            return;

        }
    }
}
