
using FCARDIO.Protocol.USB.OfflinePatrol.SystemParameter.ExpireTime;
using FCARDIO.Protocol.USB.OfflinePatrol.SystemParameter.RecordStorageMode;
using FCARDIO.Protocol.USB.OfflinePatrol.SystemParameter.SN;
using FCARDIO.Protocol.USB.OfflinePatrol.SystemParameter.SystemStatus;
using FCARDIO.Protocol.USB.OfflinePatrol.SystemParameter.Version;
using System;
using System.Windows.Forms;

namespace FCARDIO.Protocol.USB.OfflinePatrol.Test
{
    public partial class frmSystem : frmNodeForm
    {
        #region 单例模式
        private static object lockobj = new object();
        private static frmSystem onlyObj;
        public static frmSystem GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmSystem(main);
                        FrmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        private frmSystem(INMain main) : base(main)
        {
            InitializeComponent();
        }
        #endregion


        #region 地址码
        private void BtnReadSN_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadSN cmd = new ReadSN(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                SN_Result result = cmde.Command.getResult() as SN_Result;
                string sn = result.SN.ToString();
                Invoke(() =>
                {
                    txtAddress.Text = sn;
                });
                mMainForm.AddCmdLog(cmde, $"地址号：{sn}");
            };
        }

        private void BtnWriteSN_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            byte sn = 0;
            if (!byte.TryParse(txtAddress.Text,out sn))
            {
                MessageBox.Show("SN格式不正确！");
                return;
            }
            SN_Parameter par = new SN_Parameter(sn);
            WriteSN cmd = new WriteSN(cmdDtl,par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, $"写入SN成功");
            };
        }

        #endregion
        /// <summary>
        /// 读取 版本号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadVersion_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadVersion cmd = new ReadVersion(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadVersion_Result result = cmde.Command.getResult() as ReadVersion_Result;
                
                Invoke(() =>
                {
                    txtVersion.Text = "版本号：" + result.VerNum + "." + result.Revise;
                });
                mMainForm.AddCmdLog(cmde, $"{txtVersion.Text}");
            };
        }

        /// <summary>
        /// 读取 截止日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadExpireTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadExpireTime cmd = new ReadExpireTime(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ExpireTime_Result result = cmde.Command.getResult() as ExpireTime_Result;

                Invoke(() =>
                {
                    dtpExpireTime.Value = result.Time;
                });
                mMainForm.AddCmdLog(cmde, $"{txtVersion.Text}");
            };
        }

        /// <summary>
        /// 写入 截止日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWriteExpireTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            ExpireTime_Parameter par = new ExpireTime_Parameter(dtpExpireTime.Value);
            WriteExpireTime cmd = new WriteExpireTime(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, $"使用截止日期：{dtpExpireTime.Value.ToString("yyyy-MM-dd")}");
            };
        }

        /// <summary>
        /// 读取运行信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadSystemStatus_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadSystemStatus cmd = new ReadSystemStatus(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadSystemStatus_Result result = cmde.Command.getResult() as ReadSystemStatus_Result;

                Invoke(() =>
                {
                    lbTime.Text = result.Time.ToString("yyyy-MM-dd");
                    lbFormatCount.Text = result.FormatCount.ToString();
                    lbElectricity.Text = result.Electricity.ToString();
                    lbPatrolEmpl.Text = result.PatrolEmpl.ToString();
                    lbPatrolEmplCount.Text = result.PatrolEmplCount.ToString();
                    lbStartCount.Text = result.StartCount.ToString();
                    lbSystemRecordCount.Text = result.SystemRecordCount.ToString();
                    lbVoltage.Text = result.Voltage;
                    lbCardRecordCount.Text = result.CardRecordCount.ToString();
                });
                //mMainForm.AddCmdLog(cmde, $"{txtVersion.Text}");
            };
        }

        #region 开机保持
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadStartupHoldTime_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWriteStartupHoldTime_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 出厂日期
        private void ReadCreateTime_Click(object sender, EventArgs e)
        {

        }

        private void WriteCreateTime_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region LED开灯保持
        private void ReadLEDOpenTime_Click(object sender, EventArgs e)
        {

        }

        private void WriteLEDOpenTime_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void BtnReadRecordStorageMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadRecordStorageMode cmd = new ReadRecordStorageMode(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadRecordStorageMode_Result result = cmde.Command.getResult() as ReadRecordStorageMode_Result;

                Invoke(() =>
                {
                    rbStorageMode0.Checked = result.Mode == 0;
                    rbStorageMode1.Checked = result.Mode == 1;
                });
                //mMainForm.AddCmdLog(cmde, $"{txtVersion.Text}");
            };
        }

        private void BtnWriteRecordStorageMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            WriteRecordStorageMode_Parameter par = new WriteRecordStorageMode_Parameter((byte)(rbStorageMode0.Checked ? 0 : 1));
            WriteRecordStorageMode cmd = new WriteRecordStorageMode(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                string text = rbStorageMode0.Checked ? rbStorageMode0.Text : rbStorageMode1.Text;
                mMainForm.AddCmdLog(cmde, $"记录存储方式：{text}");
            };
        }
    }
}
