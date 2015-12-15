using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation;
using System.IO;
using FISCA.LogAgent;

namespace JHSchool.Permrec.EduAdminExtendCotnrols.EducationDataCreator
{
    public partial class EducationCodeCreateForm : FISCA.Presentation.Controls.BaseForm
    {
        List<DAL.StudentEntity> _StudentEntityList;        
        
        public EducationCodeCreateForm()
        {
            InitializeComponent();

            // 預設新生
            cbxEntStudent.Checked = true;
        }

        private void btnExport_Click(object sender, EventArgs e)        
        {
            List<string> StudentIDs = new List<string>();
            btnExport.Enabled = false;
            // 取得學生 ID

            foreach (StudentRecord studRec in Student.Instance.SelectedList)
                StudentIDs.Add(studRec.ID);

            object[] data = new object[1] { StudentIDs };
            BackgroundWorker bwWork = new BackgroundWorker();
            bwWork.DoWork += new DoWorkEventHandler(bwWork_DoWork);
            bwWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwWork_RunWorkerCompleted);
            bwWork.RunWorkerAsync(data);

            //ApplicationLog.Log("產生教育程度檔", "產生","完成.共產生筆學生資料");
            ApplicationLog.CreateLogSaverInstance().Log("產生教育程度檔", "產生", "完成.共產生筆學生資料");
        }

        void bwWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<string> _ExportText=(List<string>) e.Result;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "存檔";
            sfd.FileName = "教育程度檔";
            sfd.AddExtension = true;
            sfd.Filter = "文字檔 (*.txt)|*.txt|所有檔案 (*.*)|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 需要轉換成 Big5
                    StreamWriter sw = new StreamWriter(sfd.FileName,false,Encoding.GetEncoding(950));
                    foreach (string str in _ExportText)
                    {
                        sw.WriteLine(str);
                    }
                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("完成.共產生"+ _ExportText.Count +"筆學生資料");
                    PermRecLogProcess prlp = new PermRecLogProcess();
                    prlp.SaveLog("學生.學生教育程度資料", "產生", "共產生" + _ExportText.Count + "筆學生教育程度資料.");
                }
                catch
                {
                    MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
            }

            
            
            btnExport.Enabled = true;
        }

        void bwWork_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arg = (object[])e.Argument;
            List<string> StudentIDs = (List<string>)arg[0];

            _StudentEntityList = DAL.Transfer.GetStudentEntityListByStudentIDList(StudentIDs);

            EducationCodeCreator ecc = new EducationCodeCreator();
            ecc.AddStudentList(_StudentEntityList);
            if (cbxGraduate.Checked)
                ecc.SetStatus(EducationCodeCreator.SelectType.畢業);
            else
                ecc.SetStatus(EducationCodeCreator.SelectType.新生);
            ecc.CheckErrorData();
            e.Result = ecc.GetExportText();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}
