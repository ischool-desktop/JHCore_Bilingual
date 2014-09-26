﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Aspose.Cells;
using K12.Data;

namespace BasicInformation
{
    internal static class CommonMethods
    {
        //Excel報表
        public static void ExcelReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string reportName;
            string path;
            Workbook wb;

            object[] result = (object[])e.Result;
            reportName = (string)result[0];
            path = (string)result[1];
            wb = (Workbook)result[2];

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
                FISCA.Presentation.MotherForm.SetStatusBarMessage(reportName + "產生完成");
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".xls";
                sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd.FileName, FileFormatType.Excel2003);
                    }
                    catch
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        //Word報表
        public static void WordReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string reportName;
            string path;
            Aspose.Words.Document doc;

            object[] result = (object[])e.Result;
            reportName = (string)result[0];
            path = (string)result[1];
            doc = (Aspose.Words.Document)result[2];

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
                doc.Save(path, Aspose.Words.SaveFormat.Doc);
                FISCA.Presentation.MotherForm.SetStatusBarMessage(reportName + "產生完成");
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".doc";
                sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        doc.Save(sd.FileName, Aspose.Words.SaveFormat.Doc);

                    }
                    catch
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        //回報進度
        public static void Report_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("" + e.UserState + "產生中...", e.ProgressPercentage);
        }

        internal static string GetChineseDayOfWeek(DateTime date)
        {
            string dayOfWeek = "";

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dayOfWeek = "一";
                    break;
                case DayOfWeek.Tuesday:
                    dayOfWeek = "二";
                    break;
                case DayOfWeek.Wednesday:
                    dayOfWeek = "三";
                    break;
                case DayOfWeek.Thursday:
                    dayOfWeek = "四";
                    break;
                case DayOfWeek.Friday:
                    dayOfWeek = "五";
                    break;
                case DayOfWeek.Saturday:
                    dayOfWeek = "六";
                    break;
                case DayOfWeek.Sunday:
                    dayOfWeek = "日";
                    break;
            }

            return dayOfWeek;
        }

        public static int ClassSeatNoComparer(StudentRecord x, StudentRecord y)
        {
            string xx1 = x.Class == null ? "" : x.Class.Name;
            string yy1 = y.Class == null ? "" : y.Class.Name;

            string xx2 = x.SeatNo.HasValue ? "::" + x.SeatNo.Value.ToString().PadLeft(2, '0') : "00";
            string yy2 = y.SeatNo.HasValue ? "::" + y.SeatNo.Value.ToString().PadLeft(2, '0') : "00";
            xx1 += xx2;
            yy1 += yy2;
            return xx1.CompareTo(yy1);
        }

        public static int JHClassSeatNoComparer(StudentRecord x, StudentRecord y)
        {
            string xx = (x.Class == null ? "" : x.Class.Name) + "::" + (x.SeatNo == null ? "" : x.SeatNo.ToString().PadLeft(2, '0'));
            string yy = (y.Class == null ? "" : y.Class.Name) + "::" + (y.SeatNo == null ? "" : y.SeatNo.ToString().PadLeft(2, '0'));

            return xx.CompareTo(yy);
        }

        public static int ClassComparer(ClassRecord x, ClassRecord y)
        {
            string xx = x.GradeYear.HasValue ? x.GradeYear.Value.ToString().PadLeft(3, '0') : "0";
            string yy = y.GradeYear.HasValue ? y.GradeYear.Value.ToString().PadLeft(3, '0') : "0";

            xx += x.DisplayOrder.PadLeft(3, '0');
            yy += y.DisplayOrder.PadLeft(3, '0');

            xx += x.Name.PadLeft(10, '0');
            yy += y.Name.PadLeft(10, '0');

            return xx.CompareTo(yy);
        }

        public static int StudentObjComparer(StudentOBJ x, StudentOBJ y)
        {
            string xx = x.GradeYear.PadLeft(3, '0');
            string yy = y.GradeYear.PadLeft(3, '0');

            xx += x.ClassName.PadLeft(10, '0');
            yy += y.ClassName.PadLeft(10, '0');

            xx += x.student.Name.PadLeft(10, '0');
            yy += y.student.Name.PadLeft(10, '0');

            return xx.CompareTo(yy);
        }
    }
}
