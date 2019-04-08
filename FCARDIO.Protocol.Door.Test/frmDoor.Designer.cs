namespace FCARDIO.Protocol.Door.Test
{
    partial class frmDoor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.butWriteSensorAlarmSetting = new System.Windows.Forms.Button();
            this.butReadSensorAlarmSetting = new System.Windows.Forms.Button();
            this.cmbWeek = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbSensorAlarmSetting = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.butWriteOvertimeAlarmSetting = new System.Windows.Forms.Button();
            this.butReadOvertimeAlarmSetting = new System.Windows.Forms.Button();
            this.cmbAlarm = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbOverTime = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbOvertimeAlarmSetting = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdAlarmPassword = new System.Windows.Forms.ComboBox();
            this.cmbAlarmOption = new System.Windows.Forms.ComboBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.butWriteAlarmPassword = new System.Windows.Forms.Button();
            this.butReadAlarmPassword = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ButWriteInvalidCardAlarmOption = new System.Windows.Forms.Button();
            this.butReadInvalidCardAlarmOption = new System.Windows.Forms.Button();
            this.cmdInvalidCardAlarmOptionUse = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdDoorNum = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(757, 593);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(749, 567);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(749, 567);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "功能2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.butWriteSensorAlarmSetting);
            this.groupBox4.Controls.Add(this.butReadSensorAlarmSetting);
            this.groupBox4.Controls.Add(this.cmbWeek);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.cmbSensorAlarmSetting);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(18, 265);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(664, 259);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "门磁报警参数";
            // 
            // butWriteSensorAlarmSetting
            // 
            this.butWriteSensorAlarmSetting.Location = new System.Drawing.Point(584, 216);
            this.butWriteSensorAlarmSetting.Name = "butWriteSensorAlarmSetting";
            this.butWriteSensorAlarmSetting.Size = new System.Drawing.Size(62, 23);
            this.butWriteSensorAlarmSetting.TabIndex = 15;
            this.butWriteSensorAlarmSetting.Text = "写入";
            this.butWriteSensorAlarmSetting.UseVisualStyleBackColor = true;
            this.butWriteSensorAlarmSetting.Click += new System.EventHandler(this.butWriteSensorAlarmSetting_Click);
            // 
            // butReadSensorAlarmSetting
            // 
            this.butReadSensorAlarmSetting.Location = new System.Drawing.Point(520, 216);
            this.butReadSensorAlarmSetting.Name = "butReadSensorAlarmSetting";
            this.butReadSensorAlarmSetting.Size = new System.Drawing.Size(62, 23);
            this.butReadSensorAlarmSetting.TabIndex = 16;
            this.butReadSensorAlarmSetting.Text = "读取";
            this.butReadSensorAlarmSetting.UseVisualStyleBackColor = true;
            this.butReadSensorAlarmSetting.Click += new System.EventHandler(this.butReadSensorAlarmSetting_Click);
            // 
            // cmbWeek
            // 
            this.cmbWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeek.FormattingEnabled = true;
            this.cmbWeek.Location = new System.Drawing.Point(345, 20);
            this.cmbWeek.Name = "cmbWeek";
            this.cmbWeek.Size = new System.Drawing.Size(97, 20);
            this.cmbWeek.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "门磁报警不生效时段:";
            // 
            // cmbSensorAlarmSetting
            // 
            this.cmbSensorAlarmSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSensorAlarmSetting.FormattingEnabled = true;
            this.cmbSensorAlarmSetting.Location = new System.Drawing.Point(95, 20);
            this.cmbSensorAlarmSetting.Name = "cmbSensorAlarmSetting";
            this.cmbSensorAlarmSetting.Size = new System.Drawing.Size(97, 20);
            this.cmbSensorAlarmSetting.TabIndex = 9;
            this.cmbSensorAlarmSetting.SelectedIndexChanged += new System.EventHandler(this.cmbSensorAlarmSetting_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "是否启用:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.butWriteOvertimeAlarmSetting);
            this.groupBox3.Controls.Add(this.butReadOvertimeAlarmSetting);
            this.groupBox3.Controls.Add(this.cmbAlarm);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmbOverTime);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmbOvertimeAlarmSetting);
            this.groupBox3.Location = new System.Drawing.Point(18, 182);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(664, 65);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "开门超时提示参数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "是否启用:";
            // 
            // butWriteOvertimeAlarmSetting
            // 
            this.butWriteOvertimeAlarmSetting.Location = new System.Drawing.Point(584, 21);
            this.butWriteOvertimeAlarmSetting.Name = "butWriteOvertimeAlarmSetting";
            this.butWriteOvertimeAlarmSetting.Size = new System.Drawing.Size(62, 23);
            this.butWriteOvertimeAlarmSetting.TabIndex = 13;
            this.butWriteOvertimeAlarmSetting.Text = "写入";
            this.butWriteOvertimeAlarmSetting.UseVisualStyleBackColor = true;
            this.butWriteOvertimeAlarmSetting.Click += new System.EventHandler(this.butWriteOvertimeAlarmSetting_Click);
            // 
            // butReadOvertimeAlarmSetting
            // 
            this.butReadOvertimeAlarmSetting.Location = new System.Drawing.Point(520, 21);
            this.butReadOvertimeAlarmSetting.Name = "butReadOvertimeAlarmSetting";
            this.butReadOvertimeAlarmSetting.Size = new System.Drawing.Size(62, 23);
            this.butReadOvertimeAlarmSetting.TabIndex = 14;
            this.butReadOvertimeAlarmSetting.Text = "读取";
            this.butReadOvertimeAlarmSetting.UseVisualStyleBackColor = true;
            this.butReadOvertimeAlarmSetting.Click += new System.EventHandler(this.butReadOvertimeAlarmSetting_Click);
            // 
            // cmbAlarm
            // 
            this.cmbAlarm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlarm.FormattingEnabled = true;
            this.cmbAlarm.Location = new System.Drawing.Point(421, 22);
            this.cmbAlarm.Name = "cmbAlarm";
            this.cmbAlarm.Size = new System.Drawing.Size(97, 20);
            this.cmbAlarm.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(360, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "报警输出:";
            // 
            // cmbOverTime
            // 
            this.cmbOverTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOverTime.FormattingEnabled = true;
            this.cmbOverTime.Location = new System.Drawing.Point(261, 22);
            this.cmbOverTime.Name = "cmbOverTime";
            this.cmbOverTime.Size = new System.Drawing.Size(97, 20);
            this.cmbOverTime.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(176, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "开门时间上限:";
            // 
            // cmbOvertimeAlarmSetting
            // 
            this.cmbOvertimeAlarmSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOvertimeAlarmSetting.FormattingEnabled = true;
            this.cmbOvertimeAlarmSetting.Location = new System.Drawing.Point(77, 22);
            this.cmbOvertimeAlarmSetting.Name = "cmbOvertimeAlarmSetting";
            this.cmbOvertimeAlarmSetting.Size = new System.Drawing.Size(97, 20);
            this.cmbOvertimeAlarmSetting.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdAlarmPassword);
            this.groupBox2.Controls.Add(this.cmbAlarmOption);
            this.groupBox2.Controls.Add(this.Password);
            this.groupBox2.Controls.Add(this.butWriteAlarmPassword);
            this.groupBox2.Controls.Add(this.butReadAlarmPassword);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(18, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(664, 94);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "胁迫报警密码";
            // 
            // cmdAlarmPassword
            // 
            this.cmdAlarmPassword.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdAlarmPassword.FormattingEnabled = true;
            this.cmdAlarmPassword.Location = new System.Drawing.Point(71, 28);
            this.cmdAlarmPassword.Name = "cmdAlarmPassword";
            this.cmdAlarmPassword.Size = new System.Drawing.Size(121, 20);
            this.cmdAlarmPassword.TabIndex = 7;
            // 
            // cmbAlarmOption
            // 
            this.cmbAlarmOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlarmOption.FormattingEnabled = true;
            this.cmbAlarmOption.Location = new System.Drawing.Point(270, 28);
            this.cmbAlarmOption.Name = "cmbAlarmOption";
            this.cmbAlarmOption.Size = new System.Drawing.Size(149, 20);
            this.cmbAlarmOption.TabIndex = 6;
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(105, 59);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(87, 21);
            this.Password.TabIndex = 5;
            this.Password.Text = "110110";
            // 
            // butWriteAlarmPassword
            // 
            this.butWriteAlarmPassword.Location = new System.Drawing.Point(276, 58);
            this.butWriteAlarmPassword.Name = "butWriteAlarmPassword";
            this.butWriteAlarmPassword.Size = new System.Drawing.Size(62, 23);
            this.butWriteAlarmPassword.TabIndex = 3;
            this.butWriteAlarmPassword.Text = "写入";
            this.butWriteAlarmPassword.UseVisualStyleBackColor = true;
            this.butWriteAlarmPassword.Click += new System.EventHandler(this.butWriteAlarmPassword_Click);
            // 
            // butReadAlarmPassword
            // 
            this.butReadAlarmPassword.Location = new System.Drawing.Point(208, 58);
            this.butReadAlarmPassword.Name = "butReadAlarmPassword";
            this.butReadAlarmPassword.Size = new System.Drawing.Size(62, 23);
            this.butReadAlarmPassword.TabIndex = 4;
            this.butReadAlarmPassword.Text = "读取";
            this.butReadAlarmPassword.UseVisualStyleBackColor = true;
            this.butReadAlarmPassword.Click += new System.EventHandler(this.butReadAlarmPassword_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "胁迫报警密码:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "报警模式:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "是否启用:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ButWriteInvalidCardAlarmOption);
            this.groupBox1.Controls.Add(this.butReadInvalidCardAlarmOption);
            this.groupBox1.Controls.Add(this.cmdInvalidCardAlarmOptionUse);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(18, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "未注册卡报警功能";
            // 
            // ButWriteInvalidCardAlarmOption
            // 
            this.ButWriteInvalidCardAlarmOption.Location = new System.Drawing.Point(257, 22);
            this.ButWriteInvalidCardAlarmOption.Name = "ButWriteInvalidCardAlarmOption";
            this.ButWriteInvalidCardAlarmOption.Size = new System.Drawing.Size(62, 23);
            this.ButWriteInvalidCardAlarmOption.TabIndex = 2;
            this.ButWriteInvalidCardAlarmOption.Text = "写入";
            this.ButWriteInvalidCardAlarmOption.UseVisualStyleBackColor = true;
            this.ButWriteInvalidCardAlarmOption.Click += new System.EventHandler(this.ButWriteInvalidCardAlarmOption_Click);
            // 
            // butReadInvalidCardAlarmOption
            // 
            this.butReadInvalidCardAlarmOption.Location = new System.Drawing.Point(189, 22);
            this.butReadInvalidCardAlarmOption.Name = "butReadInvalidCardAlarmOption";
            this.butReadInvalidCardAlarmOption.Size = new System.Drawing.Size(62, 23);
            this.butReadInvalidCardAlarmOption.TabIndex = 2;
            this.butReadInvalidCardAlarmOption.Text = "读取";
            this.butReadInvalidCardAlarmOption.UseVisualStyleBackColor = true;
            this.butReadInvalidCardAlarmOption.Click += new System.EventHandler(this.butReadInvalidCardAlarmOption_Click);
            // 
            // cmdInvalidCardAlarmOptionUse
            // 
            this.cmdInvalidCardAlarmOptionUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdInvalidCardAlarmOptionUse.FormattingEnabled = true;
            this.cmdInvalidCardAlarmOptionUse.Location = new System.Drawing.Point(62, 23);
            this.cmdInvalidCardAlarmOptionUse.Name = "cmdInvalidCardAlarmOptionUse";
            this.cmdInvalidCardAlarmOptionUse.Size = new System.Drawing.Size(121, 20);
            this.cmdInvalidCardAlarmOptionUse.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "开关:";
            // 
            // cmdDoorNum
            // 
            this.cmdDoorNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdDoorNum.FormattingEnabled = true;
            this.cmdDoorNum.Location = new System.Drawing.Point(57, 10);
            this.cmdDoorNum.Name = "cmdDoorNum";
            this.cmdDoorNum.Size = new System.Drawing.Size(121, 20);
            this.cmdDoorNum.TabIndex = 1;
            // 
            // frmDoor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 644);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cmdDoorNum);
            this.Name = "frmDoor";
            this.Text = "frmDoor";
            this.Load += new System.EventHandler(this.frmDoor_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ButWriteInvalidCardAlarmOption;
        private System.Windows.Forms.Button butReadInvalidCardAlarmOption;
        private System.Windows.Forms.ComboBox cmdInvalidCardAlarmOptionUse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbAlarmOption;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button butWriteAlarmPassword;
        private System.Windows.Forms.Button butReadAlarmPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmdDoorNum;
        private System.Windows.Forms.ComboBox cmdAlarmPassword;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbOverTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbAlarm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button butWriteOvertimeAlarmSetting;
        private System.Windows.Forms.Button butReadOvertimeAlarmSetting;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbSensorAlarmSetting;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbOvertimeAlarmSetting;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbWeek;
        private System.Windows.Forms.Button butWriteSensorAlarmSetting;
        private System.Windows.Forms.Button butReadSensorAlarmSetting;
        private System.Windows.Forms.Timer timer1;
    }
}