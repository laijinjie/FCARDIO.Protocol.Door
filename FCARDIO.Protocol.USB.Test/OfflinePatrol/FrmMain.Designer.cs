﻿namespace FCARDIO.Protocol.USB.OfflinePatrol.Test
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.txtReadAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSerialPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEvent = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.lstIO = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkShowIO = new System.Windows.Forms.CheckBox();
            this.butClear = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.lstCommand = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butClearCommand = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.butSystem = new System.Windows.Forms.ToolStripButton();
            this.butTime = new System.Windows.Forms.ToolStripButton();
            this.butDoor = new System.Windows.Forms.ToolStripButton();
            this.butHoliday = new System.Windows.Forms.ToolStripButton();
            this.butRecord = new System.Windows.Forms.ToolStripButton();
            this.lblReLoadCOMList = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tbEvent.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblReLoadCOMList);
            this.groupBox1.Controls.Add(this.txtProcess);
            this.groupBox1.Controls.Add(this.txtReadAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbSerialPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通讯参数";
            // 
            // txtProcess
            // 
            this.txtProcess.Location = new System.Drawing.Point(330, 27);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.Size = new System.Drawing.Size(235, 21);
            this.txtProcess.TabIndex = 18;
            // 
            // txtReadAddress
            // 
            this.txtReadAddress.Location = new System.Drawing.Point(267, 27);
            this.txtReadAddress.MaxLength = 3;
            this.txtReadAddress.Name = "txtReadAddress";
            this.txtReadAddress.Size = new System.Drawing.Size(41, 21);
            this.txtReadAddress.TabIndex = 3;
            this.txtReadAddress.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "地址码：";
            // 
            // cmbSerialPort
            // 
            this.cmbSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialPort.FormattingEnabled = true;
            this.cmbSerialPort.Location = new System.Drawing.Point(66, 27);
            this.cmbSerialPort.Name = "cmbSerialPort";
            this.cmbSerialPort.Size = new System.Drawing.Size(81, 20);
            this.cmbSerialPort.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号：";
            // 
            // tbEvent
            // 
            this.tbEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEvent.Controls.Add(this.tabPage5);
            this.tbEvent.Controls.Add(this.tabPage6);
            this.tbEvent.Location = new System.Drawing.Point(13, 109);
            this.tbEvent.Name = "tbEvent";
            this.tbEvent.SelectedIndex = 0;
            this.tbEvent.Size = new System.Drawing.Size(572, 506);
            this.tbEvent.TabIndex = 16;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lstIO);
            this.tabPage5.Controls.Add(this.panel1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(564, 480);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "通讯IO";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // lstIO
            // 
            this.lstIO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstIO.GridLines = true;
            this.lstIO.HideSelection = false;
            this.lstIO.Location = new System.Drawing.Point(3, 32);
            this.lstIO.Name = "lstIO";
            this.lstIO.Size = new System.Drawing.Size(558, 445);
            this.lstIO.TabIndex = 14;
            this.lstIO.UseCompatibleStateImageBehavior = false;
            this.lstIO.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkShowIO);
            this.panel1.Controls.Add(this.butClear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(558, 29);
            this.panel1.TabIndex = 15;
            // 
            // chkShowIO
            // 
            this.chkShowIO.AutoSize = true;
            this.chkShowIO.Checked = true;
            this.chkShowIO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowIO.Location = new System.Drawing.Point(87, 7);
            this.chkShowIO.Name = "chkShowIO";
            this.chkShowIO.Size = new System.Drawing.Size(84, 16);
            this.chkShowIO.TabIndex = 8;
            this.chkShowIO.Text = "显示IO日志";
            this.chkShowIO.UseVisualStyleBackColor = true;
            this.chkShowIO.CheckedChanged += new System.EventHandler(this.chkShowIO_CheckedChanged);
            // 
            // butClear
            // 
            this.butClear.Location = new System.Drawing.Point(6, 3);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(75, 23);
            this.butClear.TabIndex = 7;
            this.butClear.Text = "清空";
            this.butClear.UseVisualStyleBackColor = true;
            this.butClear.Click += new System.EventHandler(this.ButClear_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.lstCommand);
            this.tabPage6.Controls.Add(this.panel2);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(564, 376);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "命令结果";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // lstCommand
            // 
            this.lstCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCommand.FullRowSelect = true;
            this.lstCommand.GridLines = true;
            this.lstCommand.HideSelection = false;
            this.lstCommand.Location = new System.Drawing.Point(3, 32);
            this.lstCommand.Name = "lstCommand";
            this.lstCommand.Size = new System.Drawing.Size(558, 341);
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
            this.panel2.Size = new System.Drawing.Size(558, 29);
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
            this.butClearCommand.Click += new System.EventHandler(this.ButClearCommand_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.butSystem,
            this.butTime,
            this.butDoor,
            this.butHoliday,
            this.butRecord});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(597, 25);
            this.toolStrip1.TabIndex = 17;
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
            this.butSystem.Click += new System.EventHandler(this.ButSystem_Click);
            // 
            // butTime
            // 
            this.butTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butTime.Image = ((System.Drawing.Image)(resources.GetObject("butTime.Image")));
            this.butTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butTime.Name = "butTime";
            this.butTime.Size = new System.Drawing.Size(60, 22);
            this.butTime.Text = "日期时间";
            this.butTime.Click += new System.EventHandler(this.ButTime_Click);
            // 
            // butDoor
            // 
            this.butDoor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butDoor.Image = ((System.Drawing.Image)(resources.GetObject("butDoor.Image")));
            this.butDoor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butDoor.Name = "butDoor";
            this.butDoor.Size = new System.Drawing.Size(48, 22);
            this.butDoor.Text = "门参数";
            // 
            // butHoliday
            // 
            this.butHoliday.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butHoliday.Image = ((System.Drawing.Image)(resources.GetObject("butHoliday.Image")));
            this.butHoliday.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butHoliday.Name = "butHoliday";
            this.butHoliday.Size = new System.Drawing.Size(48, 22);
            this.butHoliday.Text = "节假日";
            // 
            // butRecord
            // 
            this.butRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.butRecord.Image = ((System.Drawing.Image)(resources.GetObject("butRecord.Image")));
            this.butRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butRecord.Name = "butRecord";
            this.butRecord.Size = new System.Drawing.Size(60, 22);
            this.butRecord.Text = "记录操作";
            // 
            // lblReLoadCOMList
            // 
            this.lblReLoadCOMList.AutoSize = true;
            this.lblReLoadCOMList.ForeColor = System.Drawing.Color.Blue;
            this.lblReLoadCOMList.Location = new System.Drawing.Point(154, 31);
            this.lblReLoadCOMList.Name = "lblReLoadCOMList";
            this.lblReLoadCOMList.Size = new System.Drawing.Size(29, 12);
            this.lblReLoadCOMList.TabIndex = 19;
            this.lblReLoadCOMList.Text = "刷新";
            this.lblReLoadCOMList.Click += new System.EventHandler(this.lblReLoadCOMList_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 668);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tbEvent);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmMain";
            this.Text = "巡更棒调试程序";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tbEvent.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtReadAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSerialPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tbEvent;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView lstIO;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkShowIO;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ListView lstCommand;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butClearCommand;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton butSystem;
        private System.Windows.Forms.ToolStripButton butTime;
        private System.Windows.Forms.ToolStripButton butDoor;
        private System.Windows.Forms.ToolStripButton butHoliday;
        private System.Windows.Forms.ToolStripButton butRecord;
        private System.Windows.Forms.Label lblReLoadCOMList;
    }
}