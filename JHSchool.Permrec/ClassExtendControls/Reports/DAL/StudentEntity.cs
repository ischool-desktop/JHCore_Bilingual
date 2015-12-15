using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ClassExtendControls.Reports.DAL
{
    // 報表用學生
    class StudentEntity
    {
        /// <summary>
        /// 學生編號
        /// </summary>
        public string StudentID { get; set; }
        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }
        /// <summary>
        /// 班級ID
        /// </summary>
        public string ClassID { get; set; }
        /// <summary>
        /// 班級名稱
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 座號
        /// </summary>
        public string SeatNo { get; set; }

        /// <summary>
        /// 座號 int
        /// </summary>
        public int SeatNoInt { get; set; }
        /// <summary>
        /// 性別
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 學生年級
        /// </summary>
        public string GradeYear { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 父親職業
        /// </summary>
        public string FatherJob { get; set; }

        /// <summary>
        /// 母親職業
        /// </summary>
        public string MotherJob { get; set; }

        /// <summary>
        /// 監護人職業
        /// </summary>
        public string CustodianJob { get; set; }

        /// <summary>
        /// 戶籍里
        /// </summary>
        public string PermanentDistrict { get; set; }

        /// <summary>
        /// 戶籍鄰
        /// </summary>
        public string PermanentArea { get; set; }

        /// <summary>
        /// 自訂備註
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 學生類別完整名稱
        /// </summary>
        public List<string> StudentTagFullName { get; set; }

        /// <summary>
        /// 取得年齡(新竹市)
        /// </summary>
        /// <returns></returns>
        public int GetAge()
        {
            // 依新竹市 前年 9月2日 ~ 今年9月1日
            // 9月2號 以後少不足-1
            DateTime BeginDate, EndDate, dt;
            int Age = 0;
            if (DateTime.TryParse(Birthday, out dt))
            {                
                DateTime.TryParse((dt.Year) + "/9/2", out BeginDate);
             
                Age = DateTime.Now.Year - dt.Year;
                if (dt >= BeginDate)
                    Age--;
            }
            return Age;
        }
    }
}
