//已由K12.Form.Photo取代

//namespace JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports
//{
//    partial class PhotosBatchExportForm
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.labelX1 = new DevComponents.DotNetBar.LabelX();
//            this.txtFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
//            this.btnBrowse = new DevComponents.DotNetBar.ButtonX();
//            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
//            this.cbxGraduate = new DevComponents.DotNetBar.Controls.CheckBoxX();
//            this.cbxEnroll = new DevComponents.DotNetBar.Controls.CheckBoxX();
//            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
//            this.cbxByClassNameSeatNo = new DevComponents.DotNetBar.Controls.CheckBoxX();
//            this.cbxByStudentIDNumber = new DevComponents.DotNetBar.Controls.CheckBoxX();
//            this.cbxByStudentNum = new DevComponents.DotNetBar.Controls.CheckBoxX();
//            this.btnDownload = new DevComponents.DotNetBar.ButtonX();
//            this.btnExit = new DevComponents.DotNetBar.ButtonX();
//            this.groupPanel1.SuspendLayout();
//            this.groupPanel2.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // labelX1
//            // 
//            this.labelX1.BackColor = System.Drawing.Color.Transparent;
//            this.labelX1.Location = new System.Drawing.Point(13, 15);
//            this.labelX1.Name = "labelX1";
//            this.labelX1.Size = new System.Drawing.Size(66, 23);
//            this.labelX1.TabIndex = 0;
//            this.labelX1.Text = "儲存位置";
//            // 
//            // txtFilePath
//            // 
//            // 
//            // 
//            // 
//            this.txtFilePath.Border.Class = "TextBoxBorder";
//            this.txtFilePath.Location = new System.Drawing.Point(75, 13);
//            this.txtFilePath.Name = "txtFilePath";
//            this.txtFilePath.Size = new System.Drawing.Size(182, 25);
//            this.txtFilePath.TabIndex = 1;
//            // 
//            // btnBrowse
//            // 
//            this.btnBrowse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
//            this.btnBrowse.BackColor = System.Drawing.Color.Transparent;
//            this.btnBrowse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
//            this.btnBrowse.Location = new System.Drawing.Point(275, 13);
//            this.btnBrowse.Name = "btnBrowse";
//            this.btnBrowse.Size = new System.Drawing.Size(58, 23);
//            this.btnBrowse.TabIndex = 2;
//            this.btnBrowse.Text = "瀏覽";
//            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
//            // 
//            // groupPanel1
//            // 
//            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
//            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
//            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
//            this.groupPanel1.Controls.Add(this.cbxGraduate);
//            this.groupPanel1.Controls.Add(this.cbxEnroll);
//            this.groupPanel1.Location = new System.Drawing.Point(12, 52);
//            this.groupPanel1.Name = "groupPanel1";
//            this.groupPanel1.Size = new System.Drawing.Size(324, 65);
//            // 
//            // 
//            // 
//            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
//            this.groupPanel1.Style.BackColorGradientAngle = 90;
//            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
//            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
//            this.groupPanel1.Style.BorderBottomWidth = 1;
//            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
//            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
//            this.groupPanel1.Style.BorderLeftWidth = 1;
//            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
//            this.groupPanel1.Style.BorderRightWidth = 1;
//            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
//            this.groupPanel1.Style.BorderTopWidth = 1;
//            this.groupPanel1.Style.CornerDiameter = 4;
//            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
//            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
//            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
//            this.groupPanel1.TabIndex = 3;
//            this.groupPanel1.Text = "照片項目";
//            // 
//            // cbxGraduate
//            // 
//            this.cbxGraduate.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
//            this.cbxGraduate.Location = new System.Drawing.Point(113, 7);
//            this.cbxGraduate.Name = "cbxGraduate";
//            this.cbxGraduate.Size = new System.Drawing.Size(92, 23);
//            this.cbxGraduate.TabIndex = 1;
//            this.cbxGraduate.Text = "畢業照片";
//            // 
//            // cbxEnroll
//            // 
//            this.cbxEnroll.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
//            this.cbxEnroll.Location = new System.Drawing.Point(15, 7);
//            this.cbxEnroll.Name = "cbxEnroll";
//            this.cbxEnroll.Size = new System.Drawing.Size(92, 23);
//            this.cbxEnroll.TabIndex = 0;
//            this.cbxEnroll.Text = "入學照片";
//            // 
//            // groupPanel2
//            // 
//            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
//            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
//            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
//            this.groupPanel2.Controls.Add(this.cbxByClassNameSeatNo);
//            this.groupPanel2.Controls.Add(this.cbxByStudentIDNumber);
//            this.groupPanel2.Controls.Add(this.cbxByStudentNum);
//            this.groupPanel2.Location = new System.Drawing.Point(12, 137);
//            this.groupPanel2.Name = "groupPanel2";
//            this.groupPanel2.Size = new System.Drawing.Size(324, 65);
//            // 
//            // 
//            // 
//            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
//            this.groupPanel2.Style.BackColorGradientAngle = 90;
//            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
//            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
//            this.groupPanel2.Style.BorderBottomWidth = 1;
//            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
//            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
//            this.groupPanel2.Style.BorderLeftWidth = 1;
//            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
//            this.groupPanel2.Style.BorderRightWidth = 1;
//            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
//            this.groupPanel2.Style.BorderTopWidth = 1;
//            this.groupPanel2.Style.CornerDiameter = 4;
//            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
//            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
//            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
//            this.groupPanel2.TabIndex = 4;
//            this.groupPanel2.Text = "命名方式";
//            // 
//            // cbxByClassNameSeatNo
//            // 
//            this.cbxByClassNameSeatNo.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
//            this.cbxByClassNameSeatNo.Location = new System.Drawing.Point(197, 7);
//            this.cbxByClassNameSeatNo.Name = "cbxByClassNameSeatNo";
//            this.cbxByClassNameSeatNo.Size = new System.Drawing.Size(92, 23);
//            this.cbxByClassNameSeatNo.TabIndex = 3;
//            this.cbxByClassNameSeatNo.Text = "班級座號";
//            // 
//            // cbxByStudentIDNumber
//            // 
//            this.cbxByStudentIDNumber.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
//            this.cbxByStudentIDNumber.Location = new System.Drawing.Point(99, 7);
//            this.cbxByStudentIDNumber.Name = "cbxByStudentIDNumber";
//            this.cbxByStudentIDNumber.Size = new System.Drawing.Size(92, 23);
//            this.cbxByStudentIDNumber.TabIndex = 2;
//            this.cbxByStudentIDNumber.Text = "身分證號";
//            // 
//            // cbxByStudentNum
//            // 
//            this.cbxByStudentNum.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
//            this.cbxByStudentNum.Location = new System.Drawing.Point(15, 7);
//            this.cbxByStudentNum.Name = "cbxByStudentNum";
//            this.cbxByStudentNum.Size = new System.Drawing.Size(92, 23);
//            this.cbxByStudentNum.TabIndex = 1;
//            this.cbxByStudentNum.Text = "學號";
//            // 
//            // btnDownload
//            // 
//            this.btnDownload.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
//            this.btnDownload.BackColor = System.Drawing.Color.Transparent;
//            this.btnDownload.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
//            this.btnDownload.Location = new System.Drawing.Point(189, 217);
//            this.btnDownload.Name = "btnDownload";
//            this.btnDownload.Size = new System.Drawing.Size(68, 23);
//            this.btnDownload.TabIndex = 5;
//            this.btnDownload.Text = "匯出";
//            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
//            // 
//            // btnExit
//            // 
//            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
//            this.btnExit.BackColor = System.Drawing.Color.Transparent;
//            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
//            this.btnExit.Location = new System.Drawing.Point(268, 217);
//            this.btnExit.Name = "btnExit";
//            this.btnExit.Size = new System.Drawing.Size(68, 23);
//            this.btnExit.TabIndex = 6;
//            this.btnExit.Text = "離開";
//            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
//            // 
//            // PhotosBatchExportForm
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(345, 252);
//            this.Controls.Add(this.btnExit);
//            this.Controls.Add(this.btnDownload);
//            this.Controls.Add(this.groupPanel2);
//            this.Controls.Add(this.groupPanel1);
//            this.Controls.Add(this.btnBrowse);
//            this.Controls.Add(this.txtFilePath);
//            this.Controls.Add(this.labelX1);
//            this.Name = "PhotosBatchExportForm";
//            this.Text = "匯出批次照片";
//            this.groupPanel1.ResumeLayout(false);
//            this.groupPanel2.ResumeLayout(false);
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private DevComponents.DotNetBar.LabelX labelX1;
//        private DevComponents.DotNetBar.Controls.TextBoxX txtFilePath;
//        private DevComponents.DotNetBar.ButtonX btnBrowse;
//        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
//        private DevComponents.DotNetBar.Controls.CheckBoxX cbxEnroll;
//        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
//        private DevComponents.DotNetBar.ButtonX btnDownload;
//        private DevComponents.DotNetBar.ButtonX btnExit;
//        private DevComponents.DotNetBar.Controls.CheckBoxX cbxGraduate;
//        private DevComponents.DotNetBar.Controls.CheckBoxX cbxByClassNameSeatNo;
//        private DevComponents.DotNetBar.Controls.CheckBoxX cbxByStudentIDNumber;
//        private DevComponents.DotNetBar.Controls.CheckBoxX cbxByStudentNum;
//    }
//}