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
            this.numEntrance = new System.Windows.Forms.NumericUpDown();
            this.numGraduate = new System.Windows.Forms.NumericUpDown();
            this.btnEntrance = new DevComponents.DotNetBar.ButtonX();
            this.btnGraduate = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.numEntrance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGraduate)).BeginInit();
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
            this.labelX3.Text = "學生";
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
            this.labelX4.Text = "入學年度";
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
            this.labelX5.Text = "畢業年度";
            // 
            // numEntrance
            // 
            this.numEntrance.Location = new System.Drawing.Point(94, 49);
            this.numEntrance.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numEntrance.Name = "numEntrance";
            this.numEntrance.Size = new System.Drawing.Size(115, 25);
            this.numEntrance.TabIndex = 5;
            this.numEntrance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numGraduate
            // 
            this.numGraduate.Location = new System.Drawing.Point(94, 78);
            this.numGraduate.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numGraduate.Name = "numGraduate";
            this.numGraduate.Size = new System.Drawing.Size(115, 25);
            this.numGraduate.TabIndex = 6;
            this.numGraduate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnEntrance
            // 
            this.btnEntrance.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEntrance.BackColor = System.Drawing.Color.Transparent;
            this.btnEntrance.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEntrance.Location = new System.Drawing.Point(215, 49);
            this.btnEntrance.Name = "btnEntrance";
            this.btnEntrance.Size = new System.Drawing.Size(94, 25);
            this.btnEntrance.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEntrance.TabIndex = 7;
            this.btnEntrance.Text = "設定入學年度";
            this.btnEntrance.Click += new System.EventHandler(this.btnEntrance_Click);
            // 
            // btnGraduate
            // 
            this.btnGraduate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGraduate.BackColor = System.Drawing.Color.Transparent;
            this.btnGraduate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGraduate.Location = new System.Drawing.Point(215, 78);
            this.btnGraduate.Name = "btnGraduate";
            this.btnGraduate.Size = new System.Drawing.Size(94, 25);
            this.btnGraduate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGraduate.TabIndex = 8;
            this.btnGraduate.Text = "設定畢業年度";
            this.btnGraduate.Click += new System.EventHandler(this.btnGraduate_Click);
            // 
            // SchoolYearEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 113);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.numGraduate);
            this.Controls.Add(this.numEntrance);
            this.Controls.Add(this.btnGraduate);
            this.Controls.Add(this.btnEntrance);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "SchoolYearEditor";
            this.Text = "批次修改入學及畢業年度";
            ((System.ComponentModel.ISupportInitialize)(this.numEntrance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGraduate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX lblCount;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.NumericUpDown numEntrance;
        private System.Windows.Forms.NumericUpDown numGraduate;
        private DevComponents.DotNetBar.ButtonX btnEntrance;
        private DevComponents.DotNetBar.ButtonX btnGraduate;
    }
}