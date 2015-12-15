using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JHSchool.Permrec.StudentExtendControls.Reports
{
    public partial class StudentIDCardForm : FISCA.Presentation.Controls.BaseForm 
    {
        StudentIDCardManager sidm;
        bool isDefalutTemplate = true;
        bool isUseSystemPhoto = false;

        public enum UseModuleType { 學生, 班級 };
        private UseModuleType _UseModule;

        public StudentIDCardForm(UseModuleType UseModule)
        {
            InitializeComponent();
            sidm = new StudentIDCardManager();
            if (sidm.GetisDefaultTemplate())
                cbxDefault.Checked = true;
            else
                cbxUserDefine.Checked = true;
            _UseModule = UseModule;


                
        }

        private void lnkDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sidm.SaveDefaulTemplate();
        }

        private void lnkUserDefine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sidm.SaveUserDefineTemplate();
        }

        private void lnkUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sidm.SetUserDefineTemplateToSystem();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (cbxDefault.Checked == false && cbxUserDefine.Checked == false && Student.Instance.SelectedKeys.Count < 1)
                return;

            if (cbxDefault.Checked)
                isDefalutTemplate = true;
            else
                isDefalutTemplate = false;

            if (chkUseSystemPhoto.Checked)
            {
                isUseSystemPhoto = true;
                DAL.DALTransfer.UseStudPhotp = true;
            }
            else
            {
                isUseSystemPhoto = false;
                DAL.DALTransfer.UseStudPhotp = false;
            }

            cmdPrint.Enabled = false;
            bool isPrintFinish = false;

            int LogCot = 0;

            if (_UseModule == UseModuleType.學生)
            {
                isPrintFinish = sidm.PrintData(Student.Instance.SelectedKeys, isDefalutTemplate, isUseSystemPhoto, Student.Instance.SelectedKeys.Count);
                LogCot = Student.Instance.SelectedKeys.Count;
            }

            if (_UseModule == UseModuleType.班級)
            {
                List<string> StudentIDList = new List<string>();
                foreach (ClassRecord cr in Class.Instance.SelectedList)
                {
                    foreach (StudentRecord studRec in cr.Students.GetStatusStudents("一般"))
                        StudentIDList.Add(studRec.ID);

                    foreach (StudentRecord studRec in cr.Students.GetStatusStudents("輟學"))
                        StudentIDList.Add(studRec.ID);                
                }
                isPrintFinish = sidm.PrintData(StudentIDList, isDefalutTemplate, isUseSystemPhoto,StudentIDList.Count);
                LogCot = StudentIDList.Count;
            }

            
            if (isPrintFinish)
            {
                PermRecLogProcess prlp = new PermRecLogProcess();
                prlp.SaveLog("學生.報表", "列印", "列印"+LogCot+"筆學生證資料。");
                cmdPrint.Enabled = true;
            }
        }
    }
}
