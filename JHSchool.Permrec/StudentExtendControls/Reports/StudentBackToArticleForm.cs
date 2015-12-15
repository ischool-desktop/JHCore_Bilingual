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
    public partial class StudentBackToArticleForm : FISCA.Presentation.Controls.BaseForm 
    {
        StudentBackToArticleManager sbtm;
        bool isDefalutTemplate=true ;
        public StudentBackToArticleForm()
        {            
            InitializeComponent();
            sbtm = new StudentBackToArticleManager();
            if (sbtm.GetisDefaultTemplate())
                cbxDefault.Checked = true;
            else
                cbxUserDefine.Checked = true;
        }

        private void lnkDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sbtm.SaveDefaulTemplate();
        }

        private void lnkUserDefine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sbtm.SaveUserDefineTemplate();
        }

        private void lnkUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sbtm.SetUserDefineTemplateToSystem();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (cbxDefault.Checked == false && cbxUserDefine.Checked == false && Student.Instance.SelectedKeys.Count <1)
                return;

            if (cbxDefault.Checked)
                isDefalutTemplate = true;
            else
                isDefalutTemplate = false;

            cmdPrint.Enabled = false;
            sbtm.PrintData(Student.Instance.SelectedKeys, isDefalutTemplate);            
            PermRecLogProcess prlp = new PermRecLogProcess();
            prlp.SaveLog("學生.報表", "列印", "列印" + Student.Instance.SelectedKeys.Count + "筆轉出回條資料。");
            cmdPrint.Enabled = true;
        }

    }
}
