namespace JHSchool.Permrec.StudentExtendControls
{
    partial class PhoneItem
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtHomePhone = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtMotherPhone = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtFatherPhone = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtGuardianPhone = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.t = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(14, 25);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 21);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "住家電話";
            // 
            // txtHomePhone
            // 
            // 
            // 
            // 
            this.txtHomePhone.Border.Class = "TextBoxBorder";
            this.txtHomePhone.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtHomePhone.Location = new System.Drawing.Point(81, 23);
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.Size = new System.Drawing.Size(173, 25);
            this.txtHomePhone.TabIndex = 0;
            // 
            // txtMotherPhone
            // 
            // 
            // 
            // 
            this.txtMotherPhone.Border.Class = "TextBoxBorder";
            this.txtMotherPhone.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMotherPhone.Location = new System.Drawing.Point(353, 23);
            this.txtMotherPhone.Name = "txtMotherPhone";
            this.txtMotherPhone.Size = new System.Drawing.Size(173, 25);
            this.txtMotherPhone.TabIndex = 1;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(286, 25);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 21);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "母親手機";
            // 
            // txtFatherPhone
            // 
            // 
            // 
            // 
            this.txtFatherPhone.Border.Class = "TextBoxBorder";
            this.txtFatherPhone.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFatherPhone.Location = new System.Drawing.Point(80, 72);
            this.txtFatherPhone.Name = "txtFatherPhone";
            this.txtFatherPhone.Size = new System.Drawing.Size(173, 25);
            this.txtFatherPhone.TabIndex = 2;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(13, 74);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(60, 21);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "父親手機";
            // 
            // txtGuardianPhone
            // 
            // 
            // 
            // 
            this.txtGuardianPhone.Border.Class = "TextBoxBorder";
            this.txtGuardianPhone.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGuardianPhone.Location = new System.Drawing.Point(354, 72);
            this.txtGuardianPhone.Name = "txtGuardianPhone";
            this.txtGuardianPhone.Size = new System.Drawing.Size(173, 25);
            this.txtGuardianPhone.TabIndex = 3;
            // 
            // t
            // 
            this.t.AutoSize = true;
            // 
            // 
            // 
            this.t.BackgroundStyle.Class = "";
            this.t.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.t.Location = new System.Drawing.Point(276, 74);
            this.t.Name = "t";
            this.t.Size = new System.Drawing.Size(74, 21);
            this.t.TabIndex = 6;
            this.t.Text = "監護人電話";
            // 
            // PhoneItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtGuardianPhone);
            this.Controls.Add(this.t);
            this.Controls.Add(this.txtFatherPhone);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtMotherPhone);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtHomePhone);
            this.Controls.Add(this.labelX1);
            this.Name = "PhoneItem";
            this.Size = new System.Drawing.Size(550, 125);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtHomePhone;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMotherPhone;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFatherPhone;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGuardianPhone;
        private DevComponents.DotNetBar.LabelX t;
    }
}
