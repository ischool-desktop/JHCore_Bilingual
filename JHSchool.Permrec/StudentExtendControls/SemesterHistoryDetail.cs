using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Framework;
using JHSchool.Data;
using FCode = Framework.Security.FeatureCodeAttribute;

namespace JHSchool.Permrec.StudentExtendControls
{
    [FCode("JHSchool.Student.Detail0010", "學期歷程")]
    public partial class SemesterHistoryDetail : FISCA.Presentation.DetailContent
    {
        
        private ChangeListener ChangeManager = new ChangeListener();
        private bool isBGBusy = false;
        private BackgroundWorker BGWorker;
        private JHSchool.Data.JHSemesterHistoryRecord _SemesterHistoryRec;
        private PermRecLogProcess prlp;

        public SemesterHistoryDetail()
        {
            InitializeComponent();

            

            if (User.Acl["JHSchool.Student.Detail0010"].Viewable == false && User.Acl["JHSchool.Student.Detail0010"].Editable == false)
                return;

            // 權限判斷，只能檢視
            if (User.Acl["JHSchool.Student.Detail0010"].Viewable)
                dataGridViewX1.Enabled = false;

            if (User.Acl["JHSchool.Student.Detail0010"].Editable)
                dataGridViewX1.Enabled = true;

            BGWorker = new BackgroundWorker();
            BGWorker.DoWork += new DoWorkEventHandler(BGWorker_DoWork);
            BGWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWorker_RunWorkerCompleted);

            ChangeManager.Add(new DataGridViewSource(dataGridViewX1));
            ChangeManager.StatusChanged += delegate(object sender, ChangeEventArgs e)
            {
                this.CancelButtonVisible = (e.Status == ValueStatus.Dirty);
                this.SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            };

            JHSemesterHistory.AfterUpdate += new EventHandler<K12.Data.DataChangedEventArgs>(JHSemesterHistory_AfterUpdate);
            prlp = new PermRecLogProcess();


            Disposed += new EventHandler(SemesterHistoryDetail_Disposed);
            
        }

        private void SemesterHistoryDetail_Disposed(object sender, EventArgs e)
        {
            JHSemesterHistory.AfterUpdate -= new EventHandler<K12.Data.DataChangedEventArgs>(JHSemesterHistory_AfterUpdate);
        }

