using DoNetDrive.Protocol.POS.Data;
using DoNetDrive.Protocol.POS.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetDrive.Protocol.POS.Test
{
    public partial class FrmMenu : frmNodeForm
    {
        #region 单例模式
        private static object lockobj = new object();
        private static FrmMenu onlyObj;
        public static FrmMenu GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new FrmMenu(main);
                        FrmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        private FrmMenu(INMain main) : base(main)
        {
            InitializeComponent();
        }
        #endregion

        private List<MenuDetail> ListMenuDetail = new List<MenuDetail>();

        public FrmMenu()
        {
            InitializeComponent();
        }

        private void butReadDataBase_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadMenuDataBase cmd = new ReadMenuDataBase(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadMenuDataBase_Result result = cmde.Command.getResult() as ReadMenuDataBase_Result;
                string tip = $"商品信息--最大容量：{result.SortSize},已存数量：{result.UseSize}";

                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butReadAllMenu_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadAllMenu cmd = new ReadAllMenu(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadAllMenu_Result result = cmde.Command.getResult() as ReadAllMenu_Result;
                string tip = $"";
                Invoke(() =>
                {
                    dgvMenu.AutoGenerateColumns = false;
                    dgvMenu.DataSource = new BindingList<MenuDetail>(result.MenuDetails);
                    //dgvPrintContent.DataSource = result.PrintContents;
                });
                mMainForm.AddCmdLog(cmde, tip);
            };

        }

        private void butClearDataBase_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ClearMenuDataBase cmd = new ClearMenuDataBase(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };
        }

        private void butAddAll_Click(object sender, EventArgs e)
        {

        }

        private void butAddToList_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("请输入商品名称");
                return;
            }
            int code = 0;
            if (!int.TryParse(txtCode.Text.Trim(), out code))
            {
                MessageBox.Show("请输入商品代码");
                return;
            }
            int print = 0;
            if (!int.TryParse(txtPrice.Text.Trim(), out print))
            {
                MessageBox.Show("请输入商品价格");
                return;
            }
            MenuDetail dto = new MenuDetail();
            dto.MenuBarCode = txtBarCode.Text;
            dto.MenuCode = code;
            dto.MenuName = txtName.Text;
            dto.MenuPrice = print;
            ListMenuDetail.Add(dto);

            Invoke(() =>
            {
                dgvMenu.DataSource = new BindingList<MenuDetail>(ListMenuDetail);
            });
        }

        private void butDeleteFromList_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvMenu.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvMenu.Rows[i].Cells[0];
                if ((bool)cell.FormattedValue)
                {
                    DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dgvMenu.Rows[i].Cells[1];
                    var item = ListMenuDetail.FirstOrDefault(t => t.MenuCode == int.Parse(text.Value.ToString()));
                    ListMenuDetail.Remove(item);
                }
            }
            dgvMenu.DataSource = new BindingList<MenuDetail>(ListMenuDetail);
        }

        private void butAddToDevice_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("请输入商品名称");
                return;
            }
            int code = 0;
            if (!int.TryParse(txtCode.Text.Trim(), out code))
            {
                MessageBox.Show("请输入商品代码");
                return;
            }
            int print = 0;
            if (!int.TryParse(txtPrice.Text.Trim(), out print))
            {
                MessageBox.Show("请输入商品价格");
                return;
            }
            List<MenuDetail> _list = new List<MenuDetail>();
            MenuDetail menu = new MenuDetail();
           
         
            _list.Add(menu);
            AddMenu_Parameter par = new AddMenu_Parameter(_list);

            AddMenu cmd = new AddMenu(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功：");
            };
        }
    }
}
