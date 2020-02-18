using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.FC8800.TemplateMethod;
using DoNetDrive.Protocol.POS.Data;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.Subsidy
{
    /// <summary>
    /// 读取所有补贴
    /// </summary>
    public class ReadAllSubsidy : Door.FC8800.TemplateMethod.TemplateReadData_Base<SubsidyDetail>
    {
        public ReadAllSubsidy(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x07;
            CmdIndex = 0x03;
            CmdPar = 0x00;
            CheckResponseCmdType = 0x07;
            DataLen = 0x02;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        protected override TemplateResult_Base CreateResult(List<TemplateData_Base> dataList)
        {
            ReadAllSubsidy_Result result = new ReadAllSubsidy_Result(dataList);
            return result;
        }
    }
}