        void JHSemesterHistory_AfterUpdate(object sender, K12.Data.DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, K12.Data.DataChangedEventArgs>(JHSemesterHistory_AfterUpdate), sender, e);
            }
            else
            {
                if (PrimaryKey != "")
                {
                    if (!BGWorker.IsBusy)
                        BGWorker.RunWorkerAsync();
                }
            }
        }


        private void BindDataToDataGridView()
        {
            try
            {
            ChangeManager.SuspendListen();
            dataGridViewX1.Rows.Clear();
            foreach (K12.Data.SemesterHistoryItem shi in _SemesterHistoryRec.SemesterHistoryItems)
            {
                string SchoolDayCount = "", seatNo = "";
                if (shi.SeatNo.HasValue)
                    seatNo = shi.SeatNo.Value + "";
                if (shi.SchoolDayCount.HasValue)
                    SchoolDayCount = shi.SchoolDayCount.Value + "";

                dataGridViewX1.Rows.Add(shi.SchoolYear, shi.Semester, shi.GradeYear, shi.ClassName, seatNo, shi.Teacher, SchoolDayCount);

                // 這段在記log
                string logIdxStr = shi.SchoolYear + "" + shi.Semester + "_";
                prlp.SetBeforeSaveText(logIdxStr + "學年度", shi.SchoolYear + "");
                prlp.SetBeforeSaveText(logIdxStr + "學期", shi.Semester + "");
                prlp.SetBeforeSaveText(logIdxStr + "年級", shi.GradeYear + "");
                prlp.SetBeforeSaveText(logIdxStr + "班級", shi.ClassName);
                prlp.SetBeforeSaveText(logIdxStr + "座號", seatNo);
                prlp.SetBeforeSaveText(logIdxStr + "班導師", shi.Teacher);
                prlp.SetBeforeSaveText(logIdxStr + "上課天數", SchoolDayCount);

            }


            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;
            this.ContentValidated = true;

            ChangeManager.Reset();
            ChangeManager.ResumeListen();
            this.Loading = false;
            }
             catch (Exception ex)
            {
                MessageBox.Show (ex.Message ); 
            }
        }

        void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isBGBusy)
            {
                isBGBusy = false;
                BGWorker.RunWorkerAsync();
                return;
            }

            BindDataToDataGridView();
        }

        void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _SemesterHistoryRec = JHSchool.Data.JHSemesterHistory.SelectByStudentID(PrimaryKey);
        }

        private void SemesterHistoryDetail_PrimaryKeyChanged(object sender, EventArgs e)
        {
            this.Loading = true;
            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;

            if (BGWorker.IsBusy)
                isBGBusy = true;
            else
                BGWorker.RunWorkerAsync();
        }

        private void dataGridViewX1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dataGridViewX1.EndEdit();
            bool validated = true;
            List<string> checkSchoolSemester = new List<string>();

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (row.IsNewRow)
                    continue;
                int i = 0;
                #region 驗學年度學期年級
                for (int j = 0; j < 3; j++)
                {
                    if (!int.TryParse("" + row.Cells[j].Value, out i))
                    {
                        row.Cells[j].ErrorText = "請輸入數字。";
                        dataGridViewX1.UpdateCellErrorText(j, row.Index);
                        validated = false;
                    }
                    else
                    {
                        row.Cells[j].ErrorText = "";
                        dataGridViewX1.UpdateCellErrorText(j, row.Index);
                    }
                }
                #endregion
                #region 驗座號

                if ("" + row.Cells[4].Value != "" && !int.TryParse("" + row.Cells[4].Value, out i))
                {
                    row.Cells[4].ErrorText = "請輸入空白或數字。";
                    dataGridViewX1.UpdateCellErrorText(4, row.Index);
                    validated = false;

                }
                else
                {
                    row.Cells[4].ErrorText = "";
                    dataGridViewX1.UpdateCellErrorText(4, row.Index);
                }
                #endregion

                if ("" + row.Cells[colSchoolDayCount.Index].Value != "" && !int.TryParse("" + row.Cells[colSchoolDayCount.Index].Value, out i))
                {
                    row.Cells[colSchoolDayCount.Index].ErrorText = "請輸入空白或數字。";
                    dataGridViewX1.UpdateCellErrorText(colSchoolDayCount.Index, row.Index);
                    validated = false;

                }
                else
                {
                    row.Cells[colSchoolDayCount.Index].ErrorText = "";
                    dataGridViewX1.UpdateCellErrorText(colSchoolDayCount.Index, row.Index);
                }
                if (validated)
                {
                    string checkStr = row.Cells[colSchoolYear.Index].Value + "" + row.Cells[colSemester.Index].Value;
                    if (checkSchoolSemester.Contains(checkStr))
                    {
                        row.Cells[colSemester.Index].ErrorText = "學年度學期不能重複";
                        dataGridViewX1.UpdateCellErrorText(colSemester.Index, row.Index);
                    }
                    else
                    {
                        row.Cells[colSemester.Index].ErrorText = "";
                        dataGridViewX1.UpdateCellErrorText(colSemester.Index, row.Index);
                    }
                    checkSchoolSemester.Add(checkStr);
                }
            }
            dataGridViewX1.BeginEdit(false);

            this.ContentValidated = validated;
            if (validated)
            {
                this.SaveButtonVisible = true;
                this.CancelButtonVisible = true;
            }
            else
            {
                this.SaveButtonVisible = true;
                this.CancelButtonVisible = true;

            }
        }

        // save
        private void SemesterHistoryDetail_SaveButtonClick(object sender, EventArgs e)
        {
                // 資料檢查
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    if (row.IsNewRow) continue;
                    foreach (DataGridViewCell cell in row.Cells)
                        if (cell.ErrorText != "")
                        {
                            MsgBox.Show("資料有疑問無法儲存,請檢查標紅色儲存格.", "儲存失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                }

                JHSchool.Data.JHSemesterHistoryRecord updateSemeHsitoryRec = new JHSchool.Data.JHSemesterHistoryRecord();

                updateSemeHsitoryRec.RefStudentID = PrimaryKey;

                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    int SchoolYear = 0, Semester = 0, GradeYear = 0, SchoolDayCount = 0, SeatNo = 0;

                    if (row.IsNewRow) continue;
                    K12.Data.SemesterHistoryItem shi = new K12.Data.SemesterHistoryItem();

                    if (row.Cells[colSchoolYear.Index] != null)
                        int.TryParse("" + row.Cells[colSchoolYear.Index].Value, out SchoolYear);

                    if (row.Cells[colSemester.Index] != null)
                        int.TryParse("" + row.Cells[colSemester.Index].Value, out Semester);

                    if (row.Cells[colGradeYear.Index] != null)
                        int.TryParse("" + row.Cells[colGradeYear.Index].Value, out GradeYear);

                    if (row.Cells[colSchoolDayCount.Index] != null)
                        int.TryParse("" + row.Cells[colSchoolDayCount.Index].Value, out SchoolDayCount);

                    if (row.Cells[colSeatNo.Index] != null)
                        int.TryParse("" + row.Cells[colSeatNo.Index].Value, out SeatNo);

                    shi.SchoolYear = SchoolYear;
                    shi.Semester = Semester;
                    shi.GradeYear = GradeYear;
                    if (row.Cells[colClassName.Index] != null)
                        shi.ClassName = row.Cells[colClassName.Index].Value + "";

                    if (SeatNo == 0)
                        shi.SeatNo = null;
                    else
                        shi.SeatNo = SeatNo;

                    if (row.Cells[colTeacherName.Index] != null)
                        shi.Teacher = row.Cells[colTeacherName.Index].Value + "";

                    if (SchoolDayCount == 0)
                        shi.SchoolDayCount = null;
                    else
                        shi.SchoolDayCount = SchoolDayCount;

                    updateSemeHsitoryRec.SemesterHistoryItems.Add(shi);

                    string logIdxStr = shi.SchoolYear + "" + shi.Semester + "_";
                    prlp.SetAfterSaveText(logIdxStr + "學年度", shi.SchoolYear + "");
                    prlp.SetAfterSaveText(logIdxStr + "學期", shi.Semester + "");
                    prlp.SetAfterSaveText(logIdxStr + "年級", shi.GradeYear + "");
                    prlp.SetAfterSaveText(logIdxStr + "班級", shi.ClassName);
                    prlp.SetAfterSaveText(logIdxStr + "座號", SeatNo + "");
                    prlp.SetAfterSaveText(logIdxStr + "班導師", shi.Teacher);
                    prlp.SetAfterSaveText(logIdxStr + "上課天數", SchoolDayCount + "");
                }

                JHSchool.Data.JHSemesterHistory.Update(updateSemeHsitoryRec);


                prlp.SetActionBy("學籍", "學生學期對照表");
                prlp.SetAction("修改學生學期對照表");
                JHStudentRecord studRec = JHStudent.SelectByID(PrimaryKey);
                prlp.SetDescTitle("學生姓名:" + studRec.Name + ",學號:" + studRec.StudentNumber + ",");

                prlp.SaveLog("", "", "student", PrimaryKey);

                this.CancelButtonVisible = false;
                this.SaveButtonVisible = false;
    
    }

        private void SemesterHistoryDetail_CancelButtonClick(object sender, EventArgs e)
        {
            this.Loading = true;
            dataGridViewX1.Rows.Clear();

            foreach (K12.Data.SemesterHistoryItem shi in _SemesterHistoryRec.SemesterHistoryItems)
            {
                string strSeatNo = "", strSchoolDayCount = "";

                if (shi.SeatNo.HasValue)
                    strSeatNo = "" + shi.SeatNo.Value;
                if (shi.SchoolDayCount.HasValue)
                    strSchoolDayCount = "" + shi.SchoolDayCount.Value;

                dataGridViewX1.Rows.Add(shi.SchoolYear, shi.Semester, shi.GradeYear, shi.ClassName, strSeatNo, shi.Teacher, strSchoolDayCount);
            }

            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;
            this.ContentValidated = true;
            this.Loading = false;
        }
    }
}
