namespace FCARDIO.Protocol.Door.Test
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butWriteSN_Broadcast = new System.Windows.Forms.Button();
            this.butWriteSN = new System.Windows.Forms.Button();
            this.butReadSN = new System.Windows.Forms.Button();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butWriteSN_Broadcast);
            this.groupBox1.Controls.Add(this.butWriteSN);
            this.groupBox1.Controls.Add(this.butReadSN);
            this.groupBox1.Controls.Add(this.txtSN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SN";
            // 
            // butWriteSN_Broadcast
            // 
            this.butWriteSN_Broadcast.Location = new System.Drawing.Point(129, 45);
            this.butWriteSN_Broadcast.Name = "butWriteSN_Broadcast";
            this.butWriteSN_Broadcast.Size = new System.Drawing.Size(61, 23);
            this.butWriteSN_Broadcast.TabIndex = 4;
            this.butWriteSN_Broadcast.Text = "广播写";
            this.butWriteSN_Broadcast.UseVisualStyleBackColor = true;
            this.butWriteSN_Broadcast.Click += new System.EventHandler(this.butWriteSN_Broadcast_Click);
            // 
            // butWriteSN
            // 
            this.butWriteSN.Location = new System.Drawing.Point(75, 45);
            this.butWriteSN.Name = "butWriteSN";
            this.butWriteSN.Size = new System.Drawing.Size(48, 23);
            this.butWriteSN.TabIndex = 3;
            this.butWriteSN.Text = "写入";
            this.butWriteSN.UseVisualStyleBackColor = true;
            this.butWriteSN.Click += new System.EventHandler(this.butWriteSN_Click);
            // 
            // butReadSN
            // 
            this.butReadSN.Location = new System.Drawing.Point(21, 45);
            this.butReadSN.Name = "butReadSN";
            this.butReadSN.Size = new System.Drawing.Size(48, 23);
            this.butReadSN.TabIndex = 2;
            this.butReadSN.Text = "读取";
            this.butReadSN.UseVisualStyleBackColor = true;
            this.butReadSN.Click += new System.EventHandler(this.butReadSN_Click);
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(42, 18);
            this.txtSN.MaxLength = 16;
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(152, 21);
            this.txtSN.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "SN：";
            // 
            // frmSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 481);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSystem";
            this.Text = "frmSystem";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butWriteSN_Broadcast;
        private System.Windows.Forms.Button butWriteSN;
        private System.Windows.Forms.Button butReadSN;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label label1;
    }
}