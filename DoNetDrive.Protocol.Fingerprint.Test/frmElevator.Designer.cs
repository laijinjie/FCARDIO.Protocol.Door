
namespace DoNetDrive.Protocol.Fingerprint.Test
{
    partial class frmElevator
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
            this.btnReadPersonElevatorAccess = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnWritePersonElevatorAccess = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReadPersonElevatorAccess
            // 
            this.btnReadPersonElevatorAccess.Location = new System.Drawing.Point(16, 34);
            this.btnReadPersonElevatorAccess.Name = "btnReadPersonElevatorAccess";
            this.btnReadPersonElevatorAccess.Size = new System.Drawing.Size(75, 23);
            this.btnReadPersonElevatorAccess.TabIndex = 0;
            this.btnReadPersonElevatorAccess.Text = "读取";
            this.btnReadPersonElevatorAccess.UseVisualStyleBackColor = true;
            this.btnReadPersonElevatorAccess.Click += new System.EventHandler(this.btnReadPersonElevatorAccess_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnWritePersonElevatorAccess);
            this.groupBox1.Controls.Add(this.btnReadPersonElevatorAccess);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 69);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "人员电梯权限";
            // 
            // btnWritePersonElevatorAccess
            // 
            this.btnWritePersonElevatorAccess.Location = new System.Drawing.Point(97, 34);
            this.btnWritePersonElevatorAccess.Name = "btnWritePersonElevatorAccess";
            this.btnWritePersonElevatorAccess.Size = new System.Drawing.Size(75, 23);
            this.btnWritePersonElevatorAccess.TabIndex = 1;
            this.btnWritePersonElevatorAccess.Text = "设置";
            this.btnWritePersonElevatorAccess.UseVisualStyleBackColor = true;
            this.btnWritePersonElevatorAccess.Click += new System.EventHandler(this.btnWritePersonElevatorAccess_Click);
            // 
            // frmElevator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 516);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmElevator";
            this.Text = "电梯模块";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReadPersonElevatorAccess;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnWritePersonElevatorAccess;
    }
}