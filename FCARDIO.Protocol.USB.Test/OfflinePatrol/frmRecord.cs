﻿using FCARDIO.Protocol.Transaction;
using FCARDIO.Protocol.USB.OfflinePatrol.Data.Transaction;
using FCARDIO.Core.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCARDIO.Protocol.USB.OfflinePatrol.Test
{
    public partial class frmRecord : frmNodeForm
    {
        #region 事件类型初始化
        public static string[] mWatchTypeNameList;
        public static string[] mCardTransactionList, mSystemTransactionList;
        /// <summary>
        /// 事件代码名称列表
        /// </summary>
        public static List<string[]> mTransactionCodeNameList;

        /// <summary>
        /// 初始化类型集合
        /// </summary>
        static frmRecord()
        {

            mWatchTypeNameList = new string[] { "","读卡信息", "系统信息" };
            mCardTransactionList = new string[256];
            mSystemTransactionList = new string[256];

            mTransactionCodeNameList = new List<string[]>(3);
            mTransactionCodeNameList.Add(null);//0是没有的
            mTransactionCodeNameList.Add(mCardTransactionList);
            mTransactionCodeNameList.Add(mSystemTransactionList);

            mCardTransactionList[1] = "";//

            mSystemTransactionList[1] = "";//

            mSystemTransactionList[1] = "开机";//
            mSystemTransactionList[2] = "初始化记录";//
            mSystemTransactionList[3] = "开始充电";//
            mSystemTransactionList[4] = "结束充电";//
            mSystemTransactionList[5] = "电压过低  2.9V";
            mSystemTransactionList[6] = "电压过高  4.5V";
            mSystemTransactionList[7] = "电压恢复正常 3.2V";
            mSystemTransactionList[8] = "清空巡更人员";
            mSystemTransactionList[9] = "添加巡更人员";
            mSystemTransactionList[10] = "删除巡更人员";
            mSystemTransactionList[11] = "修改巡更人员";
            mSystemTransactionList[12] = "读取读卡记录";
            mSystemTransactionList[13] = "读取系统记录";
            mSystemTransactionList[14] = "清空读卡记录";
            mSystemTransactionList[15] = "清空系统记录";
        }

        #endregion

        #region 记录类型
        public void e_TransactionDatabaseType()
        {
            string[] array = new string[2] { "读卡记录", "系统记录" };
            cboe_TransactionDatabaseType1.Items.Clear();
            cboe_TransactionDatabaseType1.Items.AddRange(array);
            cboe_TransactionDatabaseType1.SelectedIndex = 0;

            cboe_TransactionDatabaseType2.Items.Clear();
            cboe_TransactionDatabaseType2.Items.AddRange(array);
            cboe_TransactionDatabaseType2.SelectedIndex = 0;

            cboe_TransactionDatabaseType3.Items.Clear();
            cboe_TransactionDatabaseType3.Items.AddRange(array);
            cboe_TransactionDatabaseType3.SelectedIndex = 0;
        }
        #endregion


        #region 单例模式
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
                        FrmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        private frmRecord(INMain main) : base(main)
        {
            InitializeComponent();
        }
        #endregion


        private void ButTransactionDatabaseDetail_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var cmd = new Transaction.TransactionDatabaseDetail.ReadTransactionDatabaseDetail(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as Transaction.TransactionDatabaseDetail.ReadTransactionDatabaseDetail_Result;
                for (int i = 0; i < 2; i++)
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

        private void ButTransactionDatabaseWriteIndex_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType1.SelectedIndex;
            int WriteIndex = int.Parse(txtWriteIndex.Text.ToString());
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new Transaction.WriteTransactionDatabaseWriteIndex.WriteTransactionDatabaseWriteIndex_Parameter(Gete_TransactionDatabaseType(type), WriteIndex);
            var cmd = new Transaction.WriteTransactionDatabaseWriteIndex.WriteTransactionDatabaseWriteIndex(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }

        private void ButTransactionDatabaseReadIndex_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType1.SelectedIndex;
            int ReadIndex = int.Parse(txtReadIndex.Text.ToString());
            bool IsCircle = cbIsCircle.Checked ? true : false;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new Transaction.WriteTransactionDatabaseReadIndex.WriteTransactionDatabaseReadIndex_Parameter(Gete_TransactionDatabaseType(type), ReadIndex, IsCircle);
            var cmd = new Transaction.WriteTransactionDatabaseReadIndex.WriteTransactionDatabaseReadIndex(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }

        private void ButClearTransactionDatabase_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType2.SelectedIndex;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new Transaction.ClearTransactionDatabase.ClearTransactionDatabase_Parameter(Gete_TransactionDatabaseType(type));
            var cmd = new Transaction.ClearTransactionDatabase.ClearTransactionDatabase(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }

        private void ButTransactionDatabaseByIndex_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType3.SelectedIndex;
            int Quantity = int.Parse(txtQuantity.Text.ToString());
            int ReadIndex = int.Parse(txtReadIndex0.Text.ToString());
            var cmdDtl = mMainForm.GetCommandDetail();
            cmdDtl.Timeout = 2000;
            var par = new Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex_Parameter((cboe_TransactionDatabaseType3.SelectedIndex + 1), ReadIndex, Quantity);


            var cmd = new Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex(cmdDtl, par);

            mMainForm.AddCommand(cmd);



            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {

                var result = cmde.Command.getResult() as Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex_Result;
                mMainForm.AddCmdLog(cmde, $"按序号读取成功，读取数量：{result.Quantity},实际解析数量：{result.TransactionList.Count}");

                if (result.TransactionList.Count > 0)
                {
                    StringBuilder sLogs = new StringBuilder(result.TransactionList.Count * 100);

                    sLogs.AppendLine($"事件类型：{mWatchTypeNameList[result.TransactionList[0].TransactionType]}");
                    sLogs.Append("读取计数：").Append(result.Quantity).Append("；实际数量：").Append(result.TransactionList.Count).AppendLine();

                    foreach (var t in result.TransactionList)
                    {

                        PrintTransactionList(t, sLogs);
                    }
                    string sFile = SaveFile(sLogs, $"按序号读取记录_{DateTime.Now:yyyyMMddHHmmss}.txt");

                    mMainForm.AddCmdLog(cmde, $"记录在保存文件：{sFile}");

                }
            };
        }


        public static string SaveFile(StringBuilder sLogs, string sFileName)
        {
            string sPath = System.IO.Path.Combine(Application.StartupPath, "记录日志");
            if (!System.IO.Directory.Exists(sPath))
                System.IO.Directory.CreateDirectory(sPath);

            string sFile = System.IO.Path.Combine(sPath, $"按序号读取记录_{DateTime.Now:yyyyMMddHHmmss}.txt");

            System.IO.File.WriteAllText(sFile, sLogs.ToString(), Encoding.UTF8);
            return sFile;
        }

        private void PrintTransactionList(AbstractTransaction tr, StringBuilder sLogs)
        {

            sLogs.Append("序号：").Append(tr.SerialNumber.ToString());
            if (tr.IsNull())
            {
                sLogs.AppendLine(" --- 空记录");
                return;
            }
            sLogs.Append("，时间：").Append(tr.TransactionDate.ToDateTimeStr());
            sLogs.Append("，事件代码：").Append(tr.TransactionCode);
            if (tr.TransactionType == 2)//
            {
                string[] codeNameList = mTransactionCodeNameList[tr.TransactionType];
                sLogs.Append("(").Append(codeNameList[tr.TransactionCode]).Append(")");
            }
            if (tr.TransactionType == 1)//读卡记录
            {
                CardTransaction cardTrans = tr as CardTransaction;
                sLogs.Append("卡号：").Append(cardTrans.CardData).Append("，工号：").Append(cardTrans.PCode).Append("，状态：").AppendLine(cardTrans.State == 0 ? "普通卡" : "巡更人员卡");
            }
            else
            {
                sLogs.AppendLine();

            }
        }

        private void BtnReadTransactionDatabase_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType3.SelectedIndex;
            int Quantity = int.Parse(txtReadTransactionDatabaseQuantity.Text.ToString());
            int PacketSize = 0;
            if (txtReadTransactionDatabasePacketSize.Text != "")
            {
                PacketSize = int.Parse(txtReadTransactionDatabasePacketSize.Text.ToString());
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            cmdDtl.Timeout = 1000;
            cmdDtl.RestartCount = 20;

            var par = new Transaction.ReadTransactionDatabase.ReadTransactionDatabase_Parameter(Gete_TransactionDatabaseType(type), Quantity);
            if (PacketSize != 0)
            {
                par.PacketSize = PacketSize;
            }
            var cmd = new Transaction.ReadTransactionDatabase.ReadTransactionDatabase(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {

                var result = cmde.Command.getResult() as Transaction.ReadTransactionDatabase.ReadTransactionDatabase_Result;
                mMainForm.AddCmdLog(cmde, $"读取成功，读取数量：{result.Quantity},实际解析数量：{result.TransactionList.Count},剩余新记录数：{result.readable}");

                if (result.TransactionList.Count > 0)
                {
                    StringBuilder sLogs = new StringBuilder(result.TransactionList.Count * 100);
                    sLogs.AppendLine($"事件类型：{mWatchTypeNameList[result.TransactionList[0].TransactionType]}");
                    sLogs.Append("读取计数：").Append(result.Quantity).Append("；实际数量：").Append(result.TransactionList.Count).Append("；剩余新记录数：").Append(result.readable).AppendLine();

                    //按序号排序
                    result.TransactionList.Sort((x, y) => x.SerialNumber.CompareTo(y.SerialNumber));
                    foreach (var t in result.TransactionList)
                    {
                        PrintTransactionList(t, sLogs);
                    }
                    string sFile = SaveFile(sLogs, $"读取记录_{DateTime.Now:yyyyMMddHHmmss}.txt");
                    mMainForm.AddCmdLog(cmde, $"记录在保存文件：{sFile}");
                }
            };
        }

        #region 清空所有记录
        private void ButClearAllTransactionDatabase_Click(object sender, EventArgs e)
        {

            var cmdDtl = mMainForm.GetCommandDetail();
            var cmd = new Transaction.ClearTransactionDatabase.TransactionDatabaseEmpty(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }
        #endregion

        #region 判断记录类型
        private static Transaction.e_TransactionDatabaseType Gete_TransactionDatabaseType(int type)
        {
            type = type + 1;
            var i = Transaction.e_TransactionDatabaseType.OnCardTransaction;


            if (type == 2)
            {
                i = Transaction.e_TransactionDatabaseType.OnSystemTransaction;
            }
            return i;
        }
        #endregion


        private void FrmRecord_Load(object sender, EventArgs e)
        {
            e_TransactionDatabaseType();
        }

        private void FrmRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            onlyObj = null;
        }
    }
}
