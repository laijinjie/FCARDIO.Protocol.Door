namespace FCARDIO.Protocol.Door.Test
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.gbTCPClient = new System.Windows.Forms.GroupBox();
            this.txtTCPClientPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTCPClientAddr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdConnType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdProtocolType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.butClear = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.butSystem = new System.Windows.Forms.ToolStripButton();
            this.butTime = new System.Windows.Forms.ToolStripButton();
            this.butDoor = new System.Windows.Forms.ToolStripButton();
            this.butHoliday = new System.Windows.Forms.ToolStripButton();
            this.ButPassword = new System.Windows.Forms.ToolStripButton();
            this.ButTimeGroup = new System.Windows.Forms.ToolStripButton();
            this.butCard = new System.Windows.Forms.ToolStripButton();
            this.butRecord = new System.Windows.Forms.ToolStripButton();
            this.butUploadSoftware = new System.Windows.Forms.ToolStripButton();
            this.gbSerialPort = new System.Windows.Forms.GroupBox();
            this.butReloadSerialPort = new System.Windows.Forms.Button();
            this.cmbSerialPort = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gbServer = new System.Windows.Forms.GroupBox();
            this.butCloseTCPClient = new System.Windows.Forms.Button();
            this.butBeginTCPServer = new System.Windows.Forms.Button();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbTCPClient = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblLocalAddress = new System.Windows.Forms.Label();
            this.gbUDP = new System.Windows.Forms.GroupBox();
            this.butUDPBind = new System.Windows.Forms.Button();
            this.txtUDPLocalPort = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtUDPPort = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUDPAddr = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbLocalIP = new System.Windows.Forms.ComboBox();
            this.lstIO = new System.Windows.Forms.ListView();
            this.tbEvent = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lstCommand = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butClearCommand = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.gbTCPClient.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gbSerialPort.SuspendLayout();
            this.gbServer.SuspendLayout();
            this.gbUDP.SuspendLayout();
            this.tbEvent.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTCPClient
            // 
            this.gbTCPClient.Controls.Add(this.txtTCPClientPort);
            this.gbTCPClient.Controls.Add(this.label3);
            this.gbTCPClient.Controls.Add(this.txtTCPClientAddr);
            this.gbTCPClient.Controls.Add(this.label2);
            this.gbTCPClient.Location = new System.Drawing.Point(11, 59);
            this.gbTCPClient.Name = "gbTCPClient";
            this.gbTCPClient.Size = new System.Drawing.Size(321, 82);
            this.gbTCPClient.TabIndex = 0;
            this.gbTCPClient.TabStop = false;
            this.gbTCPClient.Text = "TCP客户端";
            // 
            // txtTCPClientPort
            // 
            this.txtTCPClientPort.Location = new System.Drawing.Point(69, 50);
            this.txtTCPClientPort.MaxLength = 5;
            this.txtTCPClientPort.Name = "txtTCPClientPort";
            this.txtTCPClientPort.Size = new System.Drawing.Size(164, 21);
            this.txtTCPClientPort.TabIndex = 5;
            this.txtTCPClientPort.Text = "8000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "端口：";
            // 
            // txtTCPClientAddr
            // 
            this.txtTCPClientAddr.Location = new System.Drawing.Point(69, 23);
            this.txtTCPClientAddr.MaxLength = 30;
            this.txtTCPClientAddr.Name = "txtTCPClientAddr";
            this.txtTCPClientAddr.Size = new System.Drawing.Size(164, 21);
            this.txtTCPClientAddr.TabIndex = 3;
            this.txtTCPClientAddr.Text = "192.168.1.66";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "IP：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "通讯方式：";
            // 
            // cmdConnType
            // 
            this.cmdConnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdConnType.FormattingEnabled = true;
            this.cmdConnType.Location = new System.Drawing.Point(80, 33);
            this.cmdConnType.Name = "cmdConnType";
            this.cmdConnType.Size = new System.Drawing.Size(220, 20);
            this.cmdConnType.TabIndex = 2;
            this.cmdConnType.SelectedIndexChanged += new System.EventHandler(this.cmdConnType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(342, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "协议类型：";
            // 
            // cmdProtocolType
            // 
            this.cmdProtocolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdProtocolType.FormattingEnabled = true;
            this.cmdProtocolType.Location = new System.Drawing.Point(413, 33);
            this.cmdProtocolType.Name = "cmdProtocolType";
            this.cmdProtocolType.Size = new System.Drawing.Size(209, 20);
            this.cmdProtocolType.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSN);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(344, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 82);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制器身份";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(83, 51);
            this.txtPassword.MaxLength = 8;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(164, 21);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.Text = "FFFFFFFF";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "密码：";
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(83, 24);
            this.txtSN.MaxLength = 16;
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(164, 21);
            this.txtSN.TabIndex = 7;
            this.txtSN.Text = "FC-8940H09030001";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "SN：";
            // 
            // butClear
            // 
            this.butClear.Location = new System.Drawing.Point(6, 3);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(75, 23);
            this.butClear.TabIndex = 7;
            this.butClear.Text = "清空";
            this.butClear.UseVisualStyleBackColor = true;
            this.butClear.Click += new System.EventHandler(this.butClear_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.butSystem,
            this.butTime,
            this.butDoor,
            this.butHoliday,
            this.ButPassword,
            this.ButTimeGroup,
            this.butCard,
            this.butRecord,
            this.butUploadSoftware});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(637, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // butSystem
            // 
            this.butSystem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butSystem.Image = ((System.Drawing.Image)(resources.GetObject("butSystem.Image")));
            this.butSystem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butSystem.Name = "butSystem";
            this.butSystem.Size = new System.Drawing.Size(60, 22);
            this.butSystem.Text = "系统参数";
            this.butSystem.Click += new System.EventHandler(this.butSystem_Click);
            // 
            // butTime
            // 
            this.butTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butTime.Image = ((System.Drawing.Image)(resources.GetObject("butTime.Image")));
            this.butTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butTime.Name = "butTime";
            this.butTime.Size = new System.Drawing.Size(60, 22);
            this.butTime.Text = "日期时间";
            this.butTime.Click += new System.EventHandler(this.butTime_Click);
            // 
            // butDoor
            // 
            this.butDoor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butDoor.Image = ((System.Drawing.Image)(resources.GetObject("butDoor.Image")));
            this.butDoor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butDoor.Name = "butDoor";
            this.butDoor.Size = new System.Drawing.Size(48, 22);
            this.butDoor.Text = "门参数";
            this.butDoor.Click += new System.EventHandler(this.butDoor_Click);
            // 
            // butHoliday
            // 
            this.butHoliday.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butHoliday.Image = ((System.Drawing.Image)(resources.GetObject("butHoliday.Image")));
            this.butHoliday.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butHoliday.Name = "butHoliday";
            this.butHoliday.Size = new System.Drawing.Size(48, 22);
            this.butHoliday.Text = "节假日";
            this.butHoliday.Click += new System.EventHandler(this.butHoliday_Click);
            // 
            // ButPassword
            // 
            this.ButPassword.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ButPassword.Image = ((System.Drawing.Image)(resources.GetObject("ButPassword.Image")));
            this.ButPassword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButPassword.Name = "ButPassword";
            this.ButPassword.Size = new System.Drawing.Size(36, 22);
            this.ButPassword.Text = "密码";
            this.ButPassword.Click += new System.EventHandler(this.ButPassword_Click);
            // 
            // ButTimeGroup
            // 
            this.ButTimeGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ButTimeGroup.Image = ((System.Drawing.Image)(resources.GetObject("ButTimeGroup.Image")));
            this.ButTimeGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButTimeGroup.Name = "ButTimeGroup";
            this.ButTimeGroup.Size = new System.Drawing.Size(60, 22);
            this.ButTimeGroup.Text = "开门时段";
            this.ButTimeGroup.Click += new System.EventHandler(this.ButTimeGroup_Click);
            // 
            // butCard
            // 
            this.butCard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butCard.Image = ((System.Drawing.Image)(resources.GetObject("butCard.Image")));
            this.butCard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butCard.Name = "butCard";
            this.butCard.Size = new System.Drawing.Size(60, 22);
            this.butCard.Text = "人员管理";
            this.butCard.Click += new System.EventHandler(this.butCard_Click);
            // 
            // butRecord
            // 
            this.butRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butRecord.Image = ((System.Drawing.Image)(resources.GetObject("butRecord.Image")));
            this.butRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butRecord.Name = "butRecord";
            this.butRecord.Size = new System.Drawing.Size(60, 22);
            this.butRecord.Text = "记录操作";
            this.butRecord.Click += new System.EventHandler(this.butRecord_Click);
            // 
            // butUploadSoftware
            // 
            this.butUploadSoftware.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butUploadSoftware.Image = ((System.Drawing.Image)(resources.GetObject("butUploadSoftware.Image")));
            this.butUploadSoftware.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butUploadSoftware.Name = "butUploadSoftware";
            this.butUploadSoftware.Size = new System.Drawing.Size(60, 22);
            this.butUploadSoftware.Text = "远程升级";
            this.butUploadSoftware.Click += new System.EventHandler(this.butUploadSoftware_Click);
            // 
            // gbSerialPort
            // 
            this.gbSerialPort.Controls.Add(this.butReloadSerialPort);
            this.gbSerialPort.Controls.Add(this.cmbSerialPort);
            this.gbSerialPort.Controls.Add(this.label7);
            this.gbSerialPort.Location = new System.Drawing.Point(11, 59);
            this.gbSerialPort.Name = "gbSerialPort";
            this.gbSerialPort.Size = new System.Drawing.Size(321, 82);
            this.gbSerialPort.TabIndex = 9;
            this.gbSerialPort.TabStop = false;
            this.gbSerialPort.Text = "串口";
            // 
            // butReloadSerialPort
            // 
            this.butReloadSerialPort.Location = new System.Drawing.Point(92, 46);
            this.butReloadSerialPort.Name = "butReloadSerialPort";
            this.butReloadSerialPort.Size = new System.Drawing.Size(75, 23);
            this.butReloadSerialPort.TabIndex = 8;
            this.butReloadSerialPort.Text = "刷新";
            this.butReloadSerialPort.UseVisualStyleBackColor = true;
            this.butReloadSerialPort.Click += new System.EventHandler(this.butReloadSerialPort_Click);
            // 
            // cmbSerialPort
            // 
            this.cmbSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialPort.FormattingEnabled = true;
            this.cmbSerialPort.Location = new System.Drawing.Point(92, 20);
            this.cmbSerialPort.Name = "cmbSerialPort";
            this.cmbSerialPort.Size = new System.Drawing.Size(111, 20);
            this.cmbSerialPort.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "通讯方式：";
            // 
            // gbServer
            // 
            this.gbServer.Controls.Add(this.butCloseTCPClient);
            this.gbServer.Controls.Add(this.butBeginTCPServer);
            this.gbServer.Controls.Add(this.txtServerPort);
            this.gbServer.Controls.Add(this.label9);
            this.gbServer.Controls.Add(this.cmbTCPClient);
            this.gbServer.Controls.Add(this.label8);
            this.gbServer.Location = new System.Drawing.Point(11, 59);
            this.gbServer.Name = "gbServer";
            this.gbServer.Size = new System.Drawing.Size(321, 82);
            this.gbServer.TabIndex = 10;
            this.gbServer.TabStop = false;
            this.gbServer.Text = "TCP客户端";
            // 
            // butCloseTCPClient
            // 
            this.butCloseTCPClient.Location = new System.Drawing.Point(238, 19);
            this.butCloseTCPClient.Name = "butCloseTCPClient";
            this.butCloseTCPClient.Size = new System.Drawing.Size(75, 23);
            this.butCloseTCPClient.TabIndex = 10;
            this.butCloseTCPClient.Text = "关闭客户端";
            this.butCloseTCPClient.UseVisualStyleBackColor = true;
            this.butCloseTCPClient.Click += new System.EventHandler(this.butCloseTCPClient_Click);
            // 
            // butBeginTCPServer
            // 
            this.butBeginTCPServer.Location = new System.Drawing.Point(133, 19);
            this.butBeginTCPServer.Name = "butBeginTCPServer";
            this.butBeginTCPServer.Size = new System.Drawing.Size(75, 23);
            this.butBeginTCPServer.TabIndex = 9;
            this.butBeginTCPServer.Text = "开始监听";
            this.butBeginTCPServer.UseVisualStyleBackColor = true;
            this.butBeginTCPServer.Click += new System.EventHandler(this.butBeginTCPServer_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(69, 20);
            this.txtServerPort.MaxLength = 5;
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(58, 21);
            this.txtServerPort.TabIndex = 8;
            this.txtServerPort.Text = "8000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "服务端口：";
            // 
            // cmbTCPClient
            // 
            this.cmbTCPClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTCPClient.FormattingEnabled = true;
            this.cmbTCPClient.Location = new System.Drawing.Point(4, 58);
            this.cmbTCPClient.Name = "cmbTCPClient";
            this.cmbTCPClient.Size = new System.Drawing.Size(311, 20);
            this.cmbTCPClient.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "客户端：";
            // 
            // lblLocalAddress
            // 
            this.lblLocalAddress.AutoSize = true;
            this.lblLocalAddress.Location = new System.Drawing.Point(12, 151);
            this.lblLocalAddress.Name = "lblLocalAddress";
            this.lblLocalAddress.Size = new System.Drawing.Size(53, 12);
            this.lblLocalAddress.TabIndex = 11;
            this.lblLocalAddress.Text = "本机IP：";
            // 
            // gbUDP
            // 
            this.gbUDP.Controls.Add(this.butUDPBind);
            this.gbUDP.Controls.Add(this.txtUDPLocalPort);
            this.gbUDP.Controls.Add(this.label12);
            this.gbUDP.Controls.Add(this.txtUDPPort);
            this.gbUDP.Controls.Add(this.label10);
            this.gbUDP.Controls.Add(this.txtUDPAddr);
            this.gbUDP.Controls.Add(this.label11);
            this.gbUDP.Location = new System.Drawing.Point(11, 59);
            this.gbUDP.Name = "gbUDP";
            this.gbUDP.Size = new System.Drawing.Size(321, 82);
            this.gbUDP.TabIndex = 12;
            this.gbUDP.TabStop = false;
            this.gbUDP.Text = "UDP";
            // 
            // butUDPBind
            // 
            this.butUDPBind.Location = new System.Drawing.Point(133, 19);
            this.butUDPBind.Name = "butUDPBind";
            this.butUDPBind.Size = new System.Drawing.Size(75, 23);
            this.butUDPBind.TabIndex = 10;
            this.butUDPBind.Text = "绑定";
            this.butUDPBind.UseVisualStyleBackColor = true;
            this.butUDPBind.Click += new System.EventHandler(this.butUDPBind_Click);
            // 
            // txtUDPLocalPort
            // 
            this.txtUDPLocalPort.Location = new System.Drawing.Point(81, 20);
            this.txtUDPLocalPort.MaxLength = 5;
            this.txtUDPLocalPort.Name = "txtUDPLocalPort";
            this.txtUDPLocalPort.Size = new System.Drawing.Size(46, 21);
            this.txtUDPLocalPort.TabIndex = 7;
            this.txtUDPLocalPort.Text = "9001";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 6;
            this.label12.Text = "本地端口：";
            // 
            // txtUDPPort
            // 
            this.txtUDPPort.Location = new System.Drawing.Point(237, 55);
            this.txtUDPPort.MaxLength = 5;
            this.txtUDPPort.Name = "txtUDPPort";
            this.txtUDPPort.Size = new System.Drawing.Size(46, 21);
            this.txtUDPPort.TabIndex = 5;
            this.txtUDPPort.Text = "8101";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(190, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "端口：";
            // 
            // txtUDPAddr
            // 
            this.txtUDPAddr.Location = new System.Drawing.Point(46, 55);
            this.txtUDPAddr.MaxLength = 30;
            this.txtUDPAddr.Name = "txtUDPAddr";
            this.txtUDPAddr.Size = new System.Drawing.Size(138, 21);
            this.txtUDPAddr.TabIndex = 3;
            this.txtUDPAddr.Text = "255.255.255.255";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "IP：";
            // 
            // cmbLocalIP
            // 
            this.cmbLocalIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalIP.FormattingEnabled = true;
            this.cmbLocalIP.Location = new System.Drawing.Point(69, 147);
            this.cmbLocalIP.Name = "cmbLocalIP";
            this.cmbLocalIP.Size = new System.Drawing.Size(263, 20);
            this.cmbLocalIP.TabIndex = 13;
            // 
            // lstIO
            // 
            this.lstIO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstIO.GridLines = true;
            this.lstIO.Location = new System.Drawing.Point(3, 32);
            this.lstIO.Name = "lstIO";
            this.lstIO.Size = new System.Drawing.Size(606, 462);
            this.lstIO.TabIndex = 14;
            this.lstIO.UseCompatibleStateImageBehavior = false;
            this.lstIO.View = System.Windows.Forms.View.Details;
            // 
            // tbEvent
            // 
            this.tbEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEvent.Controls.Add(this.tabPage1);
            this.tbEvent.Controls.Add(this.tabPage2);
            this.tbEvent.Location = new System.Drawing.Point(11, 202);
            this.tbEvent.Name = "tbEvent";
            this.tbEvent.SelectedIndex = 0;
            this.tbEvent.Size = new System.Drawing.Size(620, 523);
            this.tbEvent.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lstIO);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(612, 497);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "通讯IO";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butClear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(606, 29);
            this.panel1.TabIndex = 15;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lstCommand);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(612, 497);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "命令结果";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lstCommand
            // 
            this.lstCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCommand.FullRowSelect = true;
            this.lstCommand.GridLines = true;
            this.lstCommand.Location = new System.Drawing.Point(3, 32);
            this.lstCommand.Name = "lstCommand";
            this.lstCommand.Size = new System.Drawing.Size(606, 462);
            this.lstCommand.TabIndex = 16;
            this.lstCommand.UseCompatibleStateImageBehavior = false;
            this.lstCommand.View = System.Windows.Forms.View.Details;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.butClearCommand);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(606, 29);
            this.panel2.TabIndex = 17;
            // 
            // butClearCommand
            // 
            this.butClearCommand.Location = new System.Drawing.Point(28, 3);
            this.butClearCommand.Name = "butClearCommand";
            this.butClearCommand.Size = new System.Drawing.Size(75, 23);
            this.butClearCommand.TabIndex = 7;
            this.butClearCommand.Text = "清空";
            this.butClearCommand.UseVisualStyleBackColor = true;
            this.butClearCommand.Click += new System.EventHandler(this.butClearCommand_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(344, 145);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "停止所有通讯";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // txtProcess
            // 
            this.txtProcess.Location = new System.Drawing.Point(69, 175);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.Size = new System.Drawing.Size(558, 21);
            this.txtProcess.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(22, 178);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 18;
            this.label13.Text = "进度：";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 726);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtProcess);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbEvent);
            this.Controls.Add(this.cmbLocalIP);
            this.Controls.Add(this.lblLocalAddress);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdProtocolType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdConnType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbUDP);
            this.Controls.Add(this.gbServer);
            this.Controls.Add(this.gbTCPClient);
            this.Controls.Add(this.gbSerialPort);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "门禁协议调试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.gbTCPClient.ResumeLayout(false);
            this.gbTCPClient.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbSerialPort.ResumeLayout(false);
            this.gbSerialPort.PerformLayout();
            this.gbServer.ResumeLayout(false);
            this.gbServer.PerformLayout();
            this.gbUDP.ResumeLayout(false);
            this.gbUDP.PerformLayout();
            this.tbEvent.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTCPClient;
        private System.Windows.Forms.TextBox txtTCPClientPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTCPClientAddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmdConnType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmdProtocolType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton butSystem;
        private System.Windows.Forms.ToolStripButton butTime;
        private System.Windows.Forms.ToolStripButton butDoor;
        private System.Windows.Forms.ToolStripButton butHoliday;
        private System.Windows.Forms.ToolStripButton ButPassword;
        private System.Windows.Forms.ToolStripButton ButTimeGroup;
        private System.Windows.Forms.ToolStripButton butCard;
        private System.Windows.Forms.ToolStripButton butRecord;
        private System.Windows.Forms.ToolStripButton butUploadSoftware;
        private System.Windows.Forms.GroupBox gbSerialPort;
        private System.Windows.Forms.Button butReloadSerialPort;
        private System.Windows.Forms.ComboBox cmbSerialPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gbServer;
        private System.Windows.Forms.Button butCloseTCPClient;
        private System.Windows.Forms.Button butBeginTCPServer;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbTCPClient;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblLocalAddress;
        private System.Windows.Forms.GroupBox gbUDP;
        private System.Windows.Forms.TextBox txtUDPPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUDPAddr;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button butUDPBind;
        private System.Windows.Forms.TextBox txtUDPLocalPort;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbLocalIP;
        private System.Windows.Forms.ListView lstIO;
        private System.Windows.Forms.TabControl tbEvent;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lstCommand;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butClearCommand;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.Label label13;
    }
}

