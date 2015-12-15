using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using Framework;
using Framework.Legacy;
using System.Windows.Forms;

namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    // 班級學生名條
    class ClassStudentListRpt
    {
        Dictionary<string, List<string>> ClassGradeYears;
        Dictionary<string, List<StudentRecord>> ClassStudDatas;
        public ClassStudentListRpt ()
        {
            // 取得選擇班級學生資料
            ClassGradeYears = new Dictionary<string, List<string>>();
            ClassStudDatas = new Dictionary<string,List<StudentRecord>>();
            foreach (ClassRecord cr in Class.Instance.SelectedList)
            {
                if (cr.Students.Count > 0)
                {
                    if (ClassGradeYears.ContainsKey(cr.GradeYear))
                        ClassGradeYears[cr.GradeYear].Add(cr.Name);
                    else
                    {
                        List<string> tmpClass = new List<string>();
                        tmpClass.Add(cr.Name);
                        ClassGradeYears.Add(cr.GradeYear, tmpClass);
                        tmpClass = null;
                    }

                    if (ClassStudDatas.ContainsKey(cr.Name))
                        ClassStudDatas[cr.Name].AddRange(cr.Students.GetStatusStudents("一般"));
                    else
                        ClassStudDatas.Add(cr.Name, cr.Students.GetStatusStudents("一般"));
                }
            }

            // 加入用班級名稱排序
            foreach (KeyValuePair<string, List<string>> className in ClassGradeYears)
                className.Value.Sort();

            Workbook wb = new Workbook();
            for (int i = 1; i <= ClassGradeYears.Count; i++)
                wb.Worksheets.Add();

            int wstIdx = 0;
            foreach (KeyValuePair<string, List<string>> ClassGradeYear in ClassGradeYears)
            {
                int row = 0, col = 0;
                byte tmpCot = 0;
                wb.Worksheets[wstIdx].Name = ClassGradeYear.Key;
                
                
                foreach (string className in ClassGradeYear.Value)
                {   // Title
                    col = 0;                    
                    
                    wb.Worksheets[ClassGradeYear.Key].Cells.CreateRange(row,col,1,5).Merge();                    
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, 0].Style.Font.Size = 14;
                    wb.Worksheets[ClassGradeYear.Key].Cells.SetRowHeight(row, 20);
                    wb.Worksheets[ClassGradeYear.Key].AutoFitRow(row, 1, col, 5);
                    
                    
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, 0].PutValue( className+"班班級名條");
                    row++;
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, col].PutValue("座號");
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, col + 1].PutValue("學號");
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, col+2].PutValue("姓名");
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, col+3].PutValue("性別");
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, col+4].PutValue("備註");
                    wb.Worksheets[ClassGradeYear.Key].Cells.CreateRange(row, col, 1, 5).SetOutlineBorder(BorderType.TopBorder, CellBorderType.Thin, System.Drawing.Color.Black);
                    wb.Worksheets[ClassGradeYear.Key].Cells.CreateRange(row, col, 1, 5).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thin, System.Drawing.Color.Black);

                    col = 6;
                    wb.Worksheets[ClassGradeYear.Key].Cells.CreateRange(row-1, col, 1, 5).Merge();
                    wb.Worksheets[ClassGradeYear.Key].Cells[row-1, col].Style.Font.Size = 14;                    
                    wb.Worksheets[ClassGradeYear.Key].Cells[row - 1, col].Style.HorizontalAlignment = TextAlignmentType.Center;
                    wb.Worksheets[ClassGradeYear.Key].Cells[row - 1, col].PutValue( className + "班班級名條");
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, col].PutValue("座號");
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, col + 1].PutValue("學號");
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, col + 2].PutValue("姓名");
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, col + 3].PutValue("性別");
                    wb.Worksheets[ClassGradeYear.Key].Cells[row, col + 4].PutValue("備註");

                    wb.Worksheets[ClassGradeYear.Key].Cells.CreateRange(row, col, 1, 5).SetOutlineBorder(BorderType.TopBorder , CellBorderType.Thin , System.Drawing.Color.Black);
                    wb.Worksheets[ClassGradeYear.Key].Cells.CreateRange(row, col, 1, 5).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thin, System.Drawing.Color.Black);
                    for (int c = 0; c < 12; c++)
                        wb.Worksheets[ClassGradeYear.Key].Cells[row, c].Style.HorizontalAlignment = TextAlignmentType.Center;

                    row++;
                    tmpCot = 0;
                    foreach (StudentRecord studRec in ClassStudDatas[className])
                    {
                        tmpCot++;
                        col = 0;
                        wb.Worksheets[ClassGradeYear.Key].Cells[row, col].PutValue(studRec.SeatNo);
                        wb.Worksheets[ClassGradeYear.Key].Cells[row, col+1].PutValue(studRec.StudentNumber );
                        wb.Worksheets[ClassGradeYear.Key].Cells[row, col+2].PutValue(studRec.Name );
                        wb.Worksheets[ClassGradeYear.Key].Cells[row, col+3].PutValue(studRec.Gender );
                        wb.Worksheets[ClassGradeYear.Key].Cells.CreateRange(row, col, 1,5).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Dotted , System.Drawing.Color.Black);

                        col = 6;
                        wb.Worksheets[ClassGradeYear.Key].Cells[row, col].PutValue(studRec.SeatNo);
                        wb.Worksheets[ClassGradeYear.Key].Cells[row, col + 1].PutValue(studRec.StudentNumber);
                        wb.Worksheets[ClassGradeYear.Key].Cells[row, col + 2].PutValue(studRec.Name);
                        wb.Worksheets[ClassGradeYear.Key].Cells[row, col + 3].PutValue(studRec.Gender);
                        wb.Worksheets[ClassGradeYear.Key].Cells.CreateRange(row, col, 1, 5).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Dotted , System.Drawing.Color.Black);
                        if (tmpCot == 5)
                        {
                            col = 0;
                            wb.Worksheets[ClassGradeYear.Key].Cells.CreateRange(row, col, 1, 5).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thin , System.Drawing.Color.Red );
                            col = 6;
                            wb.Worksheets[ClassGradeYear.Key].Cells.CreateRange(row, col, 1, 5).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thin , System.Drawing.Color.Red );
                            tmpCot = 0;
                        }
                        row++;

                    }
                    wb.Worksheets[ClassGradeYear.Key].HPageBreaks.Add(row, 12);
                }
                for (int i = 0; i <= 10; i++)
                    wb.Worksheets[ClassGradeYear.Key].AutoFitColumn(i, 0, row + 1);
                wstIdx++;
            }
            try
            {
                for (int wst = 0; wst < ClassGradeYears.Count; wst++)
                    wb.Worksheets[wst].Name += "年級";

                wb.Save(Application.StartupPath + "\\Reports\\班級名條.xls", FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\班級名條.xls");
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
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            
                    
        }
    }
}
