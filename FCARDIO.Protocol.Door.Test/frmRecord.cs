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
    public partial class frmRecord : Form
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
        private void butClearTransactionDatabase_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType1.SelectedIndex;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Transaction.ClearTransactionDatabase.ClearTransactionDatabase_Parameter(Gete_TransactionDatabaseType(type));
            var cmd = new FC8800.Transaction.ClearTransactionDatabase.ClearTransactionDatabase(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        } 
        #endregion

        #region 判断记录类型
        private static FC8800.Transaction.e_TransactionDatabaseType Gete_TransactionDatabaseType(int type)
        {
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
    }
}
