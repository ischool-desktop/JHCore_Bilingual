using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework;
using JHSchool.Data;
using FCode = Framework.Security.FeatureCodeAttribute;

namespace JHSchool.Permrec.StudentExtendControls
{
    [FCode("JHSchool.Student.PhoneItem", "聯絡電話")]
    public partial class PhoneItem : FISCA.Presentation.DetailContent
    {
        private bool _isBGBusy = false;
        private JHStudentRecord _StudRec;
        JHPhoneRecord _PhoneRec;
        JHParentRecord _ParentRecord;
        private BackgroundWorker _BGWorker;
        private ChangeListener _DataListener { get; set; }
        K12StudentPhoto.PermRecLogProcess prlp;

        public PhoneItem()
        {
            InitializeComponent();
            Group = "聯絡電話";
            _DataListener = new ChangeListener();
            _DataListener.Add(new TextBoxSource(txtHomePhone));
            _DataListener.Add(new TextBoxSource(txtFatherPhone));
            _DataListener.Add(new TextBoxSource(txtMotherPhone));
            _DataListener.Add(new TextBoxSource(txtGuardianPhone));
            _DataListener.StatusChanged += _DataListener_StatusChanged;
            _BGWorker = new BackgroundWorker();
            _BGWorker.DoWork += _BGWorker_DoWork;
            _BGWorker.RunWorkerCompleted += _BGWorker_RunWorkerCompleted;
            prlp = new K12StudentPhoto.PermRecLogProcess();

            JHStudent.AfterChange += JHStudent_AfterChange;
            JHStudent.AfterDelete += new EventHandler<K12.Data.DataChangedEventArgs>(JHStudent_AfterDelete);
            Disposed += new EventHandler(PhonePalmerwormItem_Disposed);

        }

        void PhonePalmerwormItem_Disposed(object sender, EventArgs e)
        {
            JHStudent.AfterChange -= new EventHandler<K12.Data.DataChangedEventArgs>(JHStudent_AfterChange);
            JHStudent.AfterDelete -= new EventHandler<K12.Data.DataChangedEventArgs>(JHStudent_AfterDelete);
        }


        void JHStudent_AfterDelete(object sender, K12.Data.DataChangedEventArgs e)
        {
            JHSchool.Student.Instance.SyncAllBackground();
        }

        void JHStudent_AfterChange(object sender, K12.Data.DataChangedEventArgs e)
        {

            if (InvokeRequired)
            {
                Invoke(new Action<object, K12.Data.DataChangedEventArgs>(JHStudent_AfterChange), sender, e);
            }
            else
            {
                if (PrimaryKey != "")
                {
                    if (!_BGWorker.IsBusy)
                        _BGWorker.RunWorkerAsync();
                }
            }
        }

        void _BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_isBGBusy)
            {
                _isBGBusy = false;
                _BGWorker.RunWorkerAsync();
                return;
            }
            BindDataToForm();
        }

        private void BindDataToForm()
        {
            // 主要加當學生被刪除時檢查
            if (_StudRec != null)
            {
                _DataListener.SuspendListen();
                ClearFormValue();
                LoadDALDataToForm();
                SetBeforeEditLog();
                this.Loading = false;
                SaveButtonVisible = false;
                CancelButtonVisible = false;
                _DataListener.Reset();
                _DataListener.ResumeListen();
            }
        }

        void _BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得學生資料、學生電話、父母電話
            _StudRec = JHStudent.SelectByID(PrimaryKey);
            _PhoneRec = JHPhone.SelectByStudentID(PrimaryKey);
            _ParentRecord = JHParent.SelectByStudentID(PrimaryKey);
        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            this.Loading = true;
            if (_BGWorker.IsBusy)
                _isBGBusy = true;
            else
                _BGWorker.RunWorkerAsync();
        }

        private void ClearFormValue()
        {
            txtHomePhone.Text = txtFatherPhone.Text = txtMotherPhone.Text = txtGuardianPhone.Text = "";
        }
        void _DataListener_StatusChanged(object sender, ChangeEventArgs e)
        {
            SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            CancelButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        private void SetBeforeEditLog()
        {
            prlp.SetBeforeSaveText("住家電話", txtHomePhone.Text);
            prlp.SetBeforeSaveText("父親電話", txtFatherPhone.Text);
            prlp.SetBeforeSaveText("母親電話", txtMotherPhone.Text);
            prlp.SetBeforeSaveText("監護人電話", txtGuardianPhone.Text);
        }

        private void SetAfterEditLog()
        {
            prlp.SetAfterSaveText("住家電話", txtHomePhone.Text);
            prlp.SetAfterSaveText("父親電話", txtFatherPhone.Text);
            prlp.SetAfterSaveText("母親電話", txtMotherPhone.Text);
            prlp.SetAfterSaveText("監護人電話", txtGuardianPhone.Text);

            prlp.SetActionBy("學籍", "學生聯絡電話");
            prlp.SetAction("修改學生聯絡電話");
            prlp.SetDescTitle("姓名:" + _StudRec.Name + ",學號:" + _StudRec.StudentNumber + ",");
            prlp.SaveLog("", "", "Student", PrimaryKey);
        }

        protected override void OnCancelButtonClick(EventArgs e)
        {
            _DataListener.SuspendListen();
            ClearFormValue();
            LoadDALDataToForm();
            _DataListener.Reset();
            _DataListener.ResumeListen();
            SaveButtonVisible = false;
            CancelButtonVisible = false;
        }

        private void LoadDALDataToForm()
        {
            txtHomePhone.Text = _PhoneRec.Contact;
            txtFatherPhone.Text = _ParentRecord.FatherPhone;
            txtMotherPhone.Text = _ParentRecord.MotherPhone;
            txtGuardianPhone.Text = _ParentRecord.CustodianPhone;
        }

        protected override void OnSaveButtonClick(EventArgs e)
        {
            // 儲存資料
            _DataListener.SuspendListen();
            _PhoneRec.Contact = txtHomePhone.Text;
            _ParentRecord.Father.Phone = txtFatherPhone.Text;
            _ParentRecord.Mother.Phone = txtMotherPhone.Text;
            _ParentRecord.Custodian.Phone = txtGuardianPhone.Text;

            JHPhone.Update(_PhoneRec);
            JHParent.Update(_ParentRecord);

            SetAfterEditLog();
            _DataListener.Reset();
            _DataListener.ResumeListen();
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            JHSchool.Student.Instance.SyncDataBackground(PrimaryKey);
        }

    }
}
