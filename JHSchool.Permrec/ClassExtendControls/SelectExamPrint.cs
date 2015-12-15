using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Aspose.Cells;
using Framework;
using SmartSchool.Common;


namespace JHSchool.Permrec.ClassExtendControls
{
    public partial class SelectExamPrint : BaseForm
    {
        private bool checkedOnChange = false;

        private List<K12.Data.ClassRecord> selectedClasses = new List<K12.Data.ClassRecord>();
        private Dictionary<string, List<string>> allAssessmentSetup; //取得所有評分樣板，以及所包含的評量項目編號
        private Dictionary<string, List<K12.Data.CourseRecord>> classCourses;    //被選取的班級，以及每個班級所包含的課程

        public SelectExamPrint()
        {
            InitializeComponent();


        }


        /*
            當選取試別時： 1. 找出被選取班級的所有該學期課程中，有該試別的所有課程(科目)
         * */
        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.classCourses = new Dictionary<string, List<K12.Data.CourseRecord>>();

            checkedOnChange = true;

            //this.UseWaitCursor = true;

            listViewEx1.Items.Clear();

            //1. 根據選取的班級，找出該學期這些班級的課程，並且有這次評量的科目清單            
            List<string> subjects = new List<string>();

            List<string> selectedClassIDs = JHSchool.Class.Instance.SelectedKeys;


            foreach (K12.Data.CourseRecord cr in K12.Data.Course.SelectAll())
            {
                /*判斷班級所選的ID和班級所有的課程是否相同*/
                if (selectedClassIDs.Contains(cr.RefClassID) &&
                        isContainExam(cr.RefAssessmentSetupID, ((K12.Data.ExamRecord)cboExams.SelectedItem).ID))
                {
                    /*如果科目沒有被包含，把科目加進去*/
                    if (!subjects.Contains(cr.Subject))
                        subjects.Add(cr.Subject);

                    /*如果班級沒有包含課程，新增一個課程清單*/
                    if (!this.classCourses.ContainsKey(cr.RefClassID))
                        this.classCourses.Add(cr.RefClassID, new List<K12.Data.CourseRecord>());
                    /*把所選的課程加入*/
                    this.classCourses[cr.RefClassID].Add(cr);
                }
            }

            //將科目清單顯示在 ListView 上
            this.listViewEx1.Items.Clear();
            this.listViewEx1.CheckBoxes = true;
            this.listViewEx1.MultiSelect = true;

            this.listViewEx1.View = View.List;
            foreach (string subject in subjects)
                this.listViewEx1.Items.Add(subject);
        }

        /// <summary>
        /// 判斷指定的評分樣版是否包含該評量項目
        /// </summary>
        /// <param name="assessmentSetupID">評分樣板編號</param>
        /// <param name="examID">評量項目編號</param>
        /// <returns></returns>

        private bool isContainExam(string assessmentSetupID, string examID)
        {
            bool result = false;
            /*Dictionary*/
            if (this.allAssessmentSetup.ContainsKey(assessmentSetupID))
            {
                List<string> examIDs = this.allAssessmentSetup[assessmentSetupID];
                result = examID.Contains(examID);
            }

            return result;
        }


        /// <summary>
        /// 取得所有評分樣板及其包含的評量項目
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, List<string>> getAllAssessmentSetup()
        {
            List<K12.Data.AEIncludeRecord> records = K12.Data.AEInclude.SelectAll();

            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            foreach (K12.Data.AEIncludeRecord ae in records)
            {
                if (!result.ContainsKey(ae.RefAssessmentSetupID))
                {
                    result.Add(ae.RefAssessmentSetupID, new List<string>());
                }

                result[ae.RefAssessmentSetupID].Add(ae.RefExamID);
            }

            return result;
        }

        private void SelectExamPrint_Load(object sender, EventArgs e)
        {
            //取得所有評分樣板及其包含的評量項目
            allAssessmentSetup = getAllAssessmentSetup();

            /* 取得所有試別 */
            List<K12.Data.ExamRecord> exams = K12.Data.Exam.SelectAll();
            this.cboExams.Items.Clear();
            this.cboExams.ValueMember = "ID";
            this.cboExams.DisplayMember = "Name";
            this.cboExams.Items.AddRange(exams.ToArray());
            this.cboExams.SelectedIndex = 0;
            this.controlContainerItem1.Control = this.panelEx1;

            if (classCourses.Count == 0)
            {
                btnPrint.Enabled = false;

            }
        }

