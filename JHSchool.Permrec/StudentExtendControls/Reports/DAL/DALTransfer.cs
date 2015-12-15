using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHSchool.Data;

namespace JHSchool.Permrec.StudentExtendControls.Reports.DAL
{
    class DALTransfer
    {
        public static bool UseStudPhotp = false;
        /// <summary>
        /// 取得學生資料
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>        /// 
        public static List<StudentEntity> GetStudentEntityList(List<string> StudentIDList)
        {
            string SchoolName = JHSchool.Data.JHSchoolInfo.ChineseName;
            string SchoolAddress = JHSchool.Data.JHSchoolInfo.Address;
            string SchoolTelephone = JHSchool.Data.JHSchoolInfo.Telephone;
            string SchoolFax = JHSchool.Data.JHSchoolInfo.Fax;
            
            // 照片 Idx
            Dictionary<string, string> FreshmanPhotoDic = K12.Data.Photo.SelectFreshmanPhoto(StudentIDList);

            // 取得畢業資訊
            Dictionary<string, JHLeaveInfoRecord> StudLIR = GetStudentLeaveInfoDic(StudentIDList);

            //監護人 Index
            Dictionary <string, string> Parent1Dic = new Dictionary<string, string>();
            List<JHSchool.Data.JHParentRecord> PRecords = JHSchool.Data.JHParent.SelectByStudentIDs(StudentIDList);
            foreach (JHSchool.Data.JHParentRecord pr in PRecords)
            {
                Parent1Dic.Add(pr.RefStudentID, pr.Custodian.Name);            
            }            
            

            List<StudentEntity> StudentList = new List<StudentEntity>();
            List<JHSchool.Data.JHStudentRecord> StudRecs = JHSchool.Data.JHStudent.SelectByIDs(StudentIDList);
            foreach (JHSchool.Data.JHStudentRecord studRec in StudRecs)
            {
                StudentEntity se = new StudentEntity();
                // 學生編號
                se.StudentID = studRec.ID;
                // 學生姓名
                se.StudentName = studRec.Name;
                // 學生英文姓名
                se.StudentEnglishName = studRec.EnglishName;
                // 學校中文名稱
                se.SchoolChineseName = SchoolName;
                // 學校中文地址
                se.SchoolAddress = SchoolAddress;
                if (studRec.Class != null)
                {
                    // 班級
                    se.ClassName = studRec.Class.Name;
                    // 年級
                    if (studRec.Class.GradeYear.HasValue)
                        se.GradeYear = studRec.Class.GradeYear.Value.ToString();
                }
                // 生日
                if (studRec.Birthday.HasValue)
                    se.Birthday = studRec.Birthday.Value;
                // 座號
                if(studRec.SeatNo.HasValue)
                    se.SeatNo = studRec.SeatNo.Value.ToString();
                // 學號
                se.StudentNumber = studRec.StudentNumber;
                // 身分證號
                se.IDNumber = studRec.IDNumber;
                // 學校電話
                se.SchoolTelephone = SchoolTelephone;
                // 學校傳真號碼
                se.SchoolFax = SchoolFax;

                
                // 當有使用照片
                if (UseStudPhotp)
                    if (FreshmanPhotoDic.ContainsKey(studRec.ID))
                        se.PhotoStr = FreshmanPhotoDic[studRec.ID];
                se.Gender = studRec.Gender;
                // 取得監護人
                if (Parent1Dic.ContainsKey(studRec.ID))
                    se.Parent1 = Parent1Dic[studRec.ID];

                // 取得出生地
                se.BirthPlace = studRec.BirthPlace;

                if (StudLIR.ContainsKey(se.StudentID))
                {
                    // 畢修業
                    se.Reason = StudLIR[se.StudentID].Reason;
                    // 畢業證書字號
                    se.DiplomaNumber = StudLIR[se.StudentID].DiplomaNumber;                
                }
                
                StudentList.Add(se);
                // 排序
                StudentList.Sort(new Comparison<StudentEntity>(Sort2));
            }
            return StudentList;
        }

