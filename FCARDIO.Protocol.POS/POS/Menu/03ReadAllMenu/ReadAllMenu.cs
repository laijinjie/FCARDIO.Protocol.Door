using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.TemplateMethod;
using System.Collections.Generic;
using FCARDIO.Protocol.POS.Data;

namespace FCARDIO.Protocol.POS.Menu
{
    public class ReadAllMenu : Door.FC8800.TemplateMethod.TemplateReadData_Base<MenuDetail>
    {
        public ReadAllMenu(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x06;
            CmdIndex = 0x03;
            CmdPar = 0x00;
            CheckResponseCmdType = 0x06;
            DataLen = 0x02;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        protected override TemplateResult_Base CreateResult(List<TemplateData_Base> dataList)
        {
            ReadAllMenu_Result result = new ReadAllMenu_Result(dataList);
            return result;
        }
    }
}
