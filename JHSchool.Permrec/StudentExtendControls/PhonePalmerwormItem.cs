using System;
using System.ComponentModel;
using FISCA.Presentation;
using Framework;
using JHSchool.Data;
using JHSchool.Permrec.Legacy;
using FCode = Framework.Security.FeatureCodeAttribute;

namespace JHSchool.Permrec.StudentExtendControls
{
    [FCode("JHSchool.Student.Detail0082", "電話資料")]
    public partial class PhonePalmerwormItem : FISCA.Presentation.DetailContent
    {
        private JHSchool.Permrec.Legacy.SMSForm smsForm;
        //private SkypeControl.SkypeProxy skypeProxy;
        private string _PermanentPhone = string.Empty;
        private string _ContactPhone = string.Empty;
        private string _SMS = string.Empty;
        private string _OtherPhone1 = string.Empty;
        private string _OtherPhone2 = string.Empty;
        private string _OtherPhone3 = string.Empty;
        private PermRecLogProcess prlp;


        private bool _IsBgBusy = false;
        private BackgroundWorker _bwWork;
        
        private ChangeListener DataListener { get; set; }

        // 電話資訊
        private JHSchool.Data.JHPhoneRecord _PhoneRecord;

        public PhonePalmerwormItem()
            : base()
        {
            InitializeComponent();
            DataListener = new ChangeListener();
            DataListener.Add(new TextBoxSource(txtEverPhone));
            DataListener.Add(new TextBoxSource(txtContactPhone));
            DataListener.Add(new TextBoxSource(txtSMS));
            DataListener.Add(new TextBoxSource(txtOtherPhone));
            DataListener.StatusChanged += new EventHandler<ChangeEventArgs>(DataListener_StatusChanged);


            Group = "電話資料";
            //skypeProxy = new SkypeControl.SkypeProxy();
            //skypeProxy.CountryInfo = new SkypeControl.CountryInfo("886", "台灣");
            //skypeProxy.OnSkypeStatusChange += new SkypeControl.SkypeStatusChangeHandler(skypeProxy_OnSkypeStatusChange);
            //skypeProxy_OnSkypeStatusChange(null, null);

            _bwWork = new BackgroundWorker();
            _bwWork.DoWork += new DoWorkEventHandler(_bwWork_DoWork);
            _bwWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bwWork_RunWorkerCompleted);

            JHPhone.AfterUpdate += new EventHandler<K12.Data.DataChangedEventArgs>(JHPhone_AfterUpdate);
            prlp = new PermRecLogProcess();
            Disposed += new EventHandler(PhonePalmerwormItem_Disposed);
        }

        void PhonePalmerwormItem_Disposed(object sender, EventArgs e)
        {
            JHPhone.AfterUpdate -= new EventHandler<K12.Data.DataChangedEventArgs>(JHPhone_AfterUpdate);
        }