        // 學號排序
        public static int Sort1(StudentEntity x,StudentEntity y)
        {
            return x.StudentNumber.CompareTo(y.StudentNumber);
        }

        // 班座排序
        public static int Sort2(StudentEntity x, StudentEntity y)
        {
            string strX = string.Empty, strY = string.Empty;

            int intX, intY;

            if (int.TryParse(x.SeatNo, out intX))
                strX = x.ClassName + string.Format("{0:00}", intX);
            else
                strX = x.ClassName + "99";

            if (int.TryParse(y.SeatNo, out intY))
                strY = y.ClassName + string.Format("{0:00}", intY);
            else
                strY = y.ClassName + "99";


            return strX.CompareTo(strY);
        }

        /// <summary>
        /// 取得學生畢業資訊
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, JHLeaveInfoRecord> GetStudentLeaveInfoDic(List<string> StudentIDList)
        {
            Dictionary<string, JHLeaveInfoRecord> StudLIR = new Dictionary<string, JHLeaveInfoRecord>();
            foreach (JHLeaveInfoRecord rec in JHLeaveIfno.SelectByStudentIDs(StudentIDList))
                if (!StudLIR.ContainsKey(rec.RefStudentID))
                    StudLIR.Add(rec.RefStudentID, rec);
            return StudLIR;
        }


        /// <summary>
        /// 取得學生轉學資料
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static List<StudTransExportEntity> GetStudTransExportEntityList(List<string> StudentIDList)
        {
            List<StudTransExportEntity> StudTransExportList = new List<StudTransExportEntity>();
            string SchoolName = JHSchool.Data.JHSchoolInfo.ChineseName;

            string ChancellorName = JHSchool.Data.JHSchoolInfo.ChancellorChineseName;

            // 取得學生異動
            List<JHSchool.Data.JHUpdateRecordRecord> StudUpdateRecList = JHSchool.Data.JHUpdateRecord.SelectByStudentIDs(StudentIDList);
            Dictionary<string, JHSchool.Data.JHUpdateRecordRecord> UpdateRecDic = new Dictionary<string, JHSchool.Data.JHUpdateRecordRecord>();
            Dictionary<string, string> UpdateRecDesc04Dic = new Dictionary<string, string>();
            Dictionary<string, JHSchool.Data.JHUpdateRecordRecord> UpdateRecDic03 = new Dictionary<string, JHSchool.Data.JHUpdateRecordRecord>();


            // 取得新生異動
            foreach (JHSchool.Data.JHUpdateRecordRecord rec in StudUpdateRecList)
            {
                if (rec.UpdateCode == "1")
                {
                    if (!UpdateRecDic.ContainsKey(rec.StudentID))
                        UpdateRecDic.Add(rec.StudentID, rec);
                }

                if (rec.UpdateCode == "3")
                {
                    if (!UpdateRecDic03.ContainsKey(rec.StudentID))
                    {
                        UpdateRecDic03.Add(rec.StudentID, rec);
                    }
                    else
                    { 
                        DateTime dta,dtb;

                        DateTime.TryParse(UpdateRecDic03[rec.StudentID].UpdateDate, out dta);
                        DateTime.TryParse(rec.UpdateDate, out dtb);
                        if (dtb > dta)
                            UpdateRecDic03[rec.StudentID] = rec;                    
                    }
                }

                if (rec.UpdateCode == "4")
                {
                    if (!UpdateRecDesc04Dic.ContainsKey(rec.StudentID))
                        UpdateRecDesc04Dic.Add(rec.StudentID, rec.UpdateDescription);
                }
            }


            // 照片 Idx
            Dictionary<string, string> FreshmanPhotoDic = K12.Data.Photo.SelectFreshmanPhoto(StudentIDList);
            List<JHSchool.Data.JHStudentRecord> StudRecs = JHSchool.Data.JHStudent.SelectByIDs(StudentIDList);

            foreach (JHSchool.Data.JHStudentRecord studRec in StudRecs)
            {
                StudTransExportEntity stee = new StudTransExportEntity();
                stee.SchoolName = SchoolName;
                stee.Name = studRec.Name;
                if(studRec.Birthday.HasValue )
                    stee.Birthday = studRec.Birthday.Value;
                if (studRec.Class != null)
                    if(studRec.Class.GradeYear.HasValue )
                        stee.ClassGradeYear = ""+studRec.Class.GradeYear.Value;

                // 新生異動
                if(UpdateRecDic.ContainsKey(studRec.ID ))
                {
                    // 沒有核准日期
                    if (string.IsNullOrEmpty(UpdateRecDic[studRec.ID].ADDate))
                    {
                        stee.UR01CertDate = "";
                        stee.UR01CertDocNo = "";
                        stee.UR01Date = UpdateRecDic[studRec.ID].UpdateDate;
                    }
                    else
                    {
                        stee.UR01Date = "";
                        stee.UR01CertDate = UpdateRecDic[studRec.ID].ADDate;
                        stee.UR01CertDocNo = UpdateRecDic[studRec.ID].ADNumber;                    
                    }                
                }


                // 轉入異動
                if (UpdateRecDic03.ContainsKey(studRec.ID))
                {
                    // 沒有核准日期
                    if (string.IsNullOrEmpty(UpdateRecDic03[studRec.ID].ADDate))
                    {
                        stee.UR01CertDate = "";
                        stee.UR01CertDocNo = "";
                        stee.UR01Date = UpdateRecDic03[studRec.ID].UpdateDate;
                    }
                    else
                    {
                        stee.UR01Date = "";
                        stee.UR01CertDate = UpdateRecDic03[studRec.ID].ADDate;
                        stee.UR01CertDocNo = UpdateRecDic03[studRec.ID].ADNumber;
                    }
                }



                // 照片
                if (FreshmanPhotoDic.ContainsKey(studRec.ID))
                    stee.PhotoStr = FreshmanPhotoDic[studRec.ID];

                // 轉出原因
                if (UpdateRecDesc04Dic.ContainsKey(studRec.ID))
                    stee.UpdateDesc = UpdateRecDesc04Dic[studRec.ID];

                stee.ChancellorName = ChancellorName;
                StudTransExportList.Add(stee);
            }

            return StudTransExportList;
        }

