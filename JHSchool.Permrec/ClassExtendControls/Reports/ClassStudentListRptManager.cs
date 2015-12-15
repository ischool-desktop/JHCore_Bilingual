using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.Windows.Forms;
using System.ComponentModel;

namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    class ClassStudentListRptManager
    {
        Dictionary<string, List<DAL.ClassStudentEntity>> GradeClassStudentDic;
        /// <summary>
        /// 項目
        /// </summary>
        public List<string> selectItems { get; set;}

        /// <summary>
        /// copy 數量
        /// </summary>
        public int copyCot {get;set;}

        public ClassStudentListRptManager()
        {

            GradeClassStudentDic = DAL.Transfer.GetGradeClassStudentDic(Class.Instance.SelectedKeys, JHSchool.Data.JHStudentRecord.StudentStatus.一般);
          //  ExportData();
            selectItems = new List<string>();
        }


        /// <summary>
        /// 產生報表
        /// </summary>
        public void ExportData()
        {
            if (GradeClassStudentDic == null)
                return;

            BackgroundWorker bkWork = new BackgroundWorker();
            bkWork.DoWork += new DoWorkEventHandler(bkWork_DoWork);
            bkWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkWork_RunWorkerCompleted);
            bkWork.RunWorkerAsync();

        }

        void bkWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Workbook wb = (Workbook)e.Result;
            bool checklog = false;

            try
            {
                foreach (Worksheet wst in wb.Worksheets)
                    wst.Name += "年級";

                wb.Save(Application.StartupPath + "\\Reports\\班級名條.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\班級名條.xls");
                checklog = true;
            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd1 = new System.Windows.Forms.SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = "班級名條.xls";
                sd1.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd1.FileName, FileFormatType.Excel2003);
                        System.Diagnostics.Process.Start(sd1.FileName);
                        checklog = true;
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (checklog)
            {
                string logClassName = "";
                PermRecLogProcess prlp = new PermRecLogProcess();
                foreach (List<DAL.ClassStudentEntity> cseLst in GradeClassStudentDic.Values)
                    foreach (DAL.ClassStudentEntity cse in cseLst)
                        logClassName += cse.ClassName + ",";

                prlp.SaveLog("學籍.班級名條", "學籍班級.產生班級名條", "產生 " + logClassName + " 班級名條報表");
            }
        }

        void bkWork_DoWork(object sender, DoWorkEventArgs e)
        {
            Workbook wb = new Workbook();
                        
            for (int i = 1; i < GradeClassStudentDic.Count; i++)
                wb.Worksheets.Add();
            int idx = 0;
            List<string> strs = new List<string>();
            foreach (string str in GradeClassStudentDic.Keys)
                strs.Add(str);

            strs.Sort();

            foreach (string str in strs)
            {
                wb.Worksheets[idx].Name = str;
                wb.Worksheets[idx].PageSetup.CenterHorizontally = true;
                wb.Worksheets[idx].PageSetup.BottomMargin = 2;
                wb.Worksheets[idx].PageSetup.LeftMargin = 1;
                wb.Worksheets[idx].PageSetup.RightMargin = 1;
                wb.Worksheets[idx].PageSetup.TopMargin = 2;
                idx++;
            }            

            foreach (KeyValuePair<string, List<DAL.ClassStudentEntity>> itemGradeYear in GradeClassStudentDic)
            {
                Dictionary<string, int> idxColDic = new Dictionary<string, int>();
                int i = 0;
                foreach (string str in selectItems)
                {
                    idxColDic.Add(str, i);
                    i++;
                }
                
                int row = 0, col = 0;
                byte tmpCot = 0;
                foreach (DAL.ClassStudentEntity itemClassStud in itemGradeYear.Value)
                {
                    // 當班級沒有學生
                    if (itemClassStud.StudentEntityList.Count < 1)
                        continue;

                    wb.Worksheets[itemGradeYear.Key].Cells.CreateRange(row, col, 1, selectItems.Count).Merge();
                    wb.Worksheets[itemGradeYear.Key].Cells[row, 0].Style.Font.Size = 14;
                    wb.Worksheets[itemGradeYear.Key].Cells.SetRowHeight(row, 20);
                    wb.Worksheets[itemGradeYear.Key].AutoFitRow(row, 1, col, selectItems.Count);


                    wb.Worksheets[itemGradeYear.Key].Cells[row, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[itemGradeYear.Key].Cells[row, 0].PutValue(itemClassStud.ClassName + "班班級名條");
                    row++;
                    
                    //wb.Worksheets[itemGradeYear.Key].Cells[row, idxColDic["學號"]].PutValue("學號");
                    //wb.Worksheets[itemGradeYear.Key].Cells[row, idxColDic["座號"]].PutValue("座號");
                    //wb.Worksheets[itemGradeYear.Key].Cells[row, idxColDic["姓名"]].PutValue("姓名");
                    //wb.Worksheets[itemGradeYear.Key].Cells[row, idxColDic["性別"]].PutValue("性別");
                    //wb.Worksheets[itemGradeYear.Key].Cells[row, idxColDic["備註"]].PutValue("備註");

                    foreach (string str in selectItems )
                    {
                        wb.Worksheets[itemGradeYear.Key].Cells[row, idxColDic[str]].PutValue(str);                    
                    }

                    wb.Worksheets[itemGradeYear.Key].Cells.CreateRange(row, col, 1, selectItems.Count).SetOutlineBorder(BorderType.TopBorder, CellBorderType.Thin, System.Drawing.Color.Black);
                    wb.Worksheets[itemGradeYear.Key].Cells.CreateRange(row, col, 1, selectItems.Count).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thin, System.Drawing.Color.Black);

                    wb.Worksheets[itemGradeYear.Key].Cells.CreateRange(row, col, 1, selectItems.Count).SetOutlineBorder(BorderType.TopBorder, CellBorderType.Thin, System.Drawing.Color.Black);
                    wb.Worksheets[itemGradeYear.Key].Cells.CreateRange(row, col, 1, selectItems.Count).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thin, System.Drawing.Color.Black);
                    for (int c = 0; c < 12; c++)
                        wb.Worksheets[itemGradeYear.Key].Cells[row, c].Style.HorizontalAlignment = TextAlignmentType.Center;

                    row++;
                    tmpCot = 0;
                    foreach (DAL.StudentEntity studRec in itemClassStud.StudentEntityList)
                    {
                        tmpCot++;
                        col = 0;
                        foreach (string str in selectItems)
                        {
                            string strVal = "";
                            if (str == "學號")
                                strVal = studRec.StudentNumber;
                            if (str == "座號")
                                strVal = studRec.SeatNo;

                            if (str == "姓名")
                                strVal = studRec.Name;
                            if (str == "性別")
                                strVal = studRec.Gender;
                            wb.Worksheets[itemGradeYear.Key].Cells[row, idxColDic[str]].PutValue(strVal);

                            //wb.Worksheets[itemGradeYear.Key].Cells[row, idxColDic["學號"]].PutValue(studRec.StudentNumber);
                            //wb.Worksheets[itemGradeYear.Key].Cells[row, idxColDic["座號"]].PutValue(studRec.SeatNo);
                            //wb.Worksheets[itemGradeYear.Key].Cells[row, idxColDic["姓名"]].PutValue(studRec.Name);
                            //wb.Worksheets[itemGradeYear.Key].Cells[row, idxColDic["性別"]].PutValue(studRec.Gender);
                        }
                        wb.Worksheets[itemGradeYear.Key].Cells.CreateRange(row, col, 1, selectItems.Count).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Dotted, System.Drawing.Color.Black);
                                            
                        if (tmpCot == 5)
                        {
                            col = 0;
                            wb.Worksheets[itemGradeYear.Key].Cells.CreateRange(row, col, 1, selectItems.Count).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thin, System.Drawing.Color.Red);
                        
                            tmpCot = 0;
                        }
                        row++;

                    }
                    wb.Worksheets[itemGradeYear.Key].HPageBreaks.Add(row, 12);
                }
            }
            int idxCol = 0;
            foreach (Worksheet wst in wb.Worksheets)
            {
                int colW = wst.Cells.MaxDataColumn + 1;
                
                for (int i = 1; i <copyCot; i++)
                {
                    idxCol = wst.Cells.MaxDataColumn + 2;
                    Range rng = wst.Cells.CreateRange(0, colW, true);
                    wst.Cells.SetColumnWidth(idxCol-1, 3);
                    Range rng1 = wst.Cells.CreateRange(idxCol, colW, true);
                    rng1.Copy(rng);
                }

                for (int i = 0; i <= wst.Cells.MaxDataColumn + 1; i++)
                    wst.AutoFitColumn(i, 0, wst.Cells.MaxDataRow);
            }

            e.Result = wb;
        }
    }
}
