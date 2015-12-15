using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Words;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
//using FISCA.Presentation.Controls;
using System.Xml;
using Framework;

namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    class StudentBackToArticleManager
    {
        List<DAL.StudentEntity> _StudentList;
        private BackgroundWorker bkWorkPrint;
        private bool _isDefaultTemplate = true;
        private byte[] _buffer = null;

        private byte[] defalutTemplate;
        private string base64 = "";
        private bool _isUpload = false;
        
        /// <summary>
        /// 取得是否讀取設樣版
        /// </summary>
        public bool GetisDefaultTemplate()
        {
            return _isDefaultTemplate;        
        }

        public StudentBackToArticleManager()
        {
            _StudentList = new List<JHSchool.Permrec.StudentExtendControls.Reports.DAL.StudentEntity>();

            GetUserDefineTemplateFromSystem();
            if (Program.ModuleType == Program.ModuleFlag.HsinChu)
                defalutTemplate = JHSchool.Permrec.StudentExtendControls.Reports.RptResource.轉出回條樣版_新竹_;
            else
                defalutTemplate = JHSchool.Permrec.StudentExtendControls.Reports.RptResource.入學回覆單樣版_高雄_;
        }


        /// <summary>
        /// 取得使用者自訂範本
        /// </summary>
        public void GetUserDefineTemplateFromSystem()
        {
            School.Configuration.Sync("學生轉出回條");
            ConfigData cd = School.Configuration["學生轉出回條"];

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
            sfd.FileName = "轉出回條.doc";
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
            ofd.Title = "選擇自訂的轉出回條範本";
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
                        prlp.SaveLog("學生.報表", "上傳", "上傳學生轉出回條樣版.");
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

            ConfigData cd = School.Configuration["學生轉出回條"];
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
            sfd.FileName = "自訂轉出回條.doc";
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
        public void PrintData(List<string> StudentIDList,bool isDefaultTemplate)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("開始產生資料..");
            GetUserDefineTemplateFromSystem();
            _StudentList = DAL.DALTransfer.GetStudentEntityList(StudentIDList);
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
            string fileName = "轉出回條.doc";
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
            FISCA.Presentation.MotherForm.SetStatusBarMessage("產生完成.");
        }

        private void SaveSelectItem()
        {
            ConfigData cd = School.Configuration["學生轉出回條"];

            cd["Default"] = _isDefaultTemplate.ToString();
            cd.Save();
        }


        void bkWorkPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            Document doc = new Document ();
            doc.Sections.Clear ();
            int cot = 0;
            foreach (DAL.StudentEntity se in _StudentList)
            {
                Dictionary<string, object> baseData = new Dictionary<string, object>();
                baseData.Add("學生姓名", se.StudentName);
                baseData.Add("學校中文名稱", se.SchoolChineseName);
                baseData.Add("學校地址", se.SchoolAddress);
                baseData.Add("學校電話", se.SchoolTelephone );
                baseData.Add("學校傳真", se.SchoolFax);
                baseData.Add("班級年級", se.GradeYear);
                baseData.Add("班級名稱", se.ClassName);
                baseData.Add("座號", se.SeatNo);

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
                    if(_buffer.Length <1)
                        docTemplate = new Document(new MemoryStream(defalutTemplate));
                    else
                        docTemplate = new Document(new MemoryStream(_buffer));
                }
                docTemplate.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
                docTemplate.MailMerge.RemoveEmptyParagraphs = true;
                docTemplate.MailMerge.Execute(rptKeys.ToArray(), rptValues.ToArray());
                doc.Sections.Add(doc.ImportNode(docTemplate.Sections[0],true ));
                bkWorkPrint.ReportProgress((int)(((double)cot++ * 100.0) / (double)_StudentList.Count));
            }
            e.Result = doc;
        }

        void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            
        }
    }
}