        /// <summary>
        /// 取得畢業證書需要資料
        /// </summary>
        /// <param name="StudentList"></param>
        /// <returns></returns>
        public static List<StudGraduateCertficateEntity> GetStudGraduateCertficateEntityList(List<string> StudentIDList)
        {
            List<StudGraduateCertficateEntity> retValue = new List<StudGraduateCertficateEntity>();
            // 取得畢業照片
            Dictionary<string,string> GraduatePhotoDic = K12.Data.Photo.SelectGraduatePhoto(StudentIDList);
            Dictionary<string, string> GraduateSchoolYearDic = GetGradeSchoolYear(StudentIDList);
            Dictionary<string, string> CertDocNoDic = GetCertDocNoDic(StudentIDList);

            // 取得學校資訊
            string SchoolName = JHSchool.Data.JHSchoolInfo.ChineseName;
            string SchoolEngName = JHSchool.Data.JHSchoolInfo.EnglishName;
            string ChancellorName = JHSchool.Data.JHSchoolInfo.ChancellorChineseName;
            string ChancellorEngName = JHSchool.Data.JHSchoolInfo.ChancellorEnglishName;

            // 學生基本資料
            foreach (JHSchool.Data.JHStudentRecord studRec in JHSchool.Data.JHStudent.SelectByIDs(StudentIDList))
            {
                StudGraduateCertficateEntity sgce = new StudGraduateCertficateEntity();
                if(studRec.Birthday.HasValue )
                    sgce.Birthday = studRec.Birthday.Value;
                sgce.ChancellorEngName = ChancellorEngName;
                sgce.ChancellorName = ChancellorName;
                sgce.EngName = studRec.EnglishName;
                sgce.Name = studRec.Name;
                sgce.SchoolEngName =SchoolEngName ;
                
                // 照片
                if(GraduatePhotoDic.ContainsKey(studRec.ID))
                    sgce.PhotoStr = GraduatePhotoDic[studRec.ID];

                // 畢業證書字號
                if (CertDocNoDic.ContainsKey(studRec.ID))
                    sgce.CertDocNo = CertDocNoDic[studRec.ID];

                sgce.SchoolName = SchoolName;
                
                // 畢業年月
                if (GraduateSchoolYearDic.ContainsKey(studRec.ID))
                    sgce.GraduateSchoolYear = GraduateSchoolYearDic[studRec.ID];


                retValue.Add(sgce);
            }
            return retValue;
        }

