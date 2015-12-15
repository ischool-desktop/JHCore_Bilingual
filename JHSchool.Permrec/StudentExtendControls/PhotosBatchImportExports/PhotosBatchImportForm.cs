//已由K12.Form.Photo取代

//using System;
//using System.Collections.Generic;
//using System.IO;

//namespace JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports
//{
//    public partial class PhotosBatchImportForm : FISCA.Presentation.Controls.BaseForm
//    {
//        private PhotoBatchFileManager PhotoBatchFileManager1;


//        public PhotosBatchImportForm()
//        {
//            InitializeComponent();
//            cbxEnroll.Checked = true;
//            cbxByStudentNum.Checked = true;
//        }

//        private void btnBrowse_Click(object sender, EventArgs e)
//        {
//            PhotoBatchFileManager1 = new PhotoBatchFileManager();
//            txtFilePath.Text = PhotoBatchFileManager1.GetFilefoldrBrowserDialog();
//        }

//        private void btnExit_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }

//        private void btnUpload_Click(object sender, EventArgs e)
//        {
//            DAL.StudPhotoEntity.PhotoKind PhotoKind = 0;
//            DAL.StudPhotoEntity.PhotoNameRule PhotoNameRule = 0;
//            if (PhotoBatchFileManager1 == null)
//            {
//                PhotoBatchFileManager1 = new PhotoBatchFileManager();
//                PhotoBatchFileManager1.SetFilePath(txtFilePath.Text);
//            }

//            if (string.IsNullOrEmpty(PhotoBatchFileManager1.GetFilePath()))
//                PhotoBatchFileManager1.SetFilePath(txtFilePath.Text);

//            // 入學
//            if (cbxEnroll.Checked)
//                PhotoKind = DAL.StudPhotoEntity.PhotoKind.入學;

//            // 畢業
//            if (cbxGraduate.Checked)
//                PhotoKind = DAL.StudPhotoEntity.PhotoKind.畢業;

//            if (cbxByClassNameSeatNo.Checked)
//                PhotoNameRule = DAL.StudPhotoEntity.PhotoNameRule.班級座號;

//            if (cbxByStudentIDNumber.Checked)
//                PhotoNameRule = DAL.StudPhotoEntity.PhotoNameRule.身分證號;

//            if (cbxByStudentNum.Checked)
//                PhotoNameRule = DAL.StudPhotoEntity.PhotoNameRule.學號;

//            List<DAL.StudPhotoEntity> StudPhotoEntityList = new List<JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports.DAL.StudPhotoEntity>();

//            // 當沒有選擇檔案
//            if (PhotoBatchFileManager1.GetFileInfo() == null)
//                return;

//            // 取得所選檔案資訊
//            foreach (FileInfo fi in PhotoBatchFileManager1.GetFileInfo())
//            {
//                DAL.StudPhotoEntity spe = new JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports.DAL.StudPhotoEntity();
//                spe.PhotoFileInfo = fi;
//                spe._PhotoKind = PhotoKind;
//                spe._PhotoNameRule = PhotoNameRule;

//                StudPhotoEntityList.Add(spe);
//            }

//            if (cbxByClassNameSeatNo.Checked)
//            {
//                if (PhotoBatchFileManager1.SetCurrentFullFoldersAndFilesInfo(txtFilePath.Text) == false)
//                    return;



//            }

//            // 放入待匯入資料
//            List<DAL.StudPhotoEntity> ImportStudPhotoEntityList = new List<DAL.StudPhotoEntity>();

//            // 讀取學生資料
//            StudPhotoEntityList = DAL.Transfer.SetStudBaseInfo(StudPhotoEntityList);

//            // 驗證資料
//            foreach (DAL.StudPhotoEntity spe in StudPhotoEntityList)
//            {
//                bool checkCanUpdate = false;

//                if (string.IsNullOrEmpty(spe.StudentID))
//                {
//                    spe.ErrorMessage += spe.GetPhotoFullName() + " 沒有學生 ID";
//                    checkCanUpdate = false;
//                }

//                // 當使用班級座號驗證與其他不同
//                if (cbxByClassNameSeatNo.Checked)
//                {
//                    if (spe._PhotoNameRule == JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports.DAL.StudPhotoEntity.PhotoNameRule.班級座號)
//                    {
//                        if (PhotoBatchFileManager1.CheckFolderAndFileInCurrentForClassNameSeatNo(spe.ClassName, spe.SeatNo))
//                            checkCanUpdate = true;
//                        else
//                        {
//                            spe.ErrorMessage += "檔名與命名規則不符, 命名方式:" + spe._PhotoNameRule.ToString() + ", " + spe.GetPhotoFullName();
//                            checkCanUpdate = false;
//                        }
//                    }
//                }
//                else
//                {

//                    if (spe.CheckSelectPhotoNameRule() == false)
//                    {
//                        spe.ErrorMessage += "檔名與命名規則不符, 命名方式:" + spe._PhotoNameRule.ToString() + ", " + spe.GetPhotoFullName();
//                        checkCanUpdate = false;
//                    }
//                    else
//                        checkCanUpdate = true;
//                }

//                if (checkCanUpdate)
//                {
//                    ImportStudPhotoEntityList.Add(spe);
//                }
//            }

//            // 上傳照片
//            DAL.Transfer.ImportPhotos(ImportStudPhotoEntityList);

//            List<DAL.StudPhotoEntity> ErrorData = new List<JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports.DAL.StudPhotoEntity>();

//            // 讀取錯誤
//            foreach (DAL.StudPhotoEntity spe in StudPhotoEntityList)
//            {
//                if (!string.IsNullOrEmpty(spe.ErrorMessage))
//                    ErrorData.Add(spe);
//            }
//            // 匯入完後畫面顯示
//            PhotoCompleteMessage pcm = new PhotoCompleteMessage();
//            string msg = "匯入檔案數:" + StudPhotoEntityList.Count + ",匯入成功數:" + (StudPhotoEntityList.Count - ErrorData.Count) + ",匯入失敗數:" + ErrorData.Count;

//            PermRecLogProcess prlp = new PermRecLogProcess();
//            prlp.SaveLog("學生,批次匯入照片", "匯入", "批次匯入學生照片," + msg);

//            pcm.SetFormText("匯入照片訊息");
//            pcm.SetMessageText(msg);
//            if (ErrorData.Count > 0)
//            {
//                pcm.SetStudPhotoEntity(ErrorData);
//                pcm.SetErrorMsgBottonEnable(true);
//            }
//            else
//                pcm.SetErrorMsgBottonEnable(false);
//            pcm.ShowDialog();
//        }
//    }
//}
