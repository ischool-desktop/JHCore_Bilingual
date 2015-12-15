using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Common;

namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    public partial class ClassParentJobPercentageRpt : BaseForm
    {
        private bool _lock1;
        private List<DAL.StudentEntity> _SelectStudentEntityList;
        private List<string> _FatherJobItems;
        private List<string> _MotherJobItems;
        private List<string> _CustodianItems;
        private DAL.StudentParentJobCounter.ParentJobType _SelectParentJobType;

        public ClassParentJobPercentageRpt()
        {
            InitializeComponent();
            cbxParentType.Items.Add("父親");
            cbxParentType.Items.Add("母親");
            cbxParentType.Items.Add("監護人");

            _SelectStudentEntityList = new List<JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentEntity>();
            _FatherJobItems = new List<string>();
            _MotherJobItems = new List<string>();
            _CustodianItems = new List<string>();

            // 取得所選學生資料
            _SelectStudentEntityList = DAL.Transfer.GetSelectStudentEntitys(Class.Instance.SelectedList);
            _SelectStudentEntityList = DAL.Transfer.GetClassStudentParentJob(_SelectStudentEntityList);

            foreach (DAL.StudentEntity se in _SelectStudentEntityList)
            {
                if(!string.IsNullOrEmpty(se.FatherJob ))
                if (!_FatherJobItems.Contains(se.FatherJob))
                    _FatherJobItems.Add(se.FatherJob);

                if(!string.IsNullOrEmpty(se.MotherJob))
                if (!_MotherJobItems.Contains(se.MotherJob))
                    _MotherJobItems.Add(se.MotherJob);

                if(!string.IsNullOrEmpty(se.CustodianJob ))
                if (!_CustodianItems.Contains(se.CustodianJob))
                    _CustodianItems.Add(se.CustodianJob);

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstVwJobItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_lock1 == false)
            {
                if (lstVwJobItems.Items.Count == lstVwJobItems.CheckedItems.Count)
                    cbxSelectAll.Checked = true;
                else
                    cbxSelectAll.Checked = false;
            }
        }


        private void cbxSelectAll_CheckValueChanged(object sender, EventArgs e)
        {
            if (_lock1)
            {
                if (cbxSelectAll.Checked)
                    foreach (ListViewItem lvi in lstVwJobItems.Items)
                        lvi.Checked = true;

                else
                    foreach (ListViewItem lvi in lstVwJobItems.Items)
                        lvi.Checked = false;
            }
            _lock1 = false;
        }

        private void cbxSelectAll_Click(object sender, EventArgs e)
        {
            _lock1 = true;
        }

        private void cbxParentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstVwJobItems.Items.Clear();

            if (cbxParentType.Text == "父親")
            {
                foreach (string str in _FatherJobItems)
                    lstVwJobItems.Items.Add(str);
                _SelectParentJobType = JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentParentJobCounter.ParentJobType.父親;
            }

            if (cbxParentType.Text == "母親")
            {
                foreach (string str in _MotherJobItems)
                    lstVwJobItems.Items.Add(str);
                _SelectParentJobType = JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentParentJobCounter.ParentJobType.母親;
            }

            if (cbxParentType.Text == "監護人")
            {
                foreach (string str in _CustodianItems)
                    lstVwJobItems.Items.Add(str);
                _SelectParentJobType = JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentParentJobCounter.ParentJobType.監護人;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if(lstVwJobItems.CheckedItems.Count <1)
                return ;
            List<string> SelectItemStr = new List<string>();

            foreach (ListViewItem lvi in lstVwJobItems.CheckedItems)
                SelectItemStr.Add(lvi.Text);

            DAL.StudentParentJobCounter StudParenJobCounter = new JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentParentJobCounter();
            foreach (DAL.StudentEntity se in _SelectStudentEntityList)
                StudParenJobCounter.AddParentJob(se);
            
            

            StudParenJobCounter.SetGradeClassStudCount(Class.Instance.SelectedList);
            ClassCellReportManger ccrm = new ClassCellReportManger();
            ccrm.ProcessClassStudParentJobCount("職業統計","職業統計",StudParenJobCounter,_SelectParentJobType,SelectItemStr);

        }
    }
}
