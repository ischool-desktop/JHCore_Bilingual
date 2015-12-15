using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    public partial class ClassStudentListRptForm : FISCA.Presentation.Controls.BaseForm
    {
        public ClassStudentListRptForm()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (lstSelectItem.CheckedItems.Count > 0)
            {
                ClassStudentListRptManager sslrm = new ClassStudentListRptManager();
                foreach (ListViewItem lvi in lstSelectItem.CheckedItems)
                    sslrm.selectItems.Add(lvi.Text);
                sslrm.copyCot = intCot.Value;
                sslrm.ExportData();
            }
            else
                FISCA.Presentation.Controls.MsgBox.Show("請選擇欄位.");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClassStudentListRptForm_Load(object sender, EventArgs e)
        {

            lstSelectItem.Items.Add("座號");
            lstSelectItem.Items.Add("學號");            
            lstSelectItem.Items.Add("姓名");
            lstSelectItem.Items.Add("性別");
            lstSelectItem.Items.Add("備註");

            foreach (ListViewItem lvi in lstSelectItem.Items)
                lvi.Checked = true;
        }


    }
}
