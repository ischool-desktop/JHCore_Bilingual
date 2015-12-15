namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    partial class StudentIDCardForm
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
            this.cbxUserDefine = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.cbxDefault = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lnkUpload = new System.Windows.Forms.LinkLabel();
            this.lnkUserDefine = new System.Windows.Forms.LinkLabel();
            this.cmdPrint = new DevComponents.DotNetBar.ButtonX();
            this.lnkDefault = new System.Windows.Forms.LinkLabel();
            this.chkUseSystemPhoto = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.SuspendLayout();
            // 
            // cbxUserDefine
            // 
            this.cbxUserDefine.BackColor = System.Drawing.Color.Transparent;
            this.cbxUserDefine.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxUserDefine.Location = new System.Drawing.Point(5, 34);
            this.cbxUserDefine.Name = "cbxUserDefine";
            this.cbxUserDefine.Size = new System.Drawing.Size(27, 23);
            this.cbxUserDefine.TabIndex = 11;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExit.Location = new System.Drawing.Point(106, 92);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(61, 23);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbxDefault
            // 
            this.cbxDefault.BackColor = System.Drawing.Color.Transparent;
            this.cbxDefault.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxDefault.Location = new System.Drawing.Point(5, 5);
            this.cbxDefault.Name = "cbxDefault";
            this.cbxDefault.Size = new System.Drawing.Size(27, 23);
            this.cbxDefault.TabIndex = 10;
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpload.Location = new System.Drawing.Point(117, 39);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(29, 12);
            this.lnkUpload.TabIndex = 8;
            this.lnkUpload.TabStop = true;
            this.lnkUpload.Text = "上傳";
            this.lnkUpload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpload_LinkClicked);
            // 
            // lnkUserDefine
            // 
            this.lnkUserDefine.AutoSize = true;
            this.lnkUserDefine.BackColor = System.Drawing.Color.Transparent;
            this.lnkUserDefine.Location = new System.Drawing.Point(30, 39);
            this.lnkUserDefine.Name = "lnkUserDefine";
            this.lnkUserDefine.Size = new System.Drawing.Size(77, 12);
            this.lnkUserDefine.TabIndex = 6;
            this.lnkUserDefine.TabStop = true;
            this.lnkUserDefine.Text = "檢視自訂範本";
            this.lnkUserDefine.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUserDefine_LinkClicked);
            // 
            // cmdPrint
            // 
            this.cmdPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdPrint.BackColor = System.Drawing.Color.Transparent;
            this.cmdPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdPrint.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmdPrint.Location = new System.Drawing.Point(33, 92);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(61, 23);
            this.cmdPrint.TabIndex = 7;
            this.cmdPrint.Text = "列印";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // lnkDefault
            // 
            this.lnkDefault.AutoSize = true;
            this.lnkDefault.BackColor = System.Drawing.Color.Transparent;
            this.lnkDefault.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lnkDefault.Location = new System.Drawing.Point(30, 9);
            this.lnkDefault.Name = "lnkDefault";
            this.lnkDefault.Size = new System.Drawing.Size(80, 16);
            this.lnkDefault.TabIndex = 5;
            this.lnkDefault.TabStop = true;
            this.lnkDefault.Text = "檢視預設範本";
            this.lnkDefault.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDefault_LinkClicked);
            // 
            // chkUseSystemPhoto
            // 
            this.chkUseSystemPhoto.BackColor = System.Drawing.Color.Transparent;
            this.chkUseSystemPhoto.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkUseSystemPhoto.Location = new System.Drawing.Point(5, 63);
            this.chkUseSystemPhoto.Name = "chkUseSystemPhoto";
            this.chkUseSystemPhoto.Size = new System.Drawing.Size(123, 23);
            this.chkUseSystemPhoto.TabIndex = 12;
            this.chkUseSystemPhoto.Text = "使用系統內照片";
            // 
            // StudentIDCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(178, 121);
            this.Controls.Add(this.chkUseSystemPhoto);
            this.Controls.Add(this.cbxUserDefine);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cbxDefault);
            this.Controls.Add(this.lnkUpload);
            this.Controls.Add(this.lnkUserDefine);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.lnkDefault);
            this.MaximumSize = new System.Drawing.Size(186, 155);
            this.MinimumSize = new System.Drawing.Size(186, 155);
            this.Name = "StudentIDCardForm";
            this.Text = "學生證範本設定與列印";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.CheckBoxX cbxUserDefine;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxDefault;
        private System.Windows.Forms.LinkLabel lnkUpload;
        private System.Windows.Forms.LinkLabel lnkUserDefine;
        private DevComponents.DotNetBar.ButtonX cmdPrint;
        private System.Windows.Forms.LinkLabel lnkDefault;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkUseSystemPhoto;
    }
}