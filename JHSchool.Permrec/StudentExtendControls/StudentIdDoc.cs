using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Aspose.Words;
using System.IO;
using Aspose.Words.Reporting;
using Aspose.Words.Drawing;
using Aspose.BarCode;
using System.Drawing.Imaging;
using System.Windows.Forms;
using SmartSchool.Customization.PlugIn;


namespace JHSchool.Permrec.StudentExtendControls
{
    public partial class StudentIdDoc
    {
        
        /// <summary>
        /// 定義要列印的資料是來自學生或班級。
        /// DataSourceType 是自訂的 enum 
        /// </summary>
        private DataSourceType dataSourceType { get; set; }

        /// <summary>
        /// 預設列印的資料是來自被選取的學生清單，而非班級。
        /// </summary>
        public StudentIdDoc() : this( DataSourceType.FromStudent)
        {
          
        }
        
        public StudentIdDoc(DataSourceType dataSourceType) {

            SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("處理中，請稍後", 0);
            int log = DateTime.Now.Millisecond;
            System.Console.WriteLine("firstpoint:{0}", log);
            this.dataSourceType = dataSourceType;

            Document doc = new Document(new MemoryStream(Properties.Resources.學生證樣板));

             //Document doc = new Document(new FileStream(@"C:\Documents and Settings\Jen\桌面\學生證樣板t.doc", FileMode.Open));

            List<JHSchool.Data.JHStudentRecord> students = GetStudents();  //根據 DataSourceType 取得學生清單
            
            DataTable dt = new DataTable();
            dt.Columns.Add("學校名稱",typeof(string));
            dt.Columns.Add("學號",typeof(string));
            dt.Columns.Add("姓名",typeof(string));
            dt.Columns.Add("性別",typeof(string));
            dt.Columns.Add("生日", typeof(string));
            dt.Columns.Add("監護人姓名",typeof(string));
            dt.Columns.Add("出生地", typeof(string));
            dt.Columns.Add("照片", typeof(byte[]));
            dt.Columns.Add("條碼", typeof(string));

            int log1 = DateTime.Now.TimeOfDay.Seconds;
            System.Console.WriteLine("log1:{0}", log1);

            foreach (JHSchool.Data.JHStudentRecord each in students)
            {
                string strCustodian = K12.Data.Parent.SelectByStudentID(each.ID).Custodian.Name;
                string strBirthPlace = JHSchool.Data.JHStudent.GetBirthPlace(each.ID);
                string strPhoto = K12.Data.Photo.SelectFreshmanPhoto(each.ID);
                byte[] bytePhoto = strPhoto.Equals(string.Empty)?null:Convert.FromBase64String(strPhoto);              

                dt.Rows.Add(
                    School.ChineseName,
                    each.StudentNumber,
                    each.Name,
                    each.Gender,
                    each.Birthday,
                    strCustodian,
                    strBirthPlace,
                    bytePhoto,
                    each.StudentNumber
              

                    );
              
            }
            int log2 = DateTime.Now.TimeOfDay.Seconds;
            System.Console.WriteLine("log2:{0}", log2);

            doc.MailMerge.MergeField += new MergeFieldEventHandler(MailMerge_MergeField);
            doc.MailMerge.Execute(dt);
            
            DocumentBuilder bulider = new DocumentBuilder(doc);
           
            int N =Student.Instance.SelectedKeys.Count();
            N = N % 8;
            N = 8 - N;
            int M=0;
            while (M<=N)
            {
                if (bulider.MoveToMergeField("學校名稱") == true)
                {
                  
                 (bulider.CurrentParagraph.ParentNode as Cell).ParentRow.ParentTable.Remove();
                
                    }
                M++;
                }

            int log3 = DateTime.Now.TimeOfDay.Seconds;
            System.Console.WriteLine("log3:{0}", log3);
            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath + "/Reports");
            if (!dir.Exists)
                dir.Create();
            try
            {
                SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("請選擇儲存位置", 100);
                doc.Save(dir.FullName + "/學生證.doc", SaveFormat.Doc);
            }
            catch
            {
                MessageBox.Show("檔案儲存錯誤,請檢查檔案是否開啟中!!");
            }
            System.Diagnostics.Process.Start(dir.FullName + "/學生證.doc");
            SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("已完成");
        }

        private void MailMerge_MergeField(object sender, MergeFieldEventArgs e)
        {
            if (e.FieldName == "照片")
            {
                byte[] photo = e.FieldValue as byte[];
                if (photo == null)
                    return;
                DocumentBuilder photoBuilder = new DocumentBuilder(e.Document);
                photoBuilder.MoveToField(e.Field, true);
                e.Field.Remove();

                Shape photoShape = new Shape(e.Document, ShapeType.Image);
                photoShape.ImageData.SetImage(photo);
                photoShape.WrapType = WrapType.Inline;//設定文繞圖

                //resize
                double origSizeRatio = photoShape.ImageData.ImageSize.HeightPoints / photoShape.ImageData.ImageSize.WidthPoints;
                Cell curCell = photoBuilder.CurrentParagraph.ParentNode as Cell;
                double shapeHeight = (curCell.ParentNode as Row).RowFormat.Height * 4;
                double shapeWidth = curCell.CellFormat.Width;

                if ((shapeHeight / shapeWidth) < origSizeRatio)
                    shapeWidth = shapeHeight / origSizeRatio;
                else
                    shapeHeight = shapeWidth * origSizeRatio;

                photoShape.Height = shapeHeight;
                photoShape.Width = shapeWidth;

                photoBuilder.InsertNode(photoShape);
            }

            if (e.FieldName == "條碼")
            {
                DocumentBuilder builder = new DocumentBuilder(e.Document);
                builder.MoveToField(e.Field, true);//將游標移到條碼所在欄位
                e.Field.Remove();//將原先的合併欄位刪除

                BarCodeBuilder bb = new BarCodeBuilder();
                if (e.FieldValue.ToString() !="")
                {
                    bb.CodeText = e.FieldValue.ToString();


                    bb.SymbologyType = Symbology.Code128;


                }
                else
                {
                    
                    bb.CodeLocation = CodeLocation.None;//不輸出學號
                }

                bb.xDimension = 0.5f;
                bb.BarHeight = 4.0f;
               

                MemoryStream stream = new MemoryStream();
                bb.Save(stream, ImageFormat.Jpeg);//將產生出的條碼存成圖檔

                builder.InsertImage(stream);//

            }
            
        }

        /// <summary>
        /// 根據 DataSourceType 來取得學生清單
        /// </summary>
        /// <returns></returns>
        private List<JHSchool.Data.JHStudentRecord> GetStudents()
        {
           
            List<JHSchool.Data.JHStudentRecord> students = new List<JHSchool.Data.JHStudentRecord>();

            if (this.dataSourceType == DataSourceType.FromStudent)
            {
                students = JHSchool.Data.JHStudent.SelectByIDs(Student.Instance.SelectedKeys);
            }
            else
            {
                List<JHSchool.Data.JHClassRecord> classes = JHSchool.Data.JHClass.SelectByIDs(Class.Instance.SelectedKeys);
                foreach (JHSchool.Data.JHClassRecord cr in classes)
                {
                    List<JHSchool.Data.JHStudentRecord> stus = cr.Students;
                    foreach (JHSchool.Data.JHStudentRecord stu in stus)
                    {
                        students.Add(stu);
                    }
                }
            }

            return students;
         
        }

        /// <summary>
        /// 定義要列印的資料是來自學生或班級的列舉型別
        /// </summary>
        public enum DataSourceType
        {
            FromStudent, FromClass
        }
    }

    
}
