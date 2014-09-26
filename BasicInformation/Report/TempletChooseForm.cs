using Aspose.Words;
using FISCA.Presentation;
using FISCA.Presentation.Controls;
using K12.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BasicInformation
{
	public partial class TempletChooseForm : BaseForm
	{
		BackgroundWorker BGW { get; set; }
		string ConfigName = "ClassPrint_Config_BasicInformation_0716";
		byte[] temp;

		int 學生多少個 = 150;
		int 日期多少天 = 30;

		string temp1 = "1~9早點名";
		string temp2 = "7~12午點名";
		string temp3 = "10~12早點名";
		string temp4 = "升旗點名";

		public TempletChooseForm()
		{
			InitializeComponent();

			cbbTemp.Items.Add(temp1);
			cbbTemp.Items.Add(temp2);
			cbbTemp.Items.Add(temp3);
			cbbTemp.Items.Add(temp4);

			BGW = new BackgroundWorker();
			BGW.RunWorkerCompleted += BGW_RunWorkerCompleted;
			BGW.DoWork += BGW_DoWork;
		}

		void BGW_DoWork(object sender, DoWorkEventArgs e)
		{
			#region 報表範本整理

			Campus.Report.ReportConfiguration ConfigurationInCadre = new Campus.Report.ReportConfiguration(ConfigName);
			Aspose.Words.Document Template;

			Template = new Document(new MemoryStream(temp));
			

			#endregion


			#region 範本再修改

			List<string> config = new List<string>();

			DataTable table = new DataTable();
			table.Columns.Add("學校名稱");
			table.Columns.Add("學校英文名稱");
			table.Columns.Add("班級名稱");
			table.Columns.Add("班導師");
			table.Columns.Add("學年度");
			table.Columns.Add("學期");

			table.Columns.Add("列印日期");
			table.Columns.Add("上課開始");
			table.Columns.Add("上課結束");
			table.Columns.Add("人數");

			for (int x = 1; x <= 日期多少天; x++)
			{
				table.Columns.Add(string.Format("日期{0}", x));
				table.Columns.Add(string.Format("日期{0}星期", x));
			}

			for (int x = 1; x <= 學生多少個; x++)
			{
				table.Columns.Add(string.Format("座號{0}", x));
			}

			for (int x = 1; x <= 學生多少個; x++)
			{
				table.Columns.Add(string.Format("學號{0}", x));
			}

			for (int x = 1; x <= 學生多少個; x++)
			{
				table.Columns.Add(string.Format("姓名{0}", x));
			}

			for (int x = 1; x <= 學生多少個; x++)
			{
				table.Columns.Add(string.Format("英文姓名{0}", x));
			}

			for (int x = 1; x <= 學生多少個; x++)
			{
				table.Columns.Add(string.Format("英文別名{0}", x));
			}

			for (int x = 1; x <= 學生多少個; x++)
			{
				table.Columns.Add(string.Format("性別{0}", x));
			}

			#endregion

			List<string> StudentIDList = new List<string>();
			List<K12.Data.ClassRecord> crlt = K12.Data.Class.SelectByIDs(K12.Presentation.NLDPanels.Class.SelectedSource);
            crlt.Sort(CommonMethods.ClassComparer);
            foreach (K12.Data.ClassRecord cr in crlt)
			{
				foreach (StudentRecord obj in cr.Students)
				{
					if (!StudentIDList.Contains(obj.ID))
					{
						StudentIDList.Add(obj.ID);
					}
				}
			}

			//雙語部 - 英文別名欄位
			//學生ID : 英文別名
			Dictionary<string, string> StudentEXTDic = tool.GetStudentEXT(StudentIDList);


			foreach (K12.Data.ClassRecord cr in crlt)
			{
				DataRow row = table.NewRow();
				row["學校名稱"] = K12.Data.School.ChineseName;
				row["學校英文名稱"] = K12.Data.School.EnglishName;
				row["班級名稱"] = cr.Name;
				row["學年度"] = School.DefaultSchoolYear;
				row["學期"] = School.DefaultSemester;
				row["班導師"] = cr.Teacher != null ? cr.Teacher.Name : "";

				row["列印日期"] = DateTime.Today.ToShortDateString();



				int y = 1;
				foreach (StudentRecord obj in cr.Students)
				{
					if (!(obj.Status == StudentRecord.StudentStatus.一般 || obj.Status == StudentRecord.StudentStatus.延修))
						continue;

					if (y <= 學生多少個)
					{
						row[string.Format("座號{0}", y)] = obj.SeatNo.HasValue ? obj.SeatNo.Value.ToString() : "";
						row[string.Format("學號{0}", y)] = obj.StudentNumber;
						row[string.Format("姓名{0}", y)] = obj.Name;
						row[string.Format("英文姓名{0}", y)] = obj.EnglishName;
						row[string.Format("性別{0}", y)] = obj.Gender;

						if (StudentEXTDic.ContainsKey(obj.ID))
						{
							row[string.Format("英文別名{0}", y)] = StudentEXTDic[obj.ID];
						}

						y++;

					}
				}
				row["人數"] = y - 1;
				table.Rows.Add(row);
			}

			Document PageOne = (Document)Template.Clone(true);
			PageOne.MailMerge.Execute(table);
			e.Result = PageOne;

		}

		void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.Text = "班級點名單(雙語部)";
			if (!e.Cancelled)
			{
				if (e.Error == null)
				{
					SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
					SaveFileDialog1.Filter = "Word (*.doc)|*.doc|所有檔案 (*.*)|*.*";
					SaveFileDialog1.FileName = "班級點名單(雙語部)";

					//資料
					try
					{
						if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
						{
							Aspose.Words.Document inResult = (Aspose.Words.Document)e.Result;

							inResult.Save(SaveFileDialog1.FileName);
							Process.Start(SaveFileDialog1.FileName);
							MotherForm.SetStatusBarMessage("班級點名單(雙語部),列印完成!!");
						}
						else
						{
							FISCA.Presentation.Controls.MsgBox.Show("檔案未儲存");
							return;
						}
					}
					catch
					{
						FISCA.Presentation.Controls.MsgBox.Show("檔案儲存錯誤,請檢查檔案是否開啟中!!");
						MotherForm.SetStatusBarMessage("檔案儲存錯誤,請檢查檔案是否開啟中!!");
					}
				}
				else
				{
					MsgBox.Show("列印發生錯誤..\n" + e.Error.Message);
				}
			}
			else
			{
				MsgBox.Show("作業已取消");
			}
		}

		private void btn_Print_Click(object sender, EventArgs e)
		{
			if (cbbTemp.Text == temp1)
			{
				temp = Properties.Resources.班級點名單_1_9早點名範本;
				BGW.RunWorkerAsync();
			}

			if (cbbTemp.Text == temp2)
			{
				temp = Properties.Resources.班級點名單_7_12午點名範本;
				BGW.RunWorkerAsync();
			}

			if (cbbTemp.Text == temp3)
			{
				temp = Properties.Resources.班級點名單_10_12早點名範本;
				BGW.RunWorkerAsync();
			}

			if (cbbTemp.Text == temp4)
			{
				temp = Properties.Resources.班級點名單_升旗點名範本;
				BGW.RunWorkerAsync();
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ClubPointForm cpf = new ClubPointForm();
			cpf.ShowDialog();
		}

		private void btn_close_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
