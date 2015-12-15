using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.EduAdminExtendCotnrols.ClassUpgrade
{
    // 學生狀態
    class StudentItem
    {
        public string StudentID { get; set; }
        /// <summary>
        /// 學生班級
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 學生狀態
        /// </summary>
        public string Status { get; set; }
    }
}
