﻿namespace JHSchool.Permrec.ClassExtendControls
{
    partial class ClassStudentItem
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
            this.lvwStudent = new SmartSchool.Common.ListViewEX();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lvwStudent
            // 
            // 
            // 
            // 
            this.lvwStudent.Border.Class = "ListViewBorder";
            this.lvwStudent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvwStudent.FullRowSelect = true;
            this.lvwStudent.Location = new System.Drawing.Point(26, 15);
            this.lvwStudent.MultiSelect = false;
            this.lvwStudent.Name = "lvwStudent";
            this.lvwStudent.Size = new System.Drawing.Size(498, 194);
            this.lvwStudent.TabIndex = 3;
            this.lvwStudent.UseCompatibleStateImageBehavior = false;
            this.lvwStudent.View = System.Windows.Forms.View.Details;
            this.lvwStudent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwStudent_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "座號";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "姓名";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "學號";
            this.columnHeader3.Width = 90;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "戶籍電話";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "聯絡電話";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "狀態";
            // 
            // ClassStudentItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvwStudent);
            this.Name = "ClassStudentItem";
            this.Size = new System.Drawing.Size(550, 225);
            this.ResumeLayout(false);

        }

        #endregion

        private SmartSchool.Common.ListViewEX lvwStudent;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}
