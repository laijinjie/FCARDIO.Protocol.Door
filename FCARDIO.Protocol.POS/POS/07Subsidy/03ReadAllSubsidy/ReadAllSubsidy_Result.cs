using DoNetDrive.Protocol.Door.FC8800.TemplateMethod;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.Subsidy
{
    public class ReadAllSubsidy_Result : TemplateResult_Base
    {
        /// <summary>
        /// 创建结构
        /// </summary>
        public ReadAllSubsidy_Result(List<TemplateData_Base> DataList) : base(DataList)
        {
            this.DataList = DataList;
        }
    }
}
