using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using FISCA.Presentation;
using Framework;
using JHSchool.Data;
using JHSchool.Legacy;
using FCode = Framework.Security.FeatureCodeAttribute;
using JHSchool;
using K12StudentPhoto;

namespace BasicInformation
{
    [FCode("JHSchool.Teacher.Detail0000", "�Юv�򥻸��_���y��")]
    internal partial class TeacherItem : FISCA.Presentation.DetailContent
    {
        ErrorProvider epName = new ErrorProvider();
        ErrorProvider epNick = new ErrorProvider();
        ErrorProvider epGender = new ErrorProvider();
        ErrorProvider epLoginName = new ErrorProvider();
        ErrorProvider epIDNumber = new ErrorProvider();
        private bool _isBGWorkBusy = false;
        private BackgroundWorker _BGWork;
        private JHTeacherRecord _TeacherRec;
        private Dictionary<string,string> _AllTeacherNameDic;
        private Dictionary<string, string> _AllLogIDDic;
        Dictionary<string, string> _AllIDNumberDict = new Dictionary<string, string>();
        K12StudentPhoto.PermRecLogProcess prlp;
        private ChangeListener _DataListener;

        EventHandler eh;
        string EventCord = "Res_TeacherExt";

        private TeacherRecord_Ext _TeacherRec_Ext;

        public TeacherItem()
        {
            InitializeComponent();
            Group = "�Юv�򥻸��_���y��";
            _BGWork = new BackgroundWorker();
            _BGWork.DoWork += new DoWorkEventHandler(_BGWork_DoWork);
            _BGWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWork_RunWorkerCompleted);
            _AllTeacherNameDic = new Dictionary<string, string>();
            _AllLogIDDic = new Dictionary<string, string>();
            prlp = new K12StudentPhoto.PermRecLogProcess();
            _DataListener = new ChangeListener();
            _DataListener.Add(new TextBoxSource(txtName));
            _DataListener.Add(new TextBoxSource(txtIDNumber));
            _DataListener.Add(new TextBoxSource(txtNickname));
            _DataListener.Add(new TextBoxSource(txtPhone));
            _DataListener.Add(new TextBoxSource(txtEmail));
            _DataListener.Add(new TextBoxSource(txtCategory));
            _DataListener.Add(new TextBoxSource(txtSTLoginAccount));
            _DataListener.Add(new TextBoxSource(txtSTLoginPwd));
            _DataListener.Add(new TextBoxSource(txtCellPhone));
            _DataListener.Add(new ComboBoxSource(cboAccountType, ComboBoxSource.ListenAttribute.Text));
            _DataListener.Add(new ComboBoxSource(cboGender, ComboBoxSource.ListenAttribute.Text));            
            _DataListener.StatusChanged += new EventHandler<ChangeEventArgs>(_DataListener_StatusChanged);
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            JHTeacher.AfterChange += new EventHandler<K12.Data.DataChangedEventArgs>(JHTeacher_AfterChange);
            JHTeacher.AfterDelete += new EventHandler<K12.Data.DataChangedEventArgs>(JHTeacher_AfterDelete);
            Disposed += new EventHandler(BaseInfoItem_Disposed);

            //�Ұʧ�s�ƥ�
            eh = FISCA.InteractionService.PublishEvent(EventCord);
        }

        void JHTeacher_AfterDelete(object sender, K12.Data.DataChangedEventArgs e)
        {
            Teacher.Instance.SyncAllBackground();
        }

        void BaseInfoItem_Disposed(object sender, EventArgs e)
        {            
            JHTeacher.AfterChange -= new EventHandler<K12.Data.DataChangedEventArgs>(JHTeacher_AfterChange);
            JHTeacher.AfterDelete -= new EventHandler<K12.Data.DataChangedEventArgs>(JHTeacher_AfterDelete);
        }

