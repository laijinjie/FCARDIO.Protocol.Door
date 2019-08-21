using DotNetty.Buffers;
using FCARDIO.Protocol.Transaction;
using FCARDIO.Protocol.Util;
using System;

namespace FCARDIO.Protocol.Fingerprint.Data.Transaction
{
    /// <summary>
    /// 门磁记录
    /// TransactionCode 事件代码含义表：
    /// 1  开门
    /// 2  关门 
    /// 3  进入门磁报警状态
    /// 4  退出门磁报警状态
    /// 5  门未关好
    /// 6  使用按钮开门
    /// 7  按钮开门时门已锁定
    /// 8  按钮开门时控制器已过期
    public class DoorSensorTransaction : AbstractTransaction
    {
        /// <summary>
        /// 门号
        /// </summary>
        public int Door;

        public DoorSensorTransaction()
        {
            _TransactionType = 2;
        }


        /// <summary>
        /// 指示一个事务记录所占用的缓冲区长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 8;
        }

        /// <summary>
        /// 使用缓冲区构造一个事务实例
        /// </summary>
        /// <param name="data">缓冲区</param>
        public override void SetBytes(IByteBuffer data)
        {
            try
            {
                _IsNull = CheckNull(data, 2);

                if (_IsNull)
                {
                    ReadNullRecord(data);
                    return;
                }
                Door = data.ReadByte();
                byte[] time = new byte[6];

                data.ReadBytes(time, 0, 6);
                _TransactionDate = TimeUtil.BCDTimeToDate_ssmmhhddMMyy(time);
                _TransactionDate = _TransactionDate.AddMonths(1);
                _TransactionCode = data.ReadByte();
            }
            catch (Exception e)
            {
            }

            return;
        }
    }
}
