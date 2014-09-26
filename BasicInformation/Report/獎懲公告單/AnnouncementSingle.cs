using Aspose.Words;
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

namespace BasicInformation
{
    public partial class AnnouncementSingle : BaseForm
    {
        //報表記錄檔
        private string AnnouncementSingleConfig = "BasicInformation.AnnouncementSingle.cs";

        //報表記錄檔
        private string UserConfig = "BasicInformation.AnnouncementSingle.User.Config";

        K12.Data.Configuration.ConfigData cd { get; set; }

        //移動使用
        private Run _run;

        BackgroundWorker BGW = new BackgroundWorker();

        bool PointDateType = true; //列印日期為發生日期
        bool PointMerit = true; //列印獎勵資料
        bool PointDemerit = true; //列印懲戒資料

        DateTime Start = new DateTime();
        DateTime End = new DateTime();

        Dictionary<string, StudentInfo> StudentInfoDic { get; set; }

        public AnnouncementSingle()
        {
            InitializeComponent();
        }

        private void AnnouncementSingle_Load(object sender, EventArgs e)
        {

            PointDateType = true; //列印日期為發生日期
            PointMerit = true; //列印獎勵資料
            PointDemerit = true; //列印懲戒資料

            Start = DateTime.Today.AddDays(-7);
            End = DateTime.Today;

            DateTime dt = new DateTime();
            bool point = true;

            cd = K12.Data.School.Configuration[UserConfig];

            if (DateTime.TryParse(cd["DateTimeStart"], out dt))
            {
                Start = DateTime.Parse(cd["DateTimeStart"]);
            }

            if (DateTime.TryParse(cd["DateTimeEnd"], out dt))
            {
                End = DateTime.Parse(cd["DateTimeEnd"]);
            }

            if (bool.TryParse(cd["PointDateType"], out point))
            {
                PointDateType = bool.Parse(cd["PointDateType"]);
            }

            if (bool.TryParse(cd["PointMerit"], out point))
            {
                PointMerit = bool.Parse(cd["PointMerit"]);
            }

            if (bool.TryParse(cd["PointDemerit"], out point))
            {
                PointDemerit = bool.Parse(cd["PointDemerit"]);
            }

            //記錄使用者上次所選擇的列印日期
            //如未開啟過,使用預設的當日 & 當日-7
            dateTimeInput1.Value = Start;
            dateTimeInput2.Value = End;

            BGW.DoWork += BGW_DoWork;
            BGW.RunWorkerCompleted += BGW_RunWorkerCompleted;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            if (!BGW.IsBusy)
            {
                PointDateType = checkBoxX3.Checked;
                PointMerit = checkBoxX1.Checked;
                PointDemerit = checkBoxX2.Checked;

                Start = dateTimeInput1.Value;
                End = dateTimeInput2.Value;

                btnStart.Enabled = false;

                BGW.RunWorkerAsync();

            }
            else
            {
                MsgBox.Show("系統忙碌中,請稍後!!");
            }
        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            cd.Save();

            #region 範本

            //整理取得報表範本
            Campus.Report.ReportConfiguration ConfigurationInCadre = new Campus.Report.ReportConfiguration(AnnouncementSingleConfig);
            Aspose.Words.Document Template;

            if (ConfigurationInCadre.Template == null)
            {
                //如果範本為空,則建立一個預設範本
                Campus.Report.ReportConfiguration ConfigurationInCadre_1 = new Campus.Report.ReportConfiguration(AnnouncementSingleConfig);
                ConfigurationInCadre_1.Template = new Campus.Report.ReportTemplate(Properties.Resources.實驗中學_獎懲公告單, Campus.Report.TemplateType.Word);
                Template = ConfigurationInCadre_1.Template.ToDocument();
            }
            else
            {
                //如果已有範本,則取得樣板
                Template = ConfigurationInCadre.Template.ToDocument();
            }

            #endregion

            //取得一般生清單
            StudentInfoDic = GetStudent();

            //取得日期區間內的所有獎懲記錄(含日期類型判斷)
            List<DisciplineRecord> DisciplineList = GetDiscipline(StudentInfoDic.Keys.ToList());

            foreach (DisciplineRecord each in DisciplineList)
            {
                if (StudentInfoDic.ContainsKey(each.RefStudentID))
                {
                    StudentInfoDic[each.RefStudentID].DISList.Add(each);
                }
            }

            //填資料部份
            DataTable table = new DataTable();

            table.Columns.Add("學校中文名稱");
            table.Columns.Add("學校英文名稱");
            table.Columns.Add("日期區間");
            table.Columns.Add("列印日期");
            table.Columns.Add("資料");

            DataRow row = table.NewRow();
            row["學校中文名稱"] = K12.Data.School.ChineseName;
            row["學校英文名稱"] = K12.Data.School.EnglishName;

            string PrintDay = string.Format("{0}　{1},{2}", tool.GetMonth(DateTime.Today.Month), tool.GetDay(DateTime.Today.Day), DateTime.Today.Year.ToString());
            row["列印日期"] = PrintDay;

            string Print1 = string.Format("{0}　{1},{2} ", tool.GetMonth(dateTimeInput1.Value.Month), tool.GetDay(dateTimeInput1.Value.Day), dateTimeInput1.Value.Year.ToString());
            string Print2 = string.Format("　{0}　{1},{2}", tool.GetMonth(dateTimeInput2.Value.Month), tool.GetDay(dateTimeInput2.Value.Day), dateTimeInput2.Value.Year.ToString());
            row["日期區間"] = Print1 + " To " + Print2;

            row["資料"] = "";

            table.Rows.Add(row);

            Document PageOne = (Document)Template.Clone(true);
            PageOne.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
            PageOne.MailMerge.Execute(table);
            PageOne.MailMerge.DeleteFields();
            e.Result = PageOne;
        }

