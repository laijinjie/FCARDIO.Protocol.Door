using DoNetDrive.Protocol.Door.FC8800.TemplateMethod;
using System;

namespace DoNetDrive.Protocol.POS.Card
{
    /// <summary>
    /// 添加名单命令参数
    /// </summary>
    public class AddCard_Parameter : Door.FC8800.TemplateMethod.TemplateParameter_Base
    {
        protected override bool CheckedParameterItem(TemplateData_Base Menu)
        {
            throw new NotImplementedException();
        }
    }
}
