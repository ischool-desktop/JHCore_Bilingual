using System;
using System.ComponentModel;
using System.Windows.Forms;
using FISCA.Presentation;
using Framework;
using JHSchool.Data;
using FCode = Framework.Security.FeatureCodeAttribute;

namespace JHSchool.Permrec.StudentExtendControls
{
    [FCode("JHSchool.Student.Detail0080", "畢業資訊")]
    public partial class DiplomaInfoPalmerworm : FISCA.Presentation.DetailContent
    {
        private ChangeListener DataListener = new ChangeListener();
        private EnhancedErrorProvider Errors { get; set; }
        private string _DefaultSchoolYear = "";
        private string _DefaultGDNumber = "";
        private string _DefaultMemo = "";
        private string _DefaultReason = "";
        private string _DefaultClass = "";
        private BackgroundWorker BGWork;
        private bool isBwBusy = false;
        PermRecLogProcess prlp;
        private JHSchool.Data.JHLeaveInfoRecord LeaveInfoRec;
        private ErrorProvider epSchoolYear = new ErrorProvider();

        public DiplomaInfoPalmerworm()
        {
            InitializeComponent();
            Group = "畢業資訊";
            //Errors = new EnhancedErrorProvider();

            //DataListener = new ChangeListener();
            DataListener.Add(new TextBoxSource(txtSchoolYear));
            DataListener.Add(new TextBoxSource(txtGDNumber));
            DataListener.Add(new TextBoxSource(txtMemo));
            DataListener.Add(new ComboBoxSource(cboReason, ComboBoxSource.ListenAttribute.Text));
            DataListener.Add(new TextBoxSource(txtClass));
            DataListener.StatusChanged += new EventHandler<ChangeEventArgs>(DataListener_StatusChanged);
            JHLeaveIfno.AfterUpdate += new EventHandler<K12.Data.DataChangedEventArgs>(JHLeaveIfno_AfterUpdate);
            prlp = new PermRecLogProcess();
            BGWork = new BackgroundWorker();
            BGWork.DoWork += new DoWorkEventHandler(BGWork_DoWork);
            BGWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWork_RunWorkerCompleted);
            if (!string.IsNullOrEmpty(PrimaryKey))
                BGWork.RunWorkerAsync();

            Disposed += new EventHandler(DiplomaInfoPalmerworm_Disposed);
        }

        void DiplomaInfoPalmerworm_Disposed(object sender, EventArgs e)
        {
            JHLeaveIfno.AfterUpdate -= new EventHandler<K12.Data.DataChangedEventArgs>(JHLeaveIfno_AfterUpdate);
        }

