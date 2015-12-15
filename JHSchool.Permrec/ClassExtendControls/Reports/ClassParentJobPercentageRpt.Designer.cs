namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    partial class ClassParentJobPercentageRpt
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
            this.lstVwJobItems = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.cbxSelectAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbxParentType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(107, 244);
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
            this.btnExport.Location = new System.Drawing.Point(13, 244);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "產生資料";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lstVwJobItems
            // 
            // 
            // 
            // 
            this.lstVwJobItems.Border.Class = "ListViewBorder";
            this.lstVwJobItems.CheckBoxes = true;
            this.lstVwJobItems.Location = new System.Drawing.Point(12, 39);
            this.lstVwJobItems.Name = "lstVwJobItems";
            this.lstVwJobItems.Size = new System.Drawing.Size(170, 199);
            this.lstVwJobItems.TabIndex = 5;
            this.lstVwJobItems.UseCompatibleStateImageBehavior = false;
            this.lstVwJobItems.View = System.Windows.Forms.View.List;
            this.lstVwJobItems.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstVwJobItems_ItemChecked);
            // 
            // cbxSelectAll
            // 
            this.cbxSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.cbxSelectAll.Location = new System.Drawing.Point(13, 16);
            this.cbxSelectAll.Name = "cbxSelectAll";
            this.cbxSelectAll.Size = new System.Drawing.Size(67, 23);
            this.cbxSelectAll.TabIndex = 4;
            this.cbxSelectAll.Text = "全選";
            this.cbxSelectAll.Click += new System.EventHandler(this.cbxSelectAll_Click);
            this.cbxSelectAll.CheckValueChanged += new System.EventHandler(this.cbxSelectAll_CheckValueChanged);
            // 
            // cbxParentType
            // 
            this.cbxParentType.DisplayMember = "Text";
            this.cbxParentType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxParentType.FormattingEnabled = true;
            this.cbxParentType.ItemHeight = 19;
            this.cbxParentType.Location = new System.Drawing.Point(86, 8);
            this.cbxParentType.Name = "cbxParentType";
            this.cbxParentType.Size = new System.Drawing.Size(96, 25);
            this.cbxParentType.TabIndex = 8;
            this.cbxParentType.WatermarkText = "請選父母或監護人";
            this.cbxParentType.SelectedIndexChanged += new System.EventHandler(this.cbxParentType_SelectedIndexChanged);
            // 
            // ClassParentJobPercentageRpt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(193, 279);
            this.Controls.Add(this.cbxParentType);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lstVwJobItems);
            this.Controls.Add(this.cbxSelectAll);
            this.Name = "ClassParentJobPercentageRpt";
            this.Text = "學生父母職業比例";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.Controls.ListViewEx lstVwJobItems;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxSelectAll;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxParentType;
    }
}