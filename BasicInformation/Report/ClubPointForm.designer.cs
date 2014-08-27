namespace BasicInformation
{
    partial class ClubPointForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.lbDefTemp = new System.Windows.Forms.LinkLabel();
			this.lbTempAll = new System.Windows.Forms.LinkLabel();
			this.btnStart = new DevComponents.DotNetBar.ButtonX();
			this.btnExit = new DevComponents.DotNetBar.ButtonX();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
			this.cbDay7 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.cbDay6 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.cbDay3 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.cbDay5 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.cbDay4 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.cbDay2 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.labelX5 = new DevComponents.DotNetBar.LabelX();
			this.cbDay1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.labelX4 = new DevComponents.DotNetBar.LabelX();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.dateTimeInput1 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
			this.GetDateTime = new DevComponents.DotNetBar.ButtonX();
			this.dateTimeInput2 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
			this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.labelX6 = new DevComponents.DotNetBar.LabelX();
			this.groupPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dateTimeInput1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateTimeInput2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
			this.SuspendLayout();
			// 
			// lbDefTemp
			// 
			this.lbDefTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbDefTemp.AutoSize = true;
			this.lbDefTemp.BackColor = System.Drawing.Color.Transparent;
			this.lbDefTemp.Location = new System.Drawing.Point(14, 480);
			this.lbDefTemp.Name = "lbDefTemp";
			this.lbDefTemp.Size = new System.Drawing.Size(60, 17);
			this.lbDefTemp.TabIndex = 0;
			this.lbDefTemp.TabStop = true;
			this.lbDefTemp.Text = "範本設定";
			this.lbDefTemp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbDefTemp_LinkClicked);
			// 
			// lbTempAll
			// 
			this.lbTempAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbTempAll.AutoSize = true;
			this.lbTempAll.BackColor = System.Drawing.Color.Transparent;
			this.lbTempAll.Location = new System.Drawing.Point(77, 480);
			this.lbTempAll.Name = "lbTempAll";
			this.lbTempAll.Size = new System.Drawing.Size(86, 17);
			this.lbTempAll.TabIndex = 1;
			this.lbTempAll.TabStop = true;
			this.lbTempAll.Text = "功能變數總表";
			this.lbTempAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbTempAll_LinkClicked);
			// 
			// btnStart
			// 
			this.btnStart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.BackColor = System.Drawing.Color.Transparent;
			this.btnStart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnStart.Location = new System.Drawing.Point(278, 476);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 25);
			this.btnStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnStart.TabIndex = 2;
			this.btnStart.Text = "列印";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnExit
			// 
			this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExit.BackColor = System.Drawing.Color.Transparent;
			this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnExit.Location = new System.Drawing.Point(359, 476);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(75, 25);
			this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnExit.TabIndex = 3;
			this.btnExit.Text = "離開";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// linkLabel3
			// 
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel3.Location = new System.Drawing.Point(346, 200);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(86, 17);
			this.linkLabel3.TabIndex = 13;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "清除日期清單";
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
			// 
			// groupPanel1
			// 
			this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
			this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
			this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.groupPanel1.Controls.Add(this.cbDay7);
			this.groupPanel1.Controls.Add(this.cbDay6);
			this.groupPanel1.Controls.Add(this.cbDay3);
			this.groupPanel1.Controls.Add(this.cbDay5);
			this.groupPanel1.Controls.Add(this.cbDay4);
			this.groupPanel1.Controls.Add(this.cbDay2);
			this.groupPanel1.Controls.Add(this.labelX5);
			this.groupPanel1.Controls.Add(this.cbDay1);
			this.groupPanel1.Controls.Add(this.labelX4);
			this.groupPanel1.Controls.Add(this.labelX2);
			this.groupPanel1.Controls.Add(this.labelX3);
			this.groupPanel1.Controls.Add(this.dateTimeInput1);
			this.groupPanel1.Controls.Add(this.GetDateTime);
			this.groupPanel1.Controls.Add(this.dateTimeInput2);
			this.groupPanel1.Location = new System.Drawing.Point(12, 12);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Size = new System.Drawing.Size(422, 179);
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
			this.groupPanel1.Style.Class = "";
			this.groupPanel1.Style.CornerDiameter = 4;
			this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
			// 
			// 
			// 
			this.groupPanel1.StyleMouseDown.Class = "";
			this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.groupPanel1.StyleMouseOver.Class = "";
			this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.groupPanel1.TabIndex = 15;
			this.groupPanel1.Text = "設定";
			// 
			// cbDay7
			// 
			this.cbDay7.AutoSize = true;
			// 
			// 
			// 
			this.cbDay7.BackgroundStyle.Class = "";
			this.cbDay7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.cbDay7.Location = new System.Drawing.Point(350, 25);
			this.cbDay7.Name = "cbDay7";
			this.cbDay7.Size = new System.Drawing.Size(40, 21);
			this.cbDay7.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbDay7.TabIndex = 14;
			this.cbDay7.Text = "日";
			// 
			// cbDay6
			// 
			this.cbDay6.AutoSize = true;
			// 
			// 
			// 
			this.cbDay6.BackgroundStyle.Class = "";
			this.cbDay6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.cbDay6.Location = new System.Drawing.Point(296, 25);
			this.cbDay6.Name = "cbDay6";
			this.cbDay6.Size = new System.Drawing.Size(40, 21);
			this.cbDay6.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbDay6.TabIndex = 13;
			this.cbDay6.Text = "六";
			// 
			// cbDay3
			// 
			this.cbDay3.AutoSize = true;
			// 
			// 
			// 
			this.cbDay3.BackgroundStyle.Class = "";
			this.cbDay3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.cbDay3.Checked = true;
			this.cbDay3.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbDay3.CheckValue = "Y";
			this.cbDay3.Location = new System.Drawing.Point(134, 25);
			this.cbDay3.Name = "cbDay3";
			this.cbDay3.Size = new System.Drawing.Size(40, 21);
			this.cbDay3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbDay3.TabIndex = 12;
			this.cbDay3.Text = "三";
			// 
			// cbDay5
			// 
			this.cbDay5.AutoSize = true;
			// 
			// 
			// 
			this.cbDay5.BackgroundStyle.Class = "";
			this.cbDay5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.cbDay5.Checked = true;
			this.cbDay5.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbDay5.CheckValue = "Y";
			this.cbDay5.Location = new System.Drawing.Point(242, 25);
			this.cbDay5.Name = "cbDay5";
			this.cbDay5.Size = new System.Drawing.Size(40, 21);
			this.cbDay5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbDay5.TabIndex = 11;
			this.cbDay5.Text = "五";
			// 
			// cbDay4
			// 
			this.cbDay4.AutoSize = true;
			// 
			// 
			// 
			this.cbDay4.BackgroundStyle.Class = "";
			this.cbDay4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.cbDay4.Checked = true;
			this.cbDay4.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbDay4.CheckValue = "Y";
			this.cbDay4.Location = new System.Drawing.Point(188, 25);
			this.cbDay4.Name = "cbDay4";
			this.cbDay4.Size = new System.Drawing.Size(40, 21);
			this.cbDay4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbDay4.TabIndex = 10;
			this.cbDay4.Text = "四";
			// 
			// cbDay2
			// 
			this.cbDay2.AutoSize = true;
			// 
			// 
			// 
			this.cbDay2.BackgroundStyle.Class = "";
			this.cbDay2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.cbDay2.Checked = true;
			this.cbDay2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbDay2.CheckValue = "Y";
			this.cbDay2.Location = new System.Drawing.Point(80, 25);
			this.cbDay2.Name = "cbDay2";
			this.cbDay2.Size = new System.Drawing.Size(40, 21);
			this.cbDay2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbDay2.TabIndex = 9;
			this.cbDay2.Text = "二";
			// 
			// labelX5
			// 
			this.labelX5.AutoSize = true;
			// 
			// 
			// 
			this.labelX5.BackgroundStyle.Class = "";
			this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX5.Location = new System.Drawing.Point(26, 3);
			this.labelX5.Name = "labelX5";
			this.labelX5.Size = new System.Drawing.Size(74, 21);
			this.labelX5.TabIndex = 8;
			this.labelX5.Text = "星期篩選：";
			// 
			// cbDay1
			// 
			this.cbDay1.AutoSize = true;
			// 
			// 
			// 
			this.cbDay1.BackgroundStyle.Class = "";
			this.cbDay1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.cbDay1.Checked = true;
			this.cbDay1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbDay1.CheckValue = "Y";
			this.cbDay1.Location = new System.Drawing.Point(26, 25);
			this.cbDay1.Name = "cbDay1";
			this.cbDay1.Size = new System.Drawing.Size(40, 21);
			this.cbDay1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbDay1.TabIndex = 7;
			this.cbDay1.Text = "一";
			// 
			// labelX4
			// 
			this.labelX4.AutoSize = true;
			// 
			// 
			// 
			this.labelX4.BackgroundStyle.Class = "";
			this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX4.Location = new System.Drawing.Point(217, 89);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(34, 21);
			this.labelX4.TabIndex = 4;
			this.labelX4.Text = "結束";
			// 
			// labelX2
			// 
			this.labelX2.AutoSize = true;
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Location = new System.Drawing.Point(26, 89);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(34, 21);
			this.labelX2.TabIndex = 2;
			this.labelX2.Text = "開始";
			// 
			// labelX3
			// 
			this.labelX3.AutoSize = true;
			this.labelX3.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.Class = "";
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.Location = new System.Drawing.Point(26, 60);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(74, 21);
			this.labelX3.TabIndex = 0;
			this.labelX3.Text = "日期區間：";
			// 
			// dateTimeInput1
			// 
			this.dateTimeInput1.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.dateTimeInput1.BackgroundStyle.Class = "DateTimeInputBackground";
			this.dateTimeInput1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dateTimeInput1.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
			this.dateTimeInput1.ButtonDropDown.Visible = true;
			this.dateTimeInput1.IsPopupCalendarOpen = false;
			this.dateTimeInput1.Location = new System.Drawing.Point(70, 87);
			this.dateTimeInput1.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
			this.dateTimeInput1.MinDate = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
			// 
			// 
			// 
			this.dateTimeInput1.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dateTimeInput1.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
			this.dateTimeInput1.MonthCalendar.BackgroundStyle.Class = "";
			this.dateTimeInput1.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dateTimeInput1.MonthCalendar.ClearButtonVisible = true;
			// 
			// 
			// 
			this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
			this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
			this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
			this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.Class = "";
			this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dateTimeInput1.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
			this.dateTimeInput1.MonthCalendar.DisplayMonth = new System.DateTime(2013, 3, 1, 0, 0, 0, 0);
			this.dateTimeInput1.MonthCalendar.MarkedDates = new System.DateTime[0];
			this.dateTimeInput1.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
			this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.Class = "";
			this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dateTimeInput1.MonthCalendar.TodayButtonVisible = true;
			this.dateTimeInput1.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
			this.dateTimeInput1.Name = "dateTimeInput1";
			this.dateTimeInput1.Size = new System.Drawing.Size(137, 25);
			this.dateTimeInput1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.dateTimeInput1.TabIndex = 3;
			// 
			// GetDateTime
			// 
			this.GetDateTime.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.GetDateTime.BackColor = System.Drawing.Color.Transparent;
			this.GetDateTime.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.GetDateTime.Location = new System.Drawing.Point(307, 121);
			this.GetDateTime.Name = "GetDateTime";
			this.GetDateTime.Size = new System.Drawing.Size(91, 25);
			this.GetDateTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.GetDateTime.TabIndex = 6;
			this.GetDateTime.Text = "取得日期";
			this.GetDateTime.Click += new System.EventHandler(this.GetDateTime_Click);
			// 
			// dateTimeInput2
			// 
			this.dateTimeInput2.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.dateTimeInput2.BackgroundStyle.Class = "DateTimeInputBackground";
			this.dateTimeInput2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dateTimeInput2.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
			this.dateTimeInput2.ButtonDropDown.Visible = true;
			this.dateTimeInput2.IsPopupCalendarOpen = false;
			this.dateTimeInput2.Location = new System.Drawing.Point(261, 87);
			// 
			// 
			// 
			this.dateTimeInput2.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dateTimeInput2.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
			this.dateTimeInput2.MonthCalendar.BackgroundStyle.Class = "";
			this.dateTimeInput2.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dateTimeInput2.MonthCalendar.ClearButtonVisible = true;
			// 
			// 
			// 
			this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
			this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
			this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
			this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.Class = "";
			this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dateTimeInput2.MonthCalendar.DayNames = new string[] {
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"};
			this.dateTimeInput2.MonthCalendar.DisplayMonth = new System.DateTime(2013, 3, 1, 0, 0, 0, 0);
			this.dateTimeInput2.MonthCalendar.MarkedDates = new System.DateTime[0];
			this.dateTimeInput2.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dateTimeInput2.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.dateTimeInput2.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
			this.dateTimeInput2.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.dateTimeInput2.MonthCalendar.NavigationBackgroundStyle.Class = "";
			this.dateTimeInput2.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dateTimeInput2.MonthCalendar.TodayButtonVisible = true;
			this.dateTimeInput2.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
			this.dateTimeInput2.Name = "dateTimeInput2";
			this.dateTimeInput2.Size = new System.Drawing.Size(137, 25);
			this.dateTimeInput2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.dateTimeInput2.TabIndex = 5;
			// 
			// dataGridViewX1
			// 
			this.dataGridViewX1.AllowUserToAddRows = false;
			this.dataGridViewX1.AllowUserToResizeRows = false;
			this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
			this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
			this.dataGridViewX1.Location = new System.Drawing.Point(12, 226);
			this.dataGridViewX1.Name = "dataGridViewX1";
			this.dataGridViewX1.RowTemplate.Height = 24;
			this.dataGridViewX1.Size = new System.Drawing.Size(422, 239);
			this.dataGridViewX1.TabIndex = 14;
			// 
			// Column1
			// 
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
			this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
			this.Column1.HeaderText = "日期";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Width = 180;
			// 
			// Column2
			// 
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightCyan;
			this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
			this.Column2.HeaderText = "星期";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			// 
			// labelX6
			// 
			this.labelX6.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX6.BackgroundStyle.Class = "";
			this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX6.Location = new System.Drawing.Point(12, 197);
			this.labelX6.Name = "labelX6";
			this.labelX6.Size = new System.Drawing.Size(75, 23);
			this.labelX6.TabIndex = 12;
			this.labelX6.Text = "日期清單：";
			// 
			// ClubPointForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(444, 510);
			this.Controls.Add(this.linkLabel3);
			this.Controls.Add(this.groupPanel1);
			this.Controls.Add(this.dataGridViewX1);
			this.Controls.Add(this.labelX6);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.lbTempAll);
			this.Controls.Add(this.lbDefTemp);
			this.DoubleBuffered = true;
			this.Name = "ClubPointForm";
			this.Text = "自訂點名單";
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dateTimeInput1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateTimeInput2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lbDefTemp;
        private System.Windows.Forms.LinkLabel lbTempAll;
        private DevComponents.DotNetBar.ButtonX btnStart;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbDay7;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbDay6;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbDay3;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbDay5;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbDay4;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbDay2;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbDay1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateTimeInput1;
        private DevComponents.DotNetBar.ButtonX GetDateTime;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateTimeInput2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private DevComponents.DotNetBar.LabelX labelX6;
    }
}