//已由K12.Form.Photo取代

//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Windows.Forms;
//using System.Xml;
//using FISCA.DSAUtil;
//using JHSchool.Permrec.Feature.Legacy;


//namespace JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports.DAL
//{
//    class Transfer
//    {
//        /// <summary>
//        /// 匯入照片
//        /// </summary>
//        public static void ImportPhotos(List<StudPhotoEntity> StudPhotoEntityList)
//        {
//            if (StudPhotoEntityList.Count > 0)
//            {
//                BackgroundWorker bgImportWorker = new BackgroundWorker();
//                bgImportWorker.DoWork += new DoWorkEventHandler(bgImportWorker_DoWork);
//                bgImportWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgImportWorker_RunWorkerCompleted);
//                bgImportWorker.RunWorkerAsync(StudPhotoEntityList);
//            }
//            //EditStudent.UpdatePhoto();
//        }



//        static void bgImportWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//        {
//            //MessageBox.Show("匯入完成");
//        }

//        static void bgImportWorker_DoWork(object sender, DoWorkEventArgs e)
//        {
//            List<StudPhotoEntity> StudPhotoEntityList = (List<StudPhotoEntity>)e.Argument;
//            DSXmlHelper xmlHelper = new DSXmlHelper("Request");
//            foreach (StudPhotoEntity spe in StudPhotoEntityList)
//            {
//                string b64 = string.Empty;
//                try
//                {
//                    Bitmap pic = Photo.Resize(spe.PhotoFileInfo);
//                    b64 = Photo.GetBase64Encoding(pic);

//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("B64!" + spe.GetPhotoFullName() + "轉換失敗!");
//                    return;
//                }
//                xmlHelper.AddElement("Student");
//                if (spe._PhotoKind == StudPhotoEntity.PhotoKind.入學)
//                    xmlHelper.AddElement("Student", "FreshmanPhoto", b64);
//                if (spe._PhotoKind == StudPhotoEntity.PhotoKind.畢業)
//                    xmlHelper.AddElement("Student", "GraduatePhoto", b64);

//                if (spe._PhotoNameRule == StudPhotoEntity.PhotoNameRule.身分證號)
//                    xmlHelper.AddElement("Student", "IDNumber", spe.GetPhotoName());

//                if (spe._PhotoNameRule == StudPhotoEntity.PhotoNameRule.學號)
//                    xmlHelper.AddElement("Student", "StudentNumber", spe.GetPhotoName());

//                // 班級座號轉成身分證方
//                if (spe._PhotoNameRule == StudPhotoEntity.PhotoNameRule.班級座號)
//                {
//                    // 先用身分證
//                    if (!string.IsNullOrEmpty(spe.StudentIDNumber))
//                        xmlHelper.AddElement("Student", "IDNumber", spe.StudentIDNumber);
//                    else
//                    {
//                        // 用學號
//                        if (!string.IsNullOrEmpty(spe.StudentNumber))
//                            xmlHelper.AddElement("Student", "StudentNumber", spe.StudentNumber);

//                    }
//                }
//            }

//            try
//            {
//                EditStudent.UpdatePhoto(new DSRequest(xmlHelper.BaseElement));
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("上傳照片發生錯誤!");
//                // 待寫
//                return;
//            }



//        }


//        /// <summary>
//        /// 匯出照片
//        /// </summary>
//        public static void ExportPhotos()
//        {

//        }



//        /// <summary>
//        /// 設定學生基本資訊
//        /// </summary>
//        /// <param name="StudPhtoEntityList"></param>
//        /// <param name="_PhotoNameRule"></param>
//        public static List<StudPhotoEntity> SetStudBaseInfo(List<StudPhotoEntity> StudPhtoEntityList)
//        {

//            Student.Instance.SyncAllBackground();
//            Dictionary<string, StudentRecord> StudIdx = new Dictionary<string, StudentRecord>();

//            // 要過濾非在校生

//            // 學號
//            foreach (StudentRecord studRec in Student.Instance.Items)
//                if (studRec.Status == "一般")
//                    if (!StudIdx.ContainsKey(studRec.StudentNumber))                    
//                            StudIdx.Add(studRec.StudentNumber, studRec);

