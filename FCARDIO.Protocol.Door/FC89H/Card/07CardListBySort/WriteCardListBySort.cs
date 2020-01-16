using DotNetty.Buffers;
using System;
using FCARDIO.Core.Command;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Card
{

    /// <summary>
    /// FC89H 将卡片列表写入到控制器排序区 
    /// </summary>
    public class WriteCardListBySort : FCARDIO.Protocol.Door.FC8800.Card.WriteCardListBySortBase<Data.CardDetail>
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="perameter"></param>
        public WriteCardListBySort(INCommandDetail cd, WriteCardListBySort_Parameter perameter) : base(cd, perameter)
        {
            mPacketCardMax = 8;
            MaxBufSize = (mPacketCardMax * 0x25) + 8;
        }


        /// <summary>
        /// 从错误卡列表中读取一个错误卡号，加入到cardlist中
        /// </summary>
        /// <param name="CardList"></param>
        /// <param name="buf"></param>
        protected override void ReadCardByFailBuf(List<ulong> CardList, IByteBuffer buf)
        {
            CardList.Add((UInt64)buf.ReadInt());
        }
    }
}
