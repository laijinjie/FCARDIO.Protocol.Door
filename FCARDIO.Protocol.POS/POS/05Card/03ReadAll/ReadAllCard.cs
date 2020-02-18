using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.FC8800.TemplateMethod;
using DoNetDrive.Protocol.POS.Data;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.Card
{
    /// <summary>
    /// 读取所有名单命令
    /// </summary>
    public class ReadAllCard : Door.FC8800.TemplateMethod.TemplateReadData_Base<CardDetail>
    {
        public ReadAllCard(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x05;
            CmdIndex = 0x03;
            CmdPar = 0x00;
            CheckResponseCmdType = 0x05;
            DataLen = 0x02;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        protected override TemplateResult_Base CreateResult(List<TemplateData_Base> dataList)
        {
            ReadAllCard_Result result = new ReadAllCard_Result(dataList);
            return result;
        }
    }
}