        /// <summary>
        /// 取得畢業年月
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetGradeSchoolYear(List<string> StudentIDList)
        {
            Dictionary<string, string> retValue = new Dictionary<string, string>();
            foreach (JHSchool.Data.JHUpdateRecordRecord updRec in JHSchool.Data.JHUpdateRecord.SelectByStudentIDs(StudentIDList))
            {
                if (updRec.UpdateCode == "2")
                    if (!retValue.ContainsKey(updRec.StudentID))
                        retValue.Add(updRec.StudentID, updRec.GraduateSchoolYear);
            }
            return retValue;
        }

        /// <summary>
        /// 取得畢業證書字號
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetCertDocNoDic(List<string> StudentIDList)
        {
            Dictionary<string, string> retValue = new Dictionary<string, string>();
            foreach (JHSchool.Data.JHUpdateRecordRecord updRec in JHSchool.Data.JHUpdateRecord.SelectByStudentIDs(StudentIDList))
            {
                if (updRec.UpdateCode == "2")
                    if (!retValue.ContainsKey(updRec.StudentID))
                        retValue.Add(updRec.StudentID, updRec.GraduateCertificateNumber);
            }
            return retValue;
        }


        /// <summary>
        /// 取得休學資料
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static List<StudentLeaveEntity> GetStudentLeaveEntityList(List<string> StudentIDList)
        {
            List<StudentLeaveEntity> StudentLeaveEntityList = new List<StudentLeaveEntity>();
            string SchoolName = JHSchool.Data.JHSchoolInfo.ChineseName;

            string ChancellorName = JHSchool.Data.JHSchoolInfo.ChancellorChineseName;

            // 學生地址
            Dictionary<string, JHSchool.Data.JHAddressRecord> StudAddressRecDic = new Dictionary<string, JHSchool.Data.JHAddressRecord>();

            foreach (JHSchool.Data.JHAddressRecord addRec in JHSchool.Data.JHAddress.SelectByStudentIDs(StudentIDList))
                StudAddressRecDic.Add(addRec.RefStudentID, addRec);            

            // 取得學生異動
            List<JHSchool.Data.JHUpdateRecordRecord> StudUpdateRecList = JHSchool.Data.JHUpdateRecord.SelectByStudentIDs(StudentIDList);
            Dictionary<string, JHSchool.Data.JHUpdateRecordRecord> UpdateRecDic = new Dictionary<string, JHSchool.Data.JHUpdateRecordRecord>();
            Dictionary<string, string> UpdateRecDesc05Dic = new Dictionary<string, string>();
            Dictionary<string, DateTime> UpdateRecUpdateDate05Dic = new Dictionary<string, DateTime>();
            Dictionary<string, string> CertDocNoDic = new Dictionary<string, string>();


            // 取得新生異動
            foreach (JHSchool.Data.JHUpdateRecordRecord rec in StudUpdateRecList)
            {
                if (rec.UpdateCode == "1")
                {
                    if (!UpdateRecDic.ContainsKey(rec.StudentID))
                        UpdateRecDic.Add(rec.StudentID, rec);
                }

                if (rec.UpdateCode == "5")
                {
                    if (!UpdateRecDesc05Dic.ContainsKey(rec.StudentID))
                    {
                        UpdateRecDesc05Dic.Add(rec.StudentID, rec.UpdateDescription);
                        DateTime dt;
                        if(DateTime.TryParse(rec.UpdateDate ,out dt))
                            UpdateRecUpdateDate05Dic.Add(rec.StudentID, dt);
                    }
                }

                if (rec.UpdateCode == "2")
                {
                    if (!CertDocNoDic.ContainsKey(rec.StudentID))
                        CertDocNoDic.Add(rec.StudentID, rec.GraduateCertificateNumber);
                }

            }


            // 照片 Idx
            Dictionary<string, string> FreshmanPhotoDic = K12.Data.Photo.SelectFreshmanPhoto(StudentIDList);
            List<JHSchool.Data.JHStudentRecord> StudRecs = JHSchool.Data.JHStudent.SelectByIDs(StudentIDList);

            foreach (JHSchool.Data.JHStudentRecord studRec in StudRecs)
            {
                StudentLeaveEntity stee = new StudentLeaveEntity();
                stee.SchoolName = SchoolName;
                stee.Name = studRec.Name;
                stee.Gender = studRec.Gender;
                if (studRec.Birthday.HasValue)
                    stee.Birthday = studRec.Birthday.Value;
                if (studRec.Class != null)
                    if (studRec.Class.GradeYear.HasValue)
                        stee.ClassGradeYear = "" + studRec.Class.GradeYear.Value;

                // 新生異動
                if (UpdateRecDic.ContainsKey(studRec.ID))
                {
                    // 沒有核准日期
                    if (string.IsNullOrEmpty(UpdateRecDic[studRec.ID].ADDate))
                    {
                        stee.UR01CertDate = "";
                        stee.UR01CertDocNo = "";
                        stee.UR01Date = UpdateRecDic[studRec.ID].UpdateDate;
                    }
                    else
                    {
                        stee.UR01Date = "";
                        stee.UR01CertDate = UpdateRecDic[studRec.ID].ADDate;
                        stee.UR01CertDocNo = UpdateRecDic[studRec.ID].ADNumber;
                    }
                }

                // 照片
                if (FreshmanPhotoDic.ContainsKey(studRec.ID))
                    stee.PhotoStr = FreshmanPhotoDic[studRec.ID];

                // 轉出原因
                if (UpdateRecDesc05Dic.ContainsKey(studRec.ID))
                    stee.UpdateDesc = UpdateRecDesc05Dic[studRec.ID];

                // 畢業證書字號
                if (CertDocNoDic.ContainsKey(studRec.ID))
                    stee.CertDocNo = CertDocNoDic[studRec.ID];

                if (StudAddressRecDic.ContainsKey(studRec.ID))
                    stee.ContactAddress = StudAddressRecDic[studRec.ID].Mailing.County + StudAddressRecDic[studRec.ID].Mailing.Town +StudAddressRecDic[studRec.ID].Mailing.District + StudAddressRecDic[studRec.ID].Mailing.Area + StudAddressRecDic[studRec.ID].Mailing.Detail;
                if(UpdateRecDesc05Dic.ContainsKey(studRec.ID ))
                {
                    stee.LeaveUpdateDateC = (UpdateRecUpdateDate05Dic[studRec.ID].Year - 1911) + "年" + UpdateRecUpdateDate05Dic[studRec.ID].Month + "月" + UpdateRecUpdateDate05Dic[studRec.ID].Day+"日";
                    stee.LeaveEndDateC = (UpdateRecUpdateDate05Dic[studRec.ID].Year - 1910) + "年" + UpdateRecUpdateDate05Dic[studRec.ID].Month + "月" + UpdateRecUpdateDate05Dic[studRec.ID].Day + "日";
                    
                    int SchoolYear =UpdateRecUpdateDate05Dic[studRec.ID].Year - 1911;
                    int semester=2;
                    if (UpdateRecUpdateDate05Dic[studRec.ID].Month >= 9)
                        semester = 1;
                    else
                        SchoolYear--;
                    stee.LeavePeriodString = SchoolYear + "學年度第" + semester + "學期至" + (SchoolYear + 1) + "學年度第" + semester + "學期";                    

                }
                stee.ChancellorName = ChancellorName;
                StudentLeaveEntityList.Add(stee);
            }

            return StudentLeaveEntityList;
        }
    }
}
