namespace JHSchool.Permrec.EduAdminExtendCotnrols.StudGraduateDocNo
{
    partial class BatchStudGraduateDocNo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtGRDoc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtGRNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnPreview = new DevComponents.DotNetBar.ButtonX();
            this.dgGraduateDocNoData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.lblTotalCount = new DevComponents.DotNetBar.LabelX();
            this.lblHasGraduateCount = new DevComponents.DotNetBar.LabelX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtGDNo1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtGRDoc1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cboSortBySnum = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.cboGradeYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.gp1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cboSortByClassSeatNo = new DevComponents.DotNetBar.Controls.CheckBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dgGraduateDocNoData)).BeginInit();
            this.gp1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtGRDoc
            // 
            // 
            // 
            // 
            this.txtGRDoc.Border.Class = "TextBoxBorder";
            this.txtGRDoc.Location = new System.Drawing.Point(56, 33);
            this.txtGRDoc.Name = "txtGRDoc";
            this.txtGRDoc.Size = new System.Drawing.Size(117, 23);
            this.txtGRDoc.TabIndex = 2;
            this.txtGRDoc.TextChanged += new System.EventHandler(this.txtGRDoc_TextChanged);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(178, 33);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(44, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "字 第";
            // 
            // txtGRNo
            // 
            // 
            // 
            // 
            this.txtGRNo.Border.Class = "TextBoxBorder";
            this.txtGRNo.Location = new System.Drawing.Point(225, 33);
            this.txtGRNo.Name = "txtGRNo";
            this.txtGRNo.Size = new System.Drawing.Size(117, 23);
            this.txtGRNo.TabIndex = 3;
            this.txtGRNo.WatermarkText = "例如:001";
            this.txtGRNo.TextChanged += new System.EventHandler(this.txtGRDoc_TextChanged);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(346, 33);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(35, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "號";
            // 
            // btnPreview
            // 
            this.btnPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPreview.BackColor = System.Drawing.Color.Transparent;
            this.btnPreview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPreview.Location = new System.Drawing.Point(550, 256);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(43, 23);
            this.btnPreview.TabIndex = 4;
            this.btnPreview.Text = "預覽結果";
            this.btnPreview.Visible = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // dgGraduateDocNoData
            // 
            this.dgGraduateDocNoData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgGraduateDocNoData.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgGraduateDocNoData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgGraduateDocNoData.Location = new System.Drawing.Point(4, 349);
            this.dgGraduateDocNoData.Name = "dgGraduateDocNoData";
            this.dgGraduateDocNoData.RowTemplate.Height = 24;
            this.dgGraduateDocNoData.Size = new System.Drawing.Size(540, 50);
            this.dgGraduateDocNoData.TabIndex = 5;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCount.Location = new System.Drawing.Point(9, 1);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(340, 23);
            this.lblTotalCount.TabIndex = 6;
            this.lblTotalCount.Text = "labelX3";
            // 
            // lblHasGraduateCount
            // 
            this.lblHasGraduateCount.BackColor = System.Drawing.Color.Transparent;
            this.lblHasGraduateCount.Location = new System.Drawing.Point(9, 24);
            this.lblHasGraduateCount.Name = "lblHasGraduateCount";
            this.lblHasGraduateCount.Size = new System.Drawing.Size(340, 23);
            this.lblHasGraduateCount.TabIndex = 7;
            this.lblHasGraduateCount.Text = "labelX4";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(196, 187);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "產生證書字號";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(297, 187);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            this.labelX3.Location = new System.Drawing.Point(346, 64);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(35, 23);
            this.labelX3.TabIndex = 14;
            this.labelX3.Text = "號";
            // 
            // txtGDNo1
            // 
            // 
            // 
            // 
            this.txtGDNo1.Border.Class = "TextBoxBorder";
            this.txtGDNo1.Location = new System.Drawing.Point(225, 64);
            this.txtGDNo1.Name = "txtGDNo1";
            this.txtGDNo1.Size = new System.Drawing.Size(117, 23);
            this.txtGDNo1.TabIndex = 5;
            this.txtGDNo1.WatermarkText = "例如:001";
            this.txtGDNo1.TextChanged += new System.EventHandler(this.txtGRDoc_TextChanged);
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            this.labelX4.Location = new System.Drawing.Point(178, 64);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(44, 23);
            this.labelX4.TabIndex = 12;
            this.labelX4.Text = "字 第";
            // 
            // txtGRDoc1
            // 
            // 
            // 
            // 
            this.txtGRDoc1.Border.Class = "TextBoxBorder";
            this.txtGRDoc1.Location = new System.Drawing.Point(56, 64);
            this.txtGRDoc1.Name = "txtGRDoc1";
            this.txtGRDoc1.Size = new System.Drawing.Size(117, 23);
            this.txtGRDoc1.TabIndex = 4;
            this.txtGRDoc1.TextChanged += new System.EventHandler(this.txtGRDoc_TextChanged);
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            this.labelX5.Location = new System.Drawing.Point(15, 35);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(37, 23);
            this.labelX5.TabIndex = 15;
            this.labelX5.Text = "畢業";
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            this.labelX6.Location = new System.Drawing.Point(16, 62);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(37, 23);
            this.labelX6.TabIndex = 16;
            this.labelX6.Text = "修業";
            // 
            // cboSortBySnum
            // 
            this.cboSortBySnum.BackColor = System.Drawing.Color.Transparent;
            this.cboSortBySnum.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cboSortBySnum.Location = new System.Drawing.Point(155, 4);
            this.cboSortBySnum.Name = "cboSortBySnum";
            this.cboSortBySnum.Size = new System.Drawing.Size(96, 23);
            this.cboSortBySnum.TabIndex = 9;
            this.cboSortBySnum.Text = "依學號遞增";
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(12, 187);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "匯出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cboGradeYear
            // 
            this.cboGradeYear.DisplayMember = "Text";
            this.cboGradeYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGradeYear.FormattingEnabled = true;
            this.cboGradeYear.ItemHeight = 17;
            this.cboGradeYear.Location = new System.Drawing.Point(56, 4);
            this.cboGradeYear.Name = "cboGradeYear";
            this.cboGradeYear.Size = new System.Drawing.Size(75, 23);
            this.cboGradeYear.TabIndex = 1;
            this.cboGradeYear.TextChanged += new System.EventHandler(this.cboGradeYear_TextChanged);
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            this.labelX7.Location = new System.Drawing.Point(17, 7);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(37, 23);
            this.labelX7.TabIndex = 20;
            this.labelX7.Text = "年級";
            // 
            // gp1
            // 
            this.gp1.BackColor = System.Drawing.Color.Transparent;
            this.gp1.CanvasColor = System.Drawing.SystemColors.Control;
            this.gp1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gp1.Controls.Add(this.lblTotalCount);
            this.gp1.Controls.Add(this.lblHasGraduateCount);
            this.gp1.Location = new System.Drawing.Point(12, 91);
            this.gp1.Name = "gp1";
            this.gp1.Size = new System.Drawing.Size(358, 80);
            // 
            // 
            // 
            this.gp1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gp1.Style.BackColorGradientAngle = 90;
            this.gp1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gp1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gp1.Style.BorderBottomWidth = 1;
            this.gp1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gp1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gp1.Style.BorderLeftWidth = 1;
            this.gp1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gp1.Style.BorderRightWidth = 1;
            this.gp1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gp1.Style.BorderTopWidth = 1;
            this.gp1.Style.CornerDiameter = 4;
            this.gp1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gp1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gp1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.gp1.TabIndex = 21;
            this.gp1.Text = "訊息";
            // 
            // cboSortByClassSeatNo
            // 
            this.cboSortByClassSeatNo.BackColor = System.Drawing.Color.Transparent;
            this.cboSortByClassSeatNo.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cboSortByClassSeatNo.Location = new System.Drawing.Point(249, 4);
            this.cboSortByClassSeatNo.Name = "cboSortByClassSeatNo";
            this.cboSortByClassSeatNo.Size = new System.Drawing.Size(126, 23);
            this.cboSortByClassSeatNo.TabIndex = 22;
            this.cboSortByClassSeatNo.Text = "依班級座號遞增";
            // 
            // BatchStudGraduateDocNo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(382, 222);
            this.Controls.Add(this.cboSortByClassSeatNo);
            this.Controls.Add(this.gp1);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.cboGradeYear);
            this.Controls.Add(this.cboSortBySnum);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtGDNo1);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtGRDoc1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgGraduateDocNoData);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtGRNo);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtGRDoc);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MaximumSize = new System.Drawing.Size(390, 256);
            this.MinimumSize = new System.Drawing.Size(390, 256);
            this.Name = "BatchStudGraduateDocNo";
            this.Text = "產生畢修業證書字號";
            this.Load += new System.EventHandler(this.BatchStudGraduateDocNo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgGraduateDocNoData)).EndInit();
            this.gp1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txtGRDoc;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGRNo;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnPreview;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgGraduateDocNoData;
        private DevComponents.DotNetBar.LabelX lblTotalCount;
        private DevComponents.DotNetBar.LabelX lblHasGraduateCount;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGDNo1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGRDoc1;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.CheckBoxX cboSortBySnum;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGradeYear;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.GroupPanel gp1;
        private DevComponents.DotNetBar.Controls.CheckBoxX cboSortByClassSeatNo;
    }
}