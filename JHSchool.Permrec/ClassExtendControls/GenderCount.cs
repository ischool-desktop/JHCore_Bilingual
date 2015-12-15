using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Aspose.Cells;
using JHSchool.Permrec.Properties;
using SmartSchool.Common;


namespace JHSchool.Permrec.ClassExtendControls
{
    public partial class GenderCount : BaseForm
    {
        public GenderCount()
        {
            InitializeComponent();
            dictADD();
            LoadData();
            Count_Button.Click += new EventHandler(Gender_Count);
        }
        /*顯示在LIST的班級男女人數*/
       private void LoadData()
        {
            #region MyRegion
		   listViewEx1.Items.Clear();
            List<ClassRecord> listclass =new List<ClassRecord>( Class.Instance.Items);
            foreach (ClassRecord clazz in listclass) 
            {
                List<StudentRecord> stuform =clazz.Students.GetInSchoolStudents();

                int listboy = 0;int listgirl = 0;int listun = 0;
                    
                
                foreach (StudentRecord stuzz in stuform)
                {
                    if (stuzz.Gender == "男") listboy++;

                    else if (stuzz.Gender == "女") listgirl++;

                    else
                        listun++;

               }
                ListViewItem item = listViewEx1.Items.Add(clazz.Name);
                item.SubItems.Add(clazz.Students.GetInSchoolStudents().Count.ToString());
                item.SubItems.Add(listboy.ToString());
                item.SubItems.Add(listgirl.ToString());
                item.SubItems.Add(listun.ToString());
                if (clazz.Teacher != null)
                item.SubItems.Add(clazz.Teacher.Name);

            }

            #endregion
        }
        
        
        
        /*離開*/
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        /*重新整理*/
        private void btnReload_Click(object sender, EventArgs e)
        {
            this.LoadData();
            

        }

        /*排序的class*/
        private int ClassComparer(ClassRecord a, ClassRecord b)
        {
            #region MyRegion
            int ia, ib;
            if (int.TryParse(a.GradeYear, out ia) && int.TryParse(b.GradeYear, out ib))
            {
                return ia.CompareTo(ib);
            }
            else if (int.TryParse(a.GradeYear, out ia))
            {
                return -1;
            }
            else if (int.TryParse(b.GradeYear, out ib))
            {
                return 1;
            }

            return a.GradeYear.CompareTo(b.GradeYear); 
            #endregion
        }
            
        
        /*按下按鈕所發生的事件*/
        public int yearBoyCount = 0;
        public int yearGirlCount = 0;
        public int yearUnknowGenderCount = 0;
        public string StuYear;
        
        private void Gender_Count(object seder, EventArgs e)
        {

            SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("處理中…請稍候", 0);

            Workbook book = new Workbook();
            book.Worksheets.Clear();
            book.Open(new MemoryStream(Resources.男女人數統計表));

            /*班級的excel*/
            book.Worksheets[0].Cells[1, 0].PutValue("年級");
            book.Worksheets[0].Cells[1, 1].PutValue("班級名稱");
            book.Worksheets[0].Cells[1, 2].PutValue("班級人數");
            book.Worksheets[0].Cells[1, 3].PutValue("男生人數");
            book.Worksheets[0].Cells[1, 4].PutValue("女生人數");
            book.Worksheets[0].Cells[1, 5].PutValue("未分性別");
            book.Worksheets[0].Cells[1, 6].PutValue("班導師");
            book.Worksheets[0].Cells.CreateRange(1, 0, 1, 7).SetOutlineBorder(BorderType.TopBorder, CellBorderType.Dashed, System.Drawing.Color.Black);
            book.Worksheets[0].Cells.CreateRange(1, 0, 1, 7).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Dashed, System.Drawing.Color.Black);
            book.Worksheets[0].Cells.CreateRange(1, 0, 1, 7).SetOutlineBorder(BorderType.LeftBorder, CellBorderType.Dashed, System.Drawing.Color.Black);
            book.Worksheets[0].Cells.CreateRange(1, 0, 1, 7).SetOutlineBorder(BorderType.RightBorder, CellBorderType.Dashed, System.Drawing.Color.Black);

