using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ClassExtendControls.Reports.DAL
{
    class Transfer
    {


        /// <summary>
        /// 取得學生資料
        /// </summary>
        /// <param name="ClassRecordList"></param>
        /// <returns></returns>
        public static List<StudentEntity> GetSelectStudentEntitys(List<ClassRecord> ClassRecordList)
        {
            List<StudentEntity> StudentEntityList = new List<StudentEntity>();

            foreach (ClassRecord classRec in ClassRecordList)
            {
                // 一般
                foreach (StudentRecord studRec in classRec.Students.GetStatusStudents("一般"))
                {
                    StudentEntity se = new StudentEntity();
                    se.ClassID = classRec.ID;
                    se.ClassName = classRec.Name;
                    se.Gender = studRec.Gender;
                    se.GradeYear = classRec.GradeYear;
                    se.Birthday = studRec.Birthday;
                    se.StudentID = studRec.ID;
                    se.Name = studRec.Name;
                    se.StudentNumber = studRec.StudentNumber;
                    se.SeatNo = studRec.SeatNo;
                    StudentEntityList.Add(se);
                }                

                // 輟學
                foreach (StudentRecord studRec in classRec.Students.GetStatusStudents("輟學"))
                {
                    StudentEntity se = new StudentEntity();
                    se.ClassID = classRec.ID;
                    se.ClassName = classRec.Name;
                    se.Gender = studRec.Gender;
                    se.GradeYear = classRec.GradeYear;
                    se.Birthday = studRec.Birthday;
                    se.StudentID = studRec.ID;
                    se.Name = studRec.Name;
                    se.StudentNumber = studRec.StudentNumber;
                    se.SeatNo = studRec.SeatNo;
                    StudentEntityList.Add(se);
                }

            }
            return StudentEntityList;
        }

        /// <summary>
        /// 透過 DAL取得特定狀態學生，分年級放 Dictionary 並班級內學生按座號排序
        /// </summary>
        /// <param name="ClassIDList"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        
        public static Dictionary<string,List<ClassStudentEntity>> GetGradeClassStudentDic(List<string> ClassIDList,JHSchool.Data.JHStudentRecord.StudentStatus StudStatus)
        {
            Dictionary<string ,List<ClassStudentEntity>> GradeClassStudentDic = new Dictionary<string,List<ClassStudentEntity>> ();

            // 取得班級學生
            List<JHSchool.Data.JHStudentRecord> StudRecs = JHSchool.Data.JHStudent.SelectByClassIDs(ClassIDList );
            List<JHSchool.Data.JHClassRecord> ClassRecs = JHSchool.Data.JHClass.SelectByIDs(ClassIDList);

            // 班排序
            ClassRecs.Sort(new Comparison <JHSchool.Data.JHClassRecord>(ClassDisplaySorter1));
            

            foreach (JHSchool.Data.JHClassRecord cr in ClassRecs)
            {                
                ClassStudentEntity cse = new ClassStudentEntity();
                cse.ClassID = cr.ID;
                cse.ClassName = cr.Name;
                cse.StudentEntityList = new List<StudentEntity> ();

                string strGradeYear = "0";
                if (cr.GradeYear.HasValue)
                    strGradeYear = cr.GradeYear.Value + "";

                if (GradeClassStudentDic.ContainsKey(strGradeYear))
                    GradeClassStudentDic[strGradeYear].Add(cse);
                else
                {
                    List<ClassStudentEntity> cseList = new List<ClassStudentEntity>();
                    cseList.Add(cse);
                    GradeClassStudentDic.Add(strGradeYear, cseList);
                }            
            }

            foreach (JHSchool.Data.JHStudentRecord studRec in StudRecs)
            {
                // 判斷學生狀態是否符合,不符合跳過
                if(studRec.Status != StudStatus )
                    continue ;

                string strGradeYear="0";
                string ClassID="";
                if (studRec.Class != null)
                {
                    ClassID = studRec.Class.ID;
                    if (studRec.Class.GradeYear.HasValue)
                        strGradeYear = studRec.Class.GradeYear.Value + "";
                }
                StudentEntity se = new StudentEntity();
                se.Gender = studRec.Gender;
                if (studRec.SeatNo.HasValue)
                {
                    se.SeatNo = studRec.SeatNo.Value + "";
                    se.SeatNoInt = studRec.SeatNo.Value;
                }
                else
                    se.SeatNoInt = 99;
                se.StudentNumber = studRec.StudentNumber;
                se.Name = studRec.Name;

                if (GradeClassStudentDic.ContainsKey(strGradeYear))
                { 
                    foreach (ClassStudentEntity cse in GradeClassStudentDic[strGradeYear])
                        if (cse.ClassID == ClassID)
                        {
                            cse.StudentEntityList.Add(se);
                            break;
                        }
                }            
            }

            // 排序
            foreach (List<ClassStudentEntity> cseList in GradeClassStudentDic.Values)
                foreach (ClassStudentEntity cse in cseList)
                    cse.StudentEntityList.Sort(new Comparison<StudentEntity>(ClassStudSorter1));

            return GradeClassStudentDic ;
        }

        // 座號排序用
        private static int ClassStudSorter1(StudentEntity x, StudentEntity y)
        {
            return x.SeatNoInt.CompareTo(y.SeatNoInt);        
        }


        // 班級排序
        private static int ClassDisplaySorter1(JHSchool.Data.JHClassRecord x, JHSchool.Data.JHClassRecord y)
        {
            int IntX=0,IntY=0;
            
            int.TryParse(x.DisplayOrder, out IntX);
            int.TryParse(y.DisplayOrder, out IntY);
            
            // 當有 Display order 
            if (IntX > 0)
                return IntX.CompareTo(IntY);
            else
                return x.Name.CompareTo(y.Name);
        
        }


        /// <summary>
        /// 取得班級男女人數比例
        /// </summary>
        /// <param name="StudentEntitys"></param>
        /// <param name="ClassRecordList"></param>
        /// <returns></returns>
        public static Dictionary<string, List<ClassStudentCount>> GetClassStudentPercentage(List<StudentEntity> StudentEntitys, List<ClassRecord> ClassRecordList)
        {
            Dictionary<string, List<ClassStudentCount>> ClassStudentCountDic = new Dictionary<string, List<ClassStudentCount>>();
            Dictionary<string, ClassStudentCount> tmpClassStudDic = new Dictionary<string, ClassStudentCount>();
            foreach (ClassRecord classRec in ClassRecordList)
            {
                ClassStudentCount csc = new ClassStudentCount();
                csc.ClassID = classRec.ID;
                csc.ClassName = classRec.Name;
                
                if(classRec.Teacher !=null )
                    csc.TeacherName = classRec.Teacher.Name;
                csc.DisplayOrder = classRec.DisplayOrder;
                csc.GradeYear = classRec.GradeYear;
                tmpClassStudDic.Add(csc.ClassID, csc);
            }

            foreach (StudentEntity se in StudentEntitys)
            {
                if (tmpClassStudDic.ContainsKey(se.ClassID))
                {
                    tmpClassStudDic[se.ClassID].AddGenderCount(se.Gender);
                }
            }

            foreach (ClassStudentCount csc in tmpClassStudDic.Values)
            {
                string strGradeYear = csc.GradeYear.Trim();
                if (strGradeYear == "")
                    strGradeYear = " ";

                if (ClassStudentCountDic.ContainsKey(strGradeYear))
                    ClassStudentCountDic[strGradeYear].Add(csc);
                else
                {
                    List<ClassStudentCount> cscList = new List<ClassStudentCount>();
                    cscList.Add(csc);
                    ClassStudentCountDic.Add(strGradeYear, cscList);
                }

            }
            return ClassStudentCountDic;
        }

        /// <summary>
        /// 取得年級人數比例
        /// </summary>
        /// <param name="StudentEntitys"></param>
        /// <returns></returns>
        public static Dictionary<string, ClassStudentCount> GetGradeStudentCount(List<StudentEntity> StudentEntitys)
        {
            Dictionary<string, ClassStudentCount> GradeStudentCountDic = new Dictionary<string, ClassStudentCount>();

            foreach (StudentEntity se in StudentEntitys)
            {
                string strGradeYear = se.GradeYear.Trim();
                if (strGradeYear == "")
                    strGradeYear = " ";

                if (GradeStudentCountDic.ContainsKey(strGradeYear))
                {
                    GradeStudentCountDic[strGradeYear].AddGenderCount(se.Gender);
                }
                else
                {
                    ClassStudentCount csc = new ClassStudentCount();
                    csc.ClassName = strGradeYear;
                    csc.AddGenderCount(se.Gender);
                    GradeStudentCountDic.Add(strGradeYear, csc);
                }

            }

            return GradeStudentCountDic;
        }

        /// <summary>
        /// 取得里鄰資料
        /// </summary>
        /// <param name="StudentEntityList"></param>
        /// <returns></returns>
        public static List<StudentEntity> GetClassDistrictArea(List<StudentEntity> StudentEntityList)
        {
            // 取得選取的 ID
            List<string> tmpStudentIDs = new List<string>();
            foreach (StudentEntity se in StudentEntityList)
                tmpStudentIDs.Add(se.StudentID);

            // 透過 DAL 取得

            Dictionary<string, JHSchool.Data.JHAddressRecord> tmpAddressRecDic = new Dictionary<string, JHSchool.Data.JHAddressRecord>();
            List<JHSchool.Data.JHAddressRecord> AddressRecList = JHSchool.Data.JHAddress.SelectByStudentIDs(tmpStudentIDs);                
            foreach (JHSchool.Data.JHAddressRecord AddressRec in AddressRecList)
                tmpAddressRecDic.Add(AddressRec.RefStudentID, AddressRec);

            // 取得戶籍里鄰            
            foreach (StudentEntity se in StudentEntityList)
            {
                if (tmpAddressRecDic.ContainsKey(se.StudentID))
                {
                    se.PermanentDistrict = tmpAddressRecDic[se.StudentID].Permanent.District;
                    se.PermanentArea = tmpAddressRecDic[se.StudentID].Permanent.Area;
                }
            }
            return StudentEntityList;
        }

        /// <summary>
        /// 取得父母親職業
        /// </summary>
        /// <param name="StudentEntityList"></param>
        /// <returns></returns>
        public static List<StudentEntity> GetClassStudentParentJob(List<StudentEntity> StudentEntityList)
        {
            
            List<string> StudentIDs = new List<string>();
            foreach (StudentEntity se in StudentEntityList)
                StudentIDs.Add(se.StudentID);
            // 父親
            Dictionary<string, K12.Data.Father> tmpFatherJobDic = new Dictionary<string, K12.Data.Father>();

            // 母親
            Dictionary<string, K12.Data.Mother> tmpMotherJobDic = new Dictionary<string, K12.Data.Mother>();

            // 監護人
            Dictionary<string, K12.Data.Custodian> tmpCustodianJobDic = new Dictionary<string, K12.Data.Custodian>();

            List<JHSchool.Data.JHParentRecord> ParentRecords = JHSchool.Data.JHParent.SelectByStudentIDs(StudentIDs);

            foreach (JHSchool.Data.JHParentRecord ParentRec in ParentRecords)
            {
                tmpFatherJobDic.Add(ParentRec.RefStudentID, ParentRec.Father);
                tmpMotherJobDic.Add(ParentRec.RefStudentID, ParentRec.Mother);
                tmpCustodianJobDic.Add(ParentRec.RefStudentID, ParentRec.Custodian);
            }

            foreach (StudentEntity se in StudentEntityList)
            {
                if (tmpFatherJobDic.ContainsKey(se.StudentID))
                    se.FatherJob = tmpFatherJobDic[se.StudentID].Job;

                if (tmpMotherJobDic.ContainsKey(se.StudentID))
                    se.MotherJob = tmpMotherJobDic[se.StudentID].Job;

                if (tmpCustodianJobDic.ContainsKey(se.StudentID))
                    se.CustodianJob = tmpCustodianJobDic[se.StudentID].Job;
            }
            return StudentEntityList;
        }


        /// <summary>
        /// 取得學生類別完整名稱
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetStudentTagFullName()
        {
            Dictionary<string, string> TagRecFullName = new Dictionary<string, string>();
            List<JHSchool.Data.JHTagConfigRecord> TagRecs = JHSchool.Data.JHTagConfig.SelectAll();
            foreach (JHSchool.Data.JHTagConfigRecord tcr in TagRecs)
            {
                if (tcr.Category == "Student")
                    TagRecFullName.Add(tcr.ID, tcr.FullName);
            }
            return TagRecFullName;
        }

        /// <summary>
        /// 填入學生類別到 StudentEntity
        /// </summary>
        /// <param name="StudentEntityList"></param>
        /// <returns></returns>
        public static List<StudentEntity> FillStudentTag(List<StudentEntity> StudentEntityList, Dictionary<string, string> SelectedStudTagFullName)
        {
            List<string> StudentIDList = new List<string>();
            Dictionary<string, List<string>> sidIdx = new Dictionary<string, List<string>>();
            foreach (StudentEntity se in StudentEntityList)
                StudentIDList.Add(se.StudentID);

            JHSchool.Data.JHStudentTag.SelectAll();
            List<JHSchool.Data.JHStudentTagRecord> StudTagRecList = JHSchool.Data.JHStudentTag.SelectByStudentIDs(StudentIDList);
            foreach (JHSchool.Data.JHStudentTagRecord str in StudTagRecList)
            {
                string sid = str.Student.ID;

                string strFullName = "";
                if (SelectedStudTagFullName.ContainsKey(str.RefTagID))
                    strFullName = SelectedStudTagFullName[str.RefTagID];


                if (sidIdx.ContainsKey(sid))
                {
                    sidIdx[sid].Add(strFullName);
                }
                else
                {
                    List<string> tmpName = new List<string>();
                    tmpName.Add(strFullName);
                    sidIdx.Add(sid, tmpName);
                }
            }

            foreach (StudentEntity se in StudentEntityList)
            {
                if (sidIdx.ContainsKey(se.StudentID))
                    se.StudentTagFullName = sidIdx[se.StudentID];
            }

            return StudentEntityList;
        }

    }
}
