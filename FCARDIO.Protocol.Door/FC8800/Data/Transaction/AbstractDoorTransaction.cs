using DotNetty.Buffers;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Data
{
    /// <summary>
    /// 关于门的事件抽象类
    /// </summary>
    public class AbstractDoorTransaction : AbstractTransaction
    {
        /// <summary>
        /// 门号
        /// </summary>
        public short Door;
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="type"></param>
        public AbstractDoorTransaction(int type)
        {
            TransactionType = (short)type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public override void SetBytes(IByteBuffer data)
        {
            try
            {
                Door = data.ReadByte();
                byte[] btTime = new byte[6];
                data.ReadBytes(btTime, 0, 6);

                if ((btTime[0]) == 255)
                {
                    IsNull = true;
                    //return;
                }

                TransactionDate = TimeUtil.BCDTimeToDate_yyMMddhh(btTime);
                TransactionCode = data.ReadByte();
                if (TransactionCode == 0)
                {
                    IsNull = true;
                }
            }
            catch (Exception e)
            {
            }

            return;
        }
    }
}
