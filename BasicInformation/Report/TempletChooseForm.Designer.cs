namespace BasicInformation
{
	partial class TempletChooseForm
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
			this.cbbTemp = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.btn_Print = new DevComponents.DotNetBar.ButtonX();
			this.btn_close = new DevComponents.DotNetBar.ButtonX();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
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
			this.labelX1.Location = new System.Drawing.Point(28, 20);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(42, 23);
			this.labelX1.TabIndex = 10;
			this.labelX1.Text = "樣式";
			// 
			// cbbTemp
			// 
			this.cbbTemp.DisplayMember = "Text";
			this.cbbTemp.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbTemp.FormattingEnabled = true;
			this.cbbTemp.ItemHeight = 19;
			this.cbbTemp.Location = new System.Drawing.Point(88, 20);
			this.cbbTemp.Name = "cbbTemp";
			this.cbbTemp.Size = new System.Drawing.Size(214, 25);
			this.cbbTemp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbbTemp.TabIndex = 11;
			this.cbbTemp.Text = "1~9早點名";
			// 
			// btn_Print
			// 
			this.btn_Print.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btn_Print.BackColor = System.Drawing.Color.Transparent;
			this.btn_Print.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btn_Print.Location = new System.Drawing.Point(145, 68);
			this.btn_Print.Name = "btn_Print";
			this.btn_Print.Size = new System.Drawing.Size(75, 23);
			this.btn_Print.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btn_Print.TabIndex = 12;
			this.btn_Print.Text = "列印";
			this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
			// 
			// btn_close
			// 
			this.btn_close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btn_close.BackColor = System.Drawing.Color.Transparent;
			this.btn_close.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btn_close.Location = new System.Drawing.Point(227, 67);
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new System.Drawing.Size(75, 23);
			this.btn_close.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btn_close.TabIndex = 13;
			this.btn_close.Text = "取消";
			this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel1.Location = new System.Drawing.Point(25, 74);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(73, 17);
			this.linkLabel1.TabIndex = 14;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "自訂點名單";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// TempletChooseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(315, 102);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.btn_close);
			this.Controls.Add(this.btn_Print);
			this.Controls.Add(this.cbbTemp);
			this.Controls.Add(this.labelX1);
			this.DoubleBuffered = true;
			this.Name = "TempletChooseForm";
			this.Text = "班級點名清單(雙語部)";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.Controls.ComboBoxEx cbbTemp;
		private DevComponents.DotNetBar.ButtonX btn_Print;
		private DevComponents.DotNetBar.ButtonX btn_close;
		private System.Windows.Forms.LinkLabel linkLabel1;
	}
}