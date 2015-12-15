using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.StudentExtendControls.Reports.DAL
{
    class StudentEntity
    {        
        /// <summary>
        /// 學生姓名
        /// </summary>
        public string StudentName { get; set; }
        
        public string StudentID { get; set; }

        /// <summary>
        /// 學校中文名稱
        /// </summary>
        public string SchoolChineseName { get; set; }

        /// <summary>
        /// 學校住址
        /// </summary>
        public string SchoolAddress { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 年級
        /// </summary>
        public string GradeYear { get; set; }

        /// <summary>
        /// 班級名稱
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 座號
        /// </summary>
        public string SeatNo { get; set; }

        /// <summary>
        /// 取得生日年
        /// </summary>
        /// <returns></returns>
        public string GetBirthdayChineseYear()
        {            
            return "" + (Birthday.Year - 1911);
        }

        /// <summary>
        /// 取得生日月
        /// </summary>
        /// <returns></returns>
        public string GetBirthdayMonth()
        {
            return "" + Birthday.Month;
        }

        /// <summary>
        /// 取得生日日
        /// </summary>
        /// <returns></returns>
        public string GetBirthdayDay()
        {
            return "" + Birthday.Day;
            
        }

        /// <summary>
        /// 身分證號
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 學校電話
        /// </summary>
        public string SchoolTelephone { get; set; }

        /// <summary>
        /// 學校傳真
        /// </summary>
        public string SchoolFax { get; set; }

        /// <summary>
        /// 學生英文名稱
        /// </summary>
        public string StudentEnglishName { get; set; }

        /// <summary>
        /// 取得中文生日
        /// </summary>
        /// <returns></returns>
        public string GetChineseBirthday()
        {
            if (Birthday == DateTime.MinValue )
                return string.Empty;

            return (Birthday.Year-1911)+"年" + Birthday.Month + "月" + Birthday.Day + "日";
            
        }

        public string GetBirthdayStr()
        {
            if (Birthday == DateTime.MinValue)
                return string.Empty;
            
            return Birthday.ToShortDateString();
        }

        /// <summary>
        /// 出生地
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// 家長或監護人
        /// </summary>
        public string Parent1 { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 照片字串
        /// </summary>
        public string PhotoStr { get; set; }

        /// <summary>
        /// 取得照片檔
        /// </summary>
        /// <returns></returns>
        public byte[] GetPhotoImage()
        {
            byte[] bytePhoto = PhotoStr.Equals(string.Empty) ? null : Convert.FromBase64String(PhotoStr);
            return bytePhoto;        
        }
        
        /// <summary>
        /// 畢修業
        /// </summary>
        public string Reason { get; set; }


        /// <summary>
        /// 畢業證書字號
        /// </summary>
        public string DiplomaNumber { get; set;}
    }
}
