namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    partial class ClassAgePercentageRpt
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
            this.cbxSelectAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lstVwAgeItems = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // cbxSelectAll
            // 
            this.cbxSelectAll.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbxSelectAll.BackgroundStyle.Class = "";
            this.cbxSelectAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxSelectAll.Location = new System.Drawing.Point(12, 12);
            this.cbxSelectAll.Name = "cbxSelectAll";
            this.cbxSelectAll.Size = new System.Drawing.Size(67, 23);
            this.cbxSelectAll.TabIndex = 0;
            this.cbxSelectAll.Text = "全選";
            this.cbxSelectAll.CheckValueChanged += new System.EventHandler(this.cbxSelectAll_CheckValueChanged);
            this.cbxSelectAll.Click += new System.EventHandler(this.cbxSelectAll_Click);
            // 
            // lstVwAgeItems
            // 
            // 
            // 
            // 
            this.lstVwAgeItems.Border.Class = "ListViewBorder";
            this.lstVwAgeItems.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstVwAgeItems.CheckBoxes = true;
            this.lstVwAgeItems.Location = new System.Drawing.Point(11, 36);
            this.lstVwAgeItems.Name = "lstVwAgeItems";
            this.lstVwAgeItems.Size = new System.Drawing.Size(285, 199);
            this.lstVwAgeItems.TabIndex = 1;
            this.lstVwAgeItems.UseCompatibleStateImageBehavior = false;
            this.lstVwAgeItems.View = System.Windows.Forms.View.List;
            this.lstVwAgeItems.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstVwAgeItems_ItemChecked);
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(126, 241);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "產生資料";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(220, 241);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ClassAgePercentageRpt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(305, 272);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lstVwAgeItems);
            this.Controls.Add(this.cbxSelectAll);
            this.MaximumSize = new System.Drawing.Size(313, 306);
            this.MinimumSize = new System.Drawing.Size(313, 306);
            this.Name = "ClassAgePercentageRpt";
            this.Text = "學生年齡統計";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.CheckBoxX cbxSelectAll;
        private DevComponents.DotNetBar.Controls.ListViewEx lstVwAgeItems;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.ButtonX btnExit;
    }
}