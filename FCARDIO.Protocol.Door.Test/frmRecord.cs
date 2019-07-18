using FCARDIO.Protocol.Door.FC8800.Data;
using FCARDIO.Protocol.Transaction;
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
        private string[] mWatchTypeNameList;
        private string[] mCardTransactionList, mButtonTransactionList, mDoorSensorTransactionList, mSoftwareTransactionList, mAlarmTransactionList, mSystemTransactionList;

        /// <summary>
        /// 初始化类型集合
        /// </summary>
        private void IniWatchEvent()
        {
            mWatchTypeNameList = new string[] { "", "读卡信息", "出门开关信息", "门磁信息", "远程开门信息", "报警信息", "系统信息", "连接保活消息", "连接确认信息" };
            mCardTransactionList = new string[256];
            mButtonTransactionList = new string[256];
            mDoorSensorTransactionList = new string[256];
            mSoftwareTransactionList = new string[256];
            mAlarmTransactionList = new string[256];
            mSystemTransactionList = new string[256];

            mCardTransactionList[1] = "合法开门";//
            mCardTransactionList[2] = "密码开门";//------------卡号为密码
            mCardTransactionList[3] = "卡加密码";//
            mCardTransactionList[4] = "手动输入卡加密码";//
            mCardTransactionList[5] = "首卡开门";//
            mCardTransactionList[6] = "门常开";//   ---  常开工作方式中，刷卡进入常开状态
            mCardTransactionList[7] = "多卡开门";//  --  多卡验证组合完毕后触发
            mCardTransactionList[8] = "重复读卡";//
            mCardTransactionList[9] = "有效期过期";//
            mCardTransactionList[10] = "开门时段过期";//
            mCardTransactionList[11] = "节假日无效";//
            mCardTransactionList[12] = "未注册卡";//
            mCardTransactionList[13] = "巡更卡";//  --  不开门
            mCardTransactionList[14] = "探测锁定";//
            mCardTransactionList[15] = "无有效次数";//
            mCardTransactionList[16] = "防潜回";//
            mCardTransactionList[17] = "密码错误";//------------卡号为错误密码
            mCardTransactionList[18] = "密码加卡模式密码错误";//----卡号为卡号。
            mCardTransactionList[19] = "锁定时(读卡)或(读卡加密码)开门";//
            mCardTransactionList[20] = "锁定时(密码开门)";//
            mCardTransactionList[21] = "首卡未开门";//
            mCardTransactionList[22] = "挂失卡";//
            mCardTransactionList[23] = "黑名单卡";//
            mCardTransactionList[24] = "门内上限已满，禁止入门。";//
            mCardTransactionList[25] = "开启防盗布防状态(设置卡)";//
            mCardTransactionList[26] = "撤销防盗布防状态(设置卡)";//
            mCardTransactionList[27] = "开启防盗布防状态(密码)";//
            mCardTransactionList[28] = "撤销防盗布防状态(密码)";//
            mCardTransactionList[29] = "互锁时(读卡)或(读卡加密码)开门";//
            mCardTransactionList[30] = "互锁时(密码开门)";//
            mCardTransactionList[31] = "全卡开门";//
            mCardTransactionList[32] = "多卡开门--等待下张卡";//
            mCardTransactionList[33] = "多卡开门--组合错误";//
            mCardTransactionList[34] = "非首卡时段刷卡无效";//
            mCardTransactionList[35] = "非首卡时段密码无效";//
            mCardTransactionList[36] = "禁止刷卡开门";//  --  【开门认证方式】验证模式中禁用了刷卡开门时
            mCardTransactionList[37] = "禁止密码开门";//  --  【开门认证方式】验证模式中禁用了密码开门时
            mCardTransactionList[38] = "门内已刷卡，等待门外刷卡。";//（门内外刷卡验证）
            mCardTransactionList[39] = "门外已刷卡，等待门内刷卡。";//（门内外刷卡验证）
            mCardTransactionList[40] = "请刷管理卡";//(在开启管理卡功能后提示)(电梯板)
            mCardTransactionList[41] = "请刷普通卡";//(在开启管理卡功能后提示)(电梯板)
            mCardTransactionList[42] = "首卡未读卡时禁止密码开门。";//
            mCardTransactionList[43] = "控制器已过期_刷卡";//
            mCardTransactionList[44] = "控制器已过期_密码";//
            mCardTransactionList[45] = "合法卡开门—有效期即将过期";//
            mCardTransactionList[46] = "拒绝开门--区域反潜回失去主机连接。";//
            mCardTransactionList[47] = "拒绝开门--区域互锁，失去主机连接";//
            mCardTransactionList[48] = "区域防潜回--拒绝开门";//
            mCardTransactionList[49] = "区域互锁--有门未关好，拒绝开门";//

            mButtonTransactionList[1] = "按钮开门";//
            mButtonTransactionList[2] = "开门时段过期";//
            mButtonTransactionList[3] = "锁定时按钮";//
            mButtonTransactionList[4] = "控制器已过期";//
            mButtonTransactionList[5] = "互锁时按钮(不开门)";//

            mDoorSensorTransactionList[1] = "开门";//
            mDoorSensorTransactionList[2] = "关门";//
            mDoorSensorTransactionList[3] = "进入门磁报警状态";//
            mDoorSensorTransactionList[4] = "退出门磁报警状态";//
            mDoorSensorTransactionList[5] = "门未关好";//

            mSoftwareTransactionList[1] = "软件开门";//
            mSoftwareTransactionList[2] = "软件关门";//
            mSoftwareTransactionList[3] = "软件常开";//
            mSoftwareTransactionList[4] = "控制器自动进入常开";//
            mSoftwareTransactionList[5] = "控制器自动关闭门";//
            mSoftwareTransactionList[6] = "长按出门按钮常开";//
            mSoftwareTransactionList[7] = "长按出门按钮常闭";//
            mSoftwareTransactionList[8] = "软件锁定";//
            mSoftwareTransactionList[9] = "软件解除锁定";//
            mSoftwareTransactionList[10] = "控制器定时锁定";//--到时间自动锁定
            mSoftwareTransactionList[11] = "控制器定时解除锁定";//--到时间自动解除锁定
            mSoftwareTransactionList[12] = "报警--锁定";//
            mSoftwareTransactionList[13] = "报警--解除锁定";//
            mSoftwareTransactionList[14] = "互锁时远程开门";//

            mAlarmTransactionList[1] = "门磁报警";//
            mAlarmTransactionList[2] = "匪警报警";//
            mAlarmTransactionList[3] = "消防报警";//
            mAlarmTransactionList[4] = "非法卡刷报警";//
            mAlarmTransactionList[5] = "胁迫报警";//
            mAlarmTransactionList[6] = "消防报警(命令通知)";//
            mAlarmTransactionList[7] = "烟雾报警";//
            mAlarmTransactionList[8] = "防盗报警";//
            mAlarmTransactionList[9] = "黑名单报警";//
            mAlarmTransactionList[10] = "开门超时报警";//
            mAlarmTransactionList[0x11] = "门磁报警撤销";//
            mAlarmTransactionList[0x12] = "匪警报警撤销";//
            mAlarmTransactionList[0x13] = "消防报警撤销";//
            mAlarmTransactionList[0x14] = "非法卡刷报警撤销";//
            mAlarmTransactionList[0x15] = "胁迫报警撤销";//
            mAlarmTransactionList[0x17] = "撤销烟雾报警";//
            mAlarmTransactionList[0x18] = "关闭防盗报警";//
            mAlarmTransactionList[0x19] = "关闭黑名单报警";//
            mAlarmTransactionList[0x1A] = "关闭开门超时报警";//
            mAlarmTransactionList[0x21] = "门磁报警撤销(命令通知)";//
            mAlarmTransactionList[0x22] = "匪警报警撤销(命令通知)";//
            mAlarmTransactionList[0x23] = "消防报警撤销(命令通知)";//
            mAlarmTransactionList[0x24] = "非法卡刷报警撤销(命令通知)";//
            mAlarmTransactionList[0x25] = "胁迫报警撤销(命令通知)";//
            mAlarmTransactionList[0x27] = "撤销烟雾报警(命令通知)";//
            mAlarmTransactionList[0x28] = "关闭防盗报警(软件关闭)";//
            mAlarmTransactionList[0x29] = "关闭黑名单报警(软件关闭)";//
            mAlarmTransactionList[0x2A] = "关闭开门超时报警";//

            mSystemTransactionList[1] = "系统加电";//
            mSystemTransactionList[2] = "系统错误复位（看门狗）";//
            mSystemTransactionList[3] = "设备格式化记录";//
            mSystemTransactionList[4] = "系统高温记录，温度大于>75";//
            mSystemTransactionList[5] = "系统UPS供电记录";//
            mSystemTransactionList[6] = "温度传感器损坏，温度大于>100";//
            mSystemTransactionList[7] = "电压过低，小于<09V";//
            mSystemTransactionList[8] = "电压过高，大于>14V";//
            mSystemTransactionList[9] = "读卡器接反。";//
            mSystemTransactionList[10] = "读卡器线路未接好。";//
            mSystemTransactionList[11] = "无法识别的读卡器";//
            mSystemTransactionList[12] = "电压恢复正常，小于14V，大于9V";//
            mSystemTransactionList[13] = "网线已断开";//
            mSystemTransactionList[14] = "网线已插入";//
        }

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
            IniWatchEvent();
        }

        #region 记录类型
        public void e_TransactionDatabaseType()
        {
            string[] array = new string[6] { "读卡记录", "按钮记录", "门磁记录", "远程记录", "报警记录", "系统记录" };
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
            var par = new FC8800.Transaction.ClearTransactionDatabase.ClearTransactionDatabase_Parameter();
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

        private void BtnReadTransactionDatabase_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType3.SelectedIndex;
            int Quantity = int.Parse(txtReadTransactionDatabaseQuantity.Text.ToString());
            int PacketSize = int.Parse(txtReadTransactionDatabasePacketSize.Text.ToString());
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Transaction.ReadTransactionDatabase.ReadTransactionDatabase_Parameter(Gete_TransactionDatabaseType(type), PacketSize, Quantity);

            if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC88)
            {
                //var cmd = new FC8800.Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex(cmdDtl, par);
                var cmd = new FC8800.Transaction.ReadTransactionDatabase.ReadTransactionDatabase(cmdDtl, par);

                mMainForm.AddCommand(cmd);
            }
            else
            {
                var cmd = new FC89H.Transaction.ReadTransactionDatabase.ReadTransactionDatabase(cmdDtl, par);
                mMainForm.AddCommand(cmd);
            }

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
                var result = cmde.Command.getResult() as FC8800.Transaction.ReadTransactionDatabase.ReadTransactionDatabase_Result;
                if (result.TransactionList.Count > 0)
                {
                    foreach (var transaction in result.TransactionList)
                    {

                        mMainForm.AddCmdLog(cmde, $"事件类型：{mWatchTypeNameList[transaction.TransactionType]}");
                        //mMainForm.AddCmdLog(cmde, $"序号：{item.SerialNumber}，事务类型：{item.TransactionType}，事务代码：{item.TransactionCode}，事务日期：{item.TransactionDate}");
                        mMainForm.AddCmdLog(cmde, PrintTransactionList(transaction));
                    }

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
            var par = new FC8800.Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex_Parameter((cboe_TransactionDatabaseType3.SelectedIndex + 1), ReadIndex, Quantity);

            if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC88)
            {
                var cmd = new FC8800.Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex(cmdDtl, par);

                mMainForm.AddCommand(cmd);
            }
            else
            {
                var cmd = new FC89H.Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex(cmdDtl, par);
                mMainForm.AddCommand(cmd);
            }

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
                var result = cmde.Command.getResult() as FC8800.Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex_Result;
                if (result.TransactionList.Count > 0)
                {
                    foreach (var transaction in result.TransactionList)
                    {

                        mMainForm.AddCmdLog(cmde, $"事件类型：{mWatchTypeNameList[transaction.TransactionType]}");
                        //mMainForm.AddCmdLog(cmde, $"序号：{item.SerialNumber}，事务类型：{item.TransactionType}，事务代码：{item.TransactionCode}，事务日期：{item.TransactionDate}");
                        mMainForm.AddCmdLog(cmde, PrintTransactionList(transaction));
                    }

                }
            };
        }

        private string PrintTransactionList(AbstractTransaction transaction)
        {
            string log = "";
            if (transaction.TransactionType == 1)
            {
                CardTransaction cardTrans = transaction as CardTransaction;
                log = $"序号：{cardTrans.SerialNumber.ToString()}，事务代码：{mCardTransactionList[cardTrans.TransactionCode]}，卡号：{cardTrans.CardData.ToString()}，门号：{cardTrans.DoorNum().ToString()}，时间：{cardTrans.TransactionDate}";
            }
            else if (transaction.TransactionType == 2)
            {
                ButtonTransaction buttonTrans = transaction as ButtonTransaction;
                log = $"序号：{transaction.SerialNumber}，事务代码：{mButtonTransactionList[buttonTrans.TransactionCode]}，事务日期：{transaction.TransactionDate}";
            }
            else
            {
                log = $"序号：{transaction.SerialNumber}，事务代码：{transaction.TransactionCode}，事务日期：{transaction.TransactionDate}";
            }
            return log;
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

        private void ButClearTransactionDatabase_Click(object sender, EventArgs e)
        {
            int type = cboe_TransactionDatabaseType2.SelectedIndex;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Transaction.ClearTransactionDatabase.ClearTransactionDatabase_Parameter(Gete_TransactionDatabaseType(type));
            var cmd = new FC8800.Transaction.ClearTransactionDatabase.ClearTransactionDatabase(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }
    }
}
