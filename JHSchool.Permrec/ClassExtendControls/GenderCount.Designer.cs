namespace JHSchool.Permrec.ClassExtendControls
{
    partial class GenderCount
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
            this.Count_Button = new DevComponents.DotNetBar.ButtonX();
            this.listViewEx1 = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.btnReload = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.listViewEx2 = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.checkGender = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkYear = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.SuspendLayout();
            // 
            // Count_Button
            // 
            this.Count_Button.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Count_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Count_Button.BackColor = System.Drawing.Color.Transparent;
            this.Count_Button.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.Count_Button.Location = new System.Drawing.Point(388, 218);
            this.Count_Button.Name = "Count_Button";
            this.Count_Button.Size = new System.Drawing.Size(84, 26);
            this.Count_Button.TabIndex = 0;
            this.Count_Button.Text = "匯出至Excel";
            // 
            // listViewEx1
            // 
            this.listViewEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.listViewEx1.Border.Class = "ListViewBorder";
            this.listViewEx1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewEx1.FullRowSelect = true;
            this.listViewEx1.Location = new System.Drawing.Point(28, 12);
            this.listViewEx1.Name = "listViewEx1";
            this.listViewEx1.Size = new System.Drawing.Size(494, 200);
            this.listViewEx1.TabIndex = 1;
            this.listViewEx1.UseCompatibleStateImageBehavior = false;
            this.listViewEx1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "班級名稱";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "班級人數";
            this.columnHeader2.Width = 72;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "男生人數";
            this.columnHeader3.Width = 75;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "女生人數";
            this.columnHeader4.Width = 72;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "未分性別";
            this.columnHeader5.Width = 77;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "班導師";
            this.columnHeader6.Width = 100;
            // 
            // btnReload
            // 
            this.btnReload.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.BackColor = System.Drawing.Color.Transparent;
            this.btnReload.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReload.Location = new System.Drawing.Point(374, 400);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(64, 25);
            this.btnReload.TabIndex = 2;
            this.btnReload.Text = "重新整理";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(457, 399);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 26);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // listViewEx2
            // 
            this.listViewEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.listViewEx2.Border.Class = "ListViewBorder";
            this.listViewEx2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.ColumnHeader7,
            this.ColumnHeader8,
            this.ColumnHeader9});
            this.listViewEx2.Location = new System.Drawing.Point(28, 214);
            this.listViewEx2.Name = "listViewEx2";
            this.listViewEx2.Size = new System.Drawing.Size(354, 172);
            this.listViewEx2.TabIndex = 4;
            this.listViewEx2.UseCompatibleStateImageBehavior = false;
            this.listViewEx2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "年級";
            this.columnHeader10.Width = 132;
            // 
            // ColumnHeader7
            // 
            this.ColumnHeader7.Text = "男生人數";
            this.ColumnHeader7.Width = 78;
            // 
            // ColumnHeader8
            // 
            this.ColumnHeader8.Text = "女生人數";
            this.ColumnHeader8.Width = 74;
            // 
            // ColumnHeader9
            // 
            this.ColumnHeader9.Text = "未分性別";
            this.ColumnHeader9.Width = 164;
            // 
            // checkGender
            // 
            this.checkGender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkGender.BackColor = System.Drawing.Color.Transparent;
            this.checkGender.Location = new System.Drawing.Point(28, 398);
            this.checkGender.Name = "checkGender";
            this.checkGender.Size = new System.Drawing.Size(134, 26);
            this.checkGender.TabIndex = 5;
            this.checkGender.Text = "顯示全校男女人數";
            this.checkGender.CheckedChanged += new System.EventHandler(this.checkGender_CheckedChanged);
            // 
            // checkYear
            // 
            this.checkYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkYear.BackColor = System.Drawing.Color.Transparent;
            this.checkYear.Location = new System.Drawing.Point(177, 399);
            this.checkYear.Name = "checkYear";
            this.checkYear.Size = new System.Drawing.Size(133, 23);
            this.checkYear.TabIndex = 6;
            this.checkYear.Text = "顯示年級男女人數";
            this.checkYear.CheckedChanged += new System.EventHandler(this.checkBoxX1_CheckedChanged);
            // 
            // GenderCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(534, 450);
            this.Controls.Add(this.checkYear);
            this.Controls.Add(this.checkGender);
            this.Controls.Add(this.listViewEx2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.listViewEx1);
            this.Controls.Add(this.Count_Button);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MinimizeBox = true;
            this.Name = "GenderCount";
            this.Text = "男女學生總表";
            this.TopLeftCornerSize = 4;
            this.TopRightCornerSize = 4;
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX Count_Button;
        private DevComponents.DotNetBar.Controls.ListViewEx listViewEx1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private DevComponents.DotNetBar.ButtonX btnReload;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.ListViewEx listViewEx2;
        private System.Windows.Forms.ColumnHeader ColumnHeader7;
        private System.Windows.Forms.ColumnHeader ColumnHeader8;
        private System.Windows.Forms.ColumnHeader ColumnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkGender;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkYear;
    }
}