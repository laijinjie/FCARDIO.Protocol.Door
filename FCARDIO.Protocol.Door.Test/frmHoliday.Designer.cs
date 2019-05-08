namespace FCARDIO.Protocol.Door.Test
{
    partial class frmHoliday
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
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.butReadHolidayDetail = new DevComponents.DotNetBar.ButtonX();
            this.butReadAllHoliday = new DevComponents.DotNetBar.ButtonX();
            this.butClearHoliday = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // superGridControl1
            // 
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.Location = new System.Drawing.Point(12, 96);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.Size = new System.Drawing.Size(558, 496);
            this.superGridControl1.TabIndex = 0;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // butReadHolidayDetail
            // 
            this.butReadHolidayDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butReadHolidayDetail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butReadHolidayDetail.Location = new System.Drawing.Point(12, 3);
            this.butReadHolidayDetail.Name = "butReadHolidayDetail";
            this.butReadHolidayDetail.Size = new System.Drawing.Size(164, 26);
            this.butReadHolidayDetail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butReadHolidayDetail.TabIndex = 1;
            this.butReadHolidayDetail.Text = "读取节假日存储详情";
            this.butReadHolidayDetail.Click += new System.EventHandler(this.butReadHolidayDetail_Click);
            // 
            // butReadAllHoliday
            // 
            this.butReadAllHoliday.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butReadAllHoliday.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butReadAllHoliday.Location = new System.Drawing.Point(193, 3);
            this.butReadAllHoliday.Name = "butReadAllHoliday";
            this.butReadAllHoliday.Size = new System.Drawing.Size(164, 26);
            this.butReadAllHoliday.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butReadAllHoliday.TabIndex = 2;
            this.butReadAllHoliday.Text = "从控制板读取所有节假日";
            this.butReadAllHoliday.Click += new System.EventHandler(this.ReadAllHoliday_Click);
            // 
            // butClearHoliday
            // 
            this.butClearHoliday.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.butClearHoliday.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.butClearHoliday.Location = new System.Drawing.Point(373, 3);
            this.butClearHoliday.Name = "butClearHoliday";
            this.butClearHoliday.Size = new System.Drawing.Size(164, 26);
            this.butClearHoliday.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.butClearHoliday.TabIndex = 3;
            this.butClearHoliday.Text = "删除所有节假日";
            this.butClearHoliday.Click += new System.EventHandler(this.ClearHoliday_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.butReadHolidayDetail);
            this.groupPanel1.Controls.Add(this.butClearHoliday);
            this.groupPanel1.Controls.Add(this.butReadAllHoliday);
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
            this.groupPanel1.TabIndex = 4;
            this.groupPanel1.Text = "控制板中的节假日";
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
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "节假日列表：";
            // 
            // checkBoxX1
            // 
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX1.Location = new System.Drawing.Point(12, 599);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(100, 23);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 6;
            this.checkBoxX1.Text = "反选";
            // 
            // frmHoliday
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 727);
            this.Controls.Add(this.checkBoxX1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.superGridControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "frmHoliday";
            this.Text = "frmHoliday";
            this.Load += new System.EventHandler(this.frmHoliday_Load);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.ButtonX butReadHolidayDetail;
        private DevComponents.DotNetBar.ButtonX butReadAllHoliday;
        private DevComponents.DotNetBar.ButtonX butClearHoliday;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
    }
}