//            // 身分證
//            foreach (StudentRecord studRec in Student.Instance.Items)
//                if (studRec.Status == "一般")
//                    if (!StudIdx.ContainsKey(studRec.IDNumber))
//                        StudIdx.Add(studRec.IDNumber, studRec);
//            // 班座
//            foreach (StudentRecord studRec in Student.Instance.Items)
//            {
//                if (studRec.Status == "一般")
//                if (studRec.Class != null)
//                {
//                    string key = studRec.Class.Name.Trim() + studRec.SeatNo.Trim();
//                    if (!StudIdx.ContainsKey(key))
//                        StudIdx.Add(key, studRec);
//                }
//            }

//            // 有符合以上3類填入值
//            foreach (StudPhotoEntity spe in StudPhtoEntityList)
//            {
//                string PhotoName = spe.GetPhotoName();
//                if (StudIdx.ContainsKey(PhotoName))
//                {
//                    if (StudIdx[PhotoName].Class != null)
//                        spe.ClassName = StudIdx[PhotoName].Class.Name;
//                    spe.SeatNo = StudIdx[PhotoName].SeatNo;
//                    spe.StudentID = StudIdx[PhotoName].ID;
//                    spe.StudentIDNumber = StudIdx[PhotoName].IDNumber;
//                    spe.StudentName = StudIdx[PhotoName].Name;
//                    spe.StudentNumber = StudIdx[PhotoName].StudentNumber;
//                }
//            }
//            return StudPhtoEntityList;
//        }

//        public static List<StudPhotoEntity> GetStudentPhotoBitmap(List<StudPhotoEntity> StudPhotoEntityList)
//        {
//            List<string> StudentIDList = new List<string>();
//            foreach (StudPhotoEntity spe in StudPhotoEntityList)
//                if (!string.IsNullOrEmpty(spe.StudentID))
//                    StudentIDList.Add(spe.StudentID);

//            DSXmlHelper xmlHelper = new DSXmlHelper("Request");

//            DSResponse DSRsp = QueryStudent.GetDetailList(new string[] { "ID", "FreshmanPhoto", "GraduatePhoto" }, StudentIDList.ToArray());

//            Dictionary<string, string> FreshmanPhotoStr = new Dictionary<string, string>();
//            Dictionary<string, string> GraduatePhotoStr = new Dictionary<string, string>();

//            if (DSRsp != null)
//                foreach (XmlElement elm in DSRsp.GetContent().BaseElement.SelectNodes("Student"))
//                {
//                    if (!FreshmanPhotoStr.ContainsKey(elm.GetAttribute("ID")))
//                    {
//                        if (!string.IsNullOrEmpty(elm.SelectSingleNode("FreshmanPhoto").InnerText))
//                            FreshmanPhotoStr.Add(elm.GetAttribute("ID"), elm.SelectSingleNode("FreshmanPhoto").InnerText);
//                    }
//                    if (!GraduatePhotoStr.ContainsKey(elm.GetAttribute("ID")))
//                    {
//                        if (!string.IsNullOrEmpty(elm.SelectSingleNode("GraduatePhoto").InnerText))
//                            GraduatePhotoStr.Add(elm.GetAttribute("ID"), elm.SelectSingleNode("GraduatePhoto").InnerText);
//                    }

//                }

//            foreach (StudPhotoEntity spe in StudPhotoEntityList)
//            {
//                if (spe._PhotoKind == StudPhotoEntity.PhotoKind.入學)
//                {
//                    if (FreshmanPhotoStr.ContainsKey(spe.StudentID))
//                    {
//                        spe.FreshmanPhotoBitmap = Photo.ConvertFromBase64Encoding(FreshmanPhotoStr[spe.StudentID], true);
//                    }
//                }

//                if (spe._PhotoKind == StudPhotoEntity.PhotoKind.畢業)
//                {
//                    if (GraduatePhotoStr.ContainsKey(spe.StudentID))
//                    {
//                        spe.GraduatePhotoBitmap = Photo.ConvertFromBase64Encoding(GraduatePhotoStr[spe.StudentID], true);
//                    }

//                }
//            }

//            return StudPhotoEntityList;
//        }
//    }
//}
