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
    public partial class frmRecord : frmNodeForm
    {
        private static object lockobj = new object();
        private static frmRecord onlyObj;
        public static frmRecord GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmRecord(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        INMain mMainForm;

        private frmRecord(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }

        private void frmRecord_Load(object sender, EventArgs e)
        {
            e_TransactionDatabaseType();
        }

        #region 记录类型
        public void e_TransactionDatabaseType()
        {
            cboe_TransactionDatabaseType1.Items.Clear();
            cboe_TransactionDatabaseType1.Items.AddRange(new string[] { "读卡记录", "按钮记录", "门磁记录", "远程记录", "报警记录", "系统记录" });
            cboe_TransactionDatabaseType1.SelectedIndex = 0;

            cboe_TransactionDatabaseType2.Items.Clear();
            cboe_TransactionDatabaseType2.Items.AddRange(new string[] { "读卡记录", "按钮记录", "门磁记录", "远程记录", "报警记录", "系统记录" });
            cboe_TransactionDatabaseType2.SelectedIndex = 0;

            cboe_TransactionDatabaseType3.Items.Clear();
            cboe_TransactionDatabaseType3.Items.AddRange(new string[] { "读卡记录", "按钮记录", "门磁记录", "远程记录", "报警记录", "系统记录" });
            cboe_TransactionDatabaseType3.SelectedIndex = 0;
        }
        #endregion

        #region 清空所有记录
        private void butClearAllTransactionDatabase_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType1.SelectedIndex;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Transaction.ClearTransactionDatabase.ClearTransactionDatabase_Parameter(Gete_TransactionDatabaseType(type));
            var cmd = new FC8800.Transaction.ClearTransactionDatabase.ClearTransactionDatabase(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }
        #endregion

        #region 判断记录类型
        private static FC8800.Transaction.e_TransactionDatabaseType Gete_TransactionDatabaseType(int type)
        {
            type = type + 1;
            var i = FC8800.Transaction.e_TransactionDatabaseType.OnCardTransaction;

            if (type == 2)
            {
                i = FC8800.Transaction.e_TransactionDatabaseType.OnButtonTransaction;
            }
            if (type == 3)
            {
                i = FC8800.Transaction.e_TransactionDatabaseType.OnDoorSensorTransaction;
            }
            if (type == 4)
            {
                i = FC8800.Transaction.e_TransactionDatabaseType.OnSoftwareTransaction;
            }
            if (type == 5)
            {
                i = FC8800.Transaction.e_TransactionDatabaseType.OnAlarmTransaction;
            }
            if (type == 6)
            {
                i = FC8800.Transaction.e_TransactionDatabaseType.OnSystemTransaction;
            }
            return i;
        }
        #endregion

        #region 上传记录尾号
        private void butTransactionDatabaseWriteIndex_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType1.SelectedIndex;
            int WriteIndex = int.Parse(txtWriteIndex.Text.ToString());
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Transaction.WriteTransactionDatabaseWriteIndex.WriteTransactionDatabaseWriteIndex_Parameter(Gete_TransactionDatabaseType(type), WriteIndex);
            var cmd = new FC8800.Transaction.WriteTransactionDatabaseWriteIndex.WriteTransactionDatabaseWriteIndex(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }
        #endregion

        #region 更新上传断点
        private void butTransactionDatabaseReadIndex_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType1.SelectedIndex;
            int ReadIndex = int.Parse(txtReadIndex.Text.ToString());
            bool IsCircle = cbIsCircle.Checked ? true : false;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Transaction.TransactionDatabaseReadIndex.WriteTransactionDatabaseReadIndex_Parameter(Gete_TransactionDatabaseType(type), ReadIndex, IsCircle);
            var cmd = new FC8800.Transaction.TransactionDatabaseReadIndex.WriteTransactionDatabaseReadIndex(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }
        #endregion

        #region 按序号采集信息
        private void butTransactionDatabaseByIndex_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType2.SelectedIndex;
            int Quantity = int.Parse(txtQuantity.Text.ToString());
            int ReadIndex = int.Parse(txtReadIndex0.Text.ToString());
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex_Parameter(Gete_TransactionDatabaseType(type), Quantity, ReadIndex);
            var cmd = new FC8800.Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }
        #endregion

        private void butTransactionDatabaseDetail_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var cmd = new FC8800.Transaction.TransactionDatabaseDetail.ReadTransactionDatabaseDetail(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Transaction.TransactionDatabaseDetail.ReadTransactionDatabaseDetail_Result;
                for (int i = 0; i < 6; i++)
                {
                    TextBox txtQuantity = FindControl(groupBox1, "txtQuantity" + (i + 1).ToString()) as TextBox;
                    TextBox txtNewRecord = FindControl(groupBox1, "txtNewRecord" + (i + 1).ToString()) as TextBox;
                    TextBox txtWriteIndex = FindControl(groupBox1, "txtWriteIndex" + (i + 1).ToString()) as TextBox;
                    TextBox txtReadIndex = FindControl(groupBox1, "txtReadIndex" + (i + 1).ToString()) as TextBox;
                    TextBox txtIsCircle = FindControl(groupBox1, "txtIsCircle" + (i + 1).ToString()) as TextBox;
                    Invoke(() =>
                    {
                        txtQuantity.Text = result.DatabaseDetail.ListTransaction[i].DataBaseMaxSize.ToString();
                        txtWriteIndex.Text = result.DatabaseDetail.ListTransaction[i].WriteIndex.ToString();
                        txtNewRecord.Text = result.DatabaseDetail.ListTransaction[i].readable().ToString();
                        txtReadIndex.Text = result.DatabaseDetail.ListTransaction[i].ReadIndex.ToString();
                        txtIsCircle.Text = result.DatabaseDetail.ListTransaction[i].IsCircle ? "【1、循环】" : "【0、未循环】";
                    });
                }
            };
        }

        public Control FindControl(Control parentControl, string findCtrlName)
        {
            Control _findedControl = null;
            if (!string.IsNullOrEmpty(findCtrlName) && parentControl != null)
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    if (ctrl.Name.Equals(findCtrlName))
                    {
                        _findedControl = ctrl;
                        break;
                    }
                }
            }
            return _findedControl;
        }

        private void BtnReadTransactionDatabase_Click(object sender, EventArgs e)
        {

        }

        private void ButClearTransactionDatabase_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Transaction.ClearTransactionDatabase.ClearTransactionDatabase_Parameter();
            var cmd = new FC8800.Transaction.ClearTransactionDatabase.ClearTransactionDatabase(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }
    }
}