        /*  當開始列印時：
                1. 對於每一個被選取的班級
                    1.1 對於每一個被選取的科目：
                        1.1.1 如果該科目是屬於該班級的課程，就取回該課程該次考試的全班分數。
                    1.2. 對於該班的每一位學生：
                        1.2.1 填上座號姓名
                        1.2.2 對於每一被選取科目：
                            1.2.2.1    找出該學生在該課程的分數，並填上
                        1.2.3 填上總分、平均、加權總分、加權平均、及排名的 Excel 公式。
             * 
             * */

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cboExams.SelectedItem == null)

                return;

            //Create a workbook
            Workbook book = new Workbook();
            book.Worksheets.Clear();
            book.Open(new MemoryStream(JHSchool.Permrec.Properties.Resources.班級定期評量));

            Style style = book.Worksheets["template"].Cells[4, 0].Style;

            for (byte b = 0; b < book.Worksheets["template2"].Cells.MaxColumn; b++)
            {
                book.Worksheets[0].Cells.CopyColumn(book.Worksheets["template2"].Cells, b, b);
            }

            string examID = (cboExams.SelectedItem as K12.Data.ExamRecord).ID;
            string examName = (cboExams.SelectedItem as K12.Data.ExamRecord).Name;
            //0. 找出使用者選取的科目清單
            List<string> selectedSubjects = new List<string>();
            foreach (ListViewItem lvi in this.listViewEx1.CheckedItems)
            {

                selectedSubjects.Add(lvi.Text);
            }

            int rowNo = 0;
            //1. 對於每一個被選取的班級
            foreach (K12.Data.ClassRecord cls in K12.Data.Class.SelectByIDs(JHSchool.Class.Instance.SelectedKeys))
            {
                Range _Range1 = book.Worksheets["template"].Cells.CreateRange(0, 2, false);
                Range _Range2 = book.Worksheets["template"].Cells.CreateRange(2, 2, false);
                Range _Range3 = book.Worksheets["template"].Cells.CreateRange(4, 1, false);
                Range _Range4 = book.Worksheets["template"].Cells.CreateRange(5, 6, false);
                Range _Range6 = book.Worksheets["template"].Cells.CreateRange(11, 1, false);

                book.Worksheets[0].Cells.CreateRange(rowNo, 2, false).Copy(_Range1);
                book.Worksheets[0].Cells.CreateRange(rowNo + 2, 2, false).Copy(_Range2);
                book.Worksheets[0].Cells.CreateRange(rowNo + 4, 1, false).Copy(_Range3);


                //1.0 列印該班名稱及學生清單
                string className = cls.Name; //班級名稱                

                //int rowNo = 4;
                book.Worksheets[0].Cells[rowNo, 0].PutValue(className + "  班級成績單");
                book.Worksheets[0].Cells[rowNo + 1, 0].PutValue(examName);
                rowNo += 5;
                book.Worksheets[0].Cells[rowNo - 1, 0].PutValue("學分數");

                int classTopRow = rowNo;  //開始填入第一筆資料的列數

                foreach (K12.Data.StudentRecord stud in cls.Students)
                {
                    //列印學生清單
                    book.Worksheets[0].Cells[rowNo, 0].PutValue(stud.SeatNo);
                    book.Worksheets[0].Cells[rowNo, 0].Style = style;
                    book.Worksheets[0].Cells[rowNo, 1].PutValue(stud.Name);
                    book.Worksheets[0].Cells[rowNo, 1].Style = style;

                    rowNo += 1;
                }


                List<string> selectedCourseIDs = new List<string>();
                //1.1 對於該班的課程中

                if (classCourses.Count != 0)
                {
                    foreach (K12.Data.CourseRecord course in this.classCourses[cls.ID])
                    {
                        //找出符合使用者有選取科目的課程清單
                        if (selectedSubjects.Contains(course.Subject))
                        {
                            selectedCourseIDs.Add(course.ID);
                        }
                    }
                }


                //取回這些課程的所有學生，在指定評量的成績
                List<JHSchool.Data.JHSCETakeRecord> scores = JHSchool.Data.JHSCETake.SelectByCourseAndExam(selectedCourseIDs, examID);
                //將所有成績物件轉換成 <CourseID-StudentID, Score> 的Dictionary物件，以便後續查詢
                Dictionary<string, string> allScores = new Dictionary<string, string>();
                foreach (JHSchool.Data.JHSCETakeRecord score in scores)
                {
                    allScores.Add(score.RefCourseID + "-" + score.RefStudentID, score.Score.ToString());

                }

                //Dictionary<string, decimal> studentSum = new Dictionary<string, decimal>();


                int colIndex = 2;
                //列印該班每個課程的成績
                foreach (string courseID in selectedCourseIDs)
                {
                    K12.Data.CourseRecord course = K12.Data.Course.SelectByID(courseID);

                    rowNo = classTopRow;

                    book.Worksheets[0].Cells[rowNo - 3, colIndex].PutValue(course.Subject);    //科目名稱
                    book.Worksheets[0].Cells[rowNo - 1, colIndex].PutValue(course.Credit == null ? Framework.Decimal.GetString(course.Period) : Framework.Decimal.GetString(course.Credit));    //學分數

                    foreach (K12.Data.StudentRecord stud in cls.Students)
                    {
                        string score = "";
                        if (allScores.ContainsKey(courseID + "-" + stud.ID))
                            score = allScores[courseID + "-" + stud.ID];

                        if (!string.IsNullOrEmpty(score))
                        {
                            book.Worksheets[0].Cells[rowNo, colIndex].PutValue(double.Parse(score));    //填分數
                            book.Worksheets[0].Cells[rowNo, colIndex].Style = style;
                        }
                        rowNo += 1;
                    }

                    rowNo += 1;

                    book.Worksheets[0].Cells[rowNo, 0].PutValue("各科平均");
                    string colName = ((char)(65 + colIndex)).ToString();
                    string averageRangeFormula = string.Format("=AVERAGE({0}{1}:{0}{2})", colName, (classTopRow + 1).ToString(), (rowNo - 1).ToString());
                    //book.Worksheets[0].Cells[rowNo, colIndex].IsFormula = true;
                    book.Worksheets[0].Cells[rowNo, colIndex].Formula = averageRangeFormula;


                    colIndex += 1;
                }

                //填入加權平均和排名，如果使用Excel公式，會很複雜，所以在程式中算出來
                book.Worksheets[0].Cells[classTopRow - 3, colIndex].PutValue("加權平均");
                book.Worksheets[0].Cells[classTopRow - 3, colIndex + 1].PutValue("排名");
                int row = classTopRow;
                foreach (K12.Data.StudentRecord stud in cls.Students)
                {
                    int totalCredit = 0;
                    float totalWeightScore = 0f;
                    for (int col = 2; col < colIndex; col++)
                    {
                        if (book.Worksheets[0].Cells[row, col].StringValue != "")
                        {
                            int credit = Framework.Int.Parse(book.Worksheets[0].Cells[classTopRow - 1, col].StringValue);
                            totalCredit += credit;
                            totalWeightScore += book.Worksheets[0].Cells[row, col].FloatValue * credit;
                        }
                    }
                    float weightAverageScore = (totalCredit == 0) ? 0f : (totalWeightScore / totalCredit);
                    book.Worksheets[0].Cells[row, colIndex].PutValue(weightAverageScore);   //加權平均
                    book.Worksheets[0].Cells[row, colIndex].Style = style;

                    //排名，Excel公式單純，交給Excel去算。
                    string colName = ((char)(65 + colIndex)).ToString();
                    string rankFormula = string.Format("=RANK({0}{1}, ${0}${2}:${0}${3})", colName, (row + 1).ToString(), (classTopRow + 1).ToString(), (rowNo - 1).ToString());
                    book.Worksheets[0].Cells[row, colIndex + 1].Formula = rankFormula;   //排名
                    book.Worksheets[0].Cells[row, colIndex + 1].Style = style;
                    row += 1;
                }



                //加入頁尾
                book.Worksheets[0].Cells.CreateRange(rowNo + 1, 6, false).Copy(_Range4);

                book.Worksheets[0].Cells.CreateRange(rowNo + 7, 1, false).Copy(_Range6);
                book.Worksheets[0].Cells[rowNo + 7, 0].PutValue("列印日期:" + DateTime.Now.Date.ToShortDateString());
                rowNo += 8;

                book.Worksheets[0].HPageBreaks.Add(rowNo, 100);

                book.Worksheets["template"].IsVisible = false;
                book.Worksheets["template2"].IsVisible = false;


            }

            book.Worksheets.ActiveSheetIndex = 0;

            #region
            try
            {
                SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("請選擇儲存位置", 100);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                saveFileDialog.FileName = "班級考試成績單";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    book.Save(saveFileDialog.FileName);

                    if (saveFileDialog.ShowDialog() == DialogResult.Yes)
                    {
                        Process.Start(saveFileDialog.FileName);
                    }
                }
                else
                    FISCA.Presentation.Controls.MsgBox.Show("檔案尚未儲存");
            }
            catch
            {
                FISCA.Presentation.Controls.MsgBox.Show("檔案儲存錯誤,請檢查檔案是否開啟中!!");
            }

            SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("已完成");

        }
            #endregion




    }
}