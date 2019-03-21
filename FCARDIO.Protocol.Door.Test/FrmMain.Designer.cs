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
            this.txtLog = new System.Windows.Forms.TextBox();
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
            this.gbTCPClient.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.gbTCPClient.Size = new System.Drawing.Size(289, 82);
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
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(11, 182);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(629, 451);
            this.txtLog.TabIndex = 6;
            this.txtLog.WordWrap = false;
            // 
            // butClear
            // 
            this.butClear.Location = new System.Drawing.Point(11, 153);
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
            this.toolStrip1.Size = new System.Drawing.Size(653, 25);
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
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 638);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.butClear);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdProtocolType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdConnType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbTCPClient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "门禁协议调试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.gbTCPClient.ResumeLayout(false);
            this.gbTCPClient.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.TextBox txtLog;
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
    }
}

