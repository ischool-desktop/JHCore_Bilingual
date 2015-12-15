using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ClassExtendControls.Reports.DAL
{
    class ClassStudentEntity
    {
        /// <summary>
        /// Class ID
        /// </summary>
        public string ClassID { get; set; }
        /// <summary>
        /// Class Name
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 學生屬性
        /// </summary>
        public List<StudentEntity> StudentEntityList;
    }
}
