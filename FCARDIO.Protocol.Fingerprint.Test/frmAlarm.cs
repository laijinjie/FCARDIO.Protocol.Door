using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;
using FCARDIO.Protocol.Door.FC8800.Utility;
using FCARDIO.Protocol.Fingerprint.Alarm.AlarmPassword;
using FCARDIO.Protocol.Fingerprint.Alarm.AntiDisassemblyAlarm;
using FCARDIO.Protocol.Fingerprint.Alarm.BlacklistAlarm;
using FCARDIO.Protocol.Fingerprint.Alarm.CloseAlarm;
using FCARDIO.Protocol.Fingerprint.Alarm.GateMagneticAlarm;
using FCARDIO.Protocol.Fingerprint.Alarm.IllegalVerificationAlarm;
using FCARDIO.Protocol.Fingerprint.Alarm.LegalVerificationCloseAlarm;
using FCARDIO.Protocol.Fingerprint.Alarm.OpenDoorTimeoutAlarm;
using FCARDIO.Protocol.Fingerprint.Alarm.SendFireAlarm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCARDIO.Protocol.Fingerprint.Test
{
    public partial class frmAlarm : frmNodeForm
    {
        #region 单例模式
        private static object lockobj = new object();
        private static frmAlarm onlyObj;
        public static frmAlarm GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmAlarm(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }
        WeekTimeGroup WeekTimeGroupDoorWorkDto;
        private frmAlarm(INMain main) : base(main)
        {
            InitializeComponent();
            WeekTimeGroupDoorWorkDto = new WeekTimeGroup(8);
            WeekTimeGroupDoorWorkDto.InitTimeGroup();
        }
        #endregion

        string[] modeList = new string[] { "不开门，报警输出", "开门，报警输出", "锁定门，报警，只能软件解锁" };
        private void FrmAlarm_Load(object sender, EventArgs e)
        {
            string[] times = new string[256];
            string[] timeout = new string[257];
            for (int i = 0; i < 256; i++)
            {
                times[i] = i.ToString();
                timeout[i] = i.ToString() + "秒";
            }
            timeout[0] = "禁用";
            timeout[256] = "65535秒";
            cmbIllegalVerificationTimes.Items.AddRange(times);
            cmbIllegalVerificationTimes.SelectedIndex = 0;

            cmbAlarmPasswordMode.Items.AddRange(modeList);
            cmbAlarmPasswordMode.SelectedIndex = 0;

            cmbOpenDoorTimeout.Items.AddRange(timeout);
            cmbOpenDoorTimeout.SelectedIndex = 0;
        }

        private void FrmAlarm_FormClosed(object sender, FormClosedEventArgs e)
        {
            onlyObj = null;
        }

        private void BtnSendFireAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            WriteSendFireAlarm write = new WriteSendFireAlarm(cmdDtl);
            mMainForm.AddCommand(write);
        }

        private void BtnReadBlacklistAlarm_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadBlacklistAlarm cmd = new ReadBlacklistAlarm(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadBlacklistAlarm_Result result = cmde.Command.getResult() as ReadBlacklistAlarm_Result;

                sb.Clear();
                sb.Append("黑名单报警 ");
                sb.Append(result.IsAlarm ? "是否启用：【1、启用】，" : "是否启用：【0、不启用】，");

                Invoke(() =>
                {
                    cbBlacklistAlarmUse.Checked = result.IsAlarm;
                });

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteBlacklistAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            WriteBlacklistAlarm_Parameter par = new WriteBlacklistAlarm_Parameter(cbBlacklistAlarmUse.Checked);
            WriteBlacklistAlarm write = new WriteBlacklistAlarm(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void BtnReadAntiDisassemblyAlarm_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadAntiDisassemblyAlarm cmd = new ReadAntiDisassemblyAlarm(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadAntiDisassemblyAlarm_Result result = cmde.Command.getResult() as ReadAntiDisassemblyAlarm_Result;

                sb.Clear();
                sb.Append("防拆报警开关 ");
                sb.Append(result.IsUse ? "是否启用：【1、启用】，" : "是否启用：【0、不启用】，");

                Invoke(() =>
                {
                    cbAntiDisassemblyUse.Checked = result.IsUse;
                });

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteAntiDisassemblyAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            WriteAntiDisassemblyAlarm_Parameter par = new WriteAntiDisassemblyAlarm_Parameter(cbAntiDisassemblyUse.Checked);
            WriteAntiDisassemblyAlarm write = new WriteAntiDisassemblyAlarm(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void BtnReadIllegalVerification_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadIllegalVerificationAlarm cmd = new ReadIllegalVerificationAlarm(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadIllegalVerificationAlarm_Result result = cmde.Command.getResult() as ReadIllegalVerificationAlarm_Result;

                sb.Clear();
                sb.Append("非法验证报警 ");
                sb.Append(result.IsUse ? "是否启用：【1、启用】，" : "是否启用：【0、不启用】，");

                Invoke(() =>
                {
                    cbIllegalVerificationAlarmUse.Checked = result.IsUse;
                });

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteIllegalVerification_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            WriteIllegalVerificationAlarm_Parameter par = new WriteIllegalVerificationAlarm_Parameter(cbIllegalVerificationAlarmUse.Checked
                , (byte)(cmbIllegalVerificationTimes.SelectedIndex));
            WriteIllegalVerificationAlarm write = new WriteIllegalVerificationAlarm(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void BtnReadAlarmPassword_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadAlarmPassword cmd = new ReadAlarmPassword(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadAlarmPassword_Result result = cmde.Command.getResult() as ReadAlarmPassword_Result;

                sb.Clear();
                sb.Append("胁迫报警密码 ");
                sb.Append(result.Use ? "是否启用：【1、启用】，" : "是否启用：【0、不启用】，");

                Invoke(() =>
                {
                    cbAlarmPasswordUse.Checked = result.Use;
                    cmbAlarmPasswordMode.SelectedIndex = result.AlarmOption - 1;
                    txtAlarmPassword.Text = result.Password;
                });

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteAlarmPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            WriteAlarmPassword_Parameter par = new WriteAlarmPassword_Parameter(cbAlarmPasswordUse.Checked, txtAlarmPassword.Text
                , Convert.ToByte(cmbAlarmPasswordMode.SelectedIndex + 1));
            WriteAlarmPassword write = new WriteAlarmPassword(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void BtnReadOpenDoorTimeoutAlarm_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadOpenDoorTimeoutAlarm cmd = new ReadOpenDoorTimeoutAlarm(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadOpenDoorTimeoutAlarm_Result result = cmde.Command.getResult() as ReadOpenDoorTimeoutAlarm_Result;

                sb.Clear();
                sb.Append("开门超时提示参数 ");
                sb.Append(result.IsUse ? "是否启用：【1、启用】，" : "是否启用：【0、不启用】，");

                Invoke(() =>
                {
                    cbOpenDoorTimeoutAlarmUse.Checked = result.IsUse;
                    if (result.AllowTime > 255)
                    {
                        cmbOpenDoorTimeout.SelectedIndex = cmbOpenDoorTimeout.Items.Count - 1;
                    }
                    else
                    {
                        cmbOpenDoorTimeout.SelectedIndex = result.AllowTime;
                    }
                    cbRelayOutput.Checked = cbRelayOutput.Checked;
                });

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteOpenDoorTimeoutAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ushort timeout = (ushort)cmbOpenDoorTimeout.SelectedIndex;
            if (cmbOpenDoorTimeout.SelectedIndex == cmbOpenDoorTimeout.Items.Count)
            {
                timeout = 65535;
            }
            WriteOpenDoorTimeoutAlarm_Parameter par = new WriteOpenDoorTimeoutAlarm_Parameter(cbOpenDoorTimeoutAlarmUse.Checked, timeout, cbRelayOutput.Checked);
            WriteOpenDoorTimeoutAlarm write = new WriteOpenDoorTimeoutAlarm(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void RBtnNoDoorWorkSetting_CheckedChanged(object sender, EventArgs e)
        {
            DoorOpenTimePanel.Visible = false;
            if (cbxWeek.SelectedIndex == -1)
            {
                cbxWeek.SelectedIndex = 0;
            }
        }

        private void RBtnDoorWorkSetting_CheckedChanged(object sender, EventArgs e)
        {
            DoorOpenTimePanel.Visible = true;
            if (cbxWeek.SelectedIndex == -1)
            {
                cbxWeek.SelectedIndex = 0;
            }
        }

        private void BtnReadGateMagneticAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadGateMagneticAlarm cmd = new ReadGateMagneticAlarm(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadGateMagneticAlarm_Result result = cmde.Command.getResult() as ReadGateMagneticAlarm_Result;
                StringBuilder sb = new StringBuilder();
                string OpenDoorWayStr = string.Empty;
                string DoorTriggerModeStr = string.Empty;
                if (!result.IsUse)
                {
                    sb.AppendLine("门磁报警 是否启用：【0、不启用】");
                    mMainForm.AddCmdLog(cmde, sb.ToString());
                }
                else
                {
                    for (int i = 0; i < 7; i++)
                    {
                        sb.Append("\r\n");
                        sb.Append(StringUtility.GetWeekStr(i));
                        for (int j = 0; j < 8; j++)
                        {
                            sb.Append("  时段" + (j + 1) + "：" + StringUtility.TimeHourAndMinuteStr(result.WeekTimeGroup.GetItem(i).GetItem(j).GetBeginTime(), result.WeekTimeGroup.GetItem(i).GetItem(j).GetEndTime()));
                        }
                        // mMainForm.AddCmdLog(null, sb.ToString());
                    }
                    mMainForm.AddCmdLog(cmde, sb.ToString());
                }
                Invoke(() =>
                {
                    if (!result.IsUse)
                    {
                        rBtnNoDoorWorkSetting.Checked = true;
                    }
                    else
                    {
                        rBtnDoorWorkSetting.Checked = true;

                        WeekTimeGroupDoorWorkDto = result.WeekTimeGroup;
                        SetAllTimePicker(DoorOpenTimePanel, "beginTimePicker", "endTimePicker", WeekTimeGroupDoorWorkDto.GetItem(0));
                    }
                });
            };
        }

        private void BtnWriteGateMagneticAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            WriteGateMagneticAlarm_Parameter par = new WriteGateMagneticAlarm_Parameter(rBtnDoorWorkSetting.Checked, WeekTimeGroupDoorWorkDto);
            WriteGateMagneticAlarm write = new WriteGateMagneticAlarm(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void CbxWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WeekTimeGroupDoorWorkDto != null)
            {
                var day = WeekTimeGroupDoorWorkDto.GetItem(cbxWeek.SelectedIndex);
                SetAllTimePicker(DoorOpenTimePanel, "beginTimePicker", "endTimePicker", day);
            }
        }

        private void BtnWriteCloseAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            byte[] list = new byte[7];
            list[0] = Convert.ToByte(checkBox1.Checked ? 1 : 0);
            list[1] = Convert.ToByte(checkBox2.Checked ? 1 : 0);
            list[2] = Convert.ToByte(checkBox3.Checked ? 1 : 0);
            list[3] = Convert.ToByte(checkBox4.Checked ? 1 : 0);
            list[4] = Convert.ToByte(checkBox5.Checked ? 1 : 0);
            list[5] = Convert.ToByte(checkBox6.Checked ? 1 : 0);
            list[6] = Convert.ToByte(checkBox7.Checked ? 1 : 0);

            WriteCloseAlarm_Parameter par = new WriteCloseAlarm_Parameter(list);
            WriteCloseAlarm write = new WriteCloseAlarm(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void BtnReadLegalVerificationCloseAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadLegalVerificationCloseAlarm cmd = new ReadLegalVerificationCloseAlarm(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                StringBuilder sb = new StringBuilder();
                ReadLegalVerificationCloseAlarm_Result result = cmde.Command.getResult() as ReadLegalVerificationCloseAlarm_Result;

                sb.Clear();
                sb.Append("合法验证解除报警开关 ");
                sb.Append(result.IsUse ? "是否启用：【1、启用】，" : "是否启用：【0、不启用】，");

                Invoke(() =>
                {
                    cbLegalVerificationCloseAlarmUse.Checked = result.IsUse;
                });

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteLegalVerificationCloseAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            WriteLegalVerificationCloseAlarm_Parameter par = new WriteLegalVerificationCloseAlarm_Parameter(cbLegalVerificationCloseAlarmUse.Checked);
            WriteLegalVerificationCloseAlarm write = new WriteLegalVerificationCloseAlarm(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void BtnFillNowTime_Click(object sender, EventArgs e)
        {
            //开门时段

            WeekTimeGroup weekTimeGroup = new WeekTimeGroup(8);
            //weekTimeGroup.mDay
            //星期一 至 星期日
            for (int y = 0; y < 7; y++)
            {
                DayTimeGroup dayTimeGroup = weekTimeGroup.GetItem(y);
                //每天时段
                for (int i = 0; i < 8; i++)
                {
                    DateTime dtBegin = DateTime.Now;
                    DateTime dtEnd = DateTime.Now;
                    //dt = dt.AddMinutes(-1);
                    TimeSegment segment = dayTimeGroup.GetItem(i);

                    dtBegin = dtBegin.AddMinutes(i + 1);
                    segment.SetBeginTime(dtBegin.Hour, dtBegin.Minute);
                    dtEnd = dtBegin.AddMinutes(1);
                    segment.SetEndTime(dtEnd.Hour, dtEnd.Minute);
                    DateTimePicker beginTimePicker = FindControl(DoorOpenTimePanel, "beginTimePicker" + (i + 1).ToString()) as DateTimePicker;
                    DateTimePicker endTimePicker = FindControl(DoorOpenTimePanel, "endTimePicker" + (i + 1).ToString()) as DateTimePicker;
                    beginTimePicker.Value = dtBegin;// segment.GetBeginTime();
                    endTimePicker.Value = dtEnd;// segment.GetEndTime();
                }
            }
        }

        private void BeginTP_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            SetWeekTimeGroupValue(WeekTimeGroupDoorWorkDto, cbxWeek.SelectedIndex, int.Parse(dtp.Name.Substring(15)) - 1, 1, dtp.Value);
        }

        private void EndTP_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            SetWeekTimeGroupValue(WeekTimeGroupDoorWorkDto, cbxWeek.SelectedIndex, int.Parse(dtp.Name.Substring(13)) - 1, 2, dtp.Value);
        }
    }
}
