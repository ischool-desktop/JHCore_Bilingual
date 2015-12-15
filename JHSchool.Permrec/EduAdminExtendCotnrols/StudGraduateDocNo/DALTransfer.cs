using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.EduAdminExtendCotnrols.StudGraduateDocNo
{
    class DALTransfer
    {
        /// <summary>
        /// 取得學生畢修業相關資料(依年級)
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static List<StudDiplomaInfoJuniorDiplomaNumber> GetStudentDiplomaInfoJuniorDiplomaNumberListByGradeYear(string GradeYear)
        {
            int intGradeYear;
            int.TryParse(GradeYear, out intGradeYear);

            List<StudDiplomaInfoJuniorDiplomaNumber> DiplomaInfoJuniorDiplomaNumberList = new List<StudDiplomaInfoJuniorDiplomaNumber>();
            List<string> StudentIDList = new List<string>();
            List<JHSchool.Data.JHStudentRecord> StudRecList = JHSchool.Data.JHStudent.SelectAll();

            // 放入學生基本資料
            foreach (JHSchool.Data.JHStudentRecord studRec in StudRecList)
            {
                // 濾過沒有班級
                if (studRec.Class == null)
                    continue;

                // 只找一般生
                if (studRec.Status != K12.Data.StudentRecord.StudentStatus.一般)
                    continue;

                if (studRec.Class.GradeYear.HasValue)
                {
                    if (intGradeYear == studRec.Class.GradeYear.Value)
                    {
                        StudDiplomaInfoJuniorDiplomaNumber sdijdn = new StudDiplomaInfoJuniorDiplomaNumber();
                        if (studRec.Class != null)
                        {
                            sdijdn.ClassName = studRec.Class.Name;
                            sdijdn.ClassDisplayOrder = studRec.Class.DisplayOrder;
                            if (studRec.Class.GradeYear.HasValue)
                                sdijdn.GradeYear = studRec.Class.GradeYear.Value;
                            sdijdn.ClassID = studRec.Class.ID;
                        }
                        sdijdn.Name = studRec.Name;
                        if (studRec.SeatNo.HasValue)
                            sdijdn.SeatNo = studRec.SeatNo.Value;
                        sdijdn.StudentID = studRec.ID;
                        sdijdn.StudentNumber = studRec.StudentNumber;
                        StudentIDList.Add(studRec.ID);
                        DiplomaInfoJuniorDiplomaNumberList.Add(sdijdn);
                    }
                }
            }

            List<JHSchool.Data.JHLeaveInfoRecord> LeaveInfoRecordList = JHSchool.Data.JHLeaveIfno.SelectByStudentIDs(StudentIDList);
            Dictionary<string, JHSchool.Data.JHLeaveInfoRecord> LIRDic = new Dictionary<string, JHSchool.Data.JHLeaveInfoRecord>();
            foreach (JHSchool.Data.JHLeaveInfoRecord rec in LeaveInfoRecordList)
                LIRDic.Add(rec.RefStudentID, rec);


            // 放入離校資訊
            foreach (StudDiplomaInfoJuniorDiplomaNumber sdijdn in DiplomaInfoJuniorDiplomaNumberList)
            {
                if (LIRDic.ContainsKey(sdijdn.StudentID))
                {
                    sdijdn.GRDocNo = LIRDic[sdijdn.StudentID].DiplomaNumber;
                    sdijdn.GRStatus = LIRDic[sdijdn.StudentID].Reason;
                    sdijdn.LeaveInfoRec = LIRDic[sdijdn.StudentID];
                }
            }

            return DiplomaInfoJuniorDiplomaNumberList;
        }

        /// <summary>
        /// 儲存學生畢修業相關資料
        /// </summary>
        /// <param name="DiplomaInfoJuniorDiplomaNumberList"></param>
        public static void SetStudentDiplomaInfoJuniorDiplomaNumberList(List<StudDiplomaInfoJuniorDiplomaNumber> DiplomaInfoJuniorDiplomaNumberList)
        {
            List<JHSchool.Data.JHLeaveInfoRecord> LIRRec = new List<JHSchool.Data.JHLeaveInfoRecord>();
            foreach (StudDiplomaInfoJuniorDiplomaNumber rec in DiplomaInfoJuniorDiplomaNumberList)
            {
                rec.LeaveInfoRec.DiplomaNumber = rec.GRDocNo;
                rec.LeaveInfoRec.Reason = rec.GRStatus;
                LIRRec.Add(rec.LeaveInfoRec);
            }

            if (LIRRec.Count > 0)
                JHSchool.Data.JHLeaveIfno.Update(LIRRec);

        }
    }
}
