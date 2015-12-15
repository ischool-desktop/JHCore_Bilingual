using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.ComponentModel;
using System.Windows.Forms;

namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    class StudentGraduateInfoManager
    {
        /// <summary>
        /// 校長中文名稱
        /// </summary>
        public string ChancellorChineseName { get; set; }
        /// <summary>
        /// 校長英文名稱
        /// </summary>
        public string ChancellorEnglishName { get; set; }

        /// <summary>
        /// 學校中文名稱
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 學校英文名稱        
        /// </summary>
        public string SchoolEnglishName { get; set; }

        public List<DAL.StudentEntity> _StudentEntityList;

        
        public StudentGraduateInfoManager()
        {            
            _StudentEntityList = new List<JHSchool.Permrec.StudentExtendControls.Reports.DAL.StudentEntity>();

            ChancellorChineseName = JHSchool.Data.JHSchoolInfo.ChancellorChineseName;
            ChancellorEnglishName = JHSchool.Data.JHSchoolInfo.ChancellorEnglishName;
            SchoolName = JHSchool.Data.JHSchoolInfo.ChineseName;
            SchoolEnglishName = JHSchool.Data.JHSchoolInfo.EnglishName;
            BackgroundWorker bkWork = new BackgroundWorker();
            bkWork.DoWork += new DoWorkEventHandler(bkWork_DoWork);
            bkWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkWork_RunWorkerCompleted);
            bkWork.RunWorkerAsync();
        }

        void bkWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Workbook wb = (Workbook)e.Result;

            try
            {
                wb.Save(Application.StartupPath + "\\Reports\\畢修業證明書相關資料.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\畢修業證明書相關資料.xls");

            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "畢修業證明書相關資料.xls";
                sd1.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd1.FileName, FileFormatType.Excel2003);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        void bkWork_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得學生資料
            _StudentEntityList = DAL.DALTransfer.GetStudentEntityList(Student.Instance.SelectedKeys);

            // 依畢業證書字號排序
            _StudentEntityList.Sort(new Comparison<DAL.StudentEntity>(Sort1));

            Workbook wb = new Workbook();
            Worksheet wst = wb.Worksheets[0];

            int col = 0;

            wst.Cells[0, col++].PutValue("班級");
            wst.Cells[0, col++].PutValue("座號");
            wst.Cells[0, col++].PutValue("學號");
            wst.Cells[0, col++].PutValue("身分證號");
            wst.Cells[0, col++].PutValue("學生中文姓名");
            wst.Cells[0, col++].PutValue("學生英文姓名");
            wst.Cells[0, col++].PutValue("學校中文名稱");
            wst.Cells[0, col++].PutValue("學校英文名稱");
            wst.Cells[0, col++].PutValue("校長中文名稱");
            wst.Cells[0, col++].PutValue("校長英文名稱");
            wst.Cells[0, col++].PutValue("學生西元生日");
            wst.Cells[0, col++].PutValue("學生民國生日");
            wst.Cells[0, col++].PutValue("畢修業");
            wst.Cells[0, col++].PutValue("畢業證書字號");

            int row = 1,DataCol=0;
            foreach (DAL.StudentEntity se in _StudentEntityList)
            {
                DataCol = 0;

                wst.Cells[row, DataCol++].PutValue(se.ClassName);
                wst.Cells[row, DataCol++].PutValue(se.SeatNo);
                wst.Cells[row, DataCol++].PutValue(se.StudentNumber);
                wst.Cells[row, DataCol++].PutValue(se.IDNumber);
                wst.Cells[row, DataCol++].PutValue(se.StudentName);
                wst.Cells[row, DataCol++].PutValue(se.StudentEnglishName);
                wst.Cells[row, DataCol++].PutValue(SchoolName);
                wst.Cells[row, DataCol++].PutValue(SchoolEnglishName);
                wst.Cells[row, DataCol++].PutValue(ChancellorChineseName);
                wst.Cells[row, DataCol++].PutValue(ChancellorEnglishName);
                wst.Cells[row, DataCol++].PutValue(se.GetBirthdayStr());
                wst.Cells[row, DataCol++].PutValue(se.GetChineseBirthday());
                wst.Cells[row, DataCol++].PutValue(se.Reason);
                wst.Cells[row, DataCol++].PutValue(se.DiplomaNumber); 
                row++;
            }

            e.Result = wb;
        }

        // 排序
        private int Sort1(DAL.StudentEntity x, DAL.StudentEntity y)
        {
            if (string.IsNullOrEmpty(x.DiplomaNumber))
                x.DiplomaNumber = "";

            if (string.IsNullOrEmpty(y.DiplomaNumber))
                y.DiplomaNumber = "";

            return x.DiplomaNumber.CompareTo(y.DiplomaNumber);        
        }

    }
}