        void JHLeaveIfno_AfterUpdate(object sender, K12.Data.DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, K12.Data.DataChangedEventArgs>(JHLeaveIfno_AfterUpdate), sender, e);
            }
            else
            {
                if (PrimaryKey != "")
                {
                    if (!BGWork.IsBusy)
                        BGWork.RunWorkerAsync();
                }
            }
        }

        void BGWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isBwBusy)
            {
                isBwBusy = false;
                BGWork.RunWorkerAsync();
                return;
            }
            BindDataToForm();
        }

        void BGWork_DoWork(object sender, DoWorkEventArgs e)
        {
            LeaveInfoRec = JHSchool.Data.JHLeaveIfno.SelectByStudentID(PrimaryKey);
        }

        private void DataListener_StatusChanged(object sender, ChangeEventArgs e)
        {
            if (Framework.User.Acl[GetType()].Editable)
                SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            else
                SaveButtonVisible = false;

            CancelButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        private void BindDataToForm()
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;

            this._DefaultSchoolYear = "";
            this._DefaultGDNumber = "";
            this._DefaultMemo = "";
            this._DefaultReason = "";
            this._DefaultClass = "";

            DataListener.SuspendListen();
            txtSchoolYear.Text = "";
            txtGDNumber.Text = "";
            txtMemo.Text = "";
            cboReason.Text = "";
            txtClass.Text = "";
            cboReason.Items.Clear();
            cboReason.Items.Add("畢業");
            cboReason.Items.Add("修業");
            cboReason.Items.Add("");
            cboReason.DropDownStyle = ComboBoxStyle.DropDownList;


            if (LeaveInfoRec.SchoolYear.HasValue)
                txtSchoolYear.Text = this._DefaultSchoolYear = "" + LeaveInfoRec.SchoolYear.Value;
            if (!string.IsNullOrEmpty(LeaveInfoRec.DiplomaNumber))
                txtGDNumber.Text = this._DefaultGDNumber = LeaveInfoRec.DiplomaNumber;

            if (!string.IsNullOrEmpty(LeaveInfoRec.Memo))
                txtMemo.Text = this._DefaultMemo = LeaveInfoRec.Memo;

            if (!string.IsNullOrEmpty(LeaveInfoRec.Reason))
                cboReason.Text = this._DefaultReason = LeaveInfoRec.Reason;

            //if (!string.IsNullOrEmpty(LeaveInfoRec.ClassName))
            //    txtClass.Text = this._DefaultClass = LeaveInfoRec.ClassName;             
            epSchoolYear.Clear();
            prlp.SetBeforeSaveText("畢業學年度", this._DefaultSchoolYear);
            prlp.SetBeforeSaveText("畢業資格", this._DefaultReason);
            prlp.SetBeforeSaveText("畢業證書字號", this._DefaultGDNumber);
            prlp.SetBeforeSaveText("畢業相關訊息", this._DefaultMemo);

            DataListener.Reset();
            DataListener.ResumeListen();
            this.Loading = false;
        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            this.Loading = true;
            if (BGWork.IsBusy)
                isBwBusy = true;
            else
                BGWork.RunWorkerAsync();
        }

        protected override void OnCancelButtonClick(EventArgs e)
        {
            txtClass.Text = this._DefaultClass;
            txtGDNumber.Text = this._DefaultGDNumber;
            txtMemo.Text = this._DefaultMemo;
            cboReason.Text = this._DefaultReason;
            txtSchoolYear.Text = this._DefaultSchoolYear;
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            epSchoolYear.Clear();
        }

        protected override void OnSaveButtonClick(EventArgs e)
        {
            LeaveInfoRec.ClassName = txtClass.Text;
            LeaveInfoRec.DiplomaNumber = txtGDNumber.Text;
            LeaveInfoRec.Memo = txtMemo.Text;
            LeaveInfoRec.Reason = cboReason.Text;
            int SchoolYear = 0;
            if (string.IsNullOrEmpty(txtSchoolYear.Text))
                LeaveInfoRec.SchoolYear = null;
            else
            {
                if (int.TryParse(txtSchoolYear.Text, out SchoolYear))
                    LeaveInfoRec.SchoolYear = SchoolYear;
                else
                {
                    epSchoolYear.SetError(txtSchoolYear, "請輸入整數");
                    return;
                }
            }
            JHSchool.Data.JHLeaveIfno.Update(LeaveInfoRec);

            SaveButtonVisible = false;
            CancelButtonVisible = false;
            //Student.Instance.SyncDataBackground(PrimaryKey);
            //            this._DefaultClass = txtClass.Text;
            this._DefaultGDNumber = txtGDNumber.Text;
            this._DefaultMemo = txtMemo.Text;
            this._DefaultSchoolYear = txtSchoolYear.Text;
            this._DefaultReason = cboReason.Text;

            prlp.SetAfterSaveText("畢業學年度", txtSchoolYear.Text);
            prlp.SetAfterSaveText("畢業資格", cboReason.Text);
            prlp.SetAfterSaveText("畢業證書字號", txtGDNumber.Text);
            prlp.SetAfterSaveText("畢業相關訊息", txtMemo.Text);
            prlp.SetActionBy("學籍", "學生畢業資訊");
            prlp.SetAction("修改學生畢業資訊");
            JHStudentRecord studRec = JHStudent.SelectByID(PrimaryKey);
            prlp.SetDescTitle("學生姓名:" + studRec.Name + ",學號:" + studRec.StudentNumber + ",");

            prlp.SaveLog("", "", "student", PrimaryKey);
           // BindDataToForm();
        }

        public DetailContent GetContent()
        {
            return new DiplomaInfoPalmerworm();
        }

        private void txtSchoolYear_TextChanged(object sender, EventArgs e)
        {
            int tempNo;
            epSchoolYear.Clear();
            if (!string.IsNullOrEmpty(txtSchoolYear.Text))
            {
                if (int.TryParse(txtSchoolYear.Text, out tempNo))
                {
                    SaveButtonVisible = true;
                    CancelButtonVisible = true;
                    if (LeaveInfoRec.SchoolYear.HasValue)
                        if (LeaveInfoRec.SchoolYear.Value == tempNo)
                        {
                            SaveButtonVisible = false;
                            CancelButtonVisible = false;
                        }
                }
                else
                {
                    epSchoolYear.SetError(txtSchoolYear, "請輸入整數");
                }
            }
        }
    }
}

