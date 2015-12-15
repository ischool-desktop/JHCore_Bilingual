using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using FCode = Framework.Security.FeatureCodeAttribute;
using JHSchool.Data;
using FISCA.Presentation;

namespace JHSchool.Permrec.ClassExtendControls
{
    [FCode("JHSchool.Class.Detail0030", "班級學生資訊")]
    public partial class ClassStudentItem : FISCA.Presentation.DetailContent 
    {
        private bool _isBGWorkBusy=false;
        private BackgroundWorker _BGWorker;
        private List<JHStudentRecord> _CurrentStudentList;
        private Dictionary<string,JHPhoneRecord> _CurrentPhoneDic;

        public ClassStudentItem()
        {
            InitializeComponent();
            Group = "班級學生資訊";
            _BGWorker = new BackgroundWorker();
            _BGWorker.DoWork += new DoWorkEventHandler(_BGWorker_DoWork);
            _BGWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWorker_RunWorkerCompleted);
            _CurrentStudentList = new List<JHStudentRecord>();
            _CurrentPhoneDic = new Dictionary<string, JHPhoneRecord>();
            
        }

        void _BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_isBGWorkBusy)
            {
                _isBGWorkBusy = false;
                _BGWorker.RunWorkerAsync();
                return;
            }
            BindDataToForm();
        }

        void _BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得一般或輟學
            _CurrentStudentList.Clear();
            _CurrentPhoneDic.Clear();

            foreach (JHStudentRecord studRec in JHStudent.SelectByClassID(PrimaryKey))
                if (studRec.Status == K12.Data.StudentRecord.StudentStatus.一般 || studRec.Status == K12.Data.StudentRecord.StudentStatus.輟學)
                    _CurrentStudentList.Add(studRec);

            // 按座號排序
            _CurrentStudentList.Sort(new Comparison <JHStudentRecord>(ClassStudSeatNoSorter)); 

            // 取得學生電話
            foreach (JHPhoneRecord phoneRec in JHPhone.SelectByStudents(_CurrentStudentList))
                _CurrentPhoneDic.Add(phoneRec.RefStudentID, phoneRec);

        }

        public DetailContent GetContent()
        {
            return new ClassStudentItem();
        }

        
        // 排序座號用
        private int ClassStudSeatNoSorter(JHStudentRecord x, JHStudentRecord y)
        {
            // 當沒座號排後
            int xSeatNo = 99,ySeatNo = 99;

            if (x.SeatNo.HasValue)
                xSeatNo = x.SeatNo.Value;

            if (y.SeatNo.HasValue)
                ySeatNo = y.SeatNo.Value;

            return xSeatNo.CompareTo(ySeatNo);        
        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            this.Loading = true;
            if (_BGWorker.IsBusy)
                _isBGWorkBusy = true;
            else
                _BGWorker.RunWorkerAsync();

        }

        private void BindDataToForm()
        { 
            lvwStudent.Items.Clear();
            lvwStudent.SuspendLayout();

            foreach (JHStudentRecord var in _CurrentStudentList)
            {
                ListViewItem item;
                if (var.SeatNo.HasValue)
                    item=lvwStudent.Items.Add(var.SeatNo.Value+"");
                else
                    item=lvwStudent.Items.Add(" ");

                item.SubItems.Add(var.Name);
                item.SubItems.Add(var.StudentNumber);

                if (_CurrentPhoneDic.ContainsKey(var.ID))
                {
                    item.SubItems.Add(_CurrentPhoneDic[var.ID].Permanent);
                    item.SubItems.Add(_CurrentPhoneDic[var.ID].Contact);
                }
                
                item.SubItems.Add(var.Status.ToString ());

                item.Tag = var.ID;
            }

            lvwStudent.ResumeLayout();
            this.Loading = false;
        }

        private void lvwStudent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwStudent.SelectedItems.Count < 1) return;

            ListViewItem item = lvwStudent.SelectedItems[0];
            string studentid = item.Tag.ToString();
            Student.Instance.PopupDetailPane(studentid);
        }
    }
}