        void JHPhone_AfterUpdate(object sender, K12.Data.DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, K12.Data.DataChangedEventArgs>(JHPhone_AfterUpdate), sender, e);
            }
            else
            {
                if (PrimaryKey != "")
                {
                    if (!_bwWork.IsBusy)
                        _bwWork.RunWorkerAsync();
                }
            }

        }

        void DataListener_StatusChanged(object sender, ChangeEventArgs e)
        {
            if (Framework.User.Acl[GetType()].Editable)
                SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            else
                SaveButtonVisible = false;

            CancelButtonVisible =(e.Status == ValueStatus.Dirty);
        }

        void _bwWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_IsBgBusy)
            {
                _IsBgBusy = false;
                _bwWork.RunWorkerAsync();
                return;
            }
            BindDataToForm();
        }

        void _bwWork_DoWork(object sender, DoWorkEventArgs e)
        {
            _PhoneRecord = JHSchool.Data.JHPhone.SelectByStudentID(PrimaryKey);
        }

        // 儲存資料
        protected override void  OnSaveButtonClick(EventArgs e)
{

        _PhoneRecord.Permanent = _PermanentPhone = txtEverPhone.Text;
        _PhoneRecord.Contact = _ContactPhone=txtContactPhone.Text;
        _PhoneRecord.Cell = _SMS = txtSMS.Text;
        if (btnOthers.Text.EndsWith("1"))
            _OtherPhone1 = txtOtherPhone.Text;
        if (btnOthers.Text.EndsWith("2"))
            _OtherPhone2 = txtOtherPhone.Text;
        if (btnOthers.Text.EndsWith("3"))
            _OtherPhone3 = txtOtherPhone.Text;

        _PhoneRecord.Phone1 = _OtherPhone1;
        _PhoneRecord.Phone2 = _OtherPhone2;
        _PhoneRecord.Phone3 = _OtherPhone3;

        prlp.SetAfterSaveText("戶籍電話", _PermanentPhone);
        prlp.SetAfterSaveText("聯絡電話", _ContactPhone);
        prlp.SetAfterSaveText("行動電話", _SMS);
        prlp.SetAfterSaveText("其他電話1", _OtherPhone1);            
        prlp.SetAfterSaveText("其他電話2", _OtherPhone2);        
        prlp.SetAfterSaveText("其他電話3", _OtherPhone3);
        JHSchool.Data.JHPhone.Update(_PhoneRecord);

    prlp.SetActionBy("學籍", "學生電話資訊");
    prlp.SetAction("修改學生電話資訊");
    JHStudentRecord studRec = JHStudent.SelectByID(PrimaryKey);
    prlp.SetDescTitle("學生姓名:" + studRec.Name + ",學號:" + studRec.StudentNumber + ",");
    Student.Instance.SyncDataBackground(PrimaryKey);
    prlp.SaveLog("", "", "student", PrimaryKey);
    BindDataToForm();
    
    
}

        // 取消資料
        protected override void  OnCancelButtonClick(EventArgs e)
{
    txtEverPhone.Text = _PermanentPhone;
    txtContactPhone.Text = _ContactPhone;
    txtSMS.Text = _SMS;

    if (btnOthers.Text.EndsWith("1"))
        txtOtherPhone.Text = _OtherPhone1;

    if (btnOthers.Text.EndsWith("2"))
        txtOtherPhone.Text = _OtherPhone2;

    if (btnOthers.Text.EndsWith("3"))
        txtOtherPhone.Text = _OtherPhone3;

    SaveButtonVisible = false;
    CancelButtonVisible = false;
	        
}

        // 當更換選擇學生
        protected override void  OnPrimaryKeyChanged(EventArgs e)
{
 	this.Loading =true;
            if(_bwWork.IsBusy )
                _IsBgBusy =true;
            else
                _bwWork.RunWorkerAsync ();

}
        //private void Initialize()
        //{
        //    if (_isInitialized) return;
        //    _isInitialized = true;
        //}


        // 載入資料
        private void BindDataToForm()
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            DataListener.SuspendListen();

            _PermanentPhone = _ContactPhone = _SMS= _OtherPhone1 = _OtherPhone2 = _OtherPhone3 = "";
            txtEverPhone.Text = "";
            txtContactPhone.Text = "";
            txtOtherPhone.Text = "";
            txtSMS.Text = "";

            if(!string.IsNullOrEmpty(_PhoneRecord.Permanent ))
                txtEverPhone.Text = _PermanentPhone = _PhoneRecord.Permanent;
            if(!string.IsNullOrEmpty(_PhoneRecord.Contact ))
                txtContactPhone.Text =_ContactPhone =_PhoneRecord.Contact;
            if (!string.IsNullOrEmpty(_PhoneRecord.Cell))
                txtSMS.Text = _SMS = _PhoneRecord.Cell;
            if (!string.IsNullOrEmpty(_PhoneRecord.Phone1))
                _OtherPhone1 = _PhoneRecord.Phone1;
            if (!string.IsNullOrEmpty(_PhoneRecord.Phone2))
                _OtherPhone2 = _PhoneRecord.Phone2;
            if (!string.IsNullOrEmpty(_PhoneRecord.Phone3))
                _OtherPhone3 = _PhoneRecord.Phone3;

            if (btnOthers.Text.EndsWith("1"))
                txtOtherPhone.Text = _OtherPhone1;

            if (btnOthers.Text.EndsWith("2"))
                txtOtherPhone.Text = _OtherPhone2;

            if (btnOthers.Text.EndsWith("3"))
                txtOtherPhone.Text = _OtherPhone3;

            prlp.SetBeforeSaveText("戶籍電話", _PermanentPhone);
            prlp.SetBeforeSaveText("聯絡電話", _ContactPhone);
            prlp.SetBeforeSaveText("行動電話", _SMS);
            prlp.SetBeforeSaveText("其他電話1", _OtherPhone1);
            prlp.SetBeforeSaveText("其他電話2", _OtherPhone2);
            prlp.SetBeforeSaveText("其他電話3", _OtherPhone3);
            LoadOtherPhone1();
            DataListener.Reset();
            DataListener.ResumeListen();
            this.Loading = false;
            
        }

        //private void txtEverPhone_TextChanged(object sender, EventArgs e)
        //{
        //    OnValueChanged("PermanentPhone", txtEverPhone.Text);
        //}

        //private void txtContactPhone_TextChanged(object sender, EventArgs e)
        //{
        //    OnValueChanged("ContactPhone", txtContactPhone.Text);
        //}

        //private void txtSMS_TextChanged(object sender, EventArgs e)
        //{
        //    OnValueChanged("SMSPhone", txtSMS.Text);
        //}

        //private void txtOtherPhone_TextChanged(object sender, EventArgs e)
        //{
        //    if (btnOthers.Text.EndsWith("1"))
        //    {
        //        _OtherPhone1 = txtOtherPhone.Text;
                
        //    }
        //    else if (btnOthers.Text.EndsWith("2"))
        //    {
        //        _OtherPhone2 = txtOtherPhone.Text;
                
        //    }
        //    else
        //    {
        //        _OtherPhone3  = txtOtherPhone.Text;                
        //    }
        //}

        private void btnOther1_Click(object sender, EventArgs e)
        {
            DataListener.SuspendListen();
            LoadOtherPhone1();
            DataListener.Reset();
            DataListener.ResumeListen();
        }

        private void btnOther2_Click(object sender, EventArgs e)
        {
            DataListener.SuspendListen();
            btnOther1.Enabled = true;
            btnOther2.Enabled = false;
            btnOther3.Enabled = true;
            btnOthers.Text = btnOther2.Text;

            txtOtherPhone.Text = _OtherPhone2;
            DataListener.Reset();
            DataListener.ResumeListen();
        }

        private void btnOther3_Click(object sender, EventArgs e)
        {
            DataListener.SuspendListen();
            btnOther1.Enabled = true;
            btnOther2.Enabled = true;
            btnOther3.Enabled = false;
            btnOthers.Text = btnOther3.Text;

            txtOtherPhone.Text = _OtherPhone3;
            DataListener.Reset();
            DataListener.ResumeListen();
        }

        private void LoadOtherPhone1()
        {
            btnOther1.Enabled = false;
            btnOther2.Enabled = true;
            btnOther3.Enabled = true;
            btnOthers.Text = btnOther1.Text;

            txtOtherPhone.Text = _OtherPhone1;
        }

        //private void CallEverPhone(object sender, EventArgs e)
        //{
        //    skypeProxy.Call(txtEverPhone.Text);
        //}

        //private void CallContactPhone(object sender, EventArgs e)
        //{
        //    skypeProxy.Call(txtContactPhone.Text);
        //}

        //private void CallOtherPhone(object sender, EventArgs e)
        //{
        //    skypeProxy.Call(txtOtherPhone.Text);
        //}

        //void skypeProxy_OnSkypeStatusChange(object theSender, SkypeControl.SkypeStatusChangeEventArgs theEventArgs)
        //{
        //    bool enabled = (skypeProxy.SkypeStatus == SkypeControl.SkypeStatusEnum.Ready);
        //    buttonItem1.Enabled = buttonItem3.Enabled = buttonItem5.Enabled = enabled;
        //    buttonItem2.Enabled = enabled;
        //}

        private void btnPSMS_Click(object sender, EventArgs e)
        {
            if (smsForm == null)
                smsForm = new SMSForm();
            smsForm.SetNumber(this.txtEverPhone.Text);
            smsForm.ShowDialog();
        }

        private void btnCSMS_Click(object sender, EventArgs e)
        {
            if (smsForm == null)
                smsForm = new SMSForm();
            smsForm.SetNumber(this.txtContactPhone.Text);
            smsForm.ShowDialog();
        }

        private void btnOSMS_Click(object sender, EventArgs e)
        {
            if (smsForm == null)
                smsForm = new SMSForm();
            smsForm.SetNumber(this.txtOtherPhone.Text);
            smsForm.ShowDialog();
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            if (smsForm == null)
                smsForm = new SMSForm();
            smsForm.SetNumber(this.txtSMS.Text);
            smsForm.ShowDialog();
        }

        //private void buttonItem2_Click(object sender, EventArgs e)
        //{
        //    skypeProxy.Call(txtSMS.Text);
        //}

        public DetailContent GetContent()
        {
            return new PhonePalmerwormItem();
        }


    }
}
