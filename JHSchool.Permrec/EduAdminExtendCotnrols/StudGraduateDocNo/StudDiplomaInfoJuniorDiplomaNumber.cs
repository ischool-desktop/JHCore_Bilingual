using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.EduAdminExtendCotnrols.StudGraduateDocNo
{
    /// <summary>
    /// 產生畢業證書字號使用
    /// </summary>
    class StudDiplomaInfoJuniorDiplomaNumber
    {
        /// <summary>
        /// 學生系統編號
        /// </summary>
        public string StudentID { get; set; }
        /// <summary>
        /// 班級
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// ClassID
        /// </summary>
        public string ClassID { get; set; }

        /// <summary>
        /// 年級
        /// </summary>
        public int GradeYear { get; set; }

        /// <summary>
        /// 班級序號
        /// </summary>
        public string ClassDisplayOrder { get; set; }
        /// <summary>
        /// 座號
        /// </summary>
        public int SeatNo { get; set; }
        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 畢修業狀態
        /// </summary>
        public string GRStatus { get; set; }
        /// <summary>
        /// 畢業證書字號
        /// </summary>
        public string GRDocNo { get; set; }
        /// <summary>
        /// JH離校紀錄
        /// </summary>
        public JHSchool.Data.JHLeaveInfoRecord LeaveInfoRec;

    }
}
