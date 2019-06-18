using FCARDIO.Protocol.Door.FC8800.Door.ReaderOption;
using FCARDIO.Protocol.Door.FC8800.Door.RelayOption;
using FCARDIO.Protocol.Door.FC8800.Door.Remote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FCARDIO.Protocol.Door.FC8800.Utility;
using FCARDIO.Protocol.Door.FC8800.Door;
using System.Collections;
using FCARDIO.Protocol.Door.FC8800.Door.ReaderWorkSetting;
using FCARDIO.Core.Util;
using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;
using FCARDIO.Protocol.Door.FC8800.Door.DoorWorkSetting;
using FCARDIO.Protocol.Door.FC8800.Door.AutoLockedSetting;
using FCARDIO.Protocol.Door.FC8800.Door.RelayReleaseTime;
using System.Text.RegularExpressions;
using FCARDIO.Protocol.Door.FC8800.Door.ReaderInterval;

namespace FCARDIO.Protocol.Door.Test
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
        }
        #endregion

        private void frmDoor_Load(object sender, EventArgs e)
        {
            cmdDoorNum.Items.Clear();
            cmdDoorNum.Items.AddRange(new string[] { "1", "2", "3", "4" });
            cmdDoorNum.SelectedIndex = 0;

            //非法读卡报警
            IniInvalidCardAlarmOptionUse();

            //胁迫报警密码
            AlarmPassword();
            AlarmOption();

            //开门超时提示参数
            OvertimeAlarmSetting();
            Alarm();
            Overtime();

            //门磁报警参数
            SensorAlarmSetting();
            Week();

            #region 读卡器字节数默认选项
            cbxDoor1ReaderOption.SelectedIndex = 2;
            cbxDoor2ReaderOption.SelectedIndex = 2;
            cbxDoor3ReaderOption.SelectedIndex = 2;
            cbxDoor4ReaderOption.SelectedIndex = 2;
            #endregion

            #region 继电器参数默认选项
            cbxDoor1RelayOption.SelectedIndex = 0;
            cbxDoor2RelayOption.SelectedIndex = 0;
            cbxDoor3RelayOption.SelectedIndex = 0;
            cbxDoor4RelayOption.SelectedIndex = 0;
            #endregion

            #region 门工作方式_星期
            cbxWeek.SelectedIndex = 0;
            #endregion

            #region 重复读卡间隔_检测模式
            cbxDetectionMode.SelectedIndex = 0;
            #endregion
        }

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
            cmbAlarmOption.SelectedIndex = 1;
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
                Invoke(() =>
                {
                    cmdAlarmPassword.SelectedIndex = result.Use ? 0 : 1;
                    cmbAlarmOption.SelectedIndex = result.AlarmOption;
                    Password.Text = result.Password;

                });

                mMainForm.AddLog($"命令成功：门号:{result.DoorNum},功能开关:{result.Use},报警密码：{result.Password},报警选项：{result.AlarmOption}");
            };
        }
        private void butWriteAlarmPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            bool use = (cmdAlarmPassword.SelectedIndex == 0);
            byte door = byte.Parse(cmdDoorNum.Text);
            String pwd = Password.Text.ToString();
            int alarmOption = cmbAlarmOption.SelectedIndex;

            var par = new FC8800.Door.AlarmPassword.WriteAlarmPassword_parameter(door, use, pwd, alarmOption);
            var cmd = new FC8800.Door.AlarmPassword.WriteAlarmPassword(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功：门号:{door},功能开关:{use},密码：{Password},报警选项：{alarmOption}");
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
            cmbOverTime.Items.Clear();
            string[] time = new string[100];
            for (int i = 0; i < 100; i++)
            {
                time[i] = i + 1 + "秒";
            }
            cmbOverTime.Items.AddRange(time);
            cmbOverTime.SelectedIndex = 0;
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
                mMainForm.AddLog($"命令成功：门号:{result.DoorNum},功能开关:{result.Use}，超时时间：{result.Overtime},是否报警 ：{result.Alarm}");
            };
        }

        private void butWriteOvertimeAlarmSetting_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();

            bool use = (cmbOvertimeAlarmSetting.SelectedIndex == 0);
            byte door = byte.Parse(cmdDoorNum.Text);
            byte overtime = byte.Parse((cmbOverTime.SelectedIndex + 1).ToString());
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
            if (cmbSensorAlarmSetting.Text == "启用")
            {
                cmbWeek.Hide();
                label10.Hide();
            }
            else
            {
                cmbWeek.Show();
                label10.Show();
            }


        }
        private void butReadSensorAlarmSetting_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Door.DoorPort_Parameter(int.Parse(cmdDoorNum.Text));
            var cmd = new FC8800.Door.SensorAlarmSetting.ReadSensorAlarmSetting(cmdDtl, par);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Door.SensorAlarmSetting.SensorAlarmSetting_Result;
                //cmbSensorAlarmSetting
                Invoke(() =>
                {
                    cmdDoorNum.Text = result.DoorNum.ToString();
                });
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

        #region 读卡器参数读写
        private void BtnReadReaderOption_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadReaderOption cmd = new ReadReaderOption(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReaderOption_Result result = cmde.Command.getResult() as ReaderOption_Result;
                StringBuilder sb = new StringBuilder();
                string[] str = new string[4];
                str[0] = "字节数：【1、韦根26(三字节)】";
                str[1] = "字节数：【2、韦根34(四字节)】";
                str[2] = "字节数：【3、韦根26(二字节)】";
                str[3] = "字节数：【4、禁用】";

                Invoke(() =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        //门1读卡器字节数
                        if (i == 0)
                        {
                            if (result.Door[i] == 1)
                            {
                                cbxDoor1ReaderOption.SelectedIndex = 2;
                                sb.Append("门" + (i + 1) + str[0]);
                            }
                            else if (result.Door[i] == 2)
                            {
                                cbxDoor1ReaderOption.SelectedIndex = 3;
                                sb.Append("门" + (i + 1) + str[1]);
                            }
                            else if (result.Door[i] == 3)
                            {
                                cbxDoor1ReaderOption.SelectedIndex = 1;
                                sb.Append("门" + (i + 1) + str[2]);
                            }
                            else if (result.Door[i] == 4)
                            {
                                cbxDoor1ReaderOption.SelectedIndex = 0;
                                sb.Append("门" + (i + 1) + str[3]);
                            }
                        }
                        //门2读卡器字节数
                        if (i == 1)
                        {
                            if (result.Door[i] == 1)
                            {
                                cbxDoor2ReaderOption.SelectedIndex = 2;
                                sb.Append("  门" + (i + 1) + str[0]);
                            }
                            else if (result.Door[i] == 2)
                            {
                                cbxDoor2ReaderOption.SelectedIndex = 3;
                                sb.Append("  门" + (i + 1) + str[1]);
                            }
                            else if (result.Door[i] == 3)
                            {
                                cbxDoor2ReaderOption.SelectedIndex = 1;
                                sb.Append("  门" + (i + 1) + str[2]);
                            }
                            else if (result.Door[i] == 4)
                            {
                                cbxDoor2ReaderOption.SelectedIndex = 0;
                                sb.Append("  门" + (i + 1) + str[3]);
                            }
                        }
                        //门3读卡器字节数
                        if (i == 2)
                        {
                            if (result.Door[i] == 1)
                            {
                                cbxDoor3ReaderOption.SelectedIndex = 2;
                                sb.Append("  门" + (i + 1) + str[0]);
                            }
                            else if (result.Door[i] == 2)
                            {
                                cbxDoor3ReaderOption.SelectedIndex = 3;
                                sb.Append("  门" + (i + 1) + str[1]);
                            }
                            else if (result.Door[i] == 3)
                            {
                                cbxDoor3ReaderOption.SelectedIndex = 1;
                                sb.Append("  门" + (i + 1) + str[2]);
                            }
                            else if (result.Door[i] == 4)
                            {
                                cbxDoor3ReaderOption.SelectedIndex = 0;
                                sb.Append("  门" + (i + 1) + str[3]);
                            }
                        }
                        //门4读卡器字节数
                        if (i == 3)
                        {
                            if (result.Door[i] == 1)
                            {
                                cbxDoor4ReaderOption.SelectedIndex = 2;
                                sb.Append("  门" + (i + 1) + str[0]);
                            }
                            else if (result.Door[i] == 2)
                            {
                                cbxDoor4ReaderOption.SelectedIndex = 3;
                                sb.Append("  门" + (i + 1) + str[1]);
                            }
                            else if (result.Door[i] == 3)
                            {
                                cbxDoor4ReaderOption.SelectedIndex = 1;
                                sb.Append("  门" + (i + 1) + str[2]);
                            }
                            else if (result.Door[i] == 4)
                            {
                                cbxDoor4ReaderOption.SelectedIndex = 0;
                                sb.Append("  门" + (i + 1) + str[3]);
                            }
                        }
                    }
                });
                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteReaderOption_Click(object sender, EventArgs e)
        {
            byte[] Door = new byte[4];
            //门1读卡器字节数
            int Door1ReaderOption = cbxDoor1ReaderOption.SelectedIndex;
            if (Door1ReaderOption == 0) //禁用
            {
                Door[0] = 4;
            }
            else if (Door1ReaderOption == 1) //二字节
            {
                Door[0] = 3;
            }
            else if (Door1ReaderOption == 2) //三字节
            {
                Door[0] = 1;
            }
            else if (Door1ReaderOption == 3) //四字节
            {
                Door[0] = 2;
            }
            //门2读卡器字节数
            int Door2ReaderOption = cbxDoor2ReaderOption.SelectedIndex;
            if (Door2ReaderOption == 0) //禁用
            {
                Door[1] = 4;
            }
            else if (Door2ReaderOption == 1) //二字节
            {
                Door[1] = 3;
            }
            else if (Door2ReaderOption == 2) //三字节
            {
                Door[1] = 1;
            }
            else if (Door2ReaderOption == 3) //四字节
            {
                Door[1] = 2;
            }
            //门3读卡器字节数
            int Door3ReaderOption = cbxDoor3ReaderOption.SelectedIndex;
            if (Door3ReaderOption == 0) //禁用
            {
                Door[2] = 4;
            }
            else if (Door3ReaderOption == 1) //二字节
            {
                Door[2] = 3;
            }
            else if (Door3ReaderOption == 2) //三字节
            {
                Door[2] = 1;
            }
            else if (Door3ReaderOption == 3) //四字节
            {
                Door[2] = 2;
            }
            //门4读卡器字节数
            int Door4ReaderOption = cbxDoor4ReaderOption.SelectedIndex;
            if (Door4ReaderOption == 0) //禁用
            {
                Door[3] = 4;
            }
            else if (Door4ReaderOption == 1) //二字节
            {
                Door[3] = 3;
            }
            else if (Door4ReaderOption == 2) //三字节
            {
                Door[3] = 1;
            }
            else if (Door4ReaderOption == 3) //四字节
            {
                Door[3] = 2;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteReaderOption cmd = new WriteReaderOption(cmdDtl, new ReaderOption_Parameter(Door));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 继电器参数读写
        private void BtnReadRelayOption_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadRelayOption cmd = new ReadRelayOption(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                RelayOption_Result result = cmde.Command.getResult() as RelayOption_Result;
                StringBuilder sb = new StringBuilder();
                string[] str = new string[3];
                str[0] = "继电器类型：【1、COM & NC】";
                str[1] = "继电器类型：【2、COM & NO】";
                str[2] = "继电器类型：【3、双稳态】";

                Invoke(() =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        //门1继电器参数
                        if (i == 0)
                        {
                            if (result.Relay[i] == 1)
                            {
                                cbxDoor1RelayOption.SelectedIndex = 0;
                                sb.Append("门" + (i + 1) + str[0]);
                            }
                            else if (result.Relay[i] == 2)
                            {
                                cbxDoor1RelayOption.SelectedIndex = 1;
                                sb.Append("门" + (i + 1) + str[1]);
                            }
                            else if (result.Relay[i] == 3)
                            {
                                cbxDoor1RelayOption.SelectedIndex = 2;
                                sb.Append("门" + (i + 1) + str[2]);
                            }
                        }
                        //门2继电器参数
                        if (i == 1)
                        {
                            if (result.Relay[i] == 1)
                            {
                                cbxDoor2RelayOption.SelectedIndex = 0;
                                sb.Append("  门" + (i + 1) + str[0]);
                            }
                            else if (result.Relay[i] == 2)
                            {
                                cbxDoor2RelayOption.SelectedIndex = 1;
                                sb.Append("  门" + (i + 1) + str[1]);
                            }
                            else if (result.Relay[i] == 3)
                            {
                                cbxDoor2RelayOption.SelectedIndex = 2;
                                sb.Append("  门" + (i + 1) + str[2]);
                            }
                        }
                        //门3继电器参数
                        if (i == 2)
                        {
                            if (result.Relay[i] == 1)
                            {
                                cbxDoor3RelayOption.SelectedIndex = 0;
                                sb.Append("  门" + (i + 1) + str[0]);
                            }
                            else if (result.Relay[i] == 2)
                            {
                                cbxDoor3RelayOption.SelectedIndex = 1;
                                sb.Append("  门" + (i + 1) + str[1]);
                            }
                            else if (result.Relay[i] == 3)
                            {
                                cbxDoor3RelayOption.SelectedIndex = 2;
                                sb.Append("  门" + (i + 1) + str[2]);
                            }
                        }
                        //门4继电器参数
                        if (i == 3)
                        {
                            if (result.Relay[i] == 1)
                            {
                                cbxDoor4RelayOption.SelectedIndex = 0;
                                sb.Append("  门" + (i + 1) + str[0]);
                            }
                            else if (result.Relay[i] == 2)
                            {
                                cbxDoor4RelayOption.SelectedIndex = 1;
                                sb.Append("  门" + (i + 1) + str[1]);
                            }
                            else if (result.Relay[i] == 3)
                            {
                                cbxDoor4RelayOption.SelectedIndex = 2;
                                sb.Append("  门" + (i + 1) + str[2]);
                            }
                        }
                    }
                });
                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteRelayOption_Click(object sender, EventArgs e)
        {
            byte[] Relay = new byte[4];
            Relay[0] = Convert.ToByte(cbxDoor1RelayOption.SelectedIndex + 1); //门1继电器参数
            Relay[1] = Convert.ToByte(cbxDoor2RelayOption.SelectedIndex + 1); //门2继电器参数
            Relay[2] = Convert.ToByte(cbxDoor3RelayOption.SelectedIndex + 1); //门3继电器参数
            Relay[3] = Convert.ToByte(cbxDoor4RelayOption.SelectedIndex + 1); //门4继电器参数

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteRelayOption cmd = new WriteRelayOption(cmdDtl, new RelayOption_Parameter(Relay));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 远程开关门
        private void BtnOpenDoor_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }

            DoorDetail<bool> Door = new DoorDetail<bool>();
            Door.SetDoor(1, cBoxDoor1.Checked);
            Door.SetDoor(2, cBoxDoor2.Checked);
            Door.SetDoor(3, cBoxDoor3.Checked);
            Door.SetDoor(4, cBoxDoor4.Checked);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            OpenDoor cmd = new OpenDoor(cmdDtl, new Remote_Parameter(Door));
            mMainForm.AddCommand(cmd);
        }

        private void BtnCloseDoor_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }

            DoorDetail<bool> Door = new DoorDetail<bool>();
            Door.SetDoor(1, cBoxDoor1.Checked);
            Door.SetDoor(2, cBoxDoor2.Checked);
            Door.SetDoor(3, cBoxDoor3.Checked);
            Door.SetDoor(4, cBoxDoor4.Checked);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            CloseDoor cmd = new CloseDoor(cmdDtl, new Remote_Parameter(Door));
            mMainForm.AddCommand(cmd);
        }

        private void BtnHoldOpenDoor_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }

            DoorDetail<bool> Door = new DoorDetail<bool>();
            Door.SetDoor(1, cBoxDoor1.Checked);
            Door.SetDoor(2, cBoxDoor2.Checked);
            Door.SetDoor(3, cBoxDoor3.Checked);
            Door.SetDoor(4, cBoxDoor4.Checked);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            HoldDoor cmd = new HoldDoor(cmdDtl, new Remote_Parameter(Door));
            mMainForm.AddCommand(cmd);
        }

        private void BtnOpenDoor_CheckNum_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }

            byte CheckNum;
            CheckNum = Convert.ToByte(StringUtility.GetRandomNum(1, 254));

            DoorDetail<bool> Door = new DoorDetail<bool>();
            Door.SetDoor(1, cBoxDoor1.Checked);
            Door.SetDoor(2, cBoxDoor2.Checked);
            Door.SetDoor(3, cBoxDoor3.Checked);
            Door.SetDoor(4, cBoxDoor4.Checked);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            OpenDoor_CheckNum cmd = new OpenDoor_CheckNum(cmdDtl, new Remote_CheckNum_Parameter(Door, CheckNum));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 门的锁定与解除
        private void BtnLockDoor_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }

            DoorDetail<bool> Door = new DoorDetail<bool>();
            Door.SetDoor(1, cBoxDoor1.Checked);
            Door.SetDoor(2, cBoxDoor2.Checked);
            Door.SetDoor(3, cBoxDoor3.Checked);
            Door.SetDoor(4, cBoxDoor4.Checked);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            LockDoor cmd = new LockDoor(cmdDtl, new Remote_Parameter(Door));
            mMainForm.AddCommand(cmd);
        }

        private void BtnUnLockDoor_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }

            DoorDetail<bool> Door = new DoorDetail<bool>();
            Door.SetDoor(1, cBoxDoor1.Checked);
            Door.SetDoor(2, cBoxDoor2.Checked);
            Door.SetDoor(3, cBoxDoor3.Checked);
            Door.SetDoor(4, cBoxDoor4.Checked);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            UnlockDoor cmd = new UnlockDoor(cmdDtl, new Remote_Parameter(Door));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 读卡认证方式的读写
        private void BtnReadDoorWorkSetting_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }
            StringBuilder sb = new StringBuilder();
            byte door = 1;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            if (cBoxDoor1.Checked)
            {
                door = 1;
                ReadReaderWorkSetting cmd = new ReadReaderWorkSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor2.Checked)
            {
                door = 2;
                ReadReaderWorkSetting cmd = new ReadReaderWorkSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor3.Checked)
            {
                door = 3;
                ReadReaderWorkSetting cmd = new ReadReaderWorkSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor4.Checked)
            {
                door = 4;
                ReadReaderWorkSetting cmd = new ReadReaderWorkSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReaderWorkSetting_Result result = cmde.Command.getResult() as ReaderWorkSetting_Result;
                string tip = "门认证方式_门" + result.Door.ToString() + "，时段详情：";
                StringBuilder sbCheckWay = new StringBuilder(8);
                StringBuilder sbCheckWayStr = new StringBuilder();
                byte[] ByteDoorAlarmStateSet = null;
                BitArray bitSet = null;
                sb.AppendLine(tip);
                for (int i = 0; i < 7; i++)
                {

                    sb.AppendLine(StringUtility.GetWeekStr(i));
                    for (int j = 0; j < 8; j++)
                    {
                        sb.Append("  时段" + (j + 1) + "：" + StringUtility.TimeHourAndMinuteStr(result.weekTimeGroup_ReaderWork.GetItem(i).GetItem(j).GetBeginTime(), result.weekTimeGroup_ReaderWork.GetItem(i).GetItem(j).GetEndTime()));
                        var tz = result.weekTimeGroup_ReaderWork.GetItem(i).GetItem(j) as TimeSegment_ReaderWork;
                        ByteDoorAlarmStateSet = new byte[] { tz.GetCheckWay() };
                        bitSet = new BitArray(ByteDoorAlarmStateSet);
                        sbCheckWay.Clear();
                        sbCheckWayStr.Clear();
                        for (int k = 7; k >= 0; k--)
                        {
                            sbCheckWay.Append(bitSet[k] ? 1 : 0);
                            if (k == 0)
                            {
                                sbCheckWayStr.Append(bitSet[k] ? "读卡开门；" : "");
                            }
                            else if (k == 1)
                            {
                                sbCheckWayStr.Append(bitSet[k] ? "密码开门；" : "");
                            }
                            else if (k == 2)
                            {
                                sbCheckWayStr.Append(bitSet[k] ? "读卡加密码；" : "");
                            }
                            else if (k == 3)
                            {
                                sbCheckWayStr.Append(bitSet[k] ? "手动输入卡加密码" : "");
                            }
                        }
                        sb.AppendLine("  认证方式：【" + sbCheckWay + "(" + sbCheckWayStr + ")】");
                    }
                }

                Invoke(() =>
                {
                    txtDoorWorkSetting.Text = sb.ToString();
                });

            };
        }
        private void BtnWriteDoorWorkSetting_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            WeekTimeGroup_ReaderWork tg = new WeekTimeGroup_ReaderWork(8);

            for (int i = 0; i < 7; i++)
            {
                var day = tg.GetItem(i);
                //for (int j = 0; j < 8; j++)
                //{
                DateTime nw = DateTime.Now;
                var tz = day.GetItem(0) as TimeSegment_ReaderWork;
                tz.SetBeginTime(0, 0);
                tz.SetEndTime(23, 59);
                tz.SetCheckWay(7);
                //}
            }

            byte door = 1;
            if (cBoxDoor1.Checked)
            {
                door = 1;
                WriteReaderWorkSetting_Parameter par = new WriteReaderWorkSetting_Parameter(door, tg);
                WriteReaderWorkSetting write = new WriteReaderWorkSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor2.Checked)
            {
                door = 2;
                WriteReaderWorkSetting_Parameter par = new WriteReaderWorkSetting_Parameter(door, tg);
                WriteReaderWorkSetting write = new WriteReaderWorkSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor3.Checked)
            {
                door = 3;
                WriteReaderWorkSetting_Parameter par = new WriteReaderWorkSetting_Parameter(door, tg);
                WriteReaderWorkSetting write = new WriteReaderWorkSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor4.Checked)
            {
                door = 4;
                WriteReaderWorkSetting_Parameter par = new WriteReaderWorkSetting_Parameter(door, tg);
                WriteReaderWorkSetting write = new WriteReaderWorkSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
        }
        #endregion

        #region 门工作方式
        private void BtnReadWorkSetting_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }
            byte door = 1;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            if (cBoxDoor1.Checked)
            {
                door = 1;
                ReadDoorWorkSetting cmd = new ReadDoorWorkSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor2.Checked)
            {
                door = 2;
                ReadDoorWorkSetting cmd = new ReadDoorWorkSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor3.Checked)
            {
                door = 3;
                ReadDoorWorkSetting cmd = new ReadDoorWorkSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor4.Checked)
            {
                door = 4;
                ReadDoorWorkSetting cmd = new ReadDoorWorkSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                DoorWorkSetting_Result result = cmde.Command.getResult() as DoorWorkSetting_Result;
                StringBuilder sb = new StringBuilder();
                string OpenDoorWayStr = string.Empty;
                string DoorTriggerModeStr = string.Empty;
                if (result.Use == 0)
                {
                    sb.AppendLine("门" + result.Door + "：是否启用：【0、不启用】");
                    mMainForm.AddCmdLog(cmde, sb.ToString());
                }
                else
                {
                    //开门方式
                    if (result.OpenDoorWay == 1)
                    {
                        OpenDoorWayStr = "【1、普通】";
                    }
                    else if (result.OpenDoorWay == 2)
                    {
                        OpenDoorWayStr = "【2、多卡】";
                    }
                    else if (result.OpenDoorWay == 3)
                    {
                        OpenDoorWayStr = "【3、首卡】";
                    }
                    else if (result.OpenDoorWay == 4)
                    {
                        OpenDoorWayStr = "【4、常开】";
                    }
                    sb.AppendLine("门" + result.Door + "：开门方式：" + OpenDoorWayStr);
                    //常开触发模式
                    if (result.DoorTriggerMode == 1)
                    {
                        DoorTriggerModeStr = "【1、合法卡】";
                    }
                    else if (result.DoorTriggerMode == 2)
                    {
                        DoorTriggerModeStr = "【2、常开卡】";
                    }
                    else if (result.DoorTriggerMode == 3)
                    {
                        DoorTriggerModeStr = "【3、自动开关】";
                    }
                    if (result.OpenDoorWay == 4)
                    {
                        sb.AppendLine("；常开触发模式：" + DoorTriggerModeStr);
                    }
                    mMainForm.AddCmdLog(cmde, sb.ToString());
                    //开门方式是首卡或者常开状态时
                    if (result.OpenDoorWay == 3 || result.OpenDoorWay == 4)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            sb.Clear();
                            sb.AppendLine(StringUtility.GetWeekStr(i));
                            for (int j = 0; j < 8; j++)
                            {
                                sb.Append("  时段" + (j + 1) + "：" + StringUtility.TimeHourAndMinuteStr(result.weekTimeGroup.GetItem(i).GetItem(j).GetBeginTime(), result.weekTimeGroup.GetItem(i).GetItem(j).GetEndTime()));
                            }
                            mMainForm.AddCmdLog(null, sb.ToString());
                        }
                    }
                }

                Invoke(() =>
                {
                    if (result.Use == 0)
                    {
                        rBtnNoDoorWorkSetting.Checked = true;
                    }
                    else
                    {
                        rBtnDoorWorkSetting.Checked = true;
                        //开门方式
                        if (result.OpenDoorWay == 1)
                        {
                            rBtnOpenDoorWay1.Checked = true;
                        }
                        else if (result.OpenDoorWay == 2)
                        {
                            rBtnOpenDoorWay2.Checked = true;
                        }
                        else if (result.OpenDoorWay == 3)
                        {
                            rBtnOpenDoorWay3.Checked = true;
                        }
                        else if (result.OpenDoorWay == 4)
                        {
                            rBtnOpenDoorWay4.Checked = true;
                        }
                        //常开触发模式
                        if (result.DoorTriggerMode == 1)
                        {
                            rBtnDoorTriggerMode1.Checked = true;
                        }
                        else if (result.DoorTriggerMode == 2)
                        {
                            rBtnDoorTriggerMode2.Checked = true;
                        }
                        else if (result.DoorTriggerMode == 3)
                        {
                            rBtnDoorTriggerMode3.Checked = true;
                        }
                        //开门方式是首卡或者常开状态时
                        if (result.OpenDoorWay == 3 || result.OpenDoorWay == 4)
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (j == 0)
                                    {
                                        beginTimePicker1.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetBeginTime().ToString("HH:mm");
                                        endTimePicker1.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetEndTime().ToString("HH:mm");
                                    }
                                    else if (j == 1)
                                    {
                                        beginTimePicker2.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetBeginTime().ToString("HH:mm");
                                        endTimePicker2.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetEndTime().ToString("HH:mm");
                                    }
                                    else if (j == 2)
                                    {
                                        beginTimePicker3.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetBeginTime().ToString("HH:mm");
                                        endTimePicker3.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetEndTime().ToString("HH:mm");
                                    }
                                    else if (j == 3)
                                    {
                                        beginTimePicker4.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetBeginTime().ToString("HH:mm");
                                        endTimePicker4.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetEndTime().ToString("HH:mm");
                                    }
                                    else if (j == 4)
                                    {
                                        beginTimePicker5.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetBeginTime().ToString("HH:mm");
                                        endTimePicker5.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetEndTime().ToString("HH:mm");
                                    }
                                    else if (j == 5)
                                    {
                                        beginTimePicker6.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetBeginTime().ToString("HH:mm");
                                        endTimePicker6.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetEndTime().ToString("HH:mm");
                                    }
                                    else if (j == 6)
                                    {
                                        beginTimePicker7.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetBeginTime().ToString("HH:mm");
                                        endTimePicker7.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetEndTime().ToString("HH:mm");
                                    }
                                    else if (j == 7)
                                    {
                                        beginTimePicker8.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetBeginTime().ToString("HH:mm");
                                        endTimePicker8.Text = result.weekTimeGroup.GetItem(i).GetItem(j).GetEndTime().ToString("HH:mm");
                                    }
                                }
                            }
                        }
                    }
                });
            };
        }

        private void BtnWriteWorkSetting_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            byte door = 1; //门
            byte use = 0; //功能是否启用
            byte openDoorWay = 1; //开门方式
            byte doorTriggerMode = 3; //门常开触发模式
            byte retainValue = 0; //保留值
            WeekTimeGroup tg = new WeekTimeGroup(8);
            if (rBtnDoorWorkSetting.Checked)
            {
                use = 1;
                if (rBtnOpenDoorWay2.Checked) //多卡
                {
                    openDoorWay = 2;
                }
                else if (rBtnOpenDoorWay3.Checked) //首卡
                {
                    openDoorWay = 3;
                }
                else if (rBtnOpenDoorWay4.Checked) //常开
                {
                    openDoorWay = 4;
                    if (rBtnDoorTriggerMode1.Checked) //常开触发模式_合法卡
                    {
                        doorTriggerMode = 1;
                    }
                    else if (rBtnDoorTriggerMode2.Checked) //常开触发模式_常开卡
                    {
                        doorTriggerMode = 2;
                    }
                }

                if (rBtnOpenDoorWay3.Checked || rBtnOpenDoorWay4.Checked)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        var day = tg.GetItem(i);
                        DateTime nw = DateTime.Now;
                        for (int j = 0; j < 8; j++)
                        {
                            var tz = day.GetItem(j) as TimeSegment;
                            if (j == 0)
                            {
                                tz.SetBeginTime(beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker1.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker1.Text).Minute);
                                tz.SetEndTime(endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker1.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker1.Text).Minute);
                            }
                            else if (j == 1)
                            {
                                tz.SetBeginTime(beginTimePicker2.Text == "" ? 0 : DateTime.Parse(beginTimePicker2.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker2.Text).Minute);
                                tz.SetEndTime(endTimePicker2.Text == "" ? 0 : DateTime.Parse(endTimePicker2.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker2.Text).Minute);
                            }
                            else if (j == 2)
                            {
                                tz.SetBeginTime(beginTimePicker3.Text == "" ? 0 : DateTime.Parse(beginTimePicker3.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker3.Text).Minute);
                                tz.SetEndTime(endTimePicker3.Text == "" ? 0 : DateTime.Parse(endTimePicker3.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker3.Text).Minute);
                            }
                            else if (j == 3)
                            {
                                tz.SetBeginTime(beginTimePicker4.Text == "" ? 0 : DateTime.Parse(beginTimePicker4.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker4.Text).Minute);
                                tz.SetEndTime(endTimePicker4.Text == "" ? 0 : DateTime.Parse(endTimePicker4.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker4.Text).Minute);
                            }
                            else if (j == 4)
                            {
                                tz.SetBeginTime(beginTimePicker5.Text == "" ? 0 : DateTime.Parse(beginTimePicker5.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker5.Text).Minute);
                                tz.SetEndTime(endTimePicker5.Text == "" ? 0 : DateTime.Parse(endTimePicker5.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker5.Text).Minute);
                            }
                            else if (j == 5)
                            {
                                tz.SetBeginTime(beginTimePicker6.Text == "" ? 0 : DateTime.Parse(beginTimePicker6.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker6.Text).Minute);
                                tz.SetEndTime(endTimePicker6.Text == "" ? 0 : DateTime.Parse(endTimePicker6.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker6.Text).Minute);
                            }
                            else if (j == 6)
                            {
                                tz.SetBeginTime(beginTimePicker7.Text == "" ? 0 : DateTime.Parse(beginTimePicker7.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker7.Text).Minute);
                                tz.SetEndTime(endTimePicker7.Text == "" ? 0 : DateTime.Parse(endTimePicker7.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker7.Text).Minute);
                            }
                            else if (j == 7)
                            {
                                tz.SetBeginTime(beginTimePicker8.Text == "" ? 0 : DateTime.Parse(beginTimePicker8.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker8.Text).Minute);
                                tz.SetEndTime(endTimePicker8.Text == "" ? 0 : DateTime.Parse(endTimePicker8.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker8.Text).Minute);
                            }
                        }
                    }
                }
            }

            if (cBoxDoor1.Checked)
            {
                door = 1;
                ReadDoorWorkSetting_Parameter par = new ReadDoorWorkSetting_Parameter(door, use, openDoorWay, doorTriggerMode, retainValue, tg);
                WriteDoorWorkSetting write = new WriteDoorWorkSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor2.Checked)
            {
                door = 2;
                ReadDoorWorkSetting_Parameter par = new ReadDoorWorkSetting_Parameter(door, use, openDoorWay, doorTriggerMode, retainValue, tg);
                WriteDoorWorkSetting write = new WriteDoorWorkSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor3.Checked)
            {
                door = 3;
                ReadDoorWorkSetting_Parameter par = new ReadDoorWorkSetting_Parameter(door, use, openDoorWay, doorTriggerMode, retainValue, tg);
                WriteDoorWorkSetting write = new WriteDoorWorkSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor4.Checked)
            {
                door = 4;
                ReadDoorWorkSetting_Parameter par = new ReadDoorWorkSetting_Parameter(door, use, openDoorWay, doorTriggerMode, retainValue, tg);
                WriteDoorWorkSetting write = new WriteDoorWorkSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
        }
        /// <summary>
        /// 选择不启用高级功能按钮时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBtnNoDoorWorkSetting_CheckedChanged(object sender, EventArgs e)
        {
            OpenDoorWayPanel.Visible = false;
            DoorTriggerModePanel.Visible = false;
            DoorOpenTimePanel.Visible = false;
        }

        /// <summary>
        /// 选择启用高级功能按钮时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBtnDoorWorkSetting_CheckedChanged(object sender, EventArgs e)
        {
            OpenDoorWayPanel.Visible = true;
        }
        /// <summary>
        /// 选择开门方式是普通按钮时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBtnOpenDoorWay1_CheckedChanged(object sender, EventArgs e)
        {
            DoorOpenTimePanel.Visible = false;
            DoorTriggerModePanel.Visible = false;
        }
        /// <summary>
        /// 选择开门方式是多卡按钮时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBtnOpenDoorWay2_CheckedChanged(object sender, EventArgs e)
        {
            DoorOpenTimePanel.Visible = false;
            DoorTriggerModePanel.Visible = false;
        }
        /// <summary>
        /// 选择开门方式是首卡按钮时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBtnOpenDoorWay3_CheckedChanged(object sender, EventArgs e)
        {
            DoorOpenTimePanel.Visible = true;
            DoorTriggerModePanel.Visible = false;
        }

        /// <summary>
        /// 选择开门方式是常开按钮时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBtnOpenDoorWay4_CheckedChanged(object sender, EventArgs e)
        {
            DoorTriggerModePanel.Visible = true;
            DoorOpenTimePanel.Visible = true;
        }
        #endregion

        #region 定时锁定门参数读写
        private void BtnReadAutoLockedSetting_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }
            StringBuilder sb = new StringBuilder();
            byte door = 1;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            if (cBoxDoor1.Checked)
            {
                door = 1;
                ReadAutoLockedSetting cmd = new ReadAutoLockedSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor2.Checked)
            {
                door = 2;
                ReadAutoLockedSetting cmd = new ReadAutoLockedSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor3.Checked)
            {
                door = 3;
                ReadAutoLockedSetting cmd = new ReadAutoLockedSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor4.Checked)
            {
                door = 4;
                ReadAutoLockedSetting cmd = new ReadAutoLockedSetting(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                AutoLockedSetting_Result result = cmde.Command.getResult() as AutoLockedSetting_Result;
                string useStr = result.Use == 0 ? "【0、不启用】" : "【1、启用】";
                string tip = "定时锁定门_门" + result.Door.ToString() + "：是否启用：" + useStr + "，时段详情：";
                sb.AppendLine(tip);
                for (int i = 0; i < 7; i++)
                {
                    sb.AppendLine(StringUtility.GetWeekStr(i));
                    for (int j = 0; j < 8; j++)
                    {
                        sb.AppendLine("  时段" + (j + 1) + "：" + StringUtility.TimeHourAndMinuteStr(result.weekTimeGroup.GetItem(i).GetItem(j).GetBeginTime(), result.weekTimeGroup.GetItem(i).GetItem(j).GetEndTime()));
                    }
                }
                sb.AppendLine("***************************************************************************************");
                Invoke(() =>
                {
                    if (result.Use == 0)
                    {
                        rBtnNoAutoLockedSetting.Checked = true;
                    }
                    txtAutoLockedSetting.Text = sb.ToString();
                });
            };
        }

        private void BtnWriteAutoLockedSetting_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            WeekTimeGroup tg = new WeekTimeGroup(8);

            for (int i = 0; i < 7; i++)
            {
                var day = tg.GetItem(i);
                DateTime nw = DateTime.Now;
                var tz = day.GetItem(0) as TimeSegment;
                tz.SetBeginTime(0, 0);
                tz.SetEndTime(0, 0);
            }

            byte door = 1;
            byte use = 0;
            if (rBtnAutoLockedSetting.Checked)
            {
                use = 1;
            }
            if (cBoxDoor1.Checked)
            {
                door = 1;
                AutoLockedSetting_Parameter par = new AutoLockedSetting_Parameter(door, use, tg);
                WriteAutoLockedSetting write = new WriteAutoLockedSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor2.Checked)
            {
                door = 2;
                AutoLockedSetting_Parameter par = new AutoLockedSetting_Parameter(door, use, tg);
                WriteAutoLockedSetting write = new WriteAutoLockedSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor3.Checked)
            {
                door = 3;
                AutoLockedSetting_Parameter par = new AutoLockedSetting_Parameter(door, use, tg);
                WriteAutoLockedSetting write = new WriteAutoLockedSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor4.Checked)
            {
                door = 4;
                AutoLockedSetting_Parameter par = new AutoLockedSetting_Parameter(door, use, tg);
                WriteAutoLockedSetting write = new WriteAutoLockedSetting(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
        }
        #endregion

        #region 开锁时输出时长
        private void BtnReadRelayReleaseTime_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }
            StringBuilder sb = new StringBuilder();
            ushort ReleaseTime = 0; //开锁时输出时长
            string tip = string.Empty;
            byte door = 1;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            if (cBoxDoor1.Checked)
            {
                door = 1;
                ReadRelayReleaseTime cmd = new ReadRelayReleaseTime(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor2.Checked)
            {
                door = 2;
                ReadRelayReleaseTime cmd = new ReadRelayReleaseTime(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor3.Checked)
            {
                door = 3;
                ReadRelayReleaseTime cmd = new ReadRelayReleaseTime(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor4.Checked)
            {
                door = 4;
                ReadRelayReleaseTime cmd = new ReadRelayReleaseTime(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                RelayReleaseTime_Result result = cmde.Command.getResult() as RelayReleaseTime_Result;

                ReleaseTime = result.ReleaseTime; //开锁时输出时长
                sb.Clear();
                sb.AppendLine("门" + result.Door + "：开锁时输出时长：");
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
                    cbxReleaseTime.Text = tip.Replace("秒", "");
                });
                sb.AppendLine(tip);
                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteRelayReleaseTime_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }

            string reg = @"^\+?[0-9]*$";
            if (!Regex.IsMatch(cbxReleaseTime.Text.Trim(), reg))
            {
                if (cbxReleaseTime.Text != "0.5")
                {
                    MsgErr("请输入正确时间！");
                    return;
                }
            }
            if (Regex.IsMatch(cbxReleaseTime.Text.Trim(), reg))
            {
                if (Convert.ToUInt32(cbxReleaseTime.Text) < 0 || Convert.ToUInt32(cbxReleaseTime.Text) > 65535)
                {
                    MsgErr("请输入正确时间！");
                    return;
                }
            }

            byte door = 1;
            ushort releaseTime = 0;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            if (cbxReleaseTime.Text != "0.5")
            {
                releaseTime = Convert.ToUInt16(cbxReleaseTime.Text);
            }
            if (cBoxDoor1.Checked)
            {
                door = 1;
                WriteRelayReleaseTime_Parameter par = new WriteRelayReleaseTime_Parameter(door, releaseTime);
                WriteRelayReleaseTime write = new WriteRelayReleaseTime(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor2.Checked)
            {
                door = 2;
                WriteRelayReleaseTime_Parameter par = new WriteRelayReleaseTime_Parameter(door, releaseTime);
                WriteRelayReleaseTime write = new WriteRelayReleaseTime(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor3.Checked)
            {
                door = 3;
                WriteRelayReleaseTime_Parameter par = new WriteRelayReleaseTime_Parameter(door, releaseTime);
                WriteRelayReleaseTime write = new WriteRelayReleaseTime(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor4.Checked)
            {
                door = 4;
                WriteRelayReleaseTime_Parameter par = new WriteRelayReleaseTime_Parameter(door, releaseTime);
                WriteRelayReleaseTime write = new WriteRelayReleaseTime(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
        }
        #endregion

        #region 重复读卡间隔参数
        private void BtnReadReaderInterval_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }
            StringBuilder sb = new StringBuilder();
            string UseStr = string.Empty;
            string DetectionModeStr = string.Empty;
            byte door = 1;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            if (cBoxDoor1.Checked)
            {
                door = 1;
                ReadReaderInterval cmd = new ReadReaderInterval(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor2.Checked)
            {
                door = 2;
                ReadReaderInterval cmd = new ReadReaderInterval(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor3.Checked)
            {
                door = 3;
                ReadReaderInterval cmd = new ReadReaderInterval(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }
            if (cBoxDoor4.Checked)
            {
                door = 4;
                ReadReaderInterval cmd = new ReadReaderInterval(cmdDtl, new DoorPort_Parameter(door));
                mMainForm.AddCommand(cmd);
            }

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReaderInterval_Result result = cmde.Command.getResult() as ReaderInterval_Result;
                UseStr = result.Use == 0 ? "【0、不启用】" : "【1、启用】";
                if (result.DetectionMode == 1)
                {
                    DetectionModeStr = "【1、读卡有记录】";
                }
                else if (result.DetectionMode == 2)
                {
                    DetectionModeStr = "【2、读卡无记录】";
                }
                else if (result.DetectionMode == 3)
                {
                    DetectionModeStr = "【3、读卡不做响应】";
                }

                sb.Clear();
                sb.AppendLine("门" + result.Door + "：功能状态：" + UseStr);
                if (result.Use == 1)
                {
                    sb.AppendLine("；检测模式：" + DetectionModeStr);
                }

                Invoke(() =>
                {
                    if (result.Use == 0)
                    {
                        rBtnNoReaderInterval.Checked = true;
                    }
                    cbxDetectionMode.SelectedIndex = result.DetectionMode - 1;
                });
                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void BtnWriteReaderInterval_Click(object sender, EventArgs e)
        {
            if (!cBoxDoor1.Checked && !cBoxDoor2.Checked && !cBoxDoor3.Checked && !cBoxDoor4.Checked)
            {
                MsgErr("请勾选需要操作的门！");
                return;
            }

            byte door = 1; //门
            byte use = 0; //功能是否启用
            byte detectionMode = 1; //检测模式
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            if (rBtnReaderInterval.Checked)
            {
                use = 1;
            }
            detectionMode = Convert.ToByte(cbxDetectionMode.SelectedIndex + 1);
            if (cBoxDoor1.Checked)
            {
                door = 1;
                WriteReaderInterval_Parameter par = new WriteReaderInterval_Parameter(door, use, detectionMode);
                WriteReaderInterval write = new WriteReaderInterval(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor2.Checked)
            {
                door = 2;
                WriteReaderInterval_Parameter par = new WriteReaderInterval_Parameter(door, use, detectionMode);
                WriteReaderInterval write = new WriteReaderInterval(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor3.Checked)
            {
                door = 3;
                WriteReaderInterval_Parameter par = new WriteReaderInterval_Parameter(door, use, detectionMode);
                WriteReaderInterval write = new WriteReaderInterval(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
            if (cBoxDoor4.Checked)
            {
                door = 4;
                WriteReaderInterval_Parameter par = new WriteReaderInterval_Parameter(door, use, detectionMode);
                WriteReaderInterval write = new WriteReaderInterval(cmdDtl, par);
                mMainForm.AddCommand(write);
            }
        }
        #endregion

        #region 非法读卡报警

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
                Invoke(() =>
                {
                    cmdInvalidCardAlarmOptionUse.SelectedIndex = result.Use ? 0 : 1;
                });

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
    }
}