        void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            if (e.FieldName == "資料")
            {
                int count = 0;
                foreach (StudentInfo stud in StudentInfoDic.Values)
                {
                    foreach (K12.Data.DisciplineRecord record in stud.DISList)
                    {
                        if (record.MeritFlag == "1" && PointMerit)
                        {
                            count++;
                        }
                        else if (record.MeritFlag == "0" && PointDemerit)
                        {
                            if (record.Cleared != "是")
                            {
                                count++;
                            }
                        }
                    }
                }

                Document PageOne = e.Document; // (Document)_template.Clone(true);
                _run = new Run(PageOne);
                DocumentBuilder builder = new DocumentBuilder(PageOne);
                builder.MoveToMergeField("資料");
                ////取得目前Cell
                Cell cell = (Cell)builder.CurrentParagraph.ParentNode;
                ////取得目前Row
                Row row = (Row)builder.CurrentParagraph.ParentNode.ParentNode;

                //建立新行
                for (int x = 1; x < count; x++)
                {
                    (cell.ParentNode.ParentNode as Table).InsertAfter(row.Clone(true), cell.ParentNode);
                }

                foreach (StudentInfo stud in StudentInfoDic.Values)
                {
                    foreach (K12.Data.DisciplineRecord record in stud.DISList)
                    {
                        if (record.MeritFlag == "1" && PointMerit)
                        {
                            #region 獎勵處理

                            List<string> list = new List<string>();
                            list.Add(stud.Class_Nmae);
                            list.Add(stud.Name);
                            list.Add(stud.EngLish_Name);
                            list.Add(record.Reason);
                            list.Add(GetMeMerit(record));

                            foreach (string listEach in list)
                            {
                                Write(cell, listEach);

                                if (cell.NextSibling != null) //是否最後一格
                                    cell = cell.NextSibling as Cell; //下一格
                            }

                            Row Nextrow = cell.ParentRow.NextSibling as Row; //取得下一個Row
                            if (Nextrow == null)
                                break;
                            cell = Nextrow.FirstCell; //第一格Cell  

                            #endregion

                        }
                        else if (record.MeritFlag == "0" && PointDemerit)
                        {
                            #region 懲戒處理
                            if (record.Cleared != "是")
                            {
                                List<string> list = new List<string>();
                                list.Add(stud.Class_Nmae);
                                list.Add(stud.Name);
                                list.Add(stud.EngLish_Name);
                                list.Add(record.Reason);
                                list.Add(GetMeDemerit(record));

                                foreach (string listEach in list)
                                {
                                    Write(cell, listEach);

                                    if (cell.NextSibling != null) //是否最後一格
                                        cell = cell.NextSibling as Cell; //下一格
                                }

                                Row Nextrow = cell.ParentRow.NextSibling as Row; //取得下一個Row
                                if (Nextrow == null)
                                    break;
                                cell = Nextrow.FirstCell; //第一格Cell  
                            }

                            #endregion
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 給我獎勵字串
        /// </summary>
        private string GetMeMerit(K12.Data.DisciplineRecord record)
        {
            StringBuilder sb = new StringBuilder();
            if (record.MeritA.HasValue)
            {
                if (record.MeritA.Value != 0)
                {
                    sb.Append("大功" + record.MeritA.Value.ToString() + "次");
                }
            }

            if (record.MeritB.HasValue)
            {
                if (record.MeritB.Value != 0)
                {
                    sb.Append("小功" + record.MeritB.Value.ToString() + "次");
                }
            }

            if (record.MeritC.HasValue)
            {
                if (record.MeritC.Value != 0)
                {
                    sb.Append("嘉獎" + record.MeritC.Value.ToString() + "次");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 給我懲戒字串
        /// </summary>
        private string GetMeDemerit(K12.Data.DisciplineRecord record)
        {
            StringBuilder sb = new StringBuilder();
            if (record.DemeritA.HasValue)
            {
                if (record.DemeritA.Value != 0)
                {
                    sb.Append("大過" + record.DemeritA.Value.ToString() + "次");
                }
            }

            if (record.DemeritB.HasValue)
            {
                if (record.DemeritB.Value != 0)
                {
                    sb.Append("小過" + record.DemeritB.Value.ToString() + "次");
                }
            }

            if (record.DemeritC.HasValue)
            {
                if (record.DemeritC.Value != 0)
                {
                    sb.Append("警告" + record.DemeritC.Value.ToString() + "次");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 依據學生ID+日期,取得學生獎懲記錄
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        private List<DisciplineRecord> GetDiscipline(List<string> StudentIDList)
        {
            List<DisciplineRecord> list = new List<DisciplineRecord>();

            //依據什麼日期進行資料取得
            if (PointDateType)
                list = K12.Data.Discipline.SelectByOccurDate(StudentIDList, dateTimeInput1.Value, dateTimeInput2.Value);
            else
                list = K12.Data.Discipline.SelectByRegisterDate(StudentIDList, dateTimeInput1.Value, dateTimeInput2.Value);

            return list;

        }

        private Dictionary<string, StudentInfo> GetStudent()
        {
            StringBuilder SQL_sb = new StringBuilder();
            SQL_sb.Append("select student.id,student.name,student.english_name,student.seat_no,class.class_name from student ");
            SQL_sb.Append("left join class on student.ref_class_id=class.id ");
            SQL_sb.Append("where student.status='1' or student.status='2' ");
            SQL_sb.Append("order by class.grade_year,class.display_order,class.class_name,student.seat_no");

            //取得一般生清單
            Dictionary<string, StudentInfo> dic = new Dictionary<string, StudentInfo>();
            DataTable dt = tool._Q.Select(SQL_sb.ToString());
            foreach (DataRow row in dt.Rows)
            {
                StudentInfo info = new StudentInfo(row);
                if (!dic.ContainsKey(info.ref_Student_id))
                {
                    dic.Add(info.ref_Student_id, info);
                }
            }

            return dic;
        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStart.Enabled = true;

            if (e.Cancelled)
            {
                MsgBox.Show("作業已被中止!!");
            }
            else
            {
                if (e.Error == null)
                {
                    Document inResult = (Document)e.Result;

                    try
                    {
                        SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

                        SaveFileDialog1.Filter = "Word (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                        SaveFileDialog1.FileName = "獎懲公告單(雙語部)";

                        if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            inResult.Save(SaveFileDialog1.FileName);
                            Process.Start(SaveFileDialog1.FileName);
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
                        return;
                    }

                    this.Close();
                }
                else
                {
                    MsgBox.Show("列印資料發生錯誤\n" + e.Error.Message);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //取得設定檔
            Campus.Report.ReportConfiguration ConfigurationInCadre = new Campus.Report.ReportConfiguration(AnnouncementSingleConfig);
            //畫面內容(範本內容,預設樣式
            Campus.Report.TemplateSettingForm TemplateForm;
            if (ConfigurationInCadre.Template != null)
            {
                TemplateForm = new Campus.Report.TemplateSettingForm(ConfigurationInCadre.Template, new Campus.Report.ReportTemplate(Properties.Resources.實驗中學_獎懲公告單, Campus.Report.TemplateType.Word));
            }
            else
            {
                ConfigurationInCadre.Template = new Campus.Report.ReportTemplate(Properties.Resources.實驗中學_獎懲公告單, Campus.Report.TemplateType.Word);
                TemplateForm = new Campus.Report.TemplateSettingForm(ConfigurationInCadre.Template, new Campus.Report.ReportTemplate(Properties.Resources.實驗中學_獎懲公告單, Campus.Report.TemplateType.Word));
            }

            //預設名稱
            TemplateForm.DefaultFileName = "獎懲公告單(範本)";
            //如果回傳為OK
            if (TemplateForm.ShowDialog() == DialogResult.OK)
            {
                //設定後樣試,回傳
                ConfigurationInCadre.Template = TemplateForm.Template;
                //儲存
                ConfigurationInCadre.Save();
            }
        }

        /// <summary>
        /// 寫入資料
        /// </summary>
        private void Write(Cell cell, string text)
        {
            if (cell.FirstParagraph == null)
                cell.Paragraphs.Add(new Paragraph(cell.Document));
            cell.FirstParagraph.Runs.Clear();
            _run.Text = text;
            _run.Font.Size = 12;
            _run.Font.Name = "標楷體";
            cell.FirstParagraph.Runs.Add(_run.Clone(true));
        }

        private void dateTimeInput1_ValueChanged(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void dateTimeInput2_ValueChanged(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void checkBoxX3_CheckedChanged(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void checkBoxX4_CheckedChanged(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void checkBoxX2_CheckedChanged(object sender, EventArgs e)
        {
            SaveConfig();
        }

        public void SaveConfig()
        {
            cd = K12.Data.School.Configuration[UserConfig];

            cd["DateTimeStart"] = dateTimeInput1.Value.ToShortDateString();
            cd["DateTimeEnd"] = dateTimeInput2.Value.ToShortDateString();
            cd["PointDateType"] = checkBoxX3.Checked.ToString();
            cd["PointMerit"] = checkBoxX1.Checked.ToString();
            cd["PointDemerit"] = checkBoxX2.Checked.ToString();

        }

        private void lbTempAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "獎懲公告單_功能變數表.doc";
            sfd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.獎懲公告單_功能變數總表, 0, Properties.Resources.獎懲公告單_功能變數總表.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }

    class StudentInfo
    {
        public StudentInfo(DataRow row)
        {
            ref_Student_id = "" + row["id"];
            Name = "" + row["name"];
            EngLish_Name = "" + row["english_name"];
            Class_Nmae = "" + row["class_name"];
            Seat_NO = "" + row["seat_no"];

            DISList = new List<K12.Data.DisciplineRecord>();
        }

        /// <summary>
        /// 學生系統編號
        /// </summary>
        public string ref_Student_id { get; set; }

        /// <summary>
        /// 學生中文姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 座號
        /// </summary>
        public string Seat_NO { get; set; }

        /// <summary>
        /// 英文姓名
        /// </summary>
        public string EngLish_Name { get; set; }

        /// <summary>
        /// 班級名稱
        /// </summary>
        public string Class_Nmae { get; set; }

        /// <summary>
        /// 學生獎懲記錄
        /// </summary>
        public List<DisciplineRecord> DISList { get; set; }
    }
}
