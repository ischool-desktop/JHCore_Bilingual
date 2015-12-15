namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    partial class ClassStudentTagCountRpt
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
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.lstVwStudTagItems = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.cbxSelectAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(280, 233);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(186, 233);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "產生資料";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lstVwStudTagItems
            // 
            // 
            // 
            // 
            this.lstVwStudTagItems.Border.Class = "ListViewBorder";
            this.lstVwStudTagItems.CheckBoxes = true;
            this.lstVwStudTagItems.Location = new System.Drawing.Point(8, 27);
            this.lstVwStudTagItems.Name = "lstVwStudTagItems";
            this.lstVwStudTagItems.Size = new System.Drawing.Size(348, 199);
            this.lstVwStudTagItems.TabIndex = 5;
            this.lstVwStudTagItems.UseCompatibleStateImageBehavior = false;
            this.lstVwStudTagItems.View = System.Windows.Forms.View.List;
            this.lstVwStudTagItems.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstVwStudTagItems_ItemChecked);
            // 
            // cbxSelectAll
            // 
            this.cbxSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.cbxSelectAll.Location = new System.Drawing.Point(9, 3);
            this.cbxSelectAll.Name = "cbxSelectAll";
            this.cbxSelectAll.Size = new System.Drawing.Size(67, 23);
            this.cbxSelectAll.TabIndex = 4;
            this.cbxSelectAll.Text = "全選";
            this.cbxSelectAll.Click += new System.EventHandler(this.cbxSelectAll_Click);
            this.cbxSelectAll.CheckValueChanged += new System.EventHandler(this.cbxSelectAll_CheckValueChanged);
            // 
            // ClassStudentTagCountRpt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 264);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lstVwStudTagItems);
            this.Controls.Add(this.cbxSelectAll);
            this.MaximumSize = new System.Drawing.Size(370, 298);
            this.MinimumSize = new System.Drawing.Size(370, 298);
            this.Name = "ClassStudentTagCountRpt";
            this.Text = "學生類別統計";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.Controls.ListViewEx lstVwStudTagItems;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxSelectAll;
    }
}