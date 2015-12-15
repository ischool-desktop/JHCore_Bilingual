using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec
{

    /// <summary>
    /// 由 Student 類別所提供的 Extend Method
    /// </summary>
    public static class Student_ExtendMethod
    {

        /// <summary>
        /// 檢查學生身份證是否重覆。
        /// </summary>
        public static bool IsIDNumberExists(this StudentRecord student, string IDNumber)
        {
            return JHSchool.Permrec.Feature.Legacy.QueryStudent.IDNumberExists(student.ID, IDNumber);
        }
    }
}