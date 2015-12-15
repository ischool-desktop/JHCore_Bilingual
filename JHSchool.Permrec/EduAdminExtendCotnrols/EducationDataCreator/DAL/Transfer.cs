using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace JHSchool.Permrec.EduAdminExtendCotnrols.EducationDataCreator.DAL
{
    class Transfer
    {

        /// <summary>
        /// 透過StudentIDs取得 StudentEntityList
        /// </summary>
        /// <param name="GradeYear"></param>
        /// <returns></returns>
        public static List<StudentEntity> GetStudentEntityListByStudentIDList(List<string> StudentIDList)
        {
            List<StudentEntity> StudentEntityList = new List<StudentEntity>();
            
            // 因 JHScool.Data 未支援，目前直接呼叫 service
            string SchoolCode = GetSchoolCode();

            List<JHSchool.Data.JHStudentRecord> AllStudentRecList = JHSchool.Data.JHStudent.SelectByIDs(StudentIDList);

            foreach (JHSchool.Data.JHStudentRecord studRec in AllStudentRecList)
            {
                // 一般
                if (studRec.Status == JHSchool.Data.JHStudentRecord.StudentStatus.一般 || studRec.Status == JHSchool.Data.JHStudentRecord.StudentStatus.輟學 || studRec.Status == JHSchool.Data.JHStudentRecord.StudentStatus.畢業或離校)
                {
                    DAL.StudentEntity se = new StudentEntity();

                    if (studRec.Class != null)
                    {
                            se.ClassName = studRec.Class.Name;
                            if(studRec.Class.GradeYear.HasValue)
                                se.GradeYear = studRec.Class.GradeYear.Value;
                    }
                    se.IDNumber = studRec.IDNumber;
                    se.Name = studRec.Name;
                    if (studRec.SeatNo.HasValue)
                        se.SeatNo = studRec.SeatNo.Value;
                    if (studRec.Birthday.HasValue)
                        se.Birthday = studRec.Birthday.Value;

                    se.ID = studRec.ID;
                    se.SchoolCode = SchoolCode;
                    se.StudStatus = studRec.Status.ToString();
                    StudentEntityList.Add(se);

                }

                //// 輟學
                //if (studRec.Status == JHSchool.Data.JHStudentRecord.StudentStatus.輟學)
                //{
                //    if (studRec.Class != null && studRec.Class.GradeYear.HasValue)
                //    {
                //        DAL.StudentEntity se = new StudentEntity();
                //        se.ClassName = studRec.Class.Name;
                //        se.GradeYear = studRec.Class.GradeYear.Value;
                //        se.IDNumber = studRec.IDNumber;
                //        se.Name = studRec.Name;
                //        if (studRec.SeatNo.HasValue)
                //            se.SeatNo = studRec.SeatNo.Value;
                //        if (studRec.Birthday.HasValue)
                //            se.Birthday = studRec.Birthday.Value;

                //        se.ID = studRec.ID;
                //        se.SchoolCode = SchoolCode;
                //        StudentEntityList.Add(se);
                //    }
                //}

            }

            // 取得畢業離校資訊
            List<JHSchool.Data.JHLeaveInfoRecord> LeaveInfoList = JHSchool.Data.JHLeaveIfno.SelectByStudentIDs(StudentIDList);
            Dictionary<string, string> StatusInfo = new Dictionary<string, string>();
            foreach (JHSchool.Data.JHLeaveInfoRecord lir in LeaveInfoList)
            {
                string Reason = "";

                if (lir.Reason != null)
                    Reason = lir.Reason.Replace(" ", "");

                StatusInfo.Add(lir.RefStudentID,Reason);
            }
            foreach (StudentEntity se in StudentEntityList)
            {
                if (StatusInfo.ContainsKey(se.ID))
                    se.Status = StatusInfo[se.ID];            
            }

            return StudentEntityList;
        }

        // 取得學校代碼
        private static string GetSchoolCode()
        {
            string code = "";
            XmlElement element = Framework.Feature.Config.GetSchoolInfo();
            code = getNodeData("Code", element, "SchoolInformation");
            return code;
        }

        private static string getNodeData(string nodeName, XmlElement Element, string nodesName)
        {
            string value = "";
            foreach (XmlElement xe in Element.SelectNodes(nodesName))
            {
                if (xe.SelectSingleNode(nodeName) != null)
                    value = xe.SelectSingleNode(nodeName).InnerText;
            }
            return value;
        }

    }
}
