namespace FCARDIO.Protocol.Elevator.Test
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSysParameterPage = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtVoltage = new System.Windows.Forms.TextBox();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.txtUPS = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtFormatCount = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtRestartCount = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.btnReadSystemStatus = new System.Windows.Forms.Button();
            this.txtRunDay = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.gbRunParameters = new System.Windows.Forms.GroupBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.btnReadVersion = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cbxDeadline = new System.Windows.Forms.ComboBox();
            this.btnWriteDeadline = new System.Windows.Forms.Button();
            this.btnReadDeadline = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butReadSN = new System.Windows.Forms.Button();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbTCP = new System.Windows.Forms.GroupBox();
            this.cbxAutoIP = new System.Windows.Forms.ComboBox();
            this.cbxProtocolType = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtServerAddr = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.txtUDPPort = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTCPPort = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDNSBackup = new System.Windows.Forms.TextBox();
            this.txtDNS = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIPGateway = new System.Windows.Forms.TextBox();
            this.txtIPMask = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.butWriteTCPSetting = new System.Windows.Forms.Button();
            this.butRendTCPSetting = new System.Windows.Forms.Button();
            this.txtMAC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbPassword = new System.Windows.Forms.GroupBox();
            this.butResetConnectPassword = new System.Windows.Forms.Button();
            this.butWriteConnectPassword = new System.Windows.Forms.Button();
            this.butReadConnectPassword = new System.Windows.Forms.Button();
            this.txtConnectPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabSysParameterPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbRunParameters.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbTCP.SuspendLayout();
            this.gbPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSysParameterPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(917, 939);
            this.tabControl1.TabIndex = 4;
            // 
            // tabSysParameterPage
            // 
            this.tabSysParameterPage.Controls.Add(this.groupBox2);
            this.tabSysParameterPage.Controls.Add(this.gbRunParameters);
            this.tabSysParameterPage.Controls.Add(this.groupBox1);
            this.tabSysParameterPage.Controls.Add(this.gbTCP);
            this.tabSysParameterPage.Controls.Add(this.gbPassword);
            this.tabSysParameterPage.Location = new System.Drawing.Point(4, 22);
            this.tabSysParameterPage.Name = "tabSysParameterPage";
            this.tabSysParameterPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabSysParameterPage.Size = new System.Drawing.Size(909, 913);
            this.tabSysParameterPage.TabIndex = 0;
            this.tabSysParameterPage.Text = "设备参数设置";
            this.tabSysParameterPage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtVoltage);
            this.groupBox2.Controls.Add(this.txtStartTime);
            this.groupBox2.Controls.Add(this.txtUPS);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.txtFormatCount);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.txtTemperature);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.txtRestartCount);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.btnReadSystemStatus);
            this.groupBox2.Controls.Add(this.txtRunDay);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Location = new System.Drawing.Point(289, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(520, 171);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设备运行信息";
            // 
            // txtVoltage
            // 
            this.txtVoltage.Location = new System.Drawing.Point(272, 75);
            this.txtVoltage.MaxLength = 16;
            this.txtVoltage.Name = "txtVoltage";
            this.txtVoltage.Size = new System.Drawing.Size(77, 21);
            this.txtVoltage.TabIndex = 18;
            // 
            // txtStartTime
            // 
            this.txtStartTime.Location = new System.Drawing.Point(106, 102);
            this.txtStartTime.MaxLength = 16;
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(243, 21);
            this.txtStartTime.TabIndex = 16;
            // 
            // txtUPS
            // 
            this.txtUPS.Location = new System.Drawing.Point(272, 45);
            this.txtUPS.MaxLength = 16;
            this.txtUPS.Name = "txtUPS";
            this.txtUPS.Size = new System.Drawing.Size(77, 21);
            this.txtUPS.TabIndex = 15;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(213, 78);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 17;
            this.label18.Text = "接入电压：";
            // 
            // txtFormatCount
            // 
            this.txtFormatCount.Location = new System.Drawing.Point(272, 17);
            this.txtFormatCount.MaxLength = 17;
            this.txtFormatCount.Name = "txtFormatCount";
            this.txtFormatCount.Size = new System.Drawing.Size(77, 21);
            this.txtFormatCount.TabIndex = 14;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(47, 105);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 12);
            this.label26.TabIndex = 13;
            this.label26.Text = "上电时间：";
            // 
            // txtTemperature
            // 
            this.txtTemperature.Location = new System.Drawing.Point(106, 75);
            this.txtTemperature.MaxLength = 16;
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(77, 21);
            this.txtTemperature.TabIndex = 11;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(47, 78);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 12);
            this.label27.TabIndex = 10;
            this.label27.Text = "设备温度：";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(195, 48);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(83, 12);
            this.label28.TabIndex = 9;
            this.label28.Text = "UPS供电状态：";
            // 
            // txtRestartCount
            // 
            this.txtRestartCount.Location = new System.Drawing.Point(106, 45);
            this.txtRestartCount.MaxLength = 16;
            this.txtRestartCount.Name = "txtRestartCount";
            this.txtRestartCount.Size = new System.Drawing.Size(77, 21);
            this.txtRestartCount.TabIndex = 7;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(11, 50);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(101, 12);
            this.label29.TabIndex = 6;
            this.label29.Text = "看门狗复位次数：";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(201, 23);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(77, 12);
            this.label30.TabIndex = 5;
            this.label30.Text = "格式化次数：";
            // 
            // btnReadSystemStatus
            // 
            this.btnReadSystemStatus.Location = new System.Drawing.Point(106, 129);
            this.btnReadSystemStatus.Name = "btnReadSystemStatus";
            this.btnReadSystemStatus.Size = new System.Drawing.Size(48, 23);
            this.btnReadSystemStatus.TabIndex = 2;
            this.btnReadSystemStatus.Text = "读取";
            this.btnReadSystemStatus.UseVisualStyleBackColor = true;
            this.btnReadSystemStatus.Click += new System.EventHandler(this.BtnReadSystemStatus_Click);
            // 
            // txtRunDay
            // 
            this.txtRunDay.Location = new System.Drawing.Point(106, 17);
            this.txtRunDay.MaxLength = 17;
            this.txtRunDay.Name = "txtRunDay";
            this.txtRunDay.Size = new System.Drawing.Size(77, 21);
            this.txtRunDay.TabIndex = 1;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(11, 23);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(101, 12);
            this.label31.TabIndex = 0;
            this.label31.Text = "设备已运行天数：";
            // 
            // gbRunParameters
            // 
            this.gbRunParameters.Controls.Add(this.txtVersion);
            this.gbRunParameters.Controls.Add(this.btnReadVersion);
            this.gbRunParameters.Controls.Add(this.label19);
            this.gbRunParameters.Controls.Add(this.label17);
            this.gbRunParameters.Controls.Add(this.cbxDeadline);
            this.gbRunParameters.Controls.Add(this.btnWriteDeadline);
            this.gbRunParameters.Controls.Add(this.btnReadDeadline);
            this.gbRunParameters.Controls.Add(this.label16);
            this.gbRunParameters.Location = new System.Drawing.Point(459, 6);
            this.gbRunParameters.Name = "gbRunParameters";
            this.gbRunParameters.Size = new System.Drawing.Size(350, 75);
            this.gbRunParameters.TabIndex = 3;
            this.gbRunParameters.TabStop = false;
            this.gbRunParameters.Text = "设备运行参数";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(81, 47);
            this.txtVersion.MaxLength = 8;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(121, 21);
            this.txtVersion.TabIndex = 8;
            // 
            // btnReadVersion
            // 
            this.btnReadVersion.Location = new System.Drawing.Point(237, 45);
            this.btnReadVersion.Name = "btnReadVersion";
            this.btnReadVersion.Size = new System.Drawing.Size(48, 23);
            this.btnReadVersion.TabIndex = 7;
            this.btnReadVersion.Text = "读取";
            this.btnReadVersion.UseVisualStyleBackColor = true;
            this.btnReadVersion.Click += new System.EventHandler(this.BtnReadVersion_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 50);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 12);
            this.label19.TabIndex = 6;
            this.label19.Text = "硬件版本号：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(208, 21);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 12);
            this.label17.TabIndex = 5;
            this.label17.Text = "/天";
            // 
            // cbxDeadline
            // 
            this.cbxDeadline.FormattingEnabled = true;
            this.cbxDeadline.IntegralHeight = false;
            this.cbxDeadline.ItemHeight = 12;
            this.cbxDeadline.Items.AddRange(new object[] {
            "失效",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "无限制(65535)"});
            this.cbxDeadline.Location = new System.Drawing.Point(81, 18);
            this.cbxDeadline.Name = "cbxDeadline";
            this.cbxDeadline.Size = new System.Drawing.Size(121, 20);
            this.cbxDeadline.TabIndex = 4;
            // 
            // btnWriteDeadline
            // 
            this.btnWriteDeadline.Location = new System.Drawing.Point(291, 15);
            this.btnWriteDeadline.Name = "btnWriteDeadline";
            this.btnWriteDeadline.Size = new System.Drawing.Size(48, 23);
            this.btnWriteDeadline.TabIndex = 3;
            this.btnWriteDeadline.Text = "写入";
            this.btnWriteDeadline.UseVisualStyleBackColor = true;
            this.btnWriteDeadline.Click += new System.EventHandler(this.BtnWriteDeadline_Click);
            // 
            // btnReadDeadline
            // 
            this.btnReadDeadline.Location = new System.Drawing.Point(237, 15);
            this.btnReadDeadline.Name = "btnReadDeadline";
            this.btnReadDeadline.Size = new System.Drawing.Size(48, 23);
            this.btnReadDeadline.TabIndex = 2;
            this.btnReadDeadline.Text = "读取";
            this.btnReadDeadline.UseVisualStyleBackColor = true;
            this.btnReadDeadline.Click += new System.EventHandler(this.BtnReadDeadline_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(31, 21);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "有效期：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butReadSN);
            this.groupBox1.Controls.Add(this.txtSN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SN";
            // 
            // butReadSN
            // 
            this.butReadSN.Location = new System.Drawing.Point(34, 45);
            this.butReadSN.Name = "butReadSN";
            this.butReadSN.Size = new System.Drawing.Size(48, 23);
            this.butReadSN.TabIndex = 2;
            this.butReadSN.Text = "读取";
            this.butReadSN.UseVisualStyleBackColor = true;
            this.butReadSN.Click += new System.EventHandler(this.ButReadSN_Click);
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(34, 17);
            this.txtSN.MaxLength = 16;
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(166, 21);
            this.txtSN.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "SN：";
            // 
            // gbTCP
            // 
            this.gbTCP.Controls.Add(this.cbxAutoIP);
            this.gbTCP.Controls.Add(this.cbxProtocolType);
            this.gbTCP.Controls.Add(this.label15);
            this.gbTCP.Controls.Add(this.txtServerAddr);
            this.gbTCP.Controls.Add(this.label13);
            this.gbTCP.Controls.Add(this.txtServerIP);
            this.gbTCP.Controls.Add(this.label14);
            this.gbTCP.Controls.Add(this.label11);
            this.gbTCP.Controls.Add(this.txtServerPort);
            this.gbTCP.Controls.Add(this.txtUDPPort);
            this.gbTCP.Controls.Add(this.label12);
            this.gbTCP.Controls.Add(this.label9);
            this.gbTCP.Controls.Add(this.txtTCPPort);
            this.gbTCP.Controls.Add(this.label10);
            this.gbTCP.Controls.Add(this.label7);
            this.gbTCP.Controls.Add(this.txtDNSBackup);
            this.gbTCP.Controls.Add(this.txtDNS);
            this.gbTCP.Controls.Add(this.label8);
            this.gbTCP.Controls.Add(this.label5);
            this.gbTCP.Controls.Add(this.txtIPGateway);
            this.gbTCP.Controls.Add(this.txtIPMask);
            this.gbTCP.Controls.Add(this.label6);
            this.gbTCP.Controls.Add(this.label4);
            this.gbTCP.Controls.Add(this.txtIP);
            this.gbTCP.Controls.Add(this.butWriteTCPSetting);
            this.gbTCP.Controls.Add(this.butRendTCPSetting);
            this.gbTCP.Controls.Add(this.txtMAC);
            this.gbTCP.Controls.Add(this.label3);
            this.gbTCP.Location = new System.Drawing.Point(8, 87);
            this.gbTCP.Name = "gbTCP";
            this.gbTCP.Size = new System.Drawing.Size(265, 446);
            this.gbTCP.TabIndex = 2;
            this.gbTCP.TabStop = false;
            this.gbTCP.Text = "TCP/IP 连接参数";
            // 
            // cbxAutoIP
            // 
            this.cbxAutoIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAutoIP.FormattingEnabled = true;
            this.cbxAutoIP.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cbxAutoIP.Location = new System.Drawing.Point(96, 324);
            this.cbxAutoIP.Name = "cbxAutoIP";
            this.cbxAutoIP.Size = new System.Drawing.Size(121, 20);
            this.cbxAutoIP.TabIndex = 29;
            // 
            // cbxProtocolType
            // 
            this.cbxProtocolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProtocolType.FormattingEnabled = true;
            this.cbxProtocolType.Items.AddRange(new object[] {
            "请选择",
            "TCP Client",
            "TCP Server",
            "混合"});
            this.cbxProtocolType.Location = new System.Drawing.Point(96, 184);
            this.cbxProtocolType.Name = "cbxProtocolType";
            this.cbxProtocolType.Size = new System.Drawing.Size(121, 20);
            this.cbxProtocolType.TabIndex = 28;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 354);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 12);
            this.label15.TabIndex = 27;
            this.label15.Text = "服务器域名：";
            // 
            // txtServerAddr
            // 
            this.txtServerAddr.Location = new System.Drawing.Point(96, 351);
            this.txtServerAddr.MaxLength = 100;
            this.txtServerAddr.Name = "txtServerAddr";
            this.txtServerAddr.Size = new System.Drawing.Size(152, 21);
            this.txtServerAddr.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 324);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 25;
            this.label13.Text = "自动获得IP：";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(96, 295);
            this.txtServerIP.MaxLength = 16;
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(152, 21);
            this.txtServerIP.TabIndex = 23;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(25, 298);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 22;
            this.label14.Text = "服务器IP：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 271);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 21;
            this.label11.Text = "服务器端口：";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(96, 268);
            this.txtServerPort.MaxLength = 16;
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(152, 21);
            this.txtServerPort.TabIndex = 20;
            // 
            // txtUDPPort
            // 
            this.txtUDPPort.Location = new System.Drawing.Point(96, 241);
            this.txtUDPPort.MaxLength = 16;
            this.txtUDPPort.Name = "txtUDPPort";
            this.txtUDPPort.Size = new System.Drawing.Size(152, 21);
            this.txtUDPPort.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 244);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 12);
            this.label12.TabIndex = 18;
            this.label12.Text = "本地UDP端口：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 214);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "本地TCP端口：";
            // 
            // txtTCPPort
            // 
            this.txtTCPPort.Location = new System.Drawing.Point(96, 210);
            this.txtTCPPort.MaxLength = 16;
            this.txtTCPPort.Name = "txtTCPPort";
            this.txtTCPPort.Size = new System.Drawing.Size(152, 21);
            this.txtTCPPort.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 187);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "TCP工作模式：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "备用DNS：";
            // 
            // txtDNSBackup
            // 
            this.txtDNSBackup.Location = new System.Drawing.Point(96, 154);
            this.txtDNSBackup.MaxLength = 16;
            this.txtDNSBackup.Name = "txtDNSBackup";
            this.txtDNSBackup.Size = new System.Drawing.Size(152, 21);
            this.txtDNSBackup.TabIndex = 12;
            // 
            // txtDNS
            // 
            this.txtDNS.Location = new System.Drawing.Point(96, 127);
            this.txtDNS.MaxLength = 16;
            this.txtDNS.Name = "txtDNS";
            this.txtDNS.Size = new System.Drawing.Size(152, 21);
            this.txtDNS.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(56, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "DNS：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "网关IP：";
            // 
            // txtIPGateway
            // 
            this.txtIPGateway.Location = new System.Drawing.Point(96, 100);
            this.txtIPGateway.MaxLength = 16;
            this.txtIPGateway.Name = "txtIPGateway";
            this.txtIPGateway.Size = new System.Drawing.Size(152, 21);
            this.txtIPGateway.TabIndex = 8;
            // 
            // txtIPMask
            // 
            this.txtIPMask.Location = new System.Drawing.Point(96, 73);
            this.txtIPMask.MaxLength = 16;
            this.txtIPMask.Name = "txtIPMask";
            this.txtIPMask.Size = new System.Drawing.Size(152, 21);
            this.txtIPMask.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "子网掩码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "IP地址：";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(96, 45);
            this.txtIP.MaxLength = 16;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(152, 21);
            this.txtIP.TabIndex = 4;
            // 
            // butWriteTCPSetting
            // 
            this.butWriteTCPSetting.Location = new System.Drawing.Point(152, 393);
            this.butWriteTCPSetting.Name = "butWriteTCPSetting";
            this.butWriteTCPSetting.Size = new System.Drawing.Size(48, 23);
            this.butWriteTCPSetting.TabIndex = 3;
            this.butWriteTCPSetting.Text = "写入";
            this.butWriteTCPSetting.UseVisualStyleBackColor = true;
            this.butWriteTCPSetting.Click += new System.EventHandler(this.ButWriteTCPSetting_Click);
            // 
            // butRendTCPSetting
            // 
            this.butRendTCPSetting.Location = new System.Drawing.Point(96, 393);
            this.butRendTCPSetting.Name = "butRendTCPSetting";
            this.butRendTCPSetting.Size = new System.Drawing.Size(48, 23);
            this.butRendTCPSetting.TabIndex = 2;
            this.butRendTCPSetting.Text = "读取";
            this.butRendTCPSetting.UseVisualStyleBackColor = true;
            this.butRendTCPSetting.Click += new System.EventHandler(this.ButRendTCPSetting_Click);
            // 
            // txtMAC
            // 
            this.txtMAC.Location = new System.Drawing.Point(96, 18);
            this.txtMAC.MaxLength = 17;
            this.txtMAC.Name = "txtMAC";
            this.txtMAC.Size = new System.Drawing.Size(152, 21);
            this.txtMAC.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "MAC地址：";
            // 
            // gbPassword
            // 
            this.gbPassword.Controls.Add(this.butResetConnectPassword);
            this.gbPassword.Controls.Add(this.butWriteConnectPassword);
            this.gbPassword.Controls.Add(this.butReadConnectPassword);
            this.gbPassword.Controls.Add(this.txtConnectPassword);
            this.gbPassword.Controls.Add(this.label2);
            this.gbPassword.Location = new System.Drawing.Point(231, 6);
            this.gbPassword.Name = "gbPassword";
            this.gbPassword.Size = new System.Drawing.Size(222, 75);
            this.gbPassword.TabIndex = 1;
            this.gbPassword.TabStop = false;
            this.gbPassword.Text = "通讯密码";
            // 
            // butResetConnectPassword
            // 
            this.butResetConnectPassword.Location = new System.Drawing.Point(150, 44);
            this.butResetConnectPassword.Name = "butResetConnectPassword";
            this.butResetConnectPassword.Size = new System.Drawing.Size(48, 23);
            this.butResetConnectPassword.TabIndex = 4;
            this.butResetConnectPassword.Text = "重置";
            this.butResetConnectPassword.UseVisualStyleBackColor = true;
            this.butResetConnectPassword.Click += new System.EventHandler(this.ButResetConnectPassword_Click);
            // 
            // butWriteConnectPassword
            // 
            this.butWriteConnectPassword.Location = new System.Drawing.Point(96, 45);
            this.butWriteConnectPassword.Name = "butWriteConnectPassword";
            this.butWriteConnectPassword.Size = new System.Drawing.Size(48, 23);
            this.butWriteConnectPassword.TabIndex = 3;
            this.butWriteConnectPassword.Text = "写入";
            this.butWriteConnectPassword.UseVisualStyleBackColor = true;
            this.butWriteConnectPassword.Click += new System.EventHandler(this.ButWriteConnectPassword_Click);
            // 
            // butReadConnectPassword
            // 
            this.butReadConnectPassword.Location = new System.Drawing.Point(42, 44);
            this.butReadConnectPassword.Name = "butReadConnectPassword";
            this.butReadConnectPassword.Size = new System.Drawing.Size(48, 23);
            this.butReadConnectPassword.TabIndex = 2;
            this.butReadConnectPassword.Text = "读取";
            this.butReadConnectPassword.UseVisualStyleBackColor = true;
            this.butReadConnectPassword.Click += new System.EventHandler(this.ButReadConnectPassword_Click);
            // 
            // txtConnectPassword
            // 
            this.txtConnectPassword.Location = new System.Drawing.Point(42, 17);
            this.txtConnectPassword.MaxLength = 8;
            this.txtConnectPassword.Name = "txtConnectPassword";
            this.txtConnectPassword.Size = new System.Drawing.Size(156, 21);
            this.txtConnectPassword.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "密码：";
            // 
            // frmSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 621);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSystem";
            this.Text = "基本参数";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSystem_FormClosed);
            this.Load += new System.EventHandler(this.FrmSystem_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabSysParameterPage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbRunParameters.ResumeLayout(false);
            this.gbRunParameters.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbTCP.ResumeLayout(false);
            this.gbTCP.PerformLayout();
            this.gbPassword.ResumeLayout(false);
            this.gbPassword.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSysParameterPage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtVoltage;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.TextBox txtUPS;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtFormatCount;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtTemperature;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtRestartCount;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btnReadSystemStatus;
        private System.Windows.Forms.TextBox txtRunDay;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.GroupBox gbRunParameters;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Button btnReadVersion;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbxDeadline;
        private System.Windows.Forms.Button btnWriteDeadline;
        private System.Windows.Forms.Button btnReadDeadline;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butReadSN;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbTCP;
        private System.Windows.Forms.ComboBox cbxAutoIP;
        private System.Windows.Forms.ComboBox cbxProtocolType;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtServerAddr;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.TextBox txtUDPPort;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTCPPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDNSBackup;
        private System.Windows.Forms.TextBox txtDNS;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIPGateway;
        private System.Windows.Forms.TextBox txtIPMask;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button butWriteTCPSetting;
        private System.Windows.Forms.Button butRendTCPSetting;
        private System.Windows.Forms.TextBox txtMAC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbPassword;
        private System.Windows.Forms.Button butResetConnectPassword;
        private System.Windows.Forms.Button butWriteConnectPassword;
        private System.Windows.Forms.Button butReadConnectPassword;
        private System.Windows.Forms.TextBox txtConnectPassword;
        private System.Windows.Forms.Label label2;
    }
}