            for (int c = 0; c <= 6; c++)
            {
                book.Worksheets[0].Cells[1, c].Style.HorizontalAlignment = TextAlignmentType.Center;
            }
            Cell A1 = book.Worksheets[0].Cells["A1"];
            
            A1.Style.Borders.SetColor(Color.Black);
            A1.Style.Font.Size = 10;
            A1.Style.Font.IsBold = true;
            A1.Style.HorizontalAlignment = TextAlignmentType.Center;
            A1.PutValue(School.DefaultSchoolYear + "學年度第" + School.DefaultSemester + "學期 男女統計清單");
            book.Worksheets[0].Cells.Merge(0,0,1,7);
              
         
            /*從row2開始*/
            int seq = 2;
            
            //年級=>性別
            Dictionary<string, Dictionary<string, int>> dict = new Dictionary<string, Dictionary<string, int>>();

            /*取出Class各班*/

            List<ClassRecord> classes = new List<ClassRecord>(Class.Instance.Items);
            
            
            /*班級做排序 */
            classes.Sort(ClassComparer);

            #region
            /*報表取得學生*/
            foreach (ClassRecord claz in Class.Instance.Items)
            {
                         
                                    
                        //seq += 1;

                

                        book.Worksheets[0].Cells[seq, 0].PutValue(claz.GradeYear);
                
                        book.Worksheets[0].Cells[seq, 1].PutValue(claz.Name);

                        book.Worksheets[0].Cells[seq, 2].PutValue(claz.Students.GetInSchoolStudents().Count);

                        if (claz.Teacher != null)
                        {
                            book.Worksheets[0].Cells[seq, 6].PutValue(claz.Teacher.Name);
                        }
                        List<StudentRecord> stuList = claz.Students.GetInSchoolStudents();

                        
                        int boy = 0;
                        int girl = 0;
                        int unkown = 0;
                        
                        foreach (StudentRecord stucount in stuList)
                        {

                            if (stucount.Gender == "男")
                                boy++;
                            else if (stucount.Gender == "女")
                                girl++;
                            else
                                unkown++;

                            /*用Dictionary收集值*/

                            string gradeYear = "";
                            
                            gradeYear = claz.GradeYear;
                                                        
                           
                           /*沒有索引值時才加入*/
                            
                            if (!dict.ContainsKey(gradeYear))
                            {
                                dict.Add(claz.GradeYear, new Dictionary<string, int>());
                                dict[gradeYear].Add("男", 0);
                                dict[gradeYear].Add("女", 0);
                                dict[gradeYear].Add("", 0);
                            }

                            dict[gradeYear][stucount.Gender]++;

                            
                        }
                             
                                    
                        book.Worksheets[0].Cells[seq, 3].PutValue(boy);
                        book.Worksheets[0].Cells[seq, 4].PutValue(girl);
                        book.Worksheets[0].Cells[seq, 5].PutValue(unkown);

                        seq++;
                       
                    }
                        #endregion

            seq++;

            int borderStartSeq = seq;


           int totalBoyCount = 0;  //全校男生人數
            int totalGirlCount = 0; //全校女生人數
            int totalUnknowGenderCount = 0; //全校未分性別人數
            
            //for迴圈
            foreach (string year in dict.Keys)
            {
               
                book.Worksheets[0].Cells[seq, 1].PutValue( ((year=="") ? "未分" : year) +"年級男女數");
                book.Worksheets[0].Cells[seq, 3].PutValue(dict[year]["男"]);
                book.Worksheets[0].Cells[seq, 4].PutValue(dict[year]["女"]);
                book.Worksheets[0].Cells[seq, 5].PutValue(dict[year][""]);
                
                StuYear=(((year=="") ? "未分" : year) +"年級男女數");
                yearBoyCount = dict[year]["男"];
                yearGirlCount = dict[year]["女"];
                yearUnknowGenderCount = dict[year][""];


               totalBoyCount += dict[year]["男"];
               totalGirlCount += dict[year]["女"];
               totalUnknowGenderCount += dict[year][""];
               
                seq++;
              
                    
                
             }
               

            #region
    
            book.Worksheets[0].Cells[seq, 1].PutValue("全校男女人數");
            book.Worksheets[0].Cells[seq, 3].PutValue(totalBoyCount);
            book.Worksheets[0].Cells[seq, 4].PutValue(totalGirlCount);
            book.Worksheets[0].Cells[seq, 5].PutValue(totalUnknowGenderCount);

            int borderStartColumnIndex = 0;
            int borderRowCount = dict.Keys.Count + 1;
            int borderColumnCount = 5;
            
            /* 報表邊框*/
            book.Worksheets[0].Cells.CreateRange(borderStartSeq, borderStartColumnIndex, borderRowCount, borderColumnCount).SetOutlineBorder(BorderType.TopBorder, CellBorderType.Thin, System.Drawing.Color.Black);
            book.Worksheets[0].Cells.CreateRange(borderStartSeq, borderStartColumnIndex, borderRowCount, borderColumnCount).SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thin, System.Drawing.Color.Black);
            book.Worksheets[0].Cells.CreateRange(borderStartSeq, borderStartColumnIndex, borderRowCount, borderColumnCount).SetOutlineBorder(BorderType.LeftBorder, CellBorderType.Thin, System.Drawing.Color.Black);
            book.Worksheets[0].Cells.CreateRange(borderStartSeq, borderStartColumnIndex, borderRowCount, borderColumnCount).SetOutlineBorder(BorderType.RightBorder, CellBorderType.Thin, System.Drawing.Color.Black);
            
    

            #endregion
          
    
    
    
    
    /*儲存資訊*/
            #region
          
            try
            {

                SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("請選擇儲存位置", 100);
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

                SaveFileDialog1.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                SaveFileDialog1.FileName = "男女統計清單";

                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    book.Save(SaveFileDialog1.FileName);
                    if (SaveFileDialog1.ShowDialog() == DialogResult.Yes)
                        Process.Start(SaveFileDialog1.FileName);
                }
                else
                {
                    MessageBox.Show("檔案未儲存");

                }
            }
            catch
            {
                MessageBox.Show("檔案儲存錯誤,請檢查檔案是否開啟中!!");
            }

            SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("已完成");

            #endregion


        }
        /*畫面上list的應用*/
        #region
        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkYear.Checked)
            {
                listViewEx2.Items.Clear();
                foreach (string each in dict.Keys)
                {
                    ListViewItem list2 = listViewEx2.Items.Add(each);

                    list2.SubItems.Add(dict[each]["男"].ToString());
                    list2.SubItems.Add(dict[each]["女"].ToString());
                    list2.SubItems.Add(dict[each][""].ToString());
                }
            }
            else
            {
                listViewEx2.Items.Clear();

            }
        }
        
        private void checkGender_CheckedChanged(object sender, EventArgs e)
        {
            if (checkGender.Checked)
            {
                int listBC = 0;int listGC = 0;int listUC = 0;
                
                listViewEx2.Items.Clear();
                foreach (string totaleach in dict.Keys) {
                    
                    listBC += dict[totaleach]["男"];
                    listGC += dict[totaleach]["女"];
                    listUC += dict[totaleach][""];
                }
                
                ListViewItem list2 = listViewEx2.Items.Add("全校人數");
                list2.SubItems.Add(listBC.ToString());
                list2.SubItems.Add(listGC.ToString());
                list2.SubItems.Add(listUC.ToString());

            }
            else
            {
                listViewEx2.Items.Clear();

            }

        }
        #endregion


        private Dictionary<string, Dictionary<string, int>> dict = new Dictionary<string, Dictionary<string, int>>();
        /*計算成績的Dictionary*/
        private void dictADD()
        {
            #region MyRegion
            
           
            //年級=>性別
            dict.Clear();

            foreach (ClassRecord claz in Class.Instance.Items)
            {
                List<StudentRecord> stuList = claz.Students.GetInSchoolStudents();

                foreach (StudentRecord stucount in stuList)
                {
                    string gradeYear = claz.GradeYear;

                    if (!dict.ContainsKey(gradeYear))
                    {
                        dict.Add(claz.GradeYear, new Dictionary<string, int>());
                        dict[gradeYear].Add("男", 0);
                        dict[gradeYear].Add("女", 0);
                        dict[gradeYear].Add("", 0);
                    }

                    dict[gradeYear][stucount.Gender]++;
                }
               
            }
        }

       
    }
            #endregion







}






