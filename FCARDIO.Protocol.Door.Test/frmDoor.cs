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

            //未注册卡报警
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

            var par = new FC8800.Door.AlarmPassword.WriteAlarmPassword_parameter(door, use,pwd,alarmOption);
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
                time[i] = i+1 + "秒";
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
                label10.Show ();
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
                    for (int i = 0; i < result.DataLength; i++)
                    {
                        //门1读卡器字节数
                        if (i == 0)
                        {
                            if (result.Door[i] == 1)
                            {
                                cbxDoor1ReaderOption.SelectedIndex = 2;
                                sb.Append("门"+ (i + 1) + str[0]);
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
            else if(Door1ReaderOption == 1) //二字节
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
                    for (int i = 0; i < result.DataLength; i++)
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

            byte[] Door = new byte[4];
            Door[0] = Convert.ToByte(cBoxDoor1.Checked ? 1 : 0);
            Door[1] = Convert.ToByte(cBoxDoor2.Checked ? 1 : 0);
            Door[2] = Convert.ToByte(cBoxDoor3.Checked ? 1 : 0);
            Door[3] = Convert.ToByte(cBoxDoor4.Checked ? 1 : 0);

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

            byte[] Door = new byte[4];
            Door[0] = Convert.ToByte(cBoxDoor1.Checked ? 1 : 0);
            Door[1] = Convert.ToByte(cBoxDoor2.Checked ? 1 : 0);
            Door[2] = Convert.ToByte(cBoxDoor3.Checked ? 1 : 0);
            Door[3] = Convert.ToByte(cBoxDoor4.Checked ? 1 : 0);

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

            byte[] Door = new byte[4];
            Door[0] = Convert.ToByte(cBoxDoor1.Checked ? 1 : 0);
            Door[1] = Convert.ToByte(cBoxDoor2.Checked ? 1 : 0);
            Door[2] = Convert.ToByte(cBoxDoor3.Checked ? 1 : 0);
            Door[3] = Convert.ToByte(cBoxDoor4.Checked ? 1 : 0);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            HoldDoor cmd = new HoldDoor(cmdDtl, new Remote_Parameter(Door));
            mMainForm.AddCommand(cmd);
        }

        private void BtnOpenDoor_CheckNum_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
