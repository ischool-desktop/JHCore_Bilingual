namespace BasicInformation.Student
{
    partial class SchoolYearEditor
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lblCount = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.btnEntrance = new DevComponents.DotNetBar.ButtonX();
            this.btnGraduate = new DevComponents.DotNetBar.ButtonX();
            this.dtEntrance = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dtLeaving = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            ((System.ComponentModel.ISupportInitialize)(this.dtEntrance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLeaving)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(56, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "已選擇";
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblCount.BackgroundStyle.Class = "";
            this.lblCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCount.ForeColor = System.Drawing.Color.Red;
            this.lblCount.Location = new System.Drawing.Point(75, 13);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(56, 23);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "0";
            this.lblCount.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(153, 13);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(56, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "位學生";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(13, 51);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(74, 23);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "入學日期";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(12, 80);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(75, 23);
            this.labelX5.TabIndex = 4;
            this.labelX5.Text = "畢業日期";
            // 
            // btnEntrance
            // 
            this.btnEntrance.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEntrance.BackColor = System.Drawing.Color.Transparent;
            this.btnEntrance.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEntrance.Location = new System.Drawing.Point(281, 49);
            this.btnEntrance.Name = "btnEntrance";
            this.btnEntrance.Size = new System.Drawing.Size(94, 25);
            this.btnEntrance.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEntrance.TabIndex = 1;
            this.btnEntrance.Text = "設定入學日期";
            this.btnEntrance.Click += new System.EventHandler(this.btnEntrance_Click);
            // 
            // btnGraduate
            // 
            this.btnGraduate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGraduate.BackColor = System.Drawing.Color.Transparent;
            this.btnGraduate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGraduate.Location = new System.Drawing.Point(281, 78);
            this.btnGraduate.Name = "btnGraduate";
            this.btnGraduate.Size = new System.Drawing.Size(94, 25);
            this.btnGraduate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGraduate.TabIndex = 3;
            this.btnGraduate.Text = "設定畢業日期";
            this.btnGraduate.Click += new System.EventHandler(this.btnGraduate_Click);
            // 
            // dtEntrance
            // 
            this.dtEntrance.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtEntrance.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtEntrance.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEntrance.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtEntrance.ButtonDropDown.Visible = true;
            this.dtEntrance.IsPopupCalendarOpen = false;
            this.dtEntrance.Location = new System.Drawing.Point(75, 49);
            // 
            // 
            // 
            this.dtEntrance.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEntrance.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtEntrance.MonthCalendar.BackgroundStyle.Class = "";
            this.dtEntrance.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEntrance.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtEntrance.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtEntrance.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEntrance.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtEntrance.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtEntrance.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtEntrance.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtEntrance.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtEntrance.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEntrance.MonthCalendar.DisplayMonth = new System.DateTime(2014, 12, 1, 0, 0, 0, 0);
            this.dtEntrance.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtEntrance.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEntrance.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtEntrance.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEntrance.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtEntrance.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtEntrance.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEntrance.MonthCalendar.TodayButtonVisible = true;
            this.dtEntrance.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtEntrance.Name = "dtEntrance";
            this.dtEntrance.Size = new System.Drawing.Size(200, 25);
            this.dtEntrance.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtEntrance.TabIndex = 5;
            // 
            // dtLeaving
            // 
            this.dtLeaving.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dtLeaving.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtLeaving.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLeaving.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtLeaving.ButtonDropDown.Visible = true;
            this.dtLeaving.IsPopupCalendarOpen = false;
            this.dtLeaving.Location = new System.Drawing.Point(75, 78);
            // 
            // 
            // 
            this.dtLeaving.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtLeaving.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtLeaving.MonthCalendar.BackgroundStyle.Class = "";
            this.dtLeaving.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLeaving.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtLeaving.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtLeaving.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtLeaving.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtLeaving.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtLeaving.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtLeaving.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtLeaving.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtLeaving.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLeaving.MonthCalendar.DisplayMonth = new System.DateTime(2014, 12, 1, 0, 0, 0, 0);
            this.dtLeaving.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtLeaving.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtLeaving.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtLeaving.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtLeaving.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtLeaving.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtLeaving.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtLeaving.MonthCalendar.TodayButtonVisible = true;
            this.dtLeaving.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtLeaving.Name = "dtLeaving";
            this.dtLeaving.Size = new System.Drawing.Size(200, 25);
            this.dtLeaving.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtLeaving.TabIndex = 6;
            // 
            // SchoolYearEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 111);
            this.Controls.Add(this.dtLeaving);
            this.Controls.Add(this.dtEntrance);
            this.Controls.Add(this.btnGraduate);
            this.Controls.Add(this.btnEntrance);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "SchoolYearEditor";
            this.Text = "批次修改入學及畢業年度";
            ((System.ComponentModel.ISupportInitialize)(this.dtEntrance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLeaving)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX lblCount;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnEntrance;
        private DevComponents.DotNetBar.ButtonX btnGraduate;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtEntrance;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtLeaving;
    }
}