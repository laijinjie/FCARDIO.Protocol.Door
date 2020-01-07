namespace FCARDIO.Protocol.Fingerprint.Test
{
    partial class frmAdditionalData
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.butUploadImage = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtCodeData = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnUploadCode = new System.Windows.Forms.Button();
            this.btnDeleteCode = new System.Windows.Forms.Button();
            this.btnCompute = new System.Windows.Forms.Button();
            this.cmbUploadSerialNumber = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbUploadType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUploadUserCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkByBlock = new System.Windows.Forms.CheckBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnGetPerson = new System.Windows.Forms.Button();
            this.cmbDownloadSerialNumber = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDownloadUserCode = new System.Windows.Forms.TextBox();
            this.cmbDownloadType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.butUpdateSoftware = new System.Windows.Forms.Button();
            this.cmbEquptType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.cmbUploadSerialNumber);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbUploadType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUploadUserCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 346);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "上传";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(6, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(626, 280);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.butUploadImage);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(618, 254);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "照片";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // butUploadImage
            // 
            this.butUploadImage.Location = new System.Drawing.Point(48, 71);
            this.butUploadImage.Name = "butUploadImage";
            this.butUploadImage.Size = new System.Drawing.Size(75, 23);
            this.butUploadImage.TabIndex = 18;
            this.butUploadImage.Text = "上传照片";
            this.butUploadImage.UseVisualStyleBackColor = true;
            this.butUploadImage.Click += new System.EventHandler(this.ButUploadImage_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtCodeData);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnUploadCode);
            this.tabPage1.Controls.Add(this.btnDeleteCode);
            this.tabPage1.Controls.Add(this.btnCompute);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(618, 254);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "特征码";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtCodeData
            // 
            this.txtCodeData.Location = new System.Drawing.Point(16, 31);
            this.txtCodeData.Multiline = true;
            this.txtCodeData.Name = "txtCodeData";
            this.txtCodeData.Size = new System.Drawing.Size(555, 188);
            this.txtCodeData.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "特征码数据(base64)：";
            // 
            // btnUploadCode
            // 
            this.btnUploadCode.Location = new System.Drawing.Point(496, 225);
            this.btnUploadCode.Name = "btnUploadCode";
            this.btnUploadCode.Size = new System.Drawing.Size(75, 23);
            this.btnUploadCode.TabIndex = 15;
            this.btnUploadCode.Text = "上传特征码";
            this.btnUploadCode.UseVisualStyleBackColor = true;
            this.btnUploadCode.Click += new System.EventHandler(this.BtnUploadCode_Click);
            // 
            // btnDeleteCode
            // 
            this.btnDeleteCode.Location = new System.Drawing.Point(16, 225);
            this.btnDeleteCode.Name = "btnDeleteCode";
            this.btnDeleteCode.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteCode.TabIndex = 13;
            this.btnDeleteCode.Text = "删除特征码";
            this.btnDeleteCode.UseVisualStyleBackColor = true;
            this.btnDeleteCode.Click += new System.EventHandler(this.BtnDeleteCode_Click);
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(396, 225);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(75, 23);
            this.btnCompute.TabIndex = 14;
            this.btnCompute.Text = "计算CRC32";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.BtnCompute_Click);
            // 
            // cmbUploadSerialNumber
            // 
            this.cmbUploadSerialNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUploadSerialNumber.FormattingEnabled = true;
            this.cmbUploadSerialNumber.Location = new System.Drawing.Point(476, 31);
            this.cmbUploadSerialNumber.Name = "cmbUploadSerialNumber";
            this.cmbUploadSerialNumber.Size = new System.Drawing.Size(105, 20);
            this.cmbUploadSerialNumber.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(429, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "序号：";
            // 
            // cmbUploadType
            // 
            this.cmbUploadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUploadType.FormattingEnabled = true;
            this.cmbUploadType.Location = new System.Drawing.Point(274, 31);
            this.cmbUploadType.Name = "cmbUploadType";
            this.cmbUploadType.Size = new System.Drawing.Size(149, 20);
            this.cmbUploadType.TabIndex = 3;
            this.cmbUploadType.SelectedIndexChanged += new System.EventHandler(this.CmbUploadType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "类型：";
            // 
            // txtUploadUserCode
            // 
            this.txtUploadUserCode.Location = new System.Drawing.Point(83, 31);
            this.txtUploadUserCode.Name = "txtUploadUserCode";
            this.txtUploadUserCode.Size = new System.Drawing.Size(100, 21);
            this.txtUploadUserCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户号：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(651, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 640);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkByBlock);
            this.groupBox2.Controls.Add(this.btnDownload);
            this.groupBox2.Controls.Add(this.btnGetPerson);
            this.groupBox2.Controls.Add(this.cmbDownloadSerialNumber);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtDownloadUserCode);
            this.groupBox2.Controls.Add(this.cmbDownloadType);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(13, 365);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(632, 97);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "下载";
            // 
            // chkByBlock
            // 
            this.chkByBlock.AutoSize = true;
            this.chkByBlock.Location = new System.Drawing.Point(404, 61);
            this.chkByBlock.Name = "chkByBlock";
            this.chkByBlock.Size = new System.Drawing.Size(84, 16);
            this.chkByBlock.TabIndex = 12;
            this.chkByBlock.Text = "分块读文件";
            this.chkByBlock.UseVisualStyleBackColor = true;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(506, 54);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 11;
            this.btnDownload.Text = "下载文件";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // btnGetPerson
            // 
            this.btnGetPerson.Location = new System.Drawing.Point(26, 54);
            this.btnGetPerson.Name = "btnGetPerson";
            this.btnGetPerson.Size = new System.Drawing.Size(142, 23);
            this.btnGetPerson.TabIndex = 11;
            this.btnGetPerson.Text = "获取人员数据库详情";
            this.btnGetPerson.UseVisualStyleBackColor = true;
            this.btnGetPerson.Click += new System.EventHandler(this.BtnGetPerson_Click);
            // 
            // cmbDownloadSerialNumber
            // 
            this.cmbDownloadSerialNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDownloadSerialNumber.FormattingEnabled = true;
            this.cmbDownloadSerialNumber.Location = new System.Drawing.Point(476, 20);
            this.cmbDownloadSerialNumber.Name = "cmbDownloadSerialNumber";
            this.cmbDownloadSerialNumber.Size = new System.Drawing.Size(105, 20);
            this.cmbDownloadSerialNumber.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(429, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "序号：";
            // 
            // txtDownloadUserCode
            // 
            this.txtDownloadUserCode.Location = new System.Drawing.Point(83, 20);
            this.txtDownloadUserCode.Name = "txtDownloadUserCode";
            this.txtDownloadUserCode.Size = new System.Drawing.Size(100, 21);
            this.txtDownloadUserCode.TabIndex = 8;
            // 
            // cmbDownloadType
            // 
            this.cmbDownloadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDownloadType.FormattingEnabled = true;
            this.cmbDownloadType.Location = new System.Drawing.Point(259, 21);
            this.cmbDownloadType.Name = "cmbDownloadType";
            this.cmbDownloadType.Size = new System.Drawing.Size(149, 20);
            this.cmbDownloadType.TabIndex = 8;
            this.cmbDownloadType.SelectedIndexChanged += new System.EventHandler(this.CmbDownloadType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "用户号：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(212, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "类型：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.butUpdateSoftware);
            this.groupBox3.Controls.Add(this.cmbEquptType);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(12, 468);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(632, 58);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "上传固件";
            // 
            // butUpdateSoftware
            // 
            this.butUpdateSoftware.Location = new System.Drawing.Point(432, 20);
            this.butUpdateSoftware.Name = "butUpdateSoftware";
            this.butUpdateSoftware.Size = new System.Drawing.Size(75, 23);
            this.butUpdateSoftware.TabIndex = 11;
            this.butUpdateSoftware.Text = "上传固件";
            this.butUpdateSoftware.UseVisualStyleBackColor = true;
            this.butUpdateSoftware.Click += new System.EventHandler(this.ButUploadSoftware_Click);
            // 
            // cmbEquptType
            // 
            this.cmbEquptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquptType.FormattingEnabled = true;
            this.cmbEquptType.Location = new System.Drawing.Point(114, 21);
            this.cmbEquptType.Name = "cmbEquptType";
            this.cmbEquptType.Size = new System.Drawing.Size(295, 20);
            this.cmbEquptType.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(43, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 7;
            this.label10.Text = "设备类型：";
            // 
            // frmAdditionalData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 666);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmAdditionalData";
            this.Text = "人员附加数据";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbUploadSerialNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbUploadType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUploadUserCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDownloadUserCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDownloadSerialNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDownloadType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnGetPerson;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtCodeData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUploadCode;
        private System.Windows.Forms.Button btnDeleteCode;
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button butUploadImage;
        private System.Windows.Forms.CheckBox chkByBlock;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button butUpdateSoftware;
        private System.Windows.Forms.ComboBox cmbEquptType;
        private System.Windows.Forms.Label label10;
    }
}