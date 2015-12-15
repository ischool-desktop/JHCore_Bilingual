//已由K12.Form.Photo取代

//using System.Drawing;
//using System.IO;

//namespace JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports.DAL
//{
//    public class StudPhotoEntity
//    {
//        /// <summary>
//        /// 命名方式
//        /// </summary>
//        public enum PhotoNameRule { 學號, 身分證號, 班級座號 };
//        /// <summary>
//        /// 照片項目
//        /// </summary>
//        public enum PhotoKind { 入學, 畢業 };

//        /// <summary>
//        /// 學生編號
//        /// </summary>
//        public string StudentID { get; set; }
//        /// <summary>
//        /// 學生姓名
//        /// </summary>
//        public string StudentName { get; set; }
//        /// <summary>
//        /// 照片檔名命名規則
//        /// </summary>
//        public PhotoNameRule _PhotoNameRule;
//        /// <summary>
//        /// 照片使用種類
//        /// </summary>
//        public PhotoKind _PhotoKind;

//        /// <summary>
//        /// 照片檔案資訊
//        /// </summary>
//        public FileInfo PhotoFileInfo { get; set; }

//        /// <summary>
//        /// 班級名稱
//        /// </summary>
//        public string ClassName { get; set; }
//        /// <summary>
//        /// 座號
//        /// </summary>
//        public string SeatNo { get; set; }
//        /// <summary>
//        /// 學號
//        /// </summary>
//        public string StudentNumber { get; set; }
//        /// <summary>
//        /// 身分證號
//        /// </summary>
//        public string StudentIDNumber { get; set; }

//        /// <summary>
//        /// 照片錯誤訊息
//        /// </summary>
//        public string ErrorMessage { get; set; }

//        /// <summary>
//        /// 入學照片圖檔
//        /// </summary>
//        public Bitmap FreshmanPhotoBitmap { get; set; }


//        /// <summary>
//        /// 存檔路徑與名稱
//        /// </summary>
//        public string SaveFilePathAndName { get; set; }

//        /// <summary>
//        /// 畢業照片圖檔
//        /// </summary>
//        public Bitmap GraduatePhotoBitmap { get; set; }
//        /// <summary>
//        /// 取得照片名稱
//        /// </summary>
//        /// <returns></returns>
//        public string GetPhotoName()
//        {
//            string returnValue = string.Empty;

//            if (_PhotoNameRule == PhotoNameRule.班級座號)
//            {
//                if (PhotoFileInfo != null)
//                    returnValue = PhotoFileInfo.Directory.Name + PhotoFileInfo.Name.Substring(0, PhotoFileInfo.Name.Length - PhotoFileInfo.Extension.Length);
//            }
//            else
//            {
//                if (PhotoFileInfo != null)
//                    returnValue = PhotoFileInfo.Name.Substring(0, PhotoFileInfo.Name.Length - PhotoFileInfo.Extension.Length);
//            }
//            return returnValue;
//        }

//        /// <summary>
//        /// 取得照片檔案完整路徑
//        /// </summary>
//        /// <returns></returns>
//        public string GetPhotoFullName()
//        {
//            if (PhotoFileInfo == null)
//                return string.Empty;
//            else
//                return PhotoFileInfo.FullName;
//        }

//        /// <summary>
//        /// 檢查使用者所選的檔檔名與命名規則是否符合
//        /// </summary>
//        /// <param name="PhotoKind"></param>
//        /// <returns></returns>
//        public bool CheckSelectPhotoNameRule()
//        {
//            bool check = true;

//            if (_PhotoNameRule == PhotoNameRule.身分證號)
//            {
//                if (string.IsNullOrEmpty(StudentIDNumber))
//                    return false;

//                if (GetPhotoName().Trim() != StudentIDNumber.Trim())
//                    check = false;
//            }

//            //if (_PhotoNameRule == PhotoNameRule.班級座號)
//            //{
//            //    if (string.IsNullOrEmpty(ClassName) || string.IsNullOrEmpty(SeatNo))
//            //        return false;

//            //    if (GetPhotoName() != (ClassName.Trim() + SeatNo.Trim()))
//            //        check = false;
//            //}
//            if (_PhotoNameRule == PhotoNameRule.學號)
//            {
//                if (string.IsNullOrEmpty(StudentNumber))
//                    return false;

//                if (GetPhotoName() != StudentNumber.Trim())
//                    check = false;
//            }
//            return check;
//        }
//    }
//}
