﻿using DoNetDrive.Protocol.USB.OfflinePatrol.Time;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoNetDrive.Protocol.USB.OfflinePatrol.Test
{
    public partial class frmTime :  frmNodeForm
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
                        FrmMain.AddNodeForms(onlyObj);
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

        private void BtnReadTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadTime cmd = new ReadTime(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadTime_Result result = cmde.Command.getResult() as ReadTime_Result;

                DateTime dtNow = DateTime.Now;
                DateTime dtDeviceTime = result.ControllerDate;
                Invoke(() =>
                {
                    txtDeviceTime.Text = dtDeviceTime.ToString("yyyy-MM-dd HH:mm:ss");
                    txtPCTime.Text = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
                    int seconds = Convert.ToInt32((dtNow - dtDeviceTime).TotalSeconds);
                    if (seconds >= 0)
                    {
                        txtTimeSpan.Text = $"设备比电脑慢 {seconds.ToString()} 秒！";
                    }
                    else
                    {
                        txtTimeSpan.Text = $"设备比电脑快 {seconds.ToString().Substring(1)} 秒！";
                    }
                });
                
                
                mMainForm.AddCmdLog(cmde, $"设备时间：{txtDeviceTime.Text}");
            };
        }

        private void BtnWriteTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            
            WriteTime cmd = new WriteTime(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, $"写入电脑时间成功");
            };
        }

        private void BtnWriteCustomTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var time = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, dtpTime.Value.Hour, dtpTime.Value.Minute, dtpTime.Value.Second);
            WriteCustomTime_Parameter par = new WriteCustomTime_Parameter(time);
            WriteCustomTime cmd = new WriteCustomTime(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, $"写入自定义时间成功{time.ToString()}");
            };
        }
    }
}
