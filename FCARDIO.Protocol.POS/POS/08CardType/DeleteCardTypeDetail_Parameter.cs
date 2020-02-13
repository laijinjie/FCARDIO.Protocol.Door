using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.POS.CardType
{
    public class DeleteCardTypeDetail_Parameter : AbstractParameter
    {
        /// <summary>
        /// 需要写入的订餐列表
        /// </summary>
        public List<byte> CardTypeList;

        public DeleteCardTypeDetail_Parameter(List<byte> cardTypeList)
        {
            CardTypeList = cardTypeList;
        }


        public override bool checkedParameter()
        {
            if (CardTypeList == null || CardTypeList.Count == 0)
            {
                return false;
            }
            
            return true;
        }


        public override void Dispose()
        {
            CardTypeList = null;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf;
        }

        public override int GetDataLen()
        {
            return 0;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            return;
        }
    }
}
