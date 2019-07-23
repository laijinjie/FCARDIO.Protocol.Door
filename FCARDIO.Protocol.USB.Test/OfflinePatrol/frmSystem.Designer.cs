namespace FCARDIO.Protocol.USB.OfflinePatrol.Test
{
    partial class frmSystem
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnReadVersion = new System.Windows.Forms.Button();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.dtpExpireTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.btnWriteExpireTime = new System.Windows.Forms.Button();
            this.btnReadExpireTime = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnWriteSN = new System.Windows.Forms.Button();
            this.btnReadSN = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbStorageMode1 = new System.Windows.Forms.RadioButton();
            this.rbStorageMode0 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnReadSystemStatus = new System.Windows.Forms.Button();
            this.lbStartCount = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lbSystemRecordCount = new System.Windows.Forms.Label();
            this.lbPatrolEmplCount = new System.Windows.Forms.Label();
            this.lbElectricity = new System.Windows.Forms.Label();
            this.lbFormatCount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbCardRecordCount = new System.Windows.Forms.Label();
            this.lbPatrolEmpl = new System.Windows.Forms.Label();
            this.lbVoltage = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnWriteStartupHoldTime = new System.Windows.Forms.Button();
            this.btnReadStartupHoldTime = new System.Windows.Forms.Button();
            this.cmbStartupHoldTime = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.WriteLEDOpenTime = new System.Windows.Forms.Button();
            this.ReadLEDOpenTime = new System.Windows.Forms.Button();
            this.cmbLEDOpenTime = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.WriteCreateTime = new System.Windows.Forms.Button();
            this.ReadCreateTime = new System.Windows.Forms.Button();
            this.dtpCreateTime = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.btnReadRecordStorageMode = new System.Windows.Forms.Button();
            this.btnWriteRecordStorageMode = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnReadVersion);
            this.groupBox3.Controls.Add(this.txtVersion);
            this.groupBox3.Controls.Add(this.dtpExpireTime);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnWriteExpireTime);
            this.groupBox3.Controls.Add(this.btnReadExpireTime);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(203, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(472, 64);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "截止日期和版本号";
            // 
            // btnReadVersion
            // 
            this.btnReadVersion.Location = new System.Drawing.Point(373, 26);
            this.btnReadVersion.Name = "btnReadVersion";
            this.btnReadVersion.Size = new System.Drawing.Size(40, 22);
            this.btnReadVersion.TabIndex = 24;
            this.btnReadVersion.Text = "读取";
            this.btnReadVersion.UseVisualStyleBackColor = true;
            this.btnReadVersion.Click += new System.EventHandler(this.BtnReadVersion_Click);
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(306, 27);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(61, 21);
            this.txtVersion.TabIndex = 22;
            // 
            // dtpExpireTime
            // 
            this.dtpExpireTime.CustomFormat = "yyyy-MM-dd";
            this.dtpExpireTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpireTime.Location = new System.Drawing.Point(64, 27);
            this.dtpExpireTime.Name = "dtpExpireTime";
            this.dtpExpireTime.Size = new System.Drawing.Size(85, 21);
            this.dtpExpireTime.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "版本号：";
            // 
            // btnWriteExpireTime
            // 
            this.btnWriteExpireTime.Location = new System.Drawing.Point(201, 26);
            this.btnWriteExpireTime.Name = "btnWriteExpireTime";
            this.btnWriteExpireTime.Size = new System.Drawing.Size(40, 22);
            this.btnWriteExpireTime.TabIndex = 21;
            this.btnWriteExpireTime.Text = "写入";
            this.btnWriteExpireTime.UseVisualStyleBackColor = true;
            this.btnWriteExpireTime.Click += new System.EventHandler(this.BtnWriteExpireTime_Click);
            // 
            // btnReadExpireTime
            // 
            this.btnReadExpireTime.Location = new System.Drawing.Point(155, 26);
            this.btnReadExpireTime.Name = "btnReadExpireTime";
            this.btnReadExpireTime.Size = new System.Drawing.Size(40, 22);
            this.btnReadExpireTime.TabIndex = 20;
            this.btnReadExpireTime.Text = "读取";
            this.btnReadExpireTime.UseVisualStyleBackColor = true;
            this.btnReadExpireTime.Click += new System.EventHandler(this.BtnReadExpireTime_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "截止日期：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnWriteSN);
            this.groupBox2.Controls.Add(this.btnReadSN);
            this.groupBox2.Controls.Add(this.txtAddress);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 64);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "地址码";
            // 
            // btnWriteSN
            // 
            this.btnWriteSN.Location = new System.Drawing.Point(141, 26);
            this.btnWriteSN.Name = "btnWriteSN";
            this.btnWriteSN.Size = new System.Drawing.Size(40, 22);
            this.btnWriteSN.TabIndex = 21;
            this.btnWriteSN.Text = "写入";
            this.btnWriteSN.UseVisualStyleBackColor = true;
            this.btnWriteSN.Click += new System.EventHandler(this.BtnWriteSN_Click);
            // 
            // btnReadSN
            // 
            this.btnReadSN.Location = new System.Drawing.Point(95, 26);
            this.btnReadSN.Name = "btnReadSN";
            this.btnReadSN.Size = new System.Drawing.Size(40, 22);
            this.btnReadSN.TabIndex = 20;
            this.btnReadSN.Text = "读取";
            this.btnReadSN.UseVisualStyleBackColor = true;
            this.btnReadSN.Click += new System.EventHandler(this.BtnReadSN_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(58, 27);
            this.txtAddress.MaxLength = 3;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(30, 21);
            this.txtAddress.TabIndex = 19;
            this.txtAddress.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "地址码：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnWriteRecordStorageMode);
            this.groupBox1.Controls.Add(this.btnReadRecordStorageMode);
            this.groupBox1.Controls.Add(this.rbStorageMode1);
            this.groupBox1.Controls.Add(this.rbStorageMode0);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(663, 53);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "功能参数";
            // 
            // rbStorageMode1
            // 
            this.rbStorageMode1.AutoSize = true;
            this.rbStorageMode1.Location = new System.Drawing.Point(325, 29);
            this.rbStorageMode1.Name = "rbStorageMode1";
            this.rbStorageMode1.Size = new System.Drawing.Size(107, 16);
            this.rbStorageMode1.TabIndex = 25;
            this.rbStorageMode1.Text = "不再保存新记录";
            this.rbStorageMode1.UseVisualStyleBackColor = true;
            // 
            // rbStorageMode0
            // 
            this.rbStorageMode0.AutoSize = true;
            this.rbStorageMode0.Checked = true;
            this.rbStorageMode0.Location = new System.Drawing.Point(221, 29);
            this.rbStorageMode0.Name = "rbStorageMode0";
            this.rbStorageMode0.Size = new System.Drawing.Size(95, 16);
            this.rbStorageMode0.TabIndex = 24;
            this.rbStorageMode0.TabStop = true;
            this.rbStorageMode0.Text = "循环覆盖存储";
            this.rbStorageMode0.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "记录存满后：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "记录存储方式：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnReadSystemStatus);
            this.groupBox4.Controls.Add(this.lbStartCount);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.lbSystemRecordCount);
            this.groupBox4.Controls.Add(this.lbPatrolEmplCount);
            this.groupBox4.Controls.Add(this.lbElectricity);
            this.groupBox4.Controls.Add(this.lbFormatCount);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.lbCardRecordCount);
            this.groupBox4.Controls.Add(this.lbPatrolEmpl);
            this.groupBox4.Controls.Add(this.lbVoltage);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.lbTime);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(13, 172);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(662, 133);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "设备运行信息";
            // 
            // btnReadSystemStatus
            // 
            this.btnReadSystemStatus.Location = new System.Drawing.Point(563, 101);
            this.btnReadSystemStatus.Name = "btnReadSystemStatus";
            this.btnReadSystemStatus.Size = new System.Drawing.Size(93, 23);
            this.btnReadSystemStatus.TabIndex = 25;
            this.btnReadSystemStatus.Text = "读取运行信息";
            this.btnReadSystemStatus.UseVisualStyleBackColor = true;
            this.btnReadSystemStatus.Click += new System.EventHandler(this.BtnReadSystemStatus_Click);
            // 
            // lbStartCount
            // 
            this.lbStartCount.AutoSize = true;
            this.lbStartCount.Location = new System.Drawing.Point(508, 21);
            this.lbStartCount.Name = "lbStartCount";
            this.lbStartCount.Size = new System.Drawing.Size(65, 12);
            this.lbStartCount.TabIndex = 17;
            this.lbStartCount.Text = "开机次数：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(437, 21);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 16;
            this.label18.Text = "开机次数：";
            // 
            // lbSystemRecordCount
            // 
            this.lbSystemRecordCount.AutoSize = true;
            this.lbSystemRecordCount.Location = new System.Drawing.Point(319, 112);
            this.lbSystemRecordCount.Name = "lbSystemRecordCount";
            this.lbSystemRecordCount.Size = new System.Drawing.Size(89, 12);
            this.lbSystemRecordCount.TabIndex = 15;
            this.lbSystemRecordCount.Text = "系统新记录数：";
            // 
            // lbPatrolEmplCount
            // 
            this.lbPatrolEmplCount.AutoSize = true;
            this.lbPatrolEmplCount.Location = new System.Drawing.Point(322, 79);
            this.lbPatrolEmplCount.Name = "lbPatrolEmplCount";
            this.lbPatrolEmplCount.Size = new System.Drawing.Size(65, 12);
            this.lbPatrolEmplCount.TabIndex = 14;
            this.lbPatrolEmplCount.Text = "已存人员：";
            // 
            // lbElectricity
            // 
            this.lbElectricity.AutoSize = true;
            this.lbElectricity.Location = new System.Drawing.Point(322, 50);
            this.lbElectricity.Name = "lbElectricity";
            this.lbElectricity.Size = new System.Drawing.Size(41, 12);
            this.lbElectricity.TabIndex = 13;
            this.lbElectricity.Text = "电量：";
            // 
            // lbFormatCount
            // 
            this.lbFormatCount.AutoSize = true;
            this.lbFormatCount.Location = new System.Drawing.Point(320, 21);
            this.lbFormatCount.Name = "lbFormatCount";
            this.lbFormatCount.Size = new System.Drawing.Size(65, 12);
            this.lbFormatCount.TabIndex = 12;
            this.lbFormatCount.Text = "初始化次数";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(226, 112);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 11;
            this.label10.Text = "系统新记录数：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(250, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "已存人员：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(274, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 9;
            this.label12.Text = "电量：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(238, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 8;
            this.label13.Text = "初始化次数：";
            // 
            // lbCardRecordCount
            // 
            this.lbCardRecordCount.AutoSize = true;
            this.lbCardRecordCount.Location = new System.Drawing.Point(91, 112);
            this.lbCardRecordCount.Name = "lbCardRecordCount";
            this.lbCardRecordCount.Size = new System.Drawing.Size(89, 12);
            this.lbCardRecordCount.TabIndex = 7;
            this.lbCardRecordCount.Text = "读卡新记录数：";
            // 
            // lbPatrolEmpl
            // 
            this.lbPatrolEmpl.AutoSize = true;
            this.lbPatrolEmpl.Location = new System.Drawing.Point(94, 79);
            this.lbPatrolEmpl.Name = "lbPatrolEmpl";
            this.lbPatrolEmpl.Size = new System.Drawing.Size(65, 12);
            this.lbPatrolEmpl.TabIndex = 6;
            this.lbPatrolEmpl.Text = "已选人员：";
            // 
            // lbVoltage
            // 
            this.lbVoltage.AutoSize = true;
            this.lbVoltage.Location = new System.Drawing.Point(94, 50);
            this.lbVoltage.Name = "lbVoltage";
            this.lbVoltage.Size = new System.Drawing.Size(41, 12);
            this.lbVoltage.TabIndex = 5;
            this.lbVoltage.Text = "电压：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(-1, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "读卡新记录数：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "已选人员：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "电压：";
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Location = new System.Drawing.Point(92, 21);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(65, 12);
            this.lbTime.TabIndex = 1;
            this.lbTime.Text = "出厂日期：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "出厂日期：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnWriteStartupHoldTime);
            this.groupBox5.Controls.Add(this.btnReadStartupHoldTime);
            this.groupBox5.Controls.Add(this.cmbStartupHoldTime);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.WriteLEDOpenTime);
            this.groupBox5.Controls.Add(this.ReadLEDOpenTime);
            this.groupBox5.Controls.Add(this.cmbLEDOpenTime);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.WriteCreateTime);
            this.groupBox5.Controls.Add(this.ReadCreateTime);
            this.groupBox5.Controls.Add(this.dtpCreateTime);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Location = new System.Drawing.Point(14, 325);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(661, 99);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "参数设置";
            // 
            // btnWriteStartupHoldTime
            // 
            this.btnWriteStartupHoldTime.Location = new System.Drawing.Point(610, 20);
            this.btnWriteStartupHoldTime.Name = "btnWriteStartupHoldTime";
            this.btnWriteStartupHoldTime.Size = new System.Drawing.Size(40, 22);
            this.btnWriteStartupHoldTime.TabIndex = 34;
            this.btnWriteStartupHoldTime.Text = "写入";
            this.btnWriteStartupHoldTime.UseVisualStyleBackColor = true;
            this.btnWriteStartupHoldTime.Click += new System.EventHandler(this.BtnWriteStartupHoldTime_Click);
            // 
            // btnReadStartupHoldTime
            // 
            this.btnReadStartupHoldTime.Location = new System.Drawing.Point(564, 20);
            this.btnReadStartupHoldTime.Name = "btnReadStartupHoldTime";
            this.btnReadStartupHoldTime.Size = new System.Drawing.Size(40, 22);
            this.btnReadStartupHoldTime.TabIndex = 33;
            this.btnReadStartupHoldTime.Text = "读取";
            this.btnReadStartupHoldTime.UseVisualStyleBackColor = true;
            this.btnReadStartupHoldTime.Click += new System.EventHandler(this.BtnReadStartupHoldTime_Click);
            // 
            // cmbStartupHoldTime
            // 
            this.cmbStartupHoldTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartupHoldTime.FormattingEnabled = true;
            this.cmbStartupHoldTime.Location = new System.Drawing.Point(473, 21);
            this.cmbStartupHoldTime.Name = "cmbStartupHoldTime";
            this.cmbStartupHoldTime.Size = new System.Drawing.Size(85, 20);
            this.cmbStartupHoldTime.TabIndex = 32;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(378, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 12);
            this.label16.TabIndex = 31;
            this.label16.Text = "开机保持(秒)：";
            // 
            // WriteLEDOpenTime
            // 
            this.WriteLEDOpenTime.Location = new System.Drawing.Point(254, 62);
            this.WriteLEDOpenTime.Name = "WriteLEDOpenTime";
            this.WriteLEDOpenTime.Size = new System.Drawing.Size(40, 22);
            this.WriteLEDOpenTime.TabIndex = 30;
            this.WriteLEDOpenTime.Text = "写入";
            this.WriteLEDOpenTime.UseVisualStyleBackColor = true;
            this.WriteLEDOpenTime.Click += new System.EventHandler(this.WriteLEDOpenTime_Click);
            // 
            // ReadLEDOpenTime
            // 
            this.ReadLEDOpenTime.Location = new System.Drawing.Point(208, 62);
            this.ReadLEDOpenTime.Name = "ReadLEDOpenTime";
            this.ReadLEDOpenTime.Size = new System.Drawing.Size(40, 22);
            this.ReadLEDOpenTime.TabIndex = 29;
            this.ReadLEDOpenTime.Text = "读取";
            this.ReadLEDOpenTime.UseVisualStyleBackColor = true;
            this.ReadLEDOpenTime.Click += new System.EventHandler(this.ReadLEDOpenTime_Click);
            // 
            // cmbLEDOpenTime
            // 
            this.cmbLEDOpenTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLEDOpenTime.FormattingEnabled = true;
            this.cmbLEDOpenTime.Location = new System.Drawing.Point(117, 63);
            this.cmbLEDOpenTime.Name = "cmbLEDOpenTime";
            this.cmbLEDOpenTime.Size = new System.Drawing.Size(85, 20);
            this.cmbLEDOpenTime.TabIndex = 28;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 66);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(107, 12);
            this.label15.TabIndex = 27;
            this.label15.Text = "LED开灯保持(秒)：";
            // 
            // WriteCreateTime
            // 
            this.WriteCreateTime.Location = new System.Drawing.Point(254, 19);
            this.WriteCreateTime.Name = "WriteCreateTime";
            this.WriteCreateTime.Size = new System.Drawing.Size(40, 22);
            this.WriteCreateTime.TabIndex = 25;
            this.WriteCreateTime.Text = "写入";
            this.WriteCreateTime.UseVisualStyleBackColor = true;
            this.WriteCreateTime.Click += new System.EventHandler(this.WriteCreateTime_Click);
            // 
            // ReadCreateTime
            // 
            this.ReadCreateTime.Location = new System.Drawing.Point(208, 19);
            this.ReadCreateTime.Name = "ReadCreateTime";
            this.ReadCreateTime.Size = new System.Drawing.Size(40, 22);
            this.ReadCreateTime.TabIndex = 25;
            this.ReadCreateTime.Text = "读取";
            this.ReadCreateTime.UseVisualStyleBackColor = true;
            this.ReadCreateTime.Click += new System.EventHandler(this.ReadCreateTime_Click);
            // 
            // dtpCreateTime
            // 
            this.dtpCreateTime.CustomFormat = "yyyy-MM-dd";
            this.dtpCreateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreateTime.Location = new System.Drawing.Point(117, 20);
            this.dtpCreateTime.Name = "dtpCreateTime";
            this.dtpCreateTime.Size = new System.Drawing.Size(85, 21);
            this.dtpCreateTime.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(46, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 26;
            this.label14.Text = "出厂日期：";
            // 
            // btnReadRecordStorageMode
            // 
            this.btnReadRecordStorageMode.Location = new System.Drawing.Point(488, 25);
            this.btnReadRecordStorageMode.Name = "btnReadRecordStorageMode";
            this.btnReadRecordStorageMode.Size = new System.Drawing.Size(40, 22);
            this.btnReadRecordStorageMode.TabIndex = 25;
            this.btnReadRecordStorageMode.Text = "读取";
            this.btnReadRecordStorageMode.UseVisualStyleBackColor = true;
            this.btnReadRecordStorageMode.Click += new System.EventHandler(this.BtnReadRecordStorageMode_Click);
            // 
            // btnWriteRecordStorageMode
            // 
            this.btnWriteRecordStorageMode.Location = new System.Drawing.Point(547, 25);
            this.btnWriteRecordStorageMode.Name = "btnWriteRecordStorageMode";
            this.btnWriteRecordStorageMode.Size = new System.Drawing.Size(40, 22);
            this.btnWriteRecordStorageMode.TabIndex = 25;
            this.btnWriteRecordStorageMode.Text = "写入";
            this.btnWriteRecordStorageMode.UseVisualStyleBackColor = true;
            this.btnWriteRecordStorageMode.Click += new System.EventHandler(this.BtnWriteRecordStorageMode_Click);
            // 
            // frmSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 555);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmSystem";
            this.Text = "frmSystem";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnReadVersion;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.DateTimePicker dtpExpireTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnWriteExpireTime;
        private System.Windows.Forms.Button btnReadExpireTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnWriteSN;
        private System.Windows.Forms.Button btnReadSN;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbStorageMode1;
        private System.Windows.Forms.RadioButton rbStorageMode0;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbCardRecordCount;
        private System.Windows.Forms.Label lbPatrolEmpl;
        private System.Windows.Forms.Label lbVoltage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbStartCount;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lbSystemRecordCount;
        private System.Windows.Forms.Label lbPatrolEmplCount;
        private System.Windows.Forms.Label lbFormatCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbElectricity;
        private System.Windows.Forms.Button btnReadSystemStatus;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button WriteLEDOpenTime;
        private System.Windows.Forms.Button ReadLEDOpenTime;
        private System.Windows.Forms.ComboBox cmbLEDOpenTime;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button WriteCreateTime;
        private System.Windows.Forms.Button ReadCreateTime;
        private System.Windows.Forms.DateTimePicker dtpCreateTime;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnWriteStartupHoldTime;
        private System.Windows.Forms.Button btnReadStartupHoldTime;
        private System.Windows.Forms.ComboBox cmbStartupHoldTime;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnWriteRecordStorageMode;
        private System.Windows.Forms.Button btnReadRecordStorageMode;
    }
}