using FCARDIO.Protocol.Door.FC8800.Data;
using FCARDIO.Protocol.Transaction;
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
using FCARDIO.Protocol.Fingerprint.Transaction.ReadTransactionDatabaseByIndex;
using FCARDIO.Protocol.Door.FC8800.Transaction.ClearTransactionDatabase;
using FCARDIO.Protocol.Fingerprint.Transaction;
using FCARDIO.Protocol.Fingerprint.Transaction.ReadTransactionDatabase;

namespace FCARDIO.Protocol.Fingerprint.Test
{
    public partial class frmRecord : frmNodeForm
    {
        #region 事件类型初始化
        public static string[] mWatchTypeNameList;
        public static string[] mCardTransactionList, mDoorSensorTransactionList, mSystemTransactionList;
        /// <summary>
        /// 事件代码名称列表
        /// </summary>
        public static List<string[]> mTransactionCodeNameList;

        /// <summary>
        /// 初始化类型集合
        /// </summary>
        static frmRecord()
        {

            mWatchTypeNameList = new string[] { "", "读卡信息",  "门磁信息","系统信息", "连接保活消息", "连接确认信息" };
            mCardTransactionList = new string[256];
            mDoorSensorTransactionList = new string[256];
            mSystemTransactionList = new string[256];

            mTransactionCodeNameList = new List<string[]>(10);
            mTransactionCodeNameList.Add(null);//0是没有的
            mTransactionCodeNameList.Add(mCardTransactionList);
            mTransactionCodeNameList.Add(mDoorSensorTransactionList);
            mTransactionCodeNameList.Add(mSystemTransactionList);

            mCardTransactionList[1] = "刷卡验证";//
            mCardTransactionList[2] = "指纹验证";//------------卡号为密码
            mCardTransactionList[3] = "人脸验证";//
            mCardTransactionList[4] = "指纹 + 刷卡";//
            mCardTransactionList[5] = "人脸 + 指纹";//
            mCardTransactionList[6] = "人脸 + 刷卡";//   ---  常开工作方式中，刷卡进入常开状态
            mCardTransactionList[7] = "刷卡 + 密码";//  --  多卡验证组合完毕后触发
            mCardTransactionList[8] = "人脸 + 密码";//
            mCardTransactionList[9] = "指纹 + 密码";//
            mCardTransactionList[10] = "手动输入用户号加密码验证";//
            mCardTransactionList[11] = "指纹+刷卡+密码";//
            mCardTransactionList[12] = "人脸+刷卡+密码";//
            mCardTransactionList[13] = "人脸+指纹+密码";//  --  不开门
            mCardTransactionList[14] = "人脸+指纹+刷卡";//
            mCardTransactionList[15] = "重复验证";//
            mCardTransactionList[16] = "有效期过期";//
            mCardTransactionList[17] = "开门时段过期";//------------卡号为错误密码
            mCardTransactionList[18] = "节假日时不能开门";//----卡号为卡号。
            mCardTransactionList[19] = "未注册用户";//
            mCardTransactionList[20] = "探测锁定";//
            mCardTransactionList[21] = "有效次数已用尽";//
            mCardTransactionList[22] = "锁定时验证，禁止开门";//
            mCardTransactionList[23] = "挂失卡";//
            mCardTransactionList[24] = "黑名单卡";//
            mCardTransactionList[25] = "免验证开门 -- 按指纹时用户号为0，刷卡时用户号是卡号";//
            mCardTransactionList[26] = "禁止刷卡验证  --  【权限认证方式】中禁用刷卡时";//
            mCardTransactionList[27] = "禁止指纹验证  --  【权限认证方式】中禁用指纹时";//
            mCardTransactionList[28] = "控制器已过期";//
            mCardTransactionList[29] = "验证通过—有效期即将过期";//
           

            mDoorSensorTransactionList[1] = "开门";//
            mDoorSensorTransactionList[2] = "关门";//
            mDoorSensorTransactionList[3] = "进入门磁报警状态";//
            mDoorSensorTransactionList[4] = "退出门磁报警状态";//
            mDoorSensorTransactionList[5] = "门未关好";//
            mDoorSensorTransactionList[6] = "使用按钮开门";//
            mDoorSensorTransactionList[7] = "按钮开门时门已锁定";//
            mDoorSensorTransactionList[8] = "按钮开门时控制器已过期";//

            mSystemTransactionList[1] = "软件开门";//
            mSystemTransactionList[2] = "软件关门";//
            mSystemTransactionList[3] = "软件常开";//
            mSystemTransactionList[4] = "控制器自动进入常开";//
            mSystemTransactionList[5] = "控制器自动关闭门";//
            mSystemTransactionList[6] = "长按出门按钮常开";//
            mSystemTransactionList[7] = "长按出门按钮常闭";//
            mSystemTransactionList[8] = "软件锁定";//
            mSystemTransactionList[9] = "软件解除锁定";//
            mSystemTransactionList[10] = "控制器定时锁定--到时间自动锁定";//
            mSystemTransactionList[11] = "控制器定时锁定--到时间自动解除锁定";//
            mSystemTransactionList[12] = "报警--锁定";//
            mSystemTransactionList[13] = "报警--解除锁定";//
            mSystemTransactionList[14] = "非法认证报警";//
            mSystemTransactionList[15] = "门磁报警";//
            mSystemTransactionList[16] = "胁迫报警";//
            mSystemTransactionList[17] = "开门超时报警";//
            mSystemTransactionList[18] = "黑名单报警";//
            mSystemTransactionList[19] = "消防报警";//
            mSystemTransactionList[20] = "防拆报警";//
            mSystemTransactionList[21] = "非法认证报警解除";//
            mSystemTransactionList[22] = "门磁报警解除";//
            mSystemTransactionList[23] = "胁迫报警解除";//
            mSystemTransactionList[24] = "开门超时报警解除";//
            mSystemTransactionList[25] = "黑名单报警解除";//
            mSystemTransactionList[26] = "消防报警解除";//
            mSystemTransactionList[27] = "防拆报警解除";//
            mSystemTransactionList[28] = "系统加电";//
            mSystemTransactionList[29] = "系统错误复位（看门狗）";//
            mSystemTransactionList[30] = "设备格式化记录";//
            mSystemTransactionList[31] = "读卡器接反";//
            mSystemTransactionList[32] = "读卡器线路未接好";//
            mSystemTransactionList[33] = "无法识别的读卡器";//
            mSystemTransactionList[34] = "网线已断开";//
            mSystemTransactionList[35] = "网线已插入";//
            mSystemTransactionList[36] = "WIFI 已连接";//
            mSystemTransactionList[37] = "WIFI 已断开";//
        }

        #endregion

        #region 窗口单例模式
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
        #endregion

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
            string[] array = new string[] { "读卡记录", "门磁记录", "系统记录" };
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

        #region 清空所有记录
        private void butClearAllTransactionDatabase_Click(object sender, EventArgs e)
        {

            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new ClearTransactionDatabase_Parameter();
            var cmd = new ClearTransactionDatabase(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }
        #endregion

        #region 判断记录类型
        private static e_TransactionDatabaseType Get_TransactionDatabaseType(int type)
        {
            type = type + 1;
            var i = Transaction.e_TransactionDatabaseType.OnCardTransaction;


            if (type == 2)
            {
                i = Transaction.e_TransactionDatabaseType.OnDoorSensorTransaction;
            }

            if (type == 3)
            {
                i = Transaction.e_TransactionDatabaseType.OnSystemTransaction;
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
            var par = new Transaction.WriteTransactionDatabaseWriteIndex.WriteTransactionDatabaseWriteIndex_Parameter(Get_TransactionDatabaseType(type), WriteIndex);
            var cmd = new Transaction.WriteTransactionDatabaseWriteIndex.WriteTransactionDatabaseWriteIndex(cmdDtl, par);
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

            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new Transaction.TransactionDatabaseReadIndex.WriteTransactionDatabaseReadIndex_Parameter(
                Get_TransactionDatabaseType(type), ReadIndex);
            var cmd = new Transaction.TransactionDatabaseReadIndex.WriteTransactionDatabaseReadIndex(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
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

            var par = new ReadTransactionDatabase_Parameter((int)Get_TransactionDatabaseType(type), Quantity);
            if (PacketSize != 0)
            {
                par.PacketSize = PacketSize;
            }

            var cmd = new ReadTransactionDatabase(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {

                var result = cmde.Command.getResult() as Protocol.Door.FC8800.Transaction.ReadTransactionDatabase.ReadTransactionDatabase_Result;
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
        #endregion

        #region 按序号采集信息
        private void butTransactionDatabaseByIndex_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType3.SelectedIndex;
            int Quantity = int.Parse(txtQuantity.Text.ToString());
            int ReadIndex = int.Parse(txtReadIndex0.Text.ToString());
            var cmdDtl = mMainForm.GetCommandDetail();
            cmdDtl.Timeout = 2000;
            var par = new ReadTransactionDatabaseByIndex_Parameter((cboe_TransactionDatabaseType3.SelectedIndex + 1), ReadIndex, Quantity);

            var cmd = new ReadTransactionDatabaseByIndex(cmdDtl, par);

            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {

                var result = cmde.Command.getResult() as Protocol.Door.FC8800.Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex_Result;
                mMainForm.AddCmdLog(cmde, $"按序号读取成功，读取数量：{result.Quantity},实际解析数量：{result.TransactionList.Count}");

                if (result.Quantity > 0)
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
            if (tr.TransactionType == 3)//1-6
            {
                string[] codeNameList = mTransactionCodeNameList[3];
                sLogs.Append("(").Append(codeNameList[tr.TransactionCode]).Append(")");
            }
            else if (tr.TransactionType == 1)//读卡记录
            {
                Data.Transaction.CardTransaction cardTrans = tr as Data.Transaction.CardTransaction;
                sLogs.Append("用户号：").Append(cardTrans.UserCode).Append("，读卡器号：").Append(cardTrans.Reader).Append("，照片：").AppendLine(cardTrans.Photo == 1 ? "" : "");
            }
            else
            {
                if (tr.TransactionType >= 2 && tr.TransactionType <= 2)
                {
                    AbstractDoorTransaction doorTr = tr as AbstractDoorTransaction;
                    sLogs.Append("，门号：").Append(doorTr.Door).AppendLine();
                }
                else
                {
                    sLogs.AppendLine();
                }
            }
        }
        #endregion

        private void butTransactionDatabaseDetail_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var cmd = new Transaction.TransactionDatabaseDetail.ReadTransactionDatabaseDetail(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as Transaction.TransactionDatabaseDetail.ReadTransactionDatabaseDetail_Result;
                for (int i = 0; i < 3; i++)
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

        private void ButClearTransactionDatabase_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType2.SelectedIndex;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new Transaction.ClearTransactionDatabase.ClearTransactionDatabase_Parameter(Get_TransactionDatabaseType(type));
            var cmd = new Transaction.ClearTransactionDatabase.ClearTransactionDatabase(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }
    }
}
