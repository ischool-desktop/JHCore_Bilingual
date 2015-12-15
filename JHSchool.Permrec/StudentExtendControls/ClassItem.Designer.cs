namespace JHSchool.Permrec.StudentExtendControls
{
    partial class ClassItem
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.txtStudentNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.cboSeatNo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label37 = new System.Windows.Forms.Label();
            this.cboGradeYear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboClass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SuspendLayout();
            // 
            // txtStudentNumber
            // 
            // 
            // 
            // 
            this.txtStudentNumber.Border.Class = "TextBoxBorder";
            this.txtStudentNumber.Location = new System.Drawing.Point(409, 11);
            this.txtStudentNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtStudentNumber.Name = "txtStudentNumber";
            this.txtStudentNumber.Size = new System.Drawing.Size(119, 25);
            this.txtStudentNumber.TabIndex = 4;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.ForeColor = System.Drawing.Color.Black;
            this.label41.Location = new System.Drawing.Point(354, 15);
            this.label41.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(47, 17);
            this.label41.TabIndex = 205;
            this.label41.Text = "學　號";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.ForeColor = System.Drawing.Color.Black;
            this.label40.Location = new System.Drawing.Point(302, 59);
            this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(47, 17);
            this.label40.TabIndex = 206;
            this.label40.Text = "年　級";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label40.Visible = false;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(23, 15);
            this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(47, 17);
            this.label38.TabIndex = 204;
            this.label38.Text = "班　級";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSeatNo
            // 
            this.cboSeatNo.DisplayMember = "Text";
            this.cboSeatNo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSeatNo.FormattingEnabled = true;
            this.cboSeatNo.ItemHeight = 19;
            this.cboSeatNo.Location = new System.Drawing.Point(266, 12);
            this.cboSeatNo.Margin = new System.Windows.Forms.Padding(4);
            this.cboSeatNo.Name = "cboSeatNo";
            this.cboSeatNo.Size = new System.Drawing.Size(76, 25);
            this.cboSeatNo.TabIndex = 3;
            this.cboSeatNo.TextChanged += new System.EventHandler(this.cboSeatNo_TextChanged);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.ForeColor = System.Drawing.Color.Black;
            this.label37.Location = new System.Drawing.Point(211, 15);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(47, 17);
            this.label37.TabIndex = 207;
            this.label37.Text = "座　號";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboGradeYear
            // 
            this.cboGradeYear.DisplayMember = "Text";
            this.cboGradeYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGradeYear.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboGradeYear.FormattingEnabled = true;
            this.cboGradeYear.ItemHeight = 16;
            this.cboGradeYear.Location = new System.Drawing.Point(357, 56);
            this.cboGradeYear.Margin = new System.Windows.Forms.Padding(4);
            this.cboGradeYear.Name = "cboGradeYear";
            this.cboGradeYear.Size = new System.Drawing.Size(119, 22);
            this.cboGradeYear.TabIndex = 1;
            this.cboGradeYear.Visible = false;
            // 
            // cboClass
            // 
            this.cboClass.DisplayMember = "Text";
            this.cboClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboClass.FormattingEnabled = true;
            this.cboClass.ItemHeight = 19;
            this.cboClass.Location = new System.Drawing.Point(77, 12);
            this.cboClass.MaxDropDownItems = 100;
            this.cboClass.Name = "cboClass";
            this.cboClass.Size = new System.Drawing.Size(119, 25);
            this.cboClass.TabIndex = 2;
            this.cboClass.TextChanged += new System.EventHandler(this.cboClass_TextChanged);
            // 
            // ClassItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.cboClass);
            this.Controls.Add(this.txtStudentNumber);
            this.Controls.Add(this.cboGradeYear);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.cboSeatNo);
            this.Controls.Add(this.label37);
            this.Name = "ClassItem";
            this.Size = new System.Drawing.Size(550, 50);
            this.Load += new System.EventHandler(this.ClassItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal DevComponents.DotNetBar.Controls.TextBoxX txtStudentNumber;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGradeYear;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label38;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSeatNo;
        private System.Windows.Forms.Label label37;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboClass;
    }
}
