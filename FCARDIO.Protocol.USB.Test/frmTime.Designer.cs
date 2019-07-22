﻿namespace FCARDIO.Protocol.USB.Test
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeviceTime = new System.Windows.Forms.TextBox();
            this.txtPCTime = new System.Windows.Forms.TextBox();
            this.btnReadTime = new System.Windows.Forms.Button();
            this.btnWriteTime = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.btnWriteCustomTime = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnWriteTime);
            this.groupBox1.Controls.Add(this.btnReadTime);
            this.groupBox1.Controls.Add(this.txtPCTime);
            this.groupBox1.Controls.Add(this.txtDeviceTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(544, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备时间";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnWriteCustomTime);
            this.groupBox2.Controls.Add(this.dtpTime);
            this.groupBox2.Controls.Add(this.dtpDate);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(545, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "自定义时间";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "计算机时间：";
            // 
            // txtDeviceTime
            // 
            this.txtDeviceTime.Location = new System.Drawing.Point(100, 27);
            this.txtDeviceTime.Name = "txtDeviceTime";
            this.txtDeviceTime.ReadOnly = true;
            this.txtDeviceTime.Size = new System.Drawing.Size(183, 21);
            this.txtDeviceTime.TabIndex = 2;
            // 
            // txtPCTime
            // 
            this.txtPCTime.Location = new System.Drawing.Point(100, 66);
            this.txtPCTime.Name = "txtPCTime";
            this.txtPCTime.ReadOnly = true;
            this.txtPCTime.Size = new System.Drawing.Size(183, 21);
            this.txtPCTime.TabIndex = 3;
            // 
            // btnReadTime
            // 
            this.btnReadTime.Location = new System.Drawing.Point(301, 26);
            this.btnReadTime.Name = "btnReadTime";
            this.btnReadTime.Size = new System.Drawing.Size(75, 23);
            this.btnReadTime.TabIndex = 4;
            this.btnReadTime.Text = "读设备时间";
            this.btnReadTime.UseVisualStyleBackColor = true;
            this.btnReadTime.Click += new System.EventHandler(this.BtnReadTime_Click);
            // 
            // btnWriteTime
            // 
            this.btnWriteTime.Location = new System.Drawing.Point(384, 26);
            this.btnWriteTime.Name = "btnWriteTime";
            this.btnWriteTime.Size = new System.Drawing.Size(94, 23);
            this.btnWriteTime.TabIndex = 5;
            this.btnWriteTime.Text = "更新设备时间";
            this.btnWriteTime.UseVisualStyleBackColor = true;
            this.btnWriteTime.Click += new System.EventHandler(this.BtnWriteTime_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "误差时间：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(370, 66);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(158, 21);
            this.textBox1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "日期时间：";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(101, 46);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(97, 21);
            this.dtpDate.TabIndex = 9;
            // 
            // dtpTime
            // 
            this.dtpTime.CustomFormat = "HH:mm:ss";
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime.Location = new System.Drawing.Point(204, 46);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(80, 21);
            this.dtpTime.TabIndex = 10;
            // 
            // btnWriteCustomTime
            // 
            this.btnWriteCustomTime.Location = new System.Drawing.Point(302, 45);
            this.btnWriteCustomTime.Name = "btnWriteCustomTime";
            this.btnWriteCustomTime.Size = new System.Drawing.Size(94, 23);
            this.btnWriteCustomTime.TabIndex = 8;
            this.btnWriteCustomTime.Text = "更新设备时间";
            this.btnWriteCustomTime.UseVisualStyleBackColor = true;
            this.btnWriteCustomTime.Click += new System.EventHandler(this.BtnWriteCustomTime_Click);
            // 
            // frmTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmTime";
            this.Text = "frmTime";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnWriteTime;
        private System.Windows.Forms.Button btnReadTime;
        private System.Windows.Forms.TextBox txtPCTime;
        private System.Windows.Forms.TextBox txtDeviceTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnWriteCustomTime;
    }
}