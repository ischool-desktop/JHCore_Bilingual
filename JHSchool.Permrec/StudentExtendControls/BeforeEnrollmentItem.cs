using System;
using System.ComponentModel;
using Framework;
using JHSchool.Data;
using FCode = Framework.Security.FeatureCodeAttribute;
using System.Windows.Forms;

namespace JHSchool.Permrec.StudentExtendControls
{
    [FCode("JHSchool.Student.Detail0100", "前級畢業資訊")]
    internal partial class BeforeEnrollmentItem : FISCA.Presentation.DetailContent
    {
        PermRecLogProcess prlp;
        JHBeforeEnrollmentRecord _BeforeEnrollmentRecord;
        private BackgroundWorker _worker = new BackgroundWorker();
        private ChangeListener listener = new ChangeListener();
        private bool PaddingWorking = false;
        private ErrorProvider epSeatNo = new ErrorProvider();

        public BeforeEnrollmentItem()
            : base()
        {
            InitializeComponent();

            prlp = new PermRecLogProcess ();
            Group = "前級畢業資訊";
        }

        void BeforeEnrollmentItem_Disposed(object sender, EventArgs e)
        {
            JHBeforeEnrollment.AfterUpdate -= new EventHandler<K12.Data.DataChangedEventArgs>(JHBeforeEnrollment_AfterUpdate);
        }

        void JHBeforeEnrollment_AfterUpdate(object sender, K12.Data.DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, K12.Data.DataChangedEventArgs>(JHBeforeEnrollment_AfterUpdate), sender, e);
            }
            else
            {
                if (PrimaryKey != "")
                {
                    if (!_worker.IsBusy)
                        _worker.RunWorkerAsync();
                }
            }
        }

        private void listener_StatusChanged(object sender, ChangeEventArgs e)
        {
            if (Framework.User.Acl[GetType()].Editable)
                SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            else
                SaveButtonVisible = false;

            CancelButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
             GetData();
        }

        /* 規格。
        <BeforeEnrollment>
        <School/>
        <SchoolLocation/>
        <ClassName/>
        <SeatNo/>
        <Memo/>
        </BeforeEnrollment>
         */
        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (PaddingWorking)
            {
                PaddingWorking = false;
                _worker.RunWorkerAsync();
            }
            else
            {
                FillData();
            }
        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;

            if (!_worker.IsBusy)
                _worker.RunWorkerAsync();
            else
                PaddingWorking = true;
        }

        protected override void OnCancelButtonClick(EventArgs e)
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            GetData();
            FillData();            
        }

        protected override void OnSaveButtonClick(EventArgs e)
        {
            _BeforeEnrollmentRecord.School = txtSchool.Text;
            _BeforeEnrollmentRecord.SchoolLocation = txtSchoolLocation.Text;
            _BeforeEnrollmentRecord.ClassName = txtClass.Text;
            int intSeatNo;
            if (string.IsNullOrEmpty(txtSeatNo.Text))
            {
                _BeforeEnrollmentRecord.SeatNo = null;
            }
            else
            {
                if (int.TryParse(txtSeatNo.Text, out intSeatNo))
                    _BeforeEnrollmentRecord.SeatNo = intSeatNo;
                else
                {
                    epSeatNo.SetError(txtSeatNo, "請填入數字.");
                    return;
                }
            }
                

                _BeforeEnrollmentRecord.Memo = txtMemo.Text;

            JHBeforeEnrollment.Update(_BeforeEnrollmentRecord);
            listener.Reset();
            SaveButtonVisible = false;
            CancelButtonVisible = SaveButtonVisible;

            prlp.SetAfterSaveText("學校名稱", txtSchool.Text);
            prlp.SetAfterSaveText("所在地", txtSchoolLocation.Text);
            prlp.SetAfterSaveText("班級", txtClass.Text);
            prlp.SetAfterSaveText("座號", txtSeatNo.Text);
            prlp.SetAfterSaveText("備註", txtMemo.Text);
            prlp.SetActionBy("學籍", "學生前級畢業資訊");
            prlp.SetAction("修改學生前級畢業資訊");
            JHStudentRecord studRec = JHStudent.SelectByID(PrimaryKey);
            prlp.SetDescTitle("學生姓名:" + studRec.Name + ",學號:" + studRec.StudentNumber + ",");
            prlp.SaveLog("", "", "student", PrimaryKey);
        }

        private void GetData()
        {
            _BeforeEnrollmentRecord = JHBeforeEnrollment.SelectByStudentID(PrimaryKey);
        }

        private void FillData()
        {
            listener.SuspendListen();

            txtSchool.Text = _BeforeEnrollmentRecord.School;
            txtSchoolLocation.Text = _BeforeEnrollmentRecord.SchoolLocation;
            txtClass.Text = _BeforeEnrollmentRecord.ClassName;
            if (_BeforeEnrollmentRecord.SeatNo.HasValue)
                txtSeatNo.Text = _BeforeEnrollmentRecord.SeatNo.Value + "";
            else
                txtSeatNo.Text = "";
            txtMemo.Text = _BeforeEnrollmentRecord.Memo;
            epSeatNo.Clear();
            listener.ResumeListen();
            listener.Reset();

            prlp.SetBeforeSaveText("學校名稱", txtSchool.Text);
            prlp.SetBeforeSaveText("所在地", txtSchoolLocation.Text );
            prlp.SetBeforeSaveText("班級", txtClass.Text);
            prlp.SetBeforeSaveText("座號", txtSeatNo.Text );
			prlp.SetBeforeSaveText("備註", txtMemo.Text);

        }

        private void txtSeatNo_TextChanged(object sender, EventArgs e)
        {
            int tempNo;
            epSeatNo.Clear();
            if (!string.IsNullOrEmpty(txtSeatNo.Text))
            {
                if (int.TryParse(txtSeatNo.Text, out tempNo))
                {
                    SaveButtonVisible = true;
                    CancelButtonVisible = true;
                    if (_BeforeEnrollmentRecord.SeatNo.HasValue)
                        if (_BeforeEnrollmentRecord.SeatNo.Value == tempNo)
                        {
                            SaveButtonVisible = false;
                            CancelButtonVisible = false;
                        }
                }
                else
                {
                    epSeatNo.SetError(txtSeatNo, "請輸入整數");
                }
            }
        }

        private void BeforeEnrollmentItem_Load(object sender, EventArgs e)
        {
            _BeforeEnrollmentRecord = JHBeforeEnrollment.SelectByStudentID(PrimaryKey);
            listener.Add(new TextBoxSource(txtSchool));
            listener.Add(new TextBoxSource(txtSchoolLocation));
            listener.Add(new TextBoxSource(txtClass));
            listener.Add(new TextBoxSource(txtSeatNo));
            listener.Add(new TextBoxSource(txtMemo));
            listener.Reset();
            listener.StatusChanged += new EventHandler<ChangeEventArgs>(listener_StatusChanged);

            JHBeforeEnrollment.AfterUpdate += new EventHandler<K12.Data.DataChangedEventArgs>(JHBeforeEnrollment_AfterUpdate);
            _worker.DoWork += new DoWorkEventHandler(_worker_DoWork);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_worker_RunWorkerCompleted);

            Disposed += new EventHandler(BeforeEnrollmentItem_Disposed);
        }
    }
}
