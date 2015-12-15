using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.EduAdminExtendCotnrols.EducationDataCreator.DAL
{
    class StudentEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 學生系統編號
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 身分證號
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 教育程度碼
        /// </summary>
        public string CertCode { get; set; }

        /// <summary>
        /// 學校代碼
        /// </summary>
        public string SchoolCode { get; set; }

        /// <summary>
        /// 年級
        /// </summary>
        public int GradeYear { get; set; }

        /// <summary>
        /// 班級
        /// </summary>
        public string ClassName { get; set;}

        /// <summary>
        /// 座號
        /// </summary>
        public int SeatNo { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set;}

        /// <summary>
        /// 狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 學生狀態
        /// </summary>
        public string StudStatus { get; set; }

        /// <summary>
        /// 取得報部生日
        /// </summary>
        /// <returns></returns>
        public string GetBirthdayGovStr()
        {
            //DateTime dt;
            string str = "";
            //if (!string.IsNullOrEmpty(Birthday))
            //{
            //    if (DateTime.TryParse(Birthday, out dt))
            //    {
            if (Birthday != null)
            {
                int Year = (Birthday.Year - 1911);
                if (Year < 100)
                    str += "0" + Year;
                else
                    str += "" + Year;

                if (Birthday.Month < 10)
                    str += "0" + Birthday.Month;
                else
                    str += "" + Birthday.Month;

                if (Birthday.Day < 10)
                    str += "0" + Birthday.Day;
                else
                    str += "" + Birthday.Day;
            }
            //    }

            //}
            return str;
        }

        /// <summary>
        /// 備註
        /// </summary>
        public string Memo { get; set; }

        public void CheckDataError()
        { 
            // Name
            if (string.IsNullOrEmpty(Name))
                Memo += "沒有姓名";

            // IDNumber
            if (string.IsNullOrEmpty(IDNumber))
                Memo += "沒有身分證";

            // Birthday
            if (Birthday == null )
                Memo += "沒有生日";

            // CetCode

            // StudentNumber
            if (string.IsNullOrEmpty(StudentNumber))
                Memo += "沒有學號";        
        }
    }
}
