using DoNetDrive.Protocol.Door.FC8800.TemplateMethod;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.Menu
{
    public class ReadAllMenu_Result : TemplateResult_Base
    {
        /// <summary>
        /// 创建结构
        /// </summary>
        public ReadAllMenu_Result(List<TemplateData_Base> DataList) : base(DataList)
        {
            this.DataList = DataList;
        }
    }
}