        void JHTeacher_AfterChange(object sender, K12.Data.DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, K12.Data.DataChangedEventArgs>(JHTeacher_AfterChange), sender, e);
            }
            else
            {
                if (PrimaryKey != "")
                {
                    if (!_BGWork.IsBusy)
                        _BGWork.RunWorkerAsync();
                }
            }
        }


        void _DataListener_StatusChanged(object sender, ChangeEventArgs e)
        {
            if (Framework.User.Acl[GetType()].Editable)
                SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            else
                SaveButtonVisible = false;

            CancelButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        void _BGWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_isBGWorkBusy)
            {
                _isBGWorkBusy = false;
                _BGWork.RunWorkerAsync();
                return;
            }

            DataBindToForm();
        }

        // ���J��ƨ�e��
        private void DataBindToForm()
        {
            if (_TeacherRec == null)
                _TeacherRec = new JHTeacherRecord();

            _DataListener.SuspendListen();            
            
            txtName.Text = _TeacherRec.Name;
            txtIDNumber.Text = _TeacherRec.IDNumber;
            cboGender.Text = _TeacherRec.Gender;
            txtNickname.Text = _TeacherRec.Nickname;
            txtPhone.Text = _TeacherRec.ContactPhone;
            txtEmail.Text = _TeacherRec.Email;
            txtCategory.Text = _TeacherRec.Category;
            txtSTLoginAccount.Text = _TeacherRec.TALoginName;
            txtSTLoginPwd.Text = _TeacherRec.TAPassword;
            cboAccountType.Text = _TeacherRec.AccountType;
            txtCellPhone.Text = _TeacherRec_Ext.CellPhone;
            try
            {

                pic1.Image = Photo.ConvertFromBase64Encoding(_TeacherRec.Photo, pic1.Width, pic1.Height);
            }
            catch (Exception)
            {
                pic1.Image = pic1.InitialImage;
            }



            // Log
            prlp.SetBeforeSaveText("�m�W", txtName.Text);
            prlp.SetBeforeSaveText("�����Ҹ�", txtIDNumber.Text);
            prlp.SetBeforeSaveText("����m�W", txtCellPhone.Text);
            prlp.SetBeforeSaveText("�ʧO", cboGender.Text);
            prlp.SetBeforeSaveText("�ʺ�", txtNickname.Text);
            prlp.SetBeforeSaveText("�p���q��", txtPhone.Text);
            prlp.SetBeforeSaveText("�q�l�H�c", txtEmail.Text);
            prlp.SetBeforeSaveText("�Юv���O", txtCategory.Text);
            prlp.SetBeforeSaveText("�n�J�b��", txtSTLoginAccount.Text);
            prlp.SetBeforeSaveText("�n�J�K�X", txtSTLoginPwd.Text);
            prlp.SetBeforeSaveText("�b������", cboAccountType.Text);
            this.Loading = false;
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            _DataListener.Reset();
            _DataListener.ResumeListen();
        }

        void _BGWork_DoWork(object sender, DoWorkEventArgs e)
        {
            _AllTeacherNameDic.Clear();
            _AllLogIDDic.Clear();
            _AllIDNumberDict.Clear();

            foreach (JHTeacherRecord TR in JHTeacher.SelectAll())
            {
                _AllTeacherNameDic.Add(TR.Name + TR.Nickname, TR.ID);
                
                if(!string.IsNullOrEmpty(TR.TALoginName))
                    _AllLogIDDic.Add(TR.TALoginName, TR.ID);

                // �����Ҹ��ˬd
                if (!string.IsNullOrWhiteSpace(TR.IDNumber))
                    if (!_AllIDNumberDict.ContainsKey(TR.IDNumber))
                        _AllIDNumberDict.Add(TR.IDNumber, TR.ID);
            }

            UpdateUDTData();

            // Ū���Юv���
            _TeacherRec = JHTeacher.SelectByID(PrimaryKey);
        }

        void UpdateUDTData()
        {
            #region ���o�Ѯv�������

            List<TeacherRecord_Ext> TeacherExtList = tool._A.Select<TeacherRecord_Ext>("ref_teacher_id='" + PrimaryKey + "'");
            if (TeacherExtList.Count == 1)
            {
                //�C�W�ǥͥu�|���@�����Ӹ��
                _TeacherRec_Ext = TeacherExtList[0];
            }
            else if (TeacherExtList.Count < 1)
            {
                //�p�G�S�����Ӹ��
                _TeacherRec_Ext = new TeacherRecord_Ext();
                _TeacherRec_Ext.RefTeacherID = PrimaryKey;

            }
            else
            {
                //�C�W�ǥͥu�|���@�����Ӹ��
                _TeacherRec_Ext = TeacherExtList[0];
                //�����O�d�����Ӹ��
                TeacherExtList.Remove(_TeacherRec_Ext);
                //�R���h�l���
                tool._A.DeletedValues(TeacherExtList);
            }

            #endregion

        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            this.Loading = true;
            if (_BGWork.IsBusy)
                _isBGWorkBusy = true;
            else
                _BGWork.RunWorkerAsync();
            
            ClearErrorMessage();
        }

        protected override void OnSaveButtonClick(EventArgs e)
        {

            // ���Ҹ��
            if(string.IsNullOrEmpty (txtName.Text.Trim()))
            {
                epName.SetError(txtName,"�m�W���i�ť�!");
                return ;
            }

            // �ˬd�b���O�_����
            if (_AllLogIDDic.ContainsKey(txtSTLoginAccount.Text ))
            {
                if (_AllLogIDDic[txtSTLoginAccount.Text] != _TeacherRec.ID)
                {
                    epLoginName.SetError(txtSTLoginAccount, "�n�J�b������!");
                    return;
                }                
            }

            // �ˬd�m�W+�ʺ٬O�_����
            string checkName = txtName.Text + txtNickname.Text;

            if (_AllTeacherNameDic.ContainsKey(checkName))
            {
                if (_AllTeacherNameDic[checkName] != _TeacherRec.ID)
                {
                    epName.SetError(txtName, "�m�W+�ʺ٭��СA���ˬd!");
                    epLoginName.SetError(txtNickname, "�m�W+�ʺ٭��СA���ˬd!");
                    return;
                }
            }

            // �ˬd�����Ҹ��O�_����
            if (_AllIDNumberDict.ContainsKey(txtIDNumber.Text))
            { 
                if(_TeacherRec.ID != _AllIDNumberDict[txtIDNumber.Text])
                {
                    epIDNumber.SetError(txtIDNumber,"�����Ҹ����ơA���ˬd!");
                    return;
                }
            }

            // �^��� DAL
            _TeacherRec.AccountType = cboAccountType.Text;
            _TeacherRec.Category = txtCategory.Text;
            _TeacherRec.ContactPhone = txtPhone.Text;
            _TeacherRec.Email = txtEmail.Text;
            _TeacherRec.Gender = cboGender.Text;
            _TeacherRec.IDNumber = txtIDNumber.Text;
            _TeacherRec.TALoginName  = txtSTLoginAccount.Text;
            _TeacherRec.Name = txtName.Text;
            _TeacherRec.Nickname = txtNickname.Text;
            _TeacherRec.TAPassword = txtSTLoginPwd.Text;
            _TeacherRec_Ext.CellPhone = txtCellPhone.Text;
            
            // �s��
            JHTeacher.Update(_TeacherRec);

            //�x�s�������
            List<TeacherRecord_Ext> list = new List<TeacherRecord_Ext>();
            list.Add(_TeacherRec_Ext);
            if (!string.IsNullOrEmpty(_TeacherRec_Ext.UID))
            {
                tool._A.UpdateValues(list);
            }
            else
            {
                tool._A.InsertValues(list);
            }

            UpdateUDTData();

            //
            eh(this, EventArgs.Empty);


            // Save Log
            prlp.SetAfterSaveText("�m�W", txtName.Text);
            prlp.SetAfterSaveText("����m�W", txtCellPhone.Text);
            prlp.SetAfterSaveText("�����Ҹ�", txtIDNumber.Text);
            prlp.SetAfterSaveText("�ʧO", cboGender.Text);
            prlp.SetAfterSaveText("�ʺ�", txtNickname.Text);
            prlp.SetAfterSaveText("�p���q��", txtPhone.Text);
            prlp.SetAfterSaveText("�q�l�H�c", txtEmail.Text);
            prlp.SetAfterSaveText("�Юv���O", txtCategory.Text);
            prlp.SetAfterSaveText("�n�J�b��", txtSTLoginAccount.Text);
            prlp.SetAfterSaveText("�n�J�K�X", txtSTLoginPwd.Text);
            prlp.SetAfterSaveText("�b������", cboAccountType.Text);


            prlp.SetDescTitle("�Юv�m�W�G" + txtName.Text + ",");
            prlp.SetActionBy("���y", "�Юv�򥻸��");
            prlp.SetAction("�ק�Юv�򥻸��");
            prlp.SaveLog("", "", "teacher", PrimaryKey);
            DataBindToForm();
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            Teacher.Instance.SyncDataBackground(PrimaryKey);
            Class.Instance.SyncAllBackground();
            ClearErrorMessage();
        }

        private void ClearErrorMessage()
        {
            epGender.Clear();
            epLoginName.Clear();
            epName.Clear();
            epNick.Clear();
            epIDNumber.Clear();
        }

        protected override void OnCancelButtonClick(EventArgs e)
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;
            DataBindToForm();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "�Ҧ��v��(*.jpg,*.jpeg,*.gif,*.png)|*.jpg;*.jpeg;*.gif;*.png;";
            if (od.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(od.FileName, FileMode.Open);
                    Bitmap orgBmp = new Bitmap(fs);
                    fs.Close();

                    Bitmap newBmp = new Bitmap(orgBmp, pic1.Size);
                    pic1.Image = newBmp;
                    
                    _TeacherRec.Photo = ToBase64String(Photo.Resize(new Bitmap(orgBmp)));
                    JHTeacher.Update(_TeacherRec);
                    prlp.SaveLog("���y�t��-�Юv�򥻸��", "�ܧ�Юv�Ӥ�", "teacher", PrimaryKey, "�ܧ�Юv:" + _TeacherRec.Name + "���Ӥ�");
                    DataBindToForm();

                }
                catch (Exception ex)
                {
                    FISCA.Presentation.Controls.MsgBox.Show(ex.Message);
                }
            }
        }

        public DetailContent GetContent()
        {
            return new TeacherItem();
        }

        private static string ToBase64String(Bitmap newBmp)
        {
            MemoryStream ms = new MemoryStream();
            newBmp.Save(ms, ImageFormat.Jpeg);
            ms.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, (int)ms.Length);
            ms.Close();

            return Convert.ToBase64String(bytes);
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            SavePicture(_TeacherRec.Photo);
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            if (FISCA.Presentation.Controls.MsgBox.Show("�z�T�w�n�M�����ǥͪ��Ӥ��ܡH", "", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                _TeacherRec.Photo = string.Empty;
                pic1.Image = pic1.InitialImage;
                JHTeacher.Update(_TeacherRec);
                prlp.SaveLog("���y�t��-�Юv�򥻸��", "�ܧ�Юv�Ӥ�", "teacher", PrimaryKey, "�ܧ�Юv:" + _TeacherRec.Name + "���Ӥ�");
                DataBindToForm();
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show(ex.Message);
            }
        }

        private void SavePicture(string imageString)
        {
            if (imageString == string.Empty)
                return;

            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "PNG �v��|*.png;";
            sd.FileName = txtIDNumber.Text + ".png";

            if (sd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sd.FileName, FileMode.Create);
                    byte[] imageData = Convert.FromBase64String(imageString);
                    fs.Write(imageData, 0, imageData.Length);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    FISCA.Presentation.Controls.MsgBox.Show(ex.Message);
                }
            }
        }

        private void txtIDNumber_TextChanged(object sender, EventArgs e)
        {
            epIDNumber.Clear();
        }
    }

}
