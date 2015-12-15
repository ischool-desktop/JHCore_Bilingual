using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;
using Aspose.Cells;
using System.IO;

namespace JHSchool.Permrec.ClassExtendControls
{
    public partial class PrintClassDomainSemesterScore : BaseForm
    {
        private const int MaxClassStudentCount = 50;

        public PrintClassDomainSemesterScore()
        {
            InitializeComponent();
        }

        private void PrintClassDomainSemesterScore_Load(object sender, EventArgs e)
        {
            string currentSchoolYear = School.DefaultSchoolYear;
            for (int i = 3; i > 0; i--)
                this.cboSchoolYear.Items.Add((int.Parse(currentSchoolYear) - i + 1)).ToString();

            this.cboSchoolYear.SelectedIndex = 2;


            string currentSemester = School.DefaultSemester;
            this.cboSemester.Items.Add("1");
            this.cboSemester.Items.Add("2");
            this.cboSemester.SelectedText = currentSemester;
            this.cboSemester.SelectedIndex = 1;
        }

        private void SaveReport(string inputReportName, Workbook inputWorkbook)
        {
            string reportName = inputReportName;

            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".xls");

            Workbook wb = inputWorkbook;

            if (File.Exists(path))
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                    if (!File.Exists(newPath))
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            try
            {
                wb.Save(path, FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".xls";
                sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd.FileName, FileFormatType.Excel2003);

                    }
                    catch
                    {
                        Framework.MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }


        //印出所選班級的領域成績
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Workbook book = new Workbook();
            book.Worksheets.Clear();
            book.Open(new MemoryStream(JHSchool.Permrec.Properties.Resources.班級學期領域成績一覽表));

            string schoolYear = this.cboSchoolYear.Text;
            string semester = this.cboSemester.Text;

            List<K12.Data.ClassRecord> selectedClasses = K12.Data.Class.SelectByIDs(Class.Instance.SelectedKeys);

            if (selectedClasses.Count > 0)
            {
                this.progressBarX1.Maximum = selectedClasses.Count;
                this.progressBarX1.Minimum = 0;

                int RowIndex = 0;

                foreach (K12.Data.ClassRecord cls in selectedClasses)
                {
                    //1. 填入全班學生座號姓名
                    List<string> studentIDs = new List<string>();
                    foreach (K12.Data.StudentRecord stud in cls.Students)
                        studentIDs.Add(stud.ID);

                    //2. 取得全班學生的領域學期成績
                    Dictionary<string, string> studDomainScores = new Dictionary<string, string>();    //一筆記錄代表一位學生在一個領域的學期成績，便於列印時比對。
                    Dictionary<string, decimal?> DomainScores = new Dictionary<string, decimal?>();
                    Dictionary<string, decimal?> FinalDomainScores = new Dictionary<string, decimal?>();

                    List<JHSchool.Data.JHSemesterScoreRecord> records = JHSchool.Data.JHSemesterScore.SelectBySchoolYearAndSemester(studentIDs, Framework.Int.ParseAllowNull(schoolYear), Framework.Int.ParseAllowNull(semester));

                    foreach (JHSchool.Data.JHSemesterScoreRecord semscore in records)
                    {
                        foreach (string key in semscore.Domains.Keys)
                        {
                            studDomainScores.Add(semscore.RefStudentID + key, semscore.Domains[key].Score.ToString());

                            if (!DomainScores.ContainsKey(key))
                                DomainScores.Add(key, semscore.Domains[key].Score);
                            else
                                DomainScores[key] += semscore.Domains[key].Score;

                        }

                        studDomainScores.Add(semscore.RefStudentID + "學習領域", semscore.LearnDomainScore.ToString());
                        studDomainScores.Add(semscore.RefStudentID + "課程學習", semscore.CourseLearnScore.ToString());
                    }

                    foreach (string key in DomainScores.Keys)
                        FinalDomainScores.Add(key , DomainScores[key] / records.Count);


                    //3. 根據領域列印全班成績

                    Aspose.Cells.Range ClassScoreRange = ClassScoreTransferformer.Execute(RowIndex, schoolYear, semester, cls, studDomainScores, FinalDomainScores);

                    book.Worksheets[0].Cells.CreateRange(RowIndex * MaxClassStudentCount, MaxClassStudentCount, false).Copy(ClassScoreRange);

                    book.Worksheets[0].HPageBreaks.Add((RowIndex + 1) * MaxClassStudentCount, 0);

                    RowIndex++;

                    progressBarX1.Value++;
                }


                SaveReport("班級學期領域成績一覽表", book);
            }
        }
    }
}