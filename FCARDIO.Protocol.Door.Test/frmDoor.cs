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
    public partial class frmDoor : Form
    {

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

        INMain mMainForm;

        private frmDoor(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }

        private void frmDoor_Load(object sender, EventArgs e)
        {

            cmdDoorNum.Items.Clear();
            cmdDoorNum.Items.AddRange(new string[] { "1", "2", "3", "4" });
            cmdDoorNum.SelectedIndex = 0;

            //未注册卡报警
            IniInvalidCardAlarmOptionUse();

            //胁迫报警密码
            AlarmPassword();
            AlarmOption();

            //开门超时提示参数
            OvertimeAlarmSetting();
            Alarm();

            //门磁报警参数
            SensorAlarmSetting();
            Week();
        }

        #region "未注册卡报警"

        private void IniInvalidCardAlarmOptionUse()
        {
            cmdInvalidCardAlarmOptionUse.Items.Clear();
            cmdInvalidCardAlarmOptionUse.Items.AddRange(new string[] { "启用", "禁用" });
            cmdInvalidCardAlarmOptionUse.SelectedIndex = 0;
        }

        private void butReadInvalidCardAlarmOption_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Door.DoorPort_Parameter(int.Parse(cmdDoorNum.Text));
            var cmd = new FC8800.Door.InvalidCardAlarmOption.ReadInvalidCardAlarmOption(cmdDtl, par);
            mMainForm.AddCommand(cmd);


            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Door.InvalidCardAlarmOption.InvalidCardAlarmOption_Result;

                mMainForm.AddLog($"命令成功：门号:{result.DoorNum},功能开关:{result.Use}");
            };
        }
        private void ButWriteInvalidCardAlarmOption_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();

            bool use = (cmdInvalidCardAlarmOptionUse.SelectedIndex == 0);
            byte door = byte.Parse(cmdDoorNum.Text);

            var par = new FC8800.Door.InvalidCardAlarmOption.WriteInvalidCardAlarmOption_Parameter(door, use);
            var cmd = new FC8800.Door.InvalidCardAlarmOption.WriteInvalidCardAlarmOption(cmdDtl, par);
            mMainForm.AddCommand(cmd);


            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功：门号:{door},功能开关:{use}");
            };
        }
        #endregion

        #region "胁迫报警密码"
        private void AlarmPassword()
        {
            cmdAlarmPassword.Items.Clear();
            cmdAlarmPassword.Items.AddRange(new string[] { "启用", "禁用" });
            cmdAlarmPassword.SelectedIndex = 0;
        }
        //报警模式
        private void AlarmOption()
        {
            cmbAlarmOption.Items.Clear();
            cmbAlarmOption.Items.AddRange(new string[] { "不开门，报警输出", "开门，报警输出", "锁定门，报警，只能软件解锁" });
            cmbAlarmOption.SelectedIndex = 0;
        }
        private void butReadAlarmPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Door.DoorPort_Parameter(int.Parse(cmdDoorNum.Text));
            var cmd = new FC8800.Door.AlarmPassword.ReadAlarmPassword(cmdDtl, par);
            mMainForm.AddCommand(cmd);


            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Door.AlarmPassword.AlarmPassword_Result;

                mMainForm.AddLog($"命令成功：门号:{result.DoorNum},功能开关:{result.Use}");
            };
        }
        private void butWriteAlarmPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();

            bool use = (cmdAlarmPassword.SelectedIndex == 0);
            byte door = byte.Parse(cmdDoorNum.Text);
            String pwd = Password.Text.ToString();
            int alarmOption = int.Parse(cmbAlarmOption.Text) + 1;

            var par = new FC8800.Door.AlarmPassword.WriteAlarmPassword_parameter(door, use, pwd, alarmOption);
            var cmd = new FC8800.Door.AlarmPassword.WriteAlarmPassword(cmdDtl, par);
            mMainForm.AddCommand(cmd);


            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功：门号:{door},功能开关:{use}");
            };
        }
        #endregion

        #region "开门超时提示参数"
        private void OvertimeAlarmSetting()
        {
            cmbOvertimeAlarmSetting.Items.Clear();
            cmbOvertimeAlarmSetting.Items.AddRange(new string[] { "启用", "禁用" });
            cmbOvertimeAlarmSetting.SelectedIndex = 0;
        }

        private void Alarm()
        {
            cmbAlarm.Items.Clear();
            cmbAlarm.Items.AddRange(new string[] { "输出", "不输出" });
            cmbAlarm.SelectedIndex = 0;
        }

        private void Overtime()
        {
            cmbAlarm.Items.Clear();
            cmbAlarm.Items.AddRange(new string[] { "1秒", "2秒" });
            cmbAlarm.SelectedIndex = 0;
        }

        private void butReadOvertimeAlarmSetting_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Door.DoorPort_Parameter(int.Parse(cmdDoorNum.Text));
            var cmd = new FC8800.Door.OvertimeAlarmSetting.ReadOvertimeAlarmSetting(cmdDtl, par);
            mMainForm.AddCommand(cmd);


            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Door.OvertimeAlarmSetting.OvertimeAlarmSetting_Result;

                mMainForm.AddLog($"命令成功：门号:{result.DoorNum},功能开关:{result.Use}");
            };
        }

        private void butWriteOvertimeAlarmSetting_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            bool use = (cmbOvertimeAlarmSetting.SelectedIndex == 0);
            byte door = byte.Parse(cmdDoorNum.Text);
            byte overtime = byte.Parse(cmdOvertime.Text);
            bool alarm = (cmdAlarmPassword.SelectedIndex == 0);
            var par = new FC8800.Door.OvertimeAlarmSetting.WriteOvertimeAlarmSetting_Parameter(door, use, overtime, alarm);
            var cmd = new FC8800.Door.OvertimeAlarmSetting.WriteOvertimeAlarmSetting(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功：门号:{door},功能开关:{use}");
            };
        }
        #endregion

        #region "门磁报警参数"
        private void SensorAlarmSetting()
        {
            cmbSensorAlarmSetting.Items.Clear();
            cmbSensorAlarmSetting.Items.AddRange(new string[] { "启用", "禁用" });
            cmbSensorAlarmSetting.SelectedIndex = 0;
        }
        //门磁报警不生效时段
        private void Week()
        {
            cmbWeek.Items.Clear();
            cmbWeek.Items.AddRange(new string[] { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期天" });
            cmbWeek.SelectedIndex = 0;
        }
        //改变下拉框
        private void cmbSensorAlarmSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void butReadSensorAlarmSetting_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Door.DoorPort_Parameter(int.Parse(cmdDoorNum.Text));
            var cmd = new FC8800.Door.SensorAlarmSetting.ReadSensorAlarmSetting(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Door.SensorAlarmSetting.SensorAlarmSetting_Result;

                mMainForm.AddLog($"命令成功：门号:{result.DoorNum},功能开关:{result.Use}");
            };
        }
        private void butWriteSensorAlarmSetting_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            bool use = (cmbSensorAlarmSetting.SelectedIndex == 0);
            byte door = byte.Parse(cmdDoorNum.Text);
            var par = new FC8800.Door.SensorAlarmSetting.WriteSensorAlarmSetting_Parameter(door, use);
            var cmd = new FC8800.Door.SensorAlarmSetting.WriteSensorAlarmSetting(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功：门号:{door},功能开关:{use}");
            };
        }
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
