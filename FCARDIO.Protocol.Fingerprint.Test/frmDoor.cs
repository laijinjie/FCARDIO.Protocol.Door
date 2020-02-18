using DoNetDrive.Protocol.Door.Door8800.Data.TimeGroup;
using DoNetDrive.Protocol.Door.Door8800.Utility;
using DoNetDrive.Protocol.Fingerprint.Door.DoorWorkSetting;
using DoNetDrive.Protocol.Fingerprint.Door.ExemptionVerificationOpen;
using DoNetDrive.Protocol.Fingerprint.Door.ExpirationPrompt;
using DoNetDrive.Protocol.Fingerprint.Door.ReaderIntervalTime;
using DoNetDrive.Protocol.Fingerprint.Door.ReaderOption;
using DoNetDrive.Protocol.Fingerprint.Door.RelayOption;
using DoNetDrive.Protocol.Fingerprint.Door.RelayReleaseTime;
using DoNetDrive.Protocol.Fingerprint.Door.Remote;
using DoNetDrive.Protocol.Fingerprint.Door.VoiceBroadcastSetting;
using System;
using System.Text;
using System.Windows.Forms;

namespace DoNetDrive.Protocol.Fingerprint.Test
{
    public partial class frmDoor : frmNodeForm
    {
        #region 单例模式
        private static object lockobj = new object();
        private static frmDoor onlyObj;
        public static frmDoor GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmDoor(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        private frmDoor(INMain main) : base(main)
        {
            InitializeComponent();
            WeekTimeGroupDoorWorkDto = new WeekTimeGroup(8);
            WeekTimeGroupDoorWorkDto.InitTimeGroup();
        }
        #endregion

        WeekTimeGroup WeekTimeGroupDoorWorkDto;
        private void BtnReadReaderOption_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadReaderOption cmd = new ReadReaderOption(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReaderOption_Result result = cmde.Command.getResult() as ReaderOption_Result;

                sb.Clear();
                sb.Append("读卡器字节数 ");
                sb.Append($"【{result.ReaderOption}】、"+ str[result.ReaderOption - 1]);

                Invoke(() =>
                {
                    cbxReaderOption.SelectedIndex = result.ReaderOption - 1;
                });

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteReaderOption_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            ReaderOption_Parameter par = new ReaderOption_Parameter( Convert.ToByte(cbxReaderOption.SelectedIndex + 1));
            WriteReaderOption write = new WriteReaderOption(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void BtnReadRelayOption_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadRelayOption cmd = new ReadRelayOption(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                RelayOption_Result result = cmde.Command.getResult() as RelayOption_Result;

                sb.Clear();
                sb.Append("继电器参数 ");
                sb.Append(result.IsSupport ? "是否支持：【1、支持】，" : "是否支持：【2、不支持】，");
                Invoke(() =>
                {
                    if (result.IsSupport)
                        cbxReaderOption.SelectedIndex = 0;
                    else
                        cbxReaderOption.SelectedIndex = 1;
                });

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteRelayOption_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            RelayOption_Parameter par = new RelayOption_Parameter(cbxRelayOption.SelectedIndex == 0);
            WriteRelayOption write = new WriteRelayOption(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        string[] str = new string[5] { "三字节", "四字节", "二字节", "禁用", "八字节" };
        string[] doorTriggerModeList = new string[] { "合法认证通过后在指定时段内即可常开"
                , "授权中标记为常开特权的在指定时段内认证通过即可常开", "自动开关，到时间自动开关门" };
        string[] modeList = new string[] { "记录读卡，不开门，有提示", "不记录读卡，不开门，有提示", "不做响应，无提示" };
        private void FrmDoor_Load(object sender, EventArgs e)
        {

            cbxReaderOption.Items.Clear();
            cbxReaderOption.Items.AddRange(str);
            cbxReaderOption.SelectedIndex = 0;

            cbxReleaseTime.Items.Clear();
            string[] time = new string[255];
            string[] checknum = new string[254];
            string[] intervaTime = new string[255];
            string[] promptTime = new string[256];
            time[0] = "0.5";
            for (int i = 1; i < 254; i++)
            {
                time[i] = i.ToString();
                checknum[i - 1] = i.ToString();
                intervaTime[i - 1] = (i - 1).ToString() + "秒";
                promptTime[i - 1] = (i - 1).ToString();
            }
            promptTime[253] = "253";
            promptTime[254] = "254";
            promptTime[255] = "255";

            time[254] = "65535";
            checknum[253] = "254";
            intervaTime[254] = "65535秒";
            intervaTime[253] = "254秒";
            cbxReleaseTime.Items.AddRange(time);
            cbxReleaseTime.SelectedIndex = 0;

            cmbCheckNum.Items.AddRange(checknum);
            cmbCheckNum.SelectedIndex = 0;

            cmbDoorTriggerMode.Items.AddRange(doorTriggerModeList);
            cmbDoorTriggerMode.SelectedIndex = 0;

            cmbIntervalTime.Items.AddRange(intervaTime);
            cmbIntervalTime.SelectedIndex = 0;

            cmbExpirationPromptTime.Items.AddRange(promptTime);
            cmbExpirationPromptTime.SelectedIndex = 0;

            cmbMode.Items.AddRange(modeList);
            cmbMode.SelectedIndex = 0;
        }

        private void FrmDoor_FormClosed(object sender, FormClosedEventArgs e)
        {
            onlyObj = null;
        }

        private void BtnReadRelayReleaseTime_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            ushort ReleaseTime = 0; //开锁时输出时长
            string tip = string.Empty;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadUnlockingTime cmd = new ReadUnlockingTime(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadUnlockingTime_Result result = cmde.Command.getResult() as ReadUnlockingTime_Result;

                ReleaseTime = result.ReleaseTime; //开锁时输出时长
                sb.Clear();
                sb.AppendLine("开锁时输出时长：");
                if (ReleaseTime == 0)
                {
                    tip = "0.5秒";
                }
                else
                {
                    tip = ReleaseTime + "秒";
                }

                Invoke(() =>
                {
                    cbxReleaseTime.SelectedIndex = ReleaseTime;
                });
                sb.AppendLine(tip);
                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteRelayReleaseTime_Click(object sender, EventArgs e)
        {

            ushort releaseTime = 0;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            //if (cbxReleaseTime.Text != "0.5")
            //{
            //    releaseTime = Convert.ToUInt16(cbxReleaseTime.Text);
            //}
            releaseTime = Convert.ToUInt16(cbxReleaseTime.SelectedIndex);
            WriteUnlockingTime_Parameter par = new WriteUnlockingTime_Parameter(releaseTime);
            WriteUnlockingTime write = new WriteUnlockingTime(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void BtnOpenDoor_CheckNum_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            Remote_CheckNum_Parameter par = new Remote_CheckNum_Parameter(Convert.ToByte(cmbCheckNum.SelectedItem));
            OpenDoor_CheckNum cmd = new OpenDoor_CheckNum(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void BtnOpenDoor_Click(object sender, EventArgs e)
        {

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            OpenDoor cmd = new OpenDoor(cmdDtl);
            mMainForm.AddCommand(cmd);
        }

        private void BtnCloseDoor_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            CloseDoor cmd = new CloseDoor(cmdDtl);
            mMainForm.AddCommand(cmd);
        }

        private void BtnHoldOpenDoor_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            HoldDoor cmd = new HoldDoor(cmdDtl);
            mMainForm.AddCommand(cmd);
        }

        private void BtnLockDoor_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            LockDoor cmd = new LockDoor(cmdDtl);
            mMainForm.AddCommand(cmd);
        }

        private void BtnUnLockDoor_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            UnlockDoor cmd = new UnlockDoor(cmdDtl);
            mMainForm.AddCommand(cmd);
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

        private void BtnReadWorkSetting_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadDoorWorkSetting cmd = new ReadDoorWorkSetting(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                DoorWorkSetting_Result result = cmde.Command.getResult() as DoorWorkSetting_Result;
                StringBuilder sb = new StringBuilder();
                string OpenDoorWayStr = string.Empty;
                string DoorTriggerModeStr = string.Empty;
                if (!result.Use)
                {
                    sb.AppendLine("是否启用：【0、不启用】");
                    mMainForm.AddCmdLog(cmde, sb.ToString());
                }
                else
                {

                    DoorTriggerModeStr = "门常开触发模式：" + "【" + result.DoorTriggerMode.ToString() + "、" + doorTriggerModeList[result.DoorTriggerMode - 1] + "】";

                    for (int i = 0; i < 7; i++)
                    {
                        sb.Append("\r\n");
                        sb.Append(StringUtility.GetWeekStr(i));
                        for (int j = 0; j < 8; j++)
                        {
                            sb.Append("  时段" + (j + 1) + "：" + StringUtility.TimeHourAndMinuteStr(result.weekTimeGroup.GetItem(i).GetItem(j).GetBeginTime(), result.weekTimeGroup.GetItem(i).GetItem(j).GetEndTime()));
                        }
                        // mMainForm.AddCmdLog(null, sb.ToString());
                    }
                    mMainForm.AddCmdLog(cmde, sb.ToString());
                }
                Invoke(() =>
                {
                    if (!result.Use)
                    {
                        rBtnNoDoorWorkSetting.Checked = true;
                    }
                    else
                    {
                        rBtnDoorWorkSetting.Checked = true;

                        WeekTimeGroupDoorWorkDto = result.weekTimeGroup;
                        SetAllTimePicker(DoorOpenTimePanel, "beginTimePicker", "endTimePicker", WeekTimeGroupDoorWorkDto.GetItem(0));
                    }
                });
            };
        }

        private void BtnWriteWorkSetting_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            byte doorTriggerMode = Convert.ToByte(cmbDoorTriggerMode.SelectedIndex + 1);
            WriteDoorWorkSetting_Parameter par = new WriteDoorWorkSetting_Parameter(rBtnDoorWorkSetting.Checked, doorTriggerMode, WeekTimeGroupDoorWorkDto);
            WriteDoorWorkSetting write = new WriteDoorWorkSetting(cmdDtl, par);
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

        private void BtnReadExemptionVerificationOpen_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadExemptionVerificationOpen cmd = new ReadExemptionVerificationOpen(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadExemptionVerificationOpen_Result result = cmde.Command.getResult() as ReadExemptionVerificationOpen_Result;

                sb.Clear();
                sb.Append("免认证开门 ");
                sb.Append(result.IsUseExemptionVerification ? "是否启用：【1、启用】，" : "是否启用：【0、不启用】，");

                Invoke(() =>
                {
                    cmbPeriodNumber.SelectedItem = result.PeriodNumber.ToString();
                    cbExemptionVerificationUse.Checked = result.IsUseExemptionVerification;
                    cbAutomaticRegistrationUse.Checked = result.IsUseAutomaticRegistration;
                });
                
                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteExemptionVerificationOpen_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            byte periodNumber = Convert.ToByte(cmbPeriodNumber.SelectedIndex + 1);
            WriteExemptionVerificationOpen_Parameter par = new WriteExemptionVerificationOpen_Parameter(cbExemptionVerificationUse.Checked
                ,cbAutomaticRegistrationUse.Checked,periodNumber);
            WriteExemptionVerificationOpen write = new WriteExemptionVerificationOpen(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void BtnReadVoiceBroadcastSetting_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadVoiceBroadcastSetting cmd = new ReadVoiceBroadcastSetting(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                VoiceBroadcastSetting_Result result = cmde.Command.getResult() as VoiceBroadcastSetting_Result;

                sb.Clear();
                sb.Append("语音播报功能 ");
                sb.Append(result.Use ? "是否启用：【1、启用】，" : "是否启用：【0、不启用】，");

                Invoke(() =>
                {
                    cbVoiceBroadcast.Checked = result.Use;
                });

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteVoiceBroadcastSetting_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            byte periodNumber = Convert.ToByte(cmbPeriodNumber.SelectedIndex + 1);
            WriteVoiceBroadcastSetting_Parameter par = new WriteVoiceBroadcastSetting_Parameter(cbVoiceBroadcast.Checked);
            WriteVoiceBroadcastSetting write = new WriteVoiceBroadcastSetting(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void BtnReadReaderIntervalTime_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadReaderIntervalTime cmd = new ReadReaderIntervalTime(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadReaderIntervalTime_Result result = cmde.Command.getResult() as ReadReaderIntervalTime_Result;

                sb.Clear();
                sb.Append("重复验证权限间隔 ");
                sb.Append(result.IsUse ? "是否启用：【1、启用】，" : "是否启用：【0、不启用】，");

                Invoke(() =>
                {
                    cbReaderIntervalTimeUse.Checked = result.IsUse;
                    if (result.IntervalTime > 255)
                    {
                        cmbIntervalTime.SelectedIndex = cmbIntervalTime.Items.Count - 1;
                    }
                    else
                    {
                        cmbIntervalTime.SelectedIndex = result.IntervalTime;
                    }
                    cmbMode.SelectedIndex = result.Mode - 1;
                });

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteReaderIntervalTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            byte periodNumber = Convert.ToByte(cmbPeriodNumber.SelectedIndex + 1);
            ushort time = 0;
            if (cmbIntervalTime.SelectedIndex == cmbIntervalTime.Items.Count - 1)
            {
                time = 65535;
            }
            else
            {
                time = Convert.ToUInt16(cmbIntervalTime.SelectedIndex);
            }
            WriteReaderIntervalTime_Parameter par = new WriteReaderIntervalTime_Parameter(cbReaderIntervalTimeUse.Checked , time,Convert.ToByte(cmbMode.SelectedIndex + 1));
            WriteReaderIntervalTime write = new WriteReaderIntervalTime(cmdDtl, par);
            mMainForm.AddCommand(write);
        }

        private void BtnReadExpirationPrompt_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadExpirationPrompt cmd = new ReadExpirationPrompt(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadExpirationPrompt_Result result = cmde.Command.getResult() as ReadExpirationPrompt_Result;

                sb.Clear();
                sb.Append("权限到期提示 ");
                sb.Append(result.IsUse ?  "是否启用：【1、启用】，" : "是否启用：【0、不启用】，");

                Invoke(() =>
                {
                    cbExpirationPromptUse.Checked = result.IsUse;
                    cmbExpirationPromptTime.SelectedIndex = result.Time;
                });

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteExpirationPrompt_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            byte time = Convert.ToByte(cmbExpirationPromptTime.SelectedIndex);


            WriteExpirationPrompt_Parameter par = new WriteExpirationPrompt_Parameter(cbExpirationPromptUse.Checked, time);
            WriteExpirationPrompt write = new WriteExpirationPrompt(cmdDtl, par);
            mMainForm.AddCommand(write);
        }
    }
}
