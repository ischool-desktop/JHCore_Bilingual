namespace JHSchool.Permrec.StudentExtendControls
{
    partial class DiplomaInfoPalmerworm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtGDNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtSchoolYear = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtClass = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtMemo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cboReason = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(28, 46);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 21);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "畢業證書字號";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtGDNumber
            // 
            // 
            // 
            // 
            this.txtGDNumber.Border.Class = "TextBoxBorder";
            this.txtGDNumber.Location = new System.Drawing.Point(121, 44);
            this.txtGDNumber.Name = "txtGDNumber";
            this.txtGDNumber.Size = new System.Drawing.Size(188, 25);
            this.txtGDNumber.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.FillWeight = 40F;
            this.dataGridViewTextBoxColumn1.HeaderText = "畢業相關訊息";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.FillWeight = 60F;
            this.dataGridViewTextBoxColumn2.HeaderText = "";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.Location = new System.Drawing.Point(41, 14);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(74, 21);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "畢業學年度";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtSchoolYear
            // 
            // 
            // 
            // 
            this.txtSchoolYear.Border.Class = "TextBoxBorder";
            this.txtSchoolYear.Location = new System.Drawing.Point(121, 11);
            this.txtSchoolYear.Name = "txtSchoolYear";
            this.txtSchoolYear.Size = new System.Drawing.Size(48, 25);
            this.txtSchoolYear.TabIndex = 1;
            this.txtSchoolYear.TextChanged += new System.EventHandler(this.txtSchoolYear_TextChanged);
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.Location = new System.Drawing.Point(339, 14);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(60, 21);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "畢業資格";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtClass
            // 
            // 
            // 
            // 
            this.txtClass.Border.Class = "TextBoxBorder";
            this.txtClass.Location = new System.Drawing.Point(405, 44);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(116, 25);
            this.txtClass.TabIndex = 4;
            this.txtClass.Visible = false;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.Location = new System.Drawing.Point(339, 46);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(60, 21);
            this.labelX5.TabIndex = 5;
            this.labelX5.Text = "離校班級";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            this.labelX5.Visible = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkRate = 0;
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // txtMemo
            // 
            // 
            // 
            // 
            this.txtMemo.Border.Class = "TextBoxBorder";
            this.txtMemo.Location = new System.Drawing.Point(121, 78);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(400, 73);
            this.txtMemo.TabIndex = 5;
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            this.labelX6.Location = new System.Drawing.Point(28, 78);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(87, 21);
            this.labelX6.TabIndex = 8;
            this.labelX6.Text = "畢業相關訊息";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cboReason
            // 
            this.cboReason.DisplayMember = "Text";
            this.cboReason.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboReason.FormattingEnabled = true;
            this.cboReason.ItemHeight = 19;
            this.cboReason.Location = new System.Drawing.Point(405, 11);
            this.cboReason.Name = "cboReason";
            this.cboReason.Size = new System.Drawing.Size(116, 25);
            this.cboReason.TabIndex = 9;
            // 
            // DiplomaInfoPalmerworm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.Controls.Add(this.cboReason);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtSchoolYear);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtGDNumber);
            this.Controls.Add(this.labelX1);
            this.Name = "DiplomaInfoPalmerworm";
            this.Size = new System.Drawing.Size(550, 165);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGDNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSchoolYear;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtClass;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMemo;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboReason;
    }
}
