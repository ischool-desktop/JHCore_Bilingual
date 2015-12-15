using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Aspose.Words;
using Framework;
using System.Windows.Forms;
using System.ComponentModel;
using Aspose.Words.Drawing;

namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    // 處理學生轉出
    class StudentTransExportManager
    {
        List<DAL.StudTransExportEntity> StudTransExportList;
        private BackgroundWorker bkWorkPrint;
        private bool _isDefaultTemplate = true;
        private byte[] _buffer = null;

        private byte[] defalutTemplate;
        private string base64 = "";        
        private string _Semester = "";
        private string _CertDoc = "";
        private string _CertNo = "";


        public StudentTransExportManager()
        {
            StudTransExportList = new List<JHSchool.Permrec.StudentExtendControls.Reports.DAL.StudTransExportEntity>();
            GetUserDefineTemplateFromSystem();

            //// 判斷使用高雄樣版
            //if (Program.ModuleType == Program.ModuleFlag.KaoHsiung)
                defalutTemplate = JHSchool.Permrec.StudentExtendControls.Reports.RptResource.高雄轉學證明書;

        
        }

        /// <summary>
        /// 取得是否讀取設樣版
        /// </summary>
        public bool GetisDefaultTemplate()
        {
            return _isDefaultTemplate;
        }

        /// <summary>
        /// 設定學期
        /// </summary>
        /// <param name="Semester"></param>
        public void SetSemester(string Semester, bool isUseNo)
        {
            if (isUseNo)
            {
                _Semester = Semester;
            }
            else
            {
                if (Semester == "1")
                    _Semester = "一";

                if (Semester == "2")
                    _Semester = "二";
            }
        }

        /// <summary>
        /// 設定文號
        /// </summary>
        /// <param name="CertDoc"></param>
        public void SetCertDoc(string CertDoc)
        {
            _CertDoc = CertDoc;
        }

        /// <summary>
        /// 設定字第
        /// </summary>
        /// <param name="CertNo"></param>
        public void SetCertNo(string CertNo)
        {
            _CertNo = CertNo;
        }

        // 取得系統今年中文年
        private string GetSystemTodayChineseDate()
        {
            return "中華民國" + (DateTime.Now.Year - 1911) + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日";
        }

               /// <summary>
        /// 取得使用者自訂範本
        /// </summary>
        public void GetUserDefineTemplateFromSystem()
        {
            School.Configuration.Sync("轉學證明書_無成績");
            ConfigData cd = School.Configuration["轉學證明書_無成績"];

            bool.TryParse(cd["Default"], out _isDefaultTemplate);

            if (cd.Contains("CustomizeTemplate"))
            {
                string templateBase64 = cd["CustomizeTemplate"];
                _buffer = Convert.FromBase64String(templateBase64);
            }
            else
            {
                #region 產生空白設定檔
                cd["Default"] = "true";
                cd["CustomizeTemplate"] = "";
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
            sfd.FileName = "轉學證明書(無成績).doc";
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
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ofd.Title = "選擇自訂的轉學證明書範本";
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
                        fs.Close();
                        SaveTemplateToSystem();
                        PermRecLogProcess prlp = new PermRecLogProcess();
                        prlp.SaveLog("學生.報表", "上傳", "上傳轉學證明書樣版.");

                        MsgBox.Show("上傳成功。");
                    }
                    else
                        MsgBox.Show("上傳檔案格式不符");
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "開啟檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }

        /// <summary>
        /// 儲存使用者定義
        /// </summary>
        public void SaveTemplateToSystem()
        {

            ConfigData cd = School.Configuration["轉學證明書_無成績"];

            cd["Default"] = _isDefaultTemplate.ToString();
            cd["CustomizeTemplate"] = base64;
            cd.Save();
        }

        /// <summary>
        /// 儲存使用者自訂樣版
        /// </summary>
        public void SaveUserDefineTemplate()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "自訂轉學證明書(無成績).doc";
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
                    MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


        }

        /// <summary>
        /// 列印資料
        /// </summary>
        /// <param name="StudentIDList"></param>        
        public void PrintData(List<string> StudentIDList, bool isDefaultTemplate)
        {
            GetUserDefineTemplateFromSystem();
            StudTransExportList = DAL.DALTransfer.GetStudTransExportEntityList(StudentIDList);
            _isDefaultTemplate = isDefaultTemplate;
            bkWorkPrint = new BackgroundWorker();
            bkWorkPrint.DoWork += new DoWorkEventHandler(bkWorkPrint_DoWork);
            bkWorkPrint.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkWorkPrint_RunWorkerCompleted);
            bkWorkPrint.ProgressChanged += new ProgressChangedEventHandler(bkWorkPrint_ProgressChanged);
            bkWorkPrint.WorkerReportsProgress = true;
            bkWorkPrint.RunWorkerAsync();
        }

        void bkWorkPrint_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("資料產生中..");
        }

        void bkWorkPrint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Document doc = (Document)e.Result;

            string filePath = Application.StartupPath + "\\Reports\\";
            string fileName = "轉學證明書(無成績).doc";
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

                    }
                    catch
                    {
                        MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            SaveSelectItem();
            FISCA.Presentation.MotherForm.SetStatusBarMessage("資料產生完成.");
        }

        private void SaveSelectItem()
        {
            ConfigData cd = School.Configuration["轉學證明書_無成績"];

            cd["Default"] = _isDefaultTemplate.ToString();
            cd.Save();
        }

        void bkWorkPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            Document doc = new Document();
            doc.Sections.Clear();
            int cot = 0;
            foreach (DAL.StudTransExportEntity se in StudTransExportList )
            {
                Dictionary<string, object> baseData = new Dictionary<string, object>();
                baseData.Add("姓名", se.Name);
                baseData.Add("學校名稱", se.SchoolName);
                baseData.Add("校長姓名", se.ChancellorName);
                baseData.Add("入學照片", se.GetPhotoImage ());
                baseData.Add("民國生日", se.GetChineseBirthday ());
                baseData.Add("班級年級", se.ClassGradeYear);
                baseData.Add("學期", _Semester);                
                baseData.Add("今天民國日期", GetSystemTodayChineseDate());
                baseData.Add("今天民國年",(DateTime.Now.Year-1911).ToString());
                baseData.Add("今天民國月", DateTime.Now.Month.ToString ());
                baseData.Add("今天民國日", DateTime.Now.Day.ToString());
                baseData.Add("文號", _CertDoc);
                baseData.Add("第號", _CertNo);
                baseData.Add("異動原因", se.UpdateDesc);
                baseData.Add("新生異動日期", se.GetChineseUR01Date ());
                baseData.Add("新生異動核准中文日期", se.GetChineseUR01CertDate ());
                baseData.Add("新生異動核准文號", se.UR01CertDocNo);
                string strCounty = string.Empty;

                //if (Program.ModuleType == Program.ModuleFlag.HsinChu)
                //    strCounty = "新竹市";

                //if (Program.ModuleType == Program.ModuleFlag.KaoHsiung)
                //    strCounty = "高雄市";

                //if (Program.ModuleType == Program.ModuleFlag.TaiChung)
                //    strCounty = "台中縣";

                strCounty = se.GetSchoolCounty();
                baseData.Add("縣市", strCounty);

                List<string> rptKeys = new List<string>();
                List<object> rptValues = new List<object>();

                foreach (KeyValuePair<string, object> item in baseData)
                {
                    rptKeys.Add(item.Key);
                    rptValues.Add(item.Value);
                }

                Document docTemplate;

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
                docTemplate.MailMerge.Execute(rptKeys.ToArray(), rptValues.ToArray());
                doc.Sections.Add(doc.ImportNode(docTemplate.Sections[0], true));

                bkWorkPrint.ReportProgress((int)(((double)cot++ * 100.0) / (double)StudTransExportList.Count));
            }
            e.Result = doc;
        }

        void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            if (e.FieldName == "入學照片")
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
                photoShape.WrapType = WrapType.Inline;//設定文繞圖

                //resize

                double origSizeRatio = photoShape.ImageData.ImageSize.HeightPoints / photoShape.ImageData.ImageSize.WidthPoints;
                Cell curCell = photoBuilder.CurrentParagraph.ParentNode as Cell;
                shapeHeight = (curCell.ParentNode as Row).RowFormat.Height;
                shapeWidth = curCell.CellFormat.Width;
                photoShape.Height = shapeHeight;
                photoShape.Width = shapeWidth;
                photoBuilder.InsertNode(photoShape);
            }

        }
    }
}
