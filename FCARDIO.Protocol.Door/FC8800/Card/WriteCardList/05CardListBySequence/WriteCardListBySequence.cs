using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardListBySequence
{
    /// <summary>
    /// FC88\MC58 将卡片列表写入到控制器非排序区 
    /// </summary>
    public class WriteCardListBySequence 
        : WriteCardListBySequenceBase<FC8800.Data.CardDetail>
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="perameter"></param>
        public WriteCardListBySequence(INCommandDetail cd, WriteCardListBySequence_Parameter perameter) : base(cd, perameter)
        {
            mPacketCardMax = 10;
            MaxBufSize = (mPacketCardMax * 0x21) + 4;
        }
    }
}
