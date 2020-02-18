using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.Door.Door89H.Card
{
    /// <summary>
    /// Door89H 将卡片列表写入到控制器非排序区 
    /// </summary>
    public class WriteCardListBySequence : DoNetDrive.Protocol.Door.Door8800.Card.WriteCardListBySequenceBase<Door89H.Data.CardDetail>
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="perameter"></param>
        public WriteCardListBySequence(INCommandDetail cd, WriteCardListBySequence_Parameter perameter) : base(cd, perameter)
        {
            mPacketCardMax = 8;
            MaxBufSize = (mPacketCardMax * 0x25) + 4;
        }
    }
}
