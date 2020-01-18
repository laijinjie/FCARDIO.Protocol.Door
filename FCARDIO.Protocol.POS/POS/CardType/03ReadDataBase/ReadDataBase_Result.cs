using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.POS.Data;
using System.Collections.Generic;

namespace FCARDIO.Protocol.POS.CardType.ReadDataBase
{
    /// <summary>
    /// 读取到的卡类返回结果
    /// </summary>
    public class ReadDataBase_Result : INCommandResult
    {
        /// <summary>
        /// 读取到的卡类列表
        /// </summary>
        public List<CardTypeDetail> CardTypeDetailList;

        public ReadDataBase_Result(List<CardTypeDetail> cardTypeDetailList)
        {
            CardTypeDetailList = cardTypeDetailList;
        }
        public void Dispose()
        {

        }

        public void SetBytes(IByteBuffer buf)
        {


        }
    }
}
