using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using Framework;
using System.Windows.Forms;

namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    // 班級男女人數比例
    class ClassGenderPercentageRpt
    {
        List<DAL.StudentEntity> StudentEntityList;
        Dictionary<string, List<DAL.ClassStudentCount>> ClassStudentCountDic;
        Dictionary<string, DAL.ClassStudentCount> GradeStudentCountDic;
        public ClassGenderPercentageRpt()
        {
            List<ClassRecord> selectClassRecList = Class.Instance.SelectedList;
            StudentEntityList = DAL.Transfer.GetSelectStudentEntitys(selectClassRecList);
            ClassStudentCountDic = DAL.Transfer.GetClassStudentPercentage(StudentEntityList, selectClassRecList);
            GradeStudentCountDic = DAL.Transfer.GetGradeStudentCount(StudentEntityList);

            List<DAL.StudentEntity> ErrorData = new List<JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentEntity>();

            foreach (DAL.StudentEntity se in StudentEntityList)
            {
                if (!string.IsNullOrEmpty(se.Memo))
                    ErrorData.Add(se);
            }

            foreach (DAL.StudentEntity se in StudentEntityList)
            {
                if ((se.Gender != "男") && (se.Gender != "女"))
                {
                    se.Memo = "未分性別";
                    ErrorData.Add(se);
                }
            }

            ClassCellReportManger ccrm = new ClassCellReportManger();
            ccrm.ProcessClassGenderPercentage("男女人數比例", "男女人數比例", ClassStudentCountDic, GradeStudentCountDic, ErrorData);
        }
    }
}