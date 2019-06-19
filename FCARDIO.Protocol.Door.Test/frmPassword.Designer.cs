namespace FCARDIO.Protocol.Door.Test
{
    partial class frmPassword
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
            this.butAddPassword = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.butReadPasswordDetail = new DevComponents.DotNetBar.ButtonX();
            this.butClearPassword = new DevComponents.DotNetBar.ButtonX();
            this.butReadAllPassword = new DevComponents.DotNetBar.ButtonX();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Holiday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HolidayTypeRender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RepeatYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // butAddPassword
            // 
            this.butAddPassword.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butAddPassword.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butAddPassword.Location = new System.Drawing.Point(320, 36);
            this.butAddPassword.Name = "butAddPassword";
            this.butAddPassword.Size = new System.Drawing.Size(109, 26);
            this.butAddPassword.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butAddPassword.TabIndex = 25;
            this.butAddPassword.Text = "添加列表密码";
            this.butAddPassword.Click += new System.EventHandler(this.ButAddPassword_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 73);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(90, 23);
            this.labelX1.TabIndex = 24;
            this.labelX1.Text = "密码列表：";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.butReadPasswordDetail);
            this.groupPanel1.Controls.Add(this.butClearPassword);
            this.groupPanel1.Controls.Add(this.butReadAllPassword);
            this.groupPanel1.Location = new System.Drawing.Point(12, 12);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(558, 57);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 23;
            this.groupPanel1.Text = "控制板中的密码表";
            // 
            // butReadPasswordDetail
            // 
            this.butReadPasswordDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butReadPasswordDetail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butReadPasswordDetail.Location = new System.Drawing.Point(12, 3);
            this.butReadPasswordDetail.Name = "butReadPasswordDetail";
            this.butReadPasswordDetail.Size = new System.Drawing.Size(121, 26);
            this.butReadPasswordDetail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butReadPasswordDetail.TabIndex = 1;
            this.butReadPasswordDetail.Text = "读取密码库存储详情";
            this.butReadPasswordDetail.Click += new System.EventHandler(this.ButReadPasswordDetail_Click);
            // 
            // butClearPassword
            // 
            this.butClearPassword.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butClearPassword.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butClearPassword.Location = new System.Drawing.Point(428, 3);
            this.butClearPassword.Name = "butClearPassword";
            this.butClearPassword.Size = new System.Drawing.Size(109, 26);
            this.butClearPassword.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butClearPassword.TabIndex = 3;
            this.butClearPassword.Text = "删除所有密码表";
            this.butClearPassword.Click += new System.EventHandler(this.ButClearPassword_Click);
            // 
            // butReadAllPassword
            // 
            this.butReadAllPassword.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butReadAllPassword.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butReadAllPassword.Location = new System.Drawing.Point(148, 3);
            this.butReadAllPassword.Name = "butReadAllPassword";
            this.butReadAllPassword.Size = new System.Drawing.Size(142, 26);
            this.butReadAllPassword.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butReadAllPassword.TabIndex = 2;
            this.butReadAllPassword.Text = "从控制板读取所有密码表";
            this.butReadAllPassword.Click += new System.EventHandler(this.ButReadAllPassword_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.Index,
            this.Holiday,
            this.HolidayTypeRender,
            this.RepeatYear});
            this.dataGridView1.Location = new System.Drawing.Point(12, 102);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(590, 412);
            this.dataGridView1.TabIndex = 26;
            // 
            // Selected
            // 
            this.Selected.DataPropertyName = "Selected";
            this.Selected.HeaderText = "选择";
            this.Selected.Name = "Selected";
            this.Selected.ReadOnly = true;
            this.Selected.Width = 50;
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Index";
            this.Index.HeaderText = "密码";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            // 
            // Holiday
            // 
            this.Holiday.DataPropertyName = "Holiday";
            this.Holiday.HeaderText = "有效门";
            this.Holiday.Name = "Holiday";
            this.Holiday.ReadOnly = true;
            this.Holiday.Width = 130;
            // 
            // HolidayTypeRender
            // 
            this.HolidayTypeRender.DataPropertyName = "HolidayTypeRender";
            this.HolidayTypeRender.HeaderText = "有效期";
            this.HolidayTypeRender.Name = "HolidayTypeRender";
            this.HolidayTypeRender.ReadOnly = true;
            this.HolidayTypeRender.Width = 180;
            // 
            // RepeatYear
            // 
            this.RepeatYear.DataPropertyName = "RepeatYear";
            this.RepeatYear.HeaderText = "有效次数";
            this.RepeatYear.Name = "RepeatYear";
            this.RepeatYear.ReadOnly = true;
            this.RepeatYear.Width = 80;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 521);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 27;
            this.checkBox1.Text = "反选";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(526, 521);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "清空表格";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 544);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 572);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "有效测试：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(76, 539);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 31;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(76, 568);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 20);
            this.comboBox1.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(276, 544);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 33;
            this.label3.Text = "权限：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 572);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 34;
            this.label4.Text = "有效期：";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy/MM/dd";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(320, 567);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(99, 21);
            this.dtpDate.TabIndex = 35;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(320, 543);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(42, 16);
            this.checkBox2.TabIndex = 36;
            this.checkBox2.Text = "门1";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(366, 543);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(42, 16);
            this.checkBox3.TabIndex = 37;
            this.checkBox3.Text = "门2";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(411, 543);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(42, 16);
            this.checkBox4.TabIndex = 38;
            this.checkBox4.Text = "门3";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(457, 543);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(42, 16);
            this.checkBox5.TabIndex = 39;
            this.checkBox5.Text = "门4";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 599);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 40;
            this.button2.Text = "新增至列表";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(101, 599);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 41;
            this.button3.Text = "从列表删除";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(254, 599);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 42;
            this.button4.Text = "新增至设备";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(335, 599);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(130, 23);
            this.button5.TabIndex = 43;
            this.button5.Text = "从设备删除单个密码";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(471, 599);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(130, 23);
            this.button6.TabIndex = 44;
            this.button6.Text = "从设备删除多个密码";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 628);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(586, 49);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "自动生成";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 46;
            this.label5.Text = "自动生成密码：";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(151, 18);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 46;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(266, 16);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 46;
            this.button7.Text = "生成";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 683);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(586, 49);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测试密码";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(67, 16);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 46;
            this.button8.Text = "测试密码";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(193, 18);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 46;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(149, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 46;
            this.label6.Text = "密码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(306, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 47;
            this.label7.Text = "端口号：";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "门1",
            "门2",
            "门3",
            "门4"});
            this.comboBox2.Location = new System.Drawing.Point(368, 18);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(100, 20);
            this.comboBox2.TabIndex = 48;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(420, 567);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(55, 21);
            this.dateTimePicker1.TabIndex = 48;
            // 
            // frmPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 737);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.butAddPassword);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.groupPanel1);
            this.Name = "frmPassword";
            this.Text = "开门密码";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPassword_FormClosed);
            this.Load += new System.EventHandler(this.frmPassword_Load);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX butAddPassword;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX butReadPasswordDetail;
        private DevComponents.DotNetBar.ButtonX butClearPassword;
        private DevComponents.DotNetBar.ButtonX butReadAllPassword;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Holiday;
        private System.Windows.Forms.DataGridViewTextBoxColumn HolidayTypeRender;
        private System.Windows.Forms.DataGridViewTextBoxColumn RepeatYear;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}