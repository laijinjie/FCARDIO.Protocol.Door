using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint.Data.Transaction
{
    /// <summary>
    /// 人脸机、指纹机记录详情
    /// </summary>
    public class TransactionDetail : DoNetDrive.Protocol.Door.Door8800.Data.TransactionDetail
    {
        /// <summary>
        /// 可用的新记录数
        /// </summary>
        /// <returns>新记录数</returns>
        public override long readable()
        {
            if (IsCircle)
            {
                return DataBaseMaxSize;
            }
            if (ReadIndex > WriteIndex)
            {
                ReadIndex = WriteIndex - DataBaseMaxSize;
                if (ReadIndex < 0) ReadIndex = 0;
            }
            if (ReadIndex == WriteIndex)
            {
                return 0;
            }

            return (WriteIndex - ReadIndex);

        }

    }
}
