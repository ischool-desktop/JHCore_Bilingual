using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHSchool.Data;
using Aspose.Cells;
using System.IO;

namespace JHSchool.Permrec.ClassExtendControls
{
    public class ClassScoreTransferformer
    {

        private static double SafeParseDouble(string strDouble)
        {
            double i = 0;

            return double.TryParse(strDouble , out i) ? i : 0;
        }

        public static Aspose.Cells.Range Execute(int classIndex, string schoolYear, string semester, K12.Data.ClassRecord Class, Dictionary<string, string> studDomainScores, Dictionary<string, decimal?> DomainScores)
        {
            Workbook book = new Workbook();
            book.Worksheets.Clear();
            book.Open(new MemoryStream(JHSchool.Permrec.Properties.Resources.班級學期領域成績一覽表));

            int RowIndex = 5;

            book.Worksheets[0].Cells[0, 0].PutValue(schoolYear+" 學年度第 "+semester +" 學期 "+Class.GradeYear.ToString()+" 年 "+Class.Name+" 班");

            book.Worksheets[0].Cells[47, 0].PutValue(" 列印日期：" + DateTime.Today.ToShortDateString());

            foreach(K12.Data.StudentRecord student in Class.Students)
            {
                book.Worksheets[0].Cells[RowIndex, 0].PutValue(student.SeatNo);
                book.Worksheets[0].Cells[RowIndex, 1].PutValue(student.Name);



                //語文 數學 自然與生活 科技 社會 藝術與人文 健康與體育 綜合活動 彈性課程
                if (studDomainScores.ContainsKey(student.ID + "語文"))
                    book.Worksheets[0].Cells[RowIndex, 2].PutValue(SafeParseDouble(studDomainScores[student.ID + "語文"]));

                if (studDomainScores.ContainsKey(student.ID + "數學"))
                    book.Worksheets[0].Cells[RowIndex, 3].PutValue(SafeParseDouble(studDomainScores[student.ID + "數學"]));

                if (studDomainScores.ContainsKey(student.ID + "自然與生活科技"))
                    book.Worksheets[0].Cells[RowIndex, 4].PutValue(SafeParseDouble(studDomainScores[student.ID + "自然與生活科技"]));

                if (studDomainScores.ContainsKey(student.ID + "社會"))
                    book.Worksheets[0].Cells[RowIndex, 5].PutValue(SafeParseDouble(studDomainScores[student.ID + "社會"]));

                if (studDomainScores.ContainsKey(student.ID + "藝術與人文"))
                    book.Worksheets[0].Cells[RowIndex, 6].PutValue(SafeParseDouble(studDomainScores[student.ID + "藝術與人文"]));

                if (studDomainScores.ContainsKey(student.ID + "健康與體育"))
                    book.Worksheets[0].Cells[RowIndex, 7].PutValue(SafeParseDouble(studDomainScores[student.ID + "健康與體育"]));

                if (studDomainScores.ContainsKey(student.ID + "綜合活動"))
                    book.Worksheets[0].Cells[RowIndex, 8].PutValue(SafeParseDouble(studDomainScores[student.ID + "綜合活動"]));

                double i = 0;

                if (studDomainScores.ContainsKey(student.ID + "學習領域"))
                {
                    if (studDomainScores[student.ID + "學習領域"].Equals(string.Empty))
                        book.Worksheets[0].Cells[RowIndex, 9].PutValue("");
                    else 
                        book.Worksheets[0].Cells[RowIndex, 9].PutValue(Convert.ToDouble(studDomainScores[student.ID + "學習領域"]));
                }

                if (studDomainScores.ContainsKey(student.ID + "課程學習"))
                {
                    if (studDomainScores[student.ID + "課程學習"].Equals(string.Empty))
                        book.Worksheets[0].Cells[RowIndex, 11].PutValue("");
                    else
                        book.Worksheets[0].Cells[RowIndex, 11].PutValue(Convert.ToDouble(studDomainScores[student.ID + "課程學習"]));
                }

                int FormulaIndex = (1 + RowIndex + (classIndex * 50));

                book.Worksheets[0].Cells[RowIndex, 10].Formula = "=IF(ISERROR(RANK(J$" + FormulaIndex.ToString() + ",J$" + (6 + classIndex * 50) + ":J$" + (40 + classIndex * 50) + ")),\"\",RANK(J$" + FormulaIndex.ToString() + ",J$" + (6 + classIndex * 50) + ":J$" + (40 + classIndex * 50) + "))";

                book.Worksheets[0].Cells[RowIndex, 12].Formula = "=IF(ISERROR(RANK(L$" + FormulaIndex.ToString() + ",L$" + (6 + classIndex * 50) + ":L$" + (40 + classIndex * 50) + ")),\"\",RANK(L$" + FormulaIndex.ToString() + ",L$" + (6 + classIndex * 50) + ":L$" + (40 + classIndex * 50) + "))";
                //if (studDomainScores.ContainsKey(student.ID + "學習領域"))
                //    book.Worksheets[0].Cells[RowIndex, 10].PutValue(studDomainScores[student.ID + "學習領域"]);

                //if (studDomainScores.ContainsKey(student.ID + "課程學習(含彈性課程)"))
                //    book.Worksheets[0].Cells[RowIndex, 12].PutValue(studDomainScores[student.ID + "課程學習(含彈性課程)"]);

							
                RowIndex++;
            }

            //平均成績
            //語文 數學 自然與生活 科技 社會 藝術與人文 健康與體育 綜合活動 彈性課程
            if (DomainScores.ContainsKey("語文"))
                book.Worksheets[0].Cells[40, 2].PutValue(string.Format("{0:F2}",DomainScores["語文"]));

            if (DomainScores.ContainsKey("數學"))
                book.Worksheets[0].Cells[40, 3].PutValue(string.Format("{0:F2}",DomainScores["數學"]));

            if (DomainScores.ContainsKey("自然與生活科技"))
                book.Worksheets[0].Cells[40, 4].PutValue(string.Format("{0:F2}",DomainScores["自然與生活科技"]));

            if (DomainScores.ContainsKey("社會"))
                book.Worksheets[0].Cells[40, 5].PutValue(string.Format("{0:F2}", DomainScores["社會"]));

            if (DomainScores.ContainsKey("藝術與人文"))
                book.Worksheets[0].Cells[40, 6].PutValue(string.Format("{0:F2}", DomainScores["藝術與人文"]));

            if (DomainScores.ContainsKey("健康與體育"))
                book.Worksheets[0].Cells[40, 7].PutValue(string.Format("{0:F2}", DomainScores["健康與體育"]));

            if (DomainScores.ContainsKey("綜合活動"))
                book.Worksheets[0].Cells[40, 8].PutValue(string.Format("{0:F2}", DomainScores["綜合活動"]));


            //RowIndex = 5;

            //foreach (JHSchool.Data.JHStudentRecord student in Class.Students)
            //{
            //    object value = book.Worksheets[0].Cells[RowIndex, 10].Value;

            //    book.Worksheets[0].Cells[RowIndex, 10].PutValue(value);
            //    book.Worksheets[0].Cells[RowIndex, 10].R1C1Formula = "";

            //    RowIndex++;
            //}

            return book.Worksheets[0].Cells.CreateRange(0, 50, false);
        }
    }
}
