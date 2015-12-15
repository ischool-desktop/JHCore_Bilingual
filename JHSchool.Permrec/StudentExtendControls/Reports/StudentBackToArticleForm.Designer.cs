namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    partial class StudentBackToArticleForm
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
            this.lnkDefault = new System.Windows.Forms.LinkLabel();
            this.cmdPrint = new DevComponents.DotNetBar.ButtonX();
            this.cbxUserDefine = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbxDefault = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lnkUpload = new System.Windows.Forms.LinkLabel();
            this.lnkUserDefine = new System.Windows.Forms.LinkLabel();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // lnkDefault
            // 
            this.lnkDefault.AutoSize = true;
            this.lnkDefault.BackColor = System.Drawing.Color.Transparent;
            this.lnkDefault.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lnkDefault.Location = new System.Drawing.Point(37, 16);
            this.lnkDefault.Name = "lnkDefault";
            this.lnkDefault.Size = new System.Drawing.Size(80, 16);
            this.lnkDefault.TabIndex = 0;
            this.lnkDefault.TabStop = true;
            this.lnkDefault.Text = "檢視預設範本";
            this.lnkDefault.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDefault_LinkClicked);
            // 
            // cmdPrint
            // 
            this.cmdPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdPrint.BackColor = System.Drawing.Color.Transparent;
            this.cmdPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdPrint.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmdPrint.Location = new System.Drawing.Point(40, 75);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(61, 23);
            this.cmdPrint.TabIndex = 1;
            this.cmdPrint.Text = "列印";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cbxUserDefine
            // 
            this.cbxUserDefine.BackColor = System.Drawing.Color.Transparent;
            this.cbxUserDefine.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxUserDefine.Location = new System.Drawing.Point(12, 41);
            this.cbxUserDefine.Name = "cbxUserDefine";
            this.cbxUserDefine.Size = new System.Drawing.Size(27, 23);
            this.cbxUserDefine.TabIndex = 4;
            // 
            // cbxDefault
            // 
            this.cbxDefault.BackColor = System.Drawing.Color.Transparent;
            this.cbxDefault.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxDefault.Location = new System.Drawing.Point(12, 12);
            this.cbxDefault.Name = "cbxDefault";
            this.cbxDefault.Size = new System.Drawing.Size(27, 23);
            this.cbxDefault.TabIndex = 3;
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpload.Location = new System.Drawing.Point(124, 46);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(32, 16);
            this.lnkUpload.TabIndex = 2;
            this.lnkUpload.TabStop = true;
            this.lnkUpload.Text = "上傳";
            this.lnkUpload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpload_LinkClicked);
            // 
            // lnkUserDefine
            // 
            this.lnkUserDefine.AutoSize = true;
            this.lnkUserDefine.BackColor = System.Drawing.Color.Transparent;
            this.lnkUserDefine.Location = new System.Drawing.Point(37, 46);
            this.lnkUserDefine.Name = "lnkUserDefine";
            this.lnkUserDefine.Size = new System.Drawing.Size(80, 16);
            this.lnkUserDefine.TabIndex = 1;
            this.lnkUserDefine.TabStop = true;
            this.lnkUserDefine.Text = "檢視自訂範本";
            this.lnkUserDefine.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUserDefine_LinkClicked);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExit.Location = new System.Drawing.Point(113, 75);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(61, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // StudentBackToArticleForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(183, 106);
            this.Controls.Add(this.cbxUserDefine);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cbxDefault);
            this.Controls.Add(this.lnkUpload);
            this.Controls.Add(this.lnkUserDefine);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.lnkDefault);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MaximumSize = new System.Drawing.Size(191, 140);
            this.MinimumSize = new System.Drawing.Size(191, 140);
            this.Name = "StudentBackToArticleForm";
            this.Text = "轉出回條範本設定與列印";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkDefault;
        private DevComponents.DotNetBar.ButtonX cmdPrint;
        private System.Windows.Forms.LinkLabel lnkUserDefine;
        private System.Windows.Forms.LinkLabel lnkUpload;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxDefault;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxUserDefine;
        private DevComponents.DotNetBar.ButtonX btnExit;
    }
}