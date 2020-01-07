﻿namespace FCARDIO.Protocol.Fingerprint.Test
{
    partial class frmTime
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtErrorTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnWriteBroadcastTime = new System.Windows.Forms.Button();
            this.btnWriteSystemTime = new System.Windows.Forms.Button();
            this.txtComputerTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSystemTime = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.btnReadSystemTime = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CustomDateTime = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnWriteCustomDateTime = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtErrorTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnWriteBroadcastTime);
            this.groupBox1.Controls.Add(this.btnWriteSystemTime);
            this.groupBox1.Controls.Add(this.txtComputerTime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSystemTime);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.btnReadSystemTime);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(607, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备时间";
            // 
            // txtErrorTime
            // 
            this.txtErrorTime.Location = new System.Drawing.Point(334, 53);
            this.txtErrorTime.MaxLength = 16;
            this.txtErrorTime.Name = "txtErrorTime";
            this.txtErrorTime.ReadOnly = true;
            this.txtErrorTime.Size = new System.Drawing.Size(259, 21);
            this.txtErrorTime.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "误差时间：";
            // 
            // btnWriteBroadcastTime
            // 
            this.btnWriteBroadcastTime.Location = new System.Drawing.Point(491, 21);
            this.btnWriteBroadcastTime.Name = "btnWriteBroadcastTime";
            this.btnWriteBroadcastTime.Size = new System.Drawing.Size(102, 23);
            this.btnWriteBroadcastTime.TabIndex = 23;
            this.btnWriteBroadcastTime.Text = "校准时间_广播";
            this.btnWriteBroadcastTime.UseVisualStyleBackColor = true;
            this.btnWriteBroadcastTime.Click += new System.EventHandler(this.BtnWriteBroadcastTime_Click);
            // 
            // btnWriteSystemTime
            // 
            this.btnWriteSystemTime.Location = new System.Drawing.Point(383, 21);
            this.btnWriteSystemTime.Name = "btnWriteSystemTime";
            this.btnWriteSystemTime.Size = new System.Drawing.Size(102, 23);
            this.btnWriteSystemTime.TabIndex = 22;
            this.btnWriteSystemTime.Text = "更新设备时间";
            this.btnWriteSystemTime.UseVisualStyleBackColor = true;
            this.btnWriteSystemTime.Click += new System.EventHandler(this.BtnWriteSystemTime_Click);
            // 
            // txtComputerTime
            // 
            this.txtComputerTime.Location = new System.Drawing.Point(77, 53);
            this.txtComputerTime.MaxLength = 16;
            this.txtComputerTime.Name = "txtComputerTime";
            this.txtComputerTime.ReadOnly = true;
            this.txtComputerTime.Size = new System.Drawing.Size(172, 21);
            this.txtComputerTime.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "计算机时间：";
            // 
            // txtSystemTime
            // 
            this.txtSystemTime.Location = new System.Drawing.Point(77, 23);
            this.txtSystemTime.MaxLength = 16;
            this.txtSystemTime.Name = "txtSystemTime";
            this.txtSystemTime.ReadOnly = true;
            this.txtSystemTime.Size = new System.Drawing.Size(172, 21);
            this.txtSystemTime.TabIndex = 19;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(18, 26);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 12);
            this.label26.TabIndex = 18;
            this.label26.Text = "设备时间：";
            // 
            // btnReadSystemTime
            // 
            this.btnReadSystemTime.Location = new System.Drawing.Point(275, 21);
            this.btnReadSystemTime.Name = "btnReadSystemTime";
            this.btnReadSystemTime.Size = new System.Drawing.Size(102, 23);
            this.btnReadSystemTime.TabIndex = 17;
            this.btnReadSystemTime.Text = "读设备时间";
            this.btnReadSystemTime.UseVisualStyleBackColor = true;
            this.btnReadSystemTime.Click += new System.EventHandler(this.BtnReadSystemTime_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CustomDateTime);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnWriteCustomDateTime);
            this.groupBox2.Location = new System.Drawing.Point(12, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(607, 56);
            this.groupBox2.TabIndex = 97;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "自定义时间";
            // 
            // CustomDateTime
            // 
            this.CustomDateTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.CustomDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.CustomDateTime.Location = new System.Drawing.Point(78, 24);
            this.CustomDateTime.Name = "CustomDateTime";
            this.CustomDateTime.Size = new System.Drawing.Size(172, 21);
            this.CustomDateTime.TabIndex = 90;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 89;
            this.label4.Text = "时间日期：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 77;
            // 
            // btnWriteCustomDateTime
            // 
            this.btnWriteCustomDateTime.Location = new System.Drawing.Point(287, 23);
            this.btnWriteCustomDateTime.Name = "btnWriteCustomDateTime";
            this.btnWriteCustomDateTime.Size = new System.Drawing.Size(90, 23);
            this.btnWriteCustomDateTime.TabIndex = 86;
            this.btnWriteCustomDateTime.Text = "更新设备时间";
            this.btnWriteCustomDateTime.UseVisualStyleBackColor = true;
            this.btnWriteCustomDateTime.Click += new System.EventHandler(this.BtnWriteCustomDateTime_Click);
            // 
            // frmTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 247);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "时钟";
            this.Load += new System.EventHandler(this.frmTime_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtComputerTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSystemTime;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnReadSystemTime;
        private System.Windows.Forms.Button btnWriteBroadcastTime;
        private System.Windows.Forms.Button btnWriteSystemTime;
        private System.Windows.Forms.TextBox txtErrorTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnWriteCustomDateTime;
        private System.Windows.Forms.DateTimePicker CustomDateTime;
    }
}