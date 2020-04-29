namespace DotNetDrive.Protocol.POS.Test
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
            this.btnWriteSystemTime = new System.Windows.Forms.Button();
            this.txtComputerTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSystemTime = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.btnReadSystemTime = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtErrorTime);
            this.groupBox1.Controls.Add(this.label2);
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
            this.txtErrorTime.Size = new System.Drawing.Size(151, 21);
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
            // frmTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 104);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTime";
            this.Load += new System.EventHandler(this.frmTime_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtComputerTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSystemTime;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnReadSystemTime;
        private System.Windows.Forms.Button btnWriteSystemTime;
        private System.Windows.Forms.TextBox txtErrorTime;
        private System.Windows.Forms.Label label2;
    }
}