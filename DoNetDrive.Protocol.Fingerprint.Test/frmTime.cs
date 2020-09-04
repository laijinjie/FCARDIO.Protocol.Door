using DoNetDrive.Protocol.Door.Door8800.Time;
using DoNetDrive.Protocol.Door.Door8800.Time.TimeErrorCorrection;
using System;
using System.Text.RegularExpressions;

namespace DoNetDrive.Protocol.Fingerprint.Test
{
    public partial class frmTime : frmNodeForm
    {
        #region 单例模式
        private static object lockobj = new object();
        private static frmTime onlyObj;
        public static frmTime GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmTime(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        private frmTime(INMain main) : base(main)
        {
            InitializeComponent();
        }
        #endregion

        private void frmTime_Load(object sender, EventArgs e)
        {
            LoadUILanguage();
        }
        public override void LoadUILanguage()
        {
            base.LoadUILanguage();
            GetLanguage(gpDeviceTime);
            GetLanguage(Lbl_DeviceTime);
            GetLanguage(Lbl_ComputerTime);
            GetLanguage(Lbl_DisparityTime);
            GetLanguage(btnReadSystemTime);
            GetLanguage(btnWriteSystemTime);
            GetLanguage(btnWriteBroadcastTime);
            GetLanguage(gpDiyTime);
            GetLanguage(Lbl_DateTime);
            GetLanguage(btnWriteCustomDateTime);
        }
        #region 设备时间读写
        private void BtnReadSystemTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadTime cmd = new ReadTime(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadTime_Result result = cmde.Command.getResult() as ReadTime_Result;
                string ControllerDate = result.ControllerDate.ToString("yyyy-MM-dd HH:mm:ss"); //设备时间
                int Seconds = 0; //误差秒数
                string tip = string.Empty;
                Invoke(() =>
                {
                    txtSystemTime.Text = ControllerDate;
                    txtComputerTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    TimeSpan ts = Convert.ToDateTime(txtComputerTime.Text) - result.ControllerDate;
                    Seconds = Math.Abs(Convert.ToInt32(ts.TotalSeconds));
                    if (Seconds < 4 && Seconds > -4)
                    {
                        tip = GetLanguage("Msg_1");
                    }
                    else
                    {
                        tip =string.Format(GetLanguage("Msg_2"), Seconds);
                    }
                    txtErrorTime.Text = tip;
                });
                string ControllerDateInfo = GetLanguage("Msg_3") + txtComputerTime.Text +System.Environment.NewLine+
                                            GetLanguage("Msg_4") + ControllerDate + System.Environment.NewLine +
                                            GetLanguage("Msg_5") + tip;
                mMainForm.AddCmdLog(cmde, ControllerDateInfo);
            };
        }

        private void BtnWriteSystemTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteTime cmd = new WriteTime(cmdDtl);
            mMainForm.AddCommand(cmd);
        }

        private void BtnWriteBroadcastTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteTimeBroadcast cmd = new WriteTimeBroadcast(cmdDtl);
            mMainForm.AddCommand(cmd);
        }
        private void BtnWriteCustomDateTime_Click(object sender, EventArgs e)
        {
            DateTime CustomTime = Convert.ToDateTime(CustomDateTime.Text);
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteCustomTime cmd = new WriteCustomTime(cmdDtl, new WriteCustomTime_Parameter(CustomTime));
            mMainForm.AddCommand(cmd);
        }
        #endregion
    }
}
