
using DoNetDrive.Protocol.POS.Data;
using DoNetDrive.Protocol.POS.TemplateMethod;
using System;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.Menu
{
    /// <summary>
    /// 添加菜单命令参数
    /// </summary>
    public class AddMenu_Parameter : TemplateParameter_Base<MenuDetail>
    {
        public AddMenu_Parameter()
        {

        }

        public AddMenu_Parameter(List<MenuDetail> list): base(list)
        {
        }

        protected override bool CheckedParameterItem(MenuDetail Menu)
        {
            if (Menu.MenuPrice < 0 || Menu.MenuPrice > 21474836)
            {
                return false;
            }
            if (Menu.MenuCode < 0 || Menu.MenuCode > 21474836)
            {
                return false;
            }
            if (Menu.MenuName?.Length > 16)
            {
                return false;
            }
            if (Menu.MenuBarCode?.Length > 40)
            {
                return false;
            }
            return true;
        }
    }
}
