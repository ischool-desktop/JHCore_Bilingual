using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation;

namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    public partial class ClassStudentTagCountRpt : FISCA.Presentation.Controls.BaseForm
    {
        List<DAL.StudentEntity> StudentEntityList;
        private bool _lock1 = false;
        public ClassStudentTagCountRpt()
        {
            InitializeComponent();

            SetlstVwStudTagItems();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetlstVwStudTagItems()
        {
            Dictionary<string, string> Items = DAL.Transfer.GetStudentTagFullName();
            lstVwStudTagItems.Clear();
            foreach (KeyValuePair<string, string> val in Items)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = val.Key;
                lvi.Text = val.Value;
                lstVwStudTagItems.Items.Add(lvi);
            }

        }

        private void cbxSelectAll_CheckValueChanged(object sender, EventArgs e)
        {
            if (_lock1)
            {
                if (cbxSelectAll.Checked)
                    foreach (ListViewItem lvi in lstVwStudTagItems.Items)
                        lvi.Checked = true;

                else
                    foreach (ListViewItem lvi in lstVwStudTagItems.Items)
                        lvi.Checked = false;
            }
            _lock1 = false;
        }

        private void cbxSelectAll_Click(object sender, EventArgs e)
        {
            _lock1 = true;
        }

        private void lstVwStudTagItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_lock1 == false)
            {
                if (lstVwStudTagItems.Items.Count == lstVwStudTagItems.CheckedItems.Count)
                    cbxSelectAll.Checked = true;
                else
                    cbxSelectAll.Checked = false;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // 當沒有勾選時
            if (lstVwStudTagItems.CheckedItems.Count < 1)
                return;

            btnExport.Enabled = false;
            // 取得畫面資料
            List<string> SelectedItems = new List<string>();
            foreach (ListViewItem lvi in lstVwStudTagItems.CheckedItems)
                SelectedItems.Add(lvi.Text);
            Dictionary<string, string> SelectTagItemName = new Dictionary<string, string>();

            foreach (ListViewItem lvi in lstVwStudTagItems.CheckedItems)
                SelectTagItemName.Add((string)lvi.Tag, lvi.Text);


            BackgroundWorker bgWork = new BackgroundWorker();
            bgWork.DoWork += new DoWorkEventHandler(bgWork_DoWork);
            bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWork_RunWorkerCompleted);
            object[] Data = new object[2] { SelectedItems, SelectTagItemName };
            bgWork.RunWorkerAsync(Data);
        }

        void bgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // 呼叫報表處理
            ClassCellReportManger ccrm = new ClassCellReportManger();
            ccrm.ProcessClassStudentTagCount("類別統計", "類別統計", e.Result as DAL.StudentTagCounter);

            btnExport.Enabled = true;
        }

        void bgWork_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arg = (object[])e.Argument;
            List<string> SelectedItems = (List<string>)arg[0];
            Dictionary<string, string> SelectTagItemName = (Dictionary<string, string>)arg[1];

            //取得學生資料
            StudentEntityList = DAL.Transfer.GetSelectStudentEntitys(Class.Instance.SelectedList);
            // 填入 Student Tag
            StudentEntityList = DAL.Transfer.FillStudentTag(StudentEntityList, SelectTagItemName);

            // 呼叫 counter
            DAL.StudentTagCounter stc = new JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentTagCounter();
            stc.SetStudentTagCountItem(SelectedItems, Class.Instance.SelectedList);

            // 放入值計算
            foreach (DAL.StudentEntity se in StudentEntityList)
            {
                stc.AddCount(se, true);
                stc.AddCount(se, false);
            }

            e.Result = stc;
        }
    }
}
