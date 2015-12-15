using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Aspose.Words;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using System.IO;
using System.Xml;
using Framework;
using System.Data;
using System.Drawing.Imaging;
using Aspose.Words.Drawing;
using Aspose.BarCode;

namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    class StudentIDCardManagerOld
    {

        private enum CountyType { 新竹, 高雄 };

        List<DAL.StudentEntity> _StudentList;
        private BackgroundWorker bkWorkPrint;
        private bool _isDefaultTemplate = true;
        private byte[] _buffer = null;
        private bool _isUseSystemPhoto = false;
        private byte[] defalutTemplate;
        private string base64 = "";
        private bool _isUpload = false;
        int _StudentCount = 0;


        private CountyType UseCountyType;
        /// <summary>
        /// 取得是否讀取設樣版
        /// </summary>
        public bool GetisDefaultTemplate()
        {
            return _isDefaultTemplate;
        }

        public List<DAL.StudentEntity> _StudentEntityList;

        public StudentIDCardManagerOld()
        {
            _StudentEntityList = new List<JHSchool.Permrec.StudentExtendControls.Reports.DAL.StudentEntity>();
            GetUserDefineTemplateFromSystem();


            // 舊式用新竹原樣板
            defalutTemplate = JHSchool.Permrec.StudentExtendControls.Reports.RptResource.學生證樣版_新竹_;
            UseCountyType = CountyType.新竹;


            //if (Program.ModuleType == Program.ModuleFlag.HsinChu)
            //{
            //    defalutTemplate = JHSchool.Permrec.StudentExtendControls.Reports.RptResource.學生證樣版_新竹_;
            //    UseCountyType = CountyType.新竹;

            //}
            //else
            //{
            //    defalutTemplate = JHSchool.Permrec.StudentExtendControls.Reports.RptResource.學生證樣板_高雄_;
            //    UseCountyType = CountyType.高雄;
            //}


        }

        /// <summary>
        /// 取得使用者自訂範本
        /// </summary>
        public void GetUserDefineTemplateFromSystem()
        {
            School.Configuration.Sync("學生學生證");
            ConfigData cd = School.Configuration["學生學生證"];

            bool.TryParse(cd["Default"], out _isDefaultTemplate);

            //if (cd["CustomizeTemplateOld"] != null)
            if (cd.Contains("CustomizeTemplateOld"))
            {
                string templateBase64 = cd["CustomizeTemplateOld"];
                _buffer = Convert.FromBase64String(templateBase64);
            }
            else
            {
                #region 產生空白設定檔
                cd["Default"] = "true";
                cd["CustomizeTemplateOld"] = "";
                cd.Save();
                #endregion
            }

        }

        /// <summary>
        /// 儲存預設樣版
        /// </summary>
        public void SaveDefaulTemplate()
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "學生證.doc";
            sfd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(defalutTemplate, 0, defalutTemplate.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        /// <summary>
        /// 設定使用者自訂範本
        /// </summary>
        public void SetUserDefineTemplateToSystem()
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇自訂的學生證範本";
            ofd.Filter = "Word檔案 (*.doc)|*.doc";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (Document.DetectFileFormat(ofd.FileName) == LoadFormat.Doc)
                    {
                        FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                        byte[] tempBuffer = new byte[fs.Length];
                        fs.Read(tempBuffer, 0, tempBuffer.Length);
                        base64 = Convert.ToBase64String(tempBuffer);
                        _isUpload = true;
                        fs.Close();
                        SaveTemplateToSystem();

                        PermRecLogProcess prlp = new PermRecLogProcess();
                        prlp.SaveLog("學生.報表", "上傳", "上傳學生證樣版.");

                        FISCA.Presentation.Controls.MsgBox.Show("上傳成功。");
                    }
                    else
                        FISCA.Presentation.Controls.MsgBox.Show("上傳檔案格式不符");
                }
                catch
                {
                    FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }

        private void SaveSelectItem()
        {
            ConfigData cd = School.Configuration["學生學生證"];

            cd["Default"] = _isDefaultTemplate.ToString();
            cd.Save();
        }


        /// <summary>
        /// 儲存使用者定義
        /// </summary>
        public void SaveTemplateToSystem()
        {

            ConfigData cd = School.Configuration["學生學生證"];

            cd["Default"] = _isDefaultTemplate.ToString();
            cd["CustomizeTemplateOld"] = base64;
            cd.Save();

            //XmlElement config = cd.GetXml("XmlData", null);
            //if (config == null)
            //{
            //    config = new XmlDocument().CreateElement("學生學生證");
            //}

            //config.SetAttribute("Default", _isDefaultTemplate.ToString());

            //XmlElement customize = config.OwnerDocument.CreateElement("CustomizeTemplateOld");

            //customize.InnerText = base64;
            //if (config.SelectSingleNode("CustomizeTemplateOld") == null)
            //    config.AppendChild(customize);
            //else
            //    config.ReplaceChild(customize, config.SelectSingleNode("CustomizeTemplateOld"));

            //cd.SetXml("XmlData", config);
            //cd.Save();
        }

        /// <summary>
        /// 儲存使用者自訂樣版
        /// </summary>
        public void SaveUserDefineTemplate()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "自訂學生證.doc";
            sfd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    if (_buffer != null && Aspose.Words.Document.DetectFileFormat(new MemoryStream(_buffer)) == Aspose.Words.LoadFormat.Doc)
                        fs.Write(_buffer, 0, _buffer.Length);
                    else
                        fs.Write(defalutTemplate, 0, defalutTemplate.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


        }

        /// <summary>
        /// 列印資料
        /// </summary>
        /// <param name="StudentIDList"></param>        
        public bool PrintData(List<string> StudentIDList, bool isDefaultTemplate, bool isUseSystemPhoto, int StudentCount)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("開始產生資料..");
            GetUserDefineTemplateFromSystem();
            _StudentList = DAL.DALTransfer.GetStudentEntityList(StudentIDList);
            _isDefaultTemplate = isDefaultTemplate;
            _isUseSystemPhoto = isUseSystemPhoto;
            _StudentCount = StudentCount;

            bkWorkPrint = new BackgroundWorker();
            bkWorkPrint.DoWork += new DoWorkEventHandler(bkWorkPrint_DoWork);
            bkWorkPrint.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkWorkPrint_RunWorkerCompleted);
            bkWorkPrint.ProgressChanged += new ProgressChangedEventHandler(bkWorkPrint_ProgressChanged);
            bkWorkPrint.WorkerReportsProgress = true;
            bkWorkPrint.RunWorkerAsync();
            return true;
        }

        void bkWorkPrint_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("資料產生中..");
        }

        void bkWorkPrint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Document doc = (Document)e.Result;

            string filePath = Application.StartupPath + "\\Reports\\";
            string fileName = "學生證.doc";
            try
            {
                doc.Save(filePath + fileName, SaveFormat.Doc);
                System.Diagnostics.Process.Start(filePath + fileName);
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = fileName;
                sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        doc.Save(sd.FileName, Aspose.Words.SaveFormat.Doc);
                        System.Diagnostics.Process.Start(sd.FileName);
                    }
                    catch
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            //SaveTemplateToSystem();
            SaveSelectItem();
            FISCA.Presentation.MotherForm.SetStatusBarMessage("產生完成.");
        }

        void bkWorkPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            Document doc = new Document();
            doc.Sections.Clear();
            int cot = 0;
            Document docTemplate = null;
            List<string> rptKeys = new List<string>();
            List<object> rptValues = new List<object>();

            DataTable dt = new DataTable();
            if (UseCountyType == CountyType.新竹)
            {
                dt.Columns.Add("姓名", typeof(string));
                dt.Columns.Add("學校名稱", typeof(string));
                dt.Columns.Add("學號", typeof(string));
                dt.Columns.Add("民國生日", typeof(string));
                dt.Columns.Add("出生地", typeof(string));
                dt.Columns.Add("監護人姓名", typeof(string));
                dt.Columns.Add("性別", typeof(string));
                if (_isUseSystemPhoto == true)
                    dt.Columns.Add("照片", typeof(byte[]));
                else
                    dt.Columns.Add("照片", typeof(string));

                dt.Columns.Add("身分證號", typeof(string));
                dt.Columns.Add("條碼", typeof(string));
                dt.Columns.Add("生日", typeof(string));

                foreach (DAL.StudentEntity se in _StudentList)
                {
                    if (_isUseSystemPhoto == true)
                    {
                        dt.Rows.Add(se.StudentName, se.SchoolChineseName, se.StudentNumber, se.GetChineseBirthday(), se.BirthPlace, se.Parent1, se.Gender, se.GetPhotoImage(), se.IDNumber, se.StudentNumber, se.Birthday.ToShortDateString());
                    }
                    else
                    {
                        dt.Rows.Add(se.StudentName, se.SchoolChineseName, se.StudentNumber, se.GetChineseBirthday(), se.BirthPlace, se.Parent1, se.Gender, "脫帽半身正面一吋照片", se.IDNumber, se.StudentNumber, se.Birthday.ToShortDateString());
                    }
                }
            }

            else
            {
                // 高雄用
                dt.Columns.Add("學校名稱", typeof(string));
                dt.Columns.Add("學號", typeof(string));
                dt.Columns.Add("姓名", typeof(string));
                dt.Columns.Add("性別", typeof(string));
                dt.Columns.Add("生日", typeof(string));
                dt.Columns.Add("監護人姓名", typeof(string));
                dt.Columns.Add("出生地", typeof(string));
                if (_isUseSystemPhoto == true)
                    dt.Columns.Add("照片", typeof(byte[]));
                else
                    dt.Columns.Add("照片", typeof(string));
                dt.Columns.Add("身分證號", typeof(string));
                dt.Columns.Add("條碼", typeof(string));
                dt.Columns.Add("民國生日", typeof(string));
                foreach (DAL.StudentEntity se in _StudentList)
                {
                    if (_isUseSystemPhoto == true)
                    {
                        dt.Rows.Add(se.SchoolChineseName, se.StudentNumber, se.StudentName, se.Gender, se.Birthday.ToShortDateString(), se.Parent1, se.BirthPlace, se.GetPhotoImage(), se.IDNumber, se.StudentNumber, se.GetChineseBirthday());
                    }
                    else
                    {
                        dt.Rows.Add(se.SchoolChineseName, se.StudentNumber, se.StudentName, se.Gender, se.Birthday.ToShortDateString(), se.Parent1, se.BirthPlace, "", se.IDNumber, se.StudentNumber, se.GetChineseBirthday());

                    }
                }

            }



            if (_isDefaultTemplate)
                docTemplate = new Document(new MemoryStream(defalutTemplate));
            else
            {
                if (_buffer.Length < 1)
                    docTemplate = new Document(new MemoryStream(defalutTemplate));
                else
                    docTemplate = new Document(new MemoryStream(_buffer));
            }

            docTemplate.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
            docTemplate.MailMerge.RemoveEmptyParagraphs = true;
            docTemplate.MailMerge.Execute(dt);
            DocumentBuilder bulider = new DocumentBuilder(docTemplate);


            int N = _StudentCount;

            if (UseCountyType == CountyType.新竹)
            {
                N = N % 6;
                N = 6 - N;
                int M = 0;
                while (M < N)
                {
                    if (bulider.MoveToMergeField("學校名稱") == true)
                    {

                        (bulider.CurrentParagraph.ParentNode as Cell).ParentRow.ParentTable.Remove();

                    }
                    M++;
                }
            }
            else
            {
                N = N % 8;
                N = 8 - N;
                int M = 0;
                while (M < N)
                {
                    if (bulider.MoveToMergeField("學校名稱") == true)
                    {

                        (bulider.CurrentParagraph.ParentNode as Cell).ParentRow.ParentTable.Remove();

                    }
                    M++;
                }

            }


            bkWorkPrint.ReportProgress((int)(((double)cot++ * 100.0) / (double)_StudentList.Count));
            e.Result = docTemplate;
        }

        void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
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
                double shapeHeight = 0;
                double shapeWidth = 0;
                if (UseCountyType == CountyType.高雄)
                {
                    photoShape.WrapType = WrapType.Inline;//設定文繞圖

                    //resize
                    //double origSizeRatio = photoShape.ImageData.ImageSize.HeightPoints / photoShape.ImageData.ImageSize.WidthPoints;
                    //Cell curCell = photoBuilder.CurrentParagraph.ParentNode as Cell;
                    //shapeHeight = (curCell.ParentNode as Row).RowFormat.Height * 4;
                    //shapeWidth = curCell.CellFormat.Width;

                    //if ((shapeHeight / shapeWidth) < origSizeRatio)
                    //    shapeWidth = shapeHeight / origSizeRatio;
                    //else
                    //    shapeHeight = shapeWidth * origSizeRatio;

                    double origSizeRatio = photoShape.ImageData.ImageSize.HeightPoints / photoShape.ImageData.ImageSize.WidthPoints;
                    Cell curCell = photoBuilder.CurrentParagraph.ParentNode as Cell;
                    //shapeHeight = (curCell.ParentNode as Row).RowFormat.Height * 4;
                    shapeHeight = (curCell.ParentNode as Row).RowFormat.Height;
                    shapeWidth = curCell.CellFormat.Width;
                }

                if (UseCountyType == CountyType.新竹)
                {
                    photoShape.WrapType = WrapType.Inline;//設定文繞圖

                    //resize
                    double origSizeRatio = photoShape.ImageData.ImageSize.HeightPoints / photoShape.ImageData.ImageSize.WidthPoints;
                    Cell curCell = photoBuilder.CurrentParagraph.ParentNode as Cell;
                    //shapeHeight = (curCell.ParentNode as Row).RowFormat.Height * 4;
                    shapeHeight = (curCell.ParentNode as Row).RowFormat.Height;
                    shapeWidth = curCell.CellFormat.Width;

                    //if ((shapeHeight / shapeWidth) < origSizeRatio)
                    //    shapeWidth = shapeHeight / origSizeRatio;
                    //else
                    //    shapeHeight = shapeWidth * origSizeRatio;
                }

                photoShape.Height = shapeHeight;
                photoShape.Width = shapeWidth ;
                //photoShape.Top = 28;


                photoBuilder.InsertNode(photoShape);
            }

            if (e.FieldName == "條碼")
            {
                DocumentBuilder builder = new DocumentBuilder(e.Document);
                builder.MoveToField(e.Field, true);//將游標移到條碼所在欄位
                e.Field.Remove();//將原先的合併欄位刪除

                BarCodeBuilder bb = new BarCodeBuilder();
                if (e.FieldValue != null)
                    if (e.FieldValue.ToString() != "")
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

    }
}
