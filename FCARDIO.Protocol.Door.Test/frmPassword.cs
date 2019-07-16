using FCARDIO.Protocol.Door.FC8800.Password;
using FCARDIO.Protocol.Door.Test.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCARDIO.Protocol.Door.Test
{
    public partial class frmPassword : frmNodeForm
    {
        private static object lockobj = new object();
        private static frmPassword onlyObj;

        private List<PasswordDto> ListPassword = new List<PasswordDto>();
        public static frmPassword GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmPassword(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        INMain mMainForm;

        private frmPassword(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
            dataGridView1.AutoGenerateColumns = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPassword_Load(object sender, EventArgs e)
        {

            string[] times = new string[302];
            times[0] = "失效";
            for (int i = 1; i <= 300; i++)
            {
                times[i] = i.ToString();
            }
            times[301] = "无限制(65535)";
            cmbOpenTimes.Items.AddRange(times);
            cmbOpenTimes.SelectedIndex = 30;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            onlyObj = null;
        }

        /// <summary>
        /// 读取密码库存储详情 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButReadPasswordDetail_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadPasswordDetail cmd = new ReadPasswordDetail(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadPasswordDetail_Result result = cmde.Command.getResult() as ReadPasswordDetail_Result;

                //dataGridView1
                string log = $"密码容量：{result.DataSize}，已存数量：{result.PasswordSize}";
                mMainForm.AddCmdLog(cmde, log);
            };
        }

        /// <summary>
        /// 从控制板读取所有密码表 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButReadAllPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC89H)
            {
                FC89H.Password.ReadAllPassword cmd
                    = new FC89H.Password.ReadAllPassword(cmdDtl);
                mMainForm.AddCommand(cmd);
            }
            else
            {
                ReadAllPassword cmd = new ReadAllPassword(cmdDtl);
                mMainForm.AddCommand(cmd);
            }

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ListPassword.Clear();
                var comdResult = cmde.Command.getResult();
                int count = 0;
                if (comdResult is ReadAllPassword_Result)
                {
                    ReadAllPassword_Result result = comdResult as ReadAllPassword_Result;
                    foreach (PasswordDetail detail in result.PasswordList)
                    {
                        PasswordDto dto = new PasswordDto();
                        dto.SetDoors(detail);
                        dto.Password = detail.Password;
                        ListPassword.Add(dto);
                    }
                    count = result.PasswordList.Count;
                }
                else if (comdResult is FC89H.Password.ReadAllPassword_Result)
                {
                    FC89H.Password.ReadAllPassword_Result result = comdResult as FC89H.Password.ReadAllPassword_Result;
                    foreach (FC89H.Password.PasswordDetail detail in result.PasswordList)
                    {
                        PasswordDto dto = new PasswordDto();
                        dto.SetDoors(detail);
                        dto.Password = detail.Password;
                        dto.OpenTimes = detail.OpenTimes;
                        dto.Expiry = detail.Expiry;
                        ListPassword.Add(dto);
                    }
                    count = result.PasswordList.Count;
                }

                Invoke(() =>
                {
                    dataGridView1.DataSource = new BindingList<PasswordDto>(ListPassword);

                });
                //dataGridView1
                string log = $"已读取到数量：{count} ";
                mMainForm.AddCmdLog(cmde, log);
            };
        }

        /// <summary>
        /// 添加列表密码 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButAddPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            
            if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC88)
            {
                List<PasswordDetail> _list = new List<PasswordDetail>();
                for (int i = 0; i < ListPassword.Count; i++)
                {
                    PasswordDetail password = new PasswordDetail();
                        password.OpenTimes = ListPassword[i].OpenTimes;
                        if (password.OpenTimes == cmbOpenTimes.Items.Count - 1)
                        {
                            password.OpenTimes = 65535;
                        }
                        password.Expiry = ListPassword[i].Expiry;
                    password.Password = ListPassword[i].Password;
                    string strDoor1 = (ListPassword[i].Door1 ? "1" : "0") + (ListPassword[i].Door2 ? "1" : "0") + (ListPassword[i].Door3 ? "1" : "0") + (ListPassword[i].Door4 ? "1" : "0");
                    password.Door = Convert.ToInt32(strDoor1, 2);
                    _list.Add(password);
                }
                Password_Parameter par = new Password_Parameter(_list);
                AddPassword cmd = new AddPassword(cmdDtl, par);
                mMainForm.AddCommand(cmd);
            }
            else
            {
                List<FC89H.Password. PasswordDetail> _list = new List<FC89H.Password.PasswordDetail>();
                for (int i = 0; i < ListPassword.Count; i++)
                {
                    FC89H.Password.PasswordDetail password = new FC89H.Password.PasswordDetail();
                    password.OpenTimes = ListPassword[i].OpenTimes;
                    if (password.OpenTimes == cmbOpenTimes.Items.Count - 1)
                    {
                        password.OpenTimes = 65535;
                    }
                    password.Expiry = ListPassword[i].Expiry;
                    password.Password = ListPassword[i].Password;
                    string strDoor1 = (ListPassword[i].Door1 ? "1" : "0") + (ListPassword[i].Door2 ? "1" : "0") + (ListPassword[i].Door3 ? "1" : "0") + (ListPassword[i].Door4 ? "1" : "0");
                    password.Door = Convert.ToInt32(strDoor1, 2);
                    _list.Add(password);
                }
                FC89H.Password.AddPassword_Parameter par = new FC89H.Password.AddPassword_Parameter(_list);
                FC89H.Password.AddPassword cmd = new FC89H.Password.AddPassword(cmdDtl, par);
                mMainForm.AddCommand(cmd);
            }

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功：");
            };

        }

        /// <summary>
        /// 删除所有密码表 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButClearPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ClearPassword cmd = new ClearPassword(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功：");

            };
        }

        /// <summary>
        /// 清空表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearList_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.Rows.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[1];
                ListPassword.RemoveAt(ListPassword.FindIndex(t => t.Password == text.Value.ToString()));
            }
            dataGridView1.DataSource = new BindingList<PasswordDto>(ListPassword);
        }

        /// <summary>
        /// 新增至列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButInsertList_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("请输入密码");
                return;
            }
            PasswordDto dto = new PasswordDto();
            dto.Door1 = cbbit0.Checked;
            dto.Door2 = cbbit1.Checked;
            dto.Door3 = cbbit2.Checked;
            dto.Door4 = cbbit3.Checked;
            dto.Password = txtPassword.Text;
            if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC89H)
            {
                if (cmbOpenTimes.SelectedIndex == cmbOpenTimes.Items.Count - 1)
                {
                    dto.OpenTimes = cmbOpenTimes.SelectedIndex;
                }
                else
                {
                    dto.OpenTimes = cmbOpenTimes.SelectedIndex;
                }
                dto.Expiry = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, dtpTime.Value.Hour, dtpTime.Value.Minute, 0);
            }
            ListPassword.Add(dto);

            Invoke(() =>
            {
                dataGridView1.DataSource = new BindingList<PasswordDto>(ListPassword);

            });
        }

        /// <summary>
        /// 从列表删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButDelList_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                if ((bool)cell.FormattedValue)
                {
                    DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[1];
                    var item = ListPassword.FirstOrDefault(t => t.Password == text.Value.ToString());
                    ListPassword.Remove(item);
                }
            }
            dataGridView1.DataSource = new BindingList<PasswordDto>(ListPassword);
        }

        /// <summary>
        /// 新增至设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddDevice_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("请输入密码");
                return;
            }
            //FC89H.Password.
            if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC89H)
            {
                List<FC89H.Password.PasswordDetail> _list = new List<FC89H.Password.PasswordDetail>();
                FC89H.Password.PasswordDetail password = new FC89H.Password.PasswordDetail();
                if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC89H)
                {
                    password.OpenTimes = cmbOpenTimes.SelectedIndex;
                    if (cmbOpenTimes.SelectedIndex == cmbOpenTimes.Items.Count - 1)
                    {
                        password.OpenTimes = 65535;
                    }
                    password.Expiry = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, dtpTime.Value.Hour, dtpTime.Value.Minute, 0);
                }
                password.Password = txtPassword.Text;
                string strDoor1 = (cbbit0.Checked ? "1" : "0") + (cbbit1.Checked ? "1" : "0") + (cbbit2.Checked ? "1" : "0") + (cbbit3.Checked ? "1" : "0");
                password.Door = Convert.ToInt32(strDoor1, 2);
                _list.Add(password);
                FC89H.Password.AddPassword_Parameter par = new FC89H.Password.AddPassword_Parameter(_list);

                FC89H.Password.AddPassword cmd = new FC89H.Password.AddPassword(cmdDtl, par);
                mMainForm.AddCommand(cmd);

                cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
                {
                    mMainForm.AddLog($"命令成功：");
                };
            }
            else
            {
                List<PasswordDetail> _list = new List<PasswordDetail>();
                PasswordDetail password = new PasswordDetail();
                if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC89H)
                {
                    password.OpenTimes = cmbOpenTimes.SelectedIndex;
                    if (cmbOpenTimes.SelectedIndex == cmbOpenTimes.Items.Count - 1)
                    {
                        password.OpenTimes = 65535;
                    }
                    password.Expiry = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, dtpTime.Value.Hour, dtpTime.Value.Minute, 0);
                }
                password.Password = txtPassword.Text;
                string strDoor1 = (cbbit0.Checked ? "1" : "0") + (cbbit1.Checked ? "1" : "0") + (cbbit2.Checked ? "1" : "0") + (cbbit3.Checked ? "1" : "0");
                password.Door = Convert.ToInt32(strDoor1, 2);
                _list.Add(password);
                Password_Parameter par = new Password_Parameter(_list);

                AddPassword cmd = new AddPassword(cmdDtl, par);
                mMainForm.AddCommand(cmd);

                cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
                {
                    mMainForm.AddLog($"命令成功：");
                };
            }
            
        }

        /// <summary>
        /// 从设备删除单个密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelDevice_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("请输入密码");
                return;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;



            if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC89H)
            {
                List<FC89H.Password.PasswordDetail> _list = new List<FC89H.Password.PasswordDetail>();
                FC89H.Password.PasswordDetail detail = new FC89H.Password.PasswordDetail();
                detail.Password = txtPassword.Text;
                _list.Add(detail);
                FC89H.Password.DeletePassword_Parameter par = new FC89H.Password.DeletePassword_Parameter(_list);
                FC89H.Password.DeletePassword cmd = new FC89H.Password.DeletePassword(cmdDtl, par);
                mMainForm.AddCommand(cmd);
            }
            else
            {
                List<PasswordDetail> _list = new List<PasswordDetail>();
                PasswordDetail detail = new PasswordDetail();
                detail.Password = txtPassword.Text;
                _list.Add(detail);
                Password_Parameter par = new Password_Parameter(_list);
                DeletePassword cmd = new DeletePassword(cmdDtl, par);
                mMainForm.AddCommand(cmd);
            }

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功：");

            };
        }

        /// <summary>
        /// 从设备删除多个密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelSelect_Click(object sender, EventArgs e)
        {
            if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC88)
            {
                List<PasswordDetail> _list = new List<PasswordDetail>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                    if ((bool)cell.FormattedValue)
                    {
                        DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[1];
                        var item = ListPassword.FirstOrDefault(t => t.Password == text.Value.ToString());
                        PasswordDetail detail = new PasswordDetail();
                        detail.Password = item.Password;
                        string strDoor1 = (item.Door1 ? "1" : "0") + (item.Door2 ? "1" : "0") + (item.Door3 ? "1" : "0") + (item.Door4 ? "1" : "0");
                        detail.Door = Convert.ToInt32(strDoor1, 2);
                        _list.Add(detail);
                        //ListPassword.Remove(item);
                    }
                }

                dataGridView1.DataSource = new BindingList<PasswordDto>(ListPassword);
                if (_list.Count > 0)
                {
                    var cmdDtl = mMainForm.GetCommandDetail();
                    if (cmdDtl == null) return;

                    Password_Parameter par = new Password_Parameter(_list);

                    DeletePassword cmd = new DeletePassword(cmdDtl, par);
                    mMainForm.AddCommand(cmd);
                    cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
                    {
                        mMainForm.AddLog($"命令成功：");

                    };
                }

            }
            else
            {
                List<FC89H.Password.PasswordDetail> _list = new List<FC89H.Password.PasswordDetail>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                    if ((bool)cell.FormattedValue)
                    {
                        DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[1];
                        var item = ListPassword.FirstOrDefault(t => t.Password == text.Value.ToString());
                        FC89H.Password.PasswordDetail detail = new FC89H.Password.PasswordDetail();
                        detail.Password = item.Password;
                        string strDoor1 = (item.Door1 ? "1" : "0") + (item.Door2 ? "1" : "0") + (item.Door3 ? "1" : "0") + (item.Door4 ? "1" : "0");
                        detail.Door = Convert.ToInt32(strDoor1, 2);
                        _list.Add(detail);
                        //ListPassword.Remove(item);
                    }
                    else
                    {

                    }
                }

                dataGridView1.DataSource = new BindingList<PasswordDto>(ListPassword);
                if (_list.Count > 0)
                {
                    var cmdDtl = mMainForm.GetCommandDetail();
                    if (cmdDtl == null) return;

                    FC89H.Password.DeletePassword_Parameter par = new FC89H.Password.DeletePassword_Parameter(_list);

                    FC89H.Password.DeletePassword cmd = new FC89H.Password.DeletePassword(cmdDtl, par);
                    mMainForm.AddCommand(cmd);
                    cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
                    {
                        mMainForm.AddLog($"命令成功：");

                    };
                }
            }

        }

        /// <summary>
        /// 生成测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRandom_Click(object sender, EventArgs e)
        {
            int iCount = 0;
            if (int.TryParse(txtCount.Text, out iCount))
            {
                ListPassword.Clear();
                Random rnd = new Random();
                Random rndDoor = new Random();
                Random rndTimes = new Random();
                ListPassword.Clear();
                for (int i = 0; i < iCount; i++)
                {
                    string password = rnd.Next(10000000, 99999999).ToString();
                    PasswordDto dto = new PasswordDto();
                    dto.Password = password;
                    int door = rndDoor.Next(16);
                    string binary = Convert.ToString(door, 2).PadLeft(4, '0');
                    dto.SetDoors(binary);
                   
                    {
                        dto.OpenTimes = rndTimes.Next(1, cmbOpenTimes.Items.Count);
                        dto.Expiry = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, dtpTime.Value.Hour, dtpTime.Value.Minute, 0);
                    }

                    ListPassword.Add(dto);
                }
                dataGridView1.DataSource = new BindingList<PasswordDto>(ListPassword);
            }
            else
            {
                MessageBox.Show("");
            }
        }

        private void SetDoors(string binary)
        {

        }

        private void CbReverse_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                if ((bool)cell.FormattedValue)
                {
                    cell.Value = false;
                    cell.EditingCellFormattedValue = false;
                }
                else
                {
                    cell.Value = true;
                    cell.EditingCellFormattedValue = true;
                }
            }
        }

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if ((bool)cell.FormattedValue)
                {
                    cell.Value = false;
                    cell.EditingCellFormattedValue = false;
                }
                else
                {
                    cell.Value = true;
                    cell.EditingCellFormattedValue = true;
                }
            }
            else
            {
                DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[1];
                var dto = ListPassword.FirstOrDefault(t => t.Password == text.Value.ToString());
                txtPassword.Text = dto.Password;
                cbbit0.Checked = dto.Door1;
                cbbit1.Checked = dto.Door2;
                cbbit2.Checked = dto.Door3;
                cbbit3.Checked = dto.Door4;
            }
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length > 8)
            {

                MessageBox.Show("密码长度太长");
                txtPassword.Text = "";
            }
            int iOut = 0;
            if (!int.TryParse(txtPassword.Text, out iOut))
            {
                MessageBox.Show("密码格式不正确");
                txtPassword.Text = "";
            }
        }
    }
}
