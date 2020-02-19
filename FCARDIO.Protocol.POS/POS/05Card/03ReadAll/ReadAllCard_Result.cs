using DoNetDrive.Protocol.Door.Door8800.TemplateMethod;

using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.Card
{
    public class ReadAllCard_Result : TemplateResult_Base
    {
        /// <summary>
        /// 创建结构
        /// </summary>
        public ReadAllCard_Result(List<TemplateData_Base> DataList) : base(DataList)
        {
            this.DataList = DataList;
        }
    }
}
