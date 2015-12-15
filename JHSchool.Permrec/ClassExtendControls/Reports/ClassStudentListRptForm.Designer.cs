namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    partial class ClassStudentListRptForm
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
            this.lstSelectItem = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.intCot = new DevComponents.Editors.IntegerInput();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.intCot)).BeginInit();
            this.SuspendLayout();
            // 
            // lstSelectItem
            // 
            // 
            // 
            // 
            this.lstSelectItem.Border.Class = "ListViewBorder";
            this.lstSelectItem.CheckBoxes = true;
            this.lstSelectItem.Location = new System.Drawing.Point(12, 35);
            this.lstSelectItem.Name = "lstSelectItem";
            this.lstSelectItem.Size = new System.Drawing.Size(187, 108);
            this.lstSelectItem.TabIndex = 0;
            this.lstSelectItem.UseCompatibleStateImageBehavior = false;
            this.lstSelectItem.View = System.Windows.Forms.View.List;
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(88, 149);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(54, 23);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "列印";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // intCot
            // 
            this.intCot.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.intCot.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intCot.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intCot.Location = new System.Drawing.Point(45, 149);
            this.intCot.MaxValue = 10;
            this.intCot.MinValue = 1;
            this.intCot.Name = "intCot";
            this.intCot.Size = new System.Drawing.Size(32, 23);
            this.intCot.TabIndex = 2;
            this.intCot.Value = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "欄位內容";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(13, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "個數";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.Color.Transparent;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(145, 149);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(54, 23);
            this.buttonX1.TabIndex = 5;
            this.buttonX1.Text = "離開";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // ClassStudentListRptForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(206, 176);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.intCot);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lstSelectItem);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MaximumSize = new System.Drawing.Size(214, 210);
            this.MinimumSize = new System.Drawing.Size(153, 210);
            this.Name = "ClassStudentListRptForm";
            this.Text = "班級名條";
            this.Load += new System.EventHandler(this.ClassStudentListRptForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.intCot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ListViewEx lstSelectItem;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.Editors.IntegerInput intCot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}