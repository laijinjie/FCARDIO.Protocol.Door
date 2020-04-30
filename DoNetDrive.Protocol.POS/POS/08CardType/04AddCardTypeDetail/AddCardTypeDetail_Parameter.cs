using DoNetDrive.Protocol.POS.Data;
using DoNetDrive.Protocol.POS.TemplateMethod;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.CardType
{
    public class AddCardTypeDetail_Parameter : TemplateParameter_Base<CardTypeDetail>
    {
        public AddCardTypeDetail_Parameter()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public List<CardTypeDetail> CardTypeList;

        public AddCardTypeDetail_Parameter(List<CardTypeDetail> cardTypeList)
        {
            CardTypeList = cardTypeList;
        }


        protected override bool CheckedParameterItem(CardTypeDetail Menu)
        {
           
            return true;
        }
    }
}
