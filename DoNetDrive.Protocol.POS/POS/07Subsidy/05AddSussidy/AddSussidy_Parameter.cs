
using DoNetDrive.Protocol.POS.Data;
using DoNetDrive.Protocol.POS.TemplateMethod;
using System;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.Subsidy
{
    public class AddSussidy_Parameter : TemplateParameter_Base<SubsidyDetail>
    {
        public AddSussidy_Parameter()
        {

        }

        public AddSussidy_Parameter(List<SubsidyDetail> list) : base(list)
        {
        }

        protected override bool CheckedParameterItem(SubsidyDetail Menu)
        {
           
            return true;
        }
    }
}
