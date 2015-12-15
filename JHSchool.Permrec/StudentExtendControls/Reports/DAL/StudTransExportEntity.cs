using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.StudentExtendControls.Reports.DAL
{
    // 學生轉出資料
    class StudTransExportEntity
    {
        /// <summary>
        /// 學校名稱
        /// </summary>
        public string SchoolName { get; set; }
        /// <summary>
        /// 學生姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 班級年級
        /// </summary>
        public string ClassGradeYear { get; set; }
        
        /// <summary>
        /// 新生異動核准日期 
        /// </summary>
        public string UR01CertDate { get; set; }

        /// <summary>
        /// 新生異動核准日期 (民國)
        /// </summary>
        public string GetChineseUR01CertDate()
        {
            string str="";
            DateTime dt;
            if (DateTime.TryParse(UR01CertDate, out dt))
            {
                str=(dt.Year - 1911) + "年" + dt.Month + "月" + dt.Day + "日";
            }

            return str;
        }


        /// <summary>
        /// 取得學校中文所在地縣市
        /// </summary>
        /// <returns></returns>
        public string GetSchoolCounty()
        {
            int len = 0;
            string returnValue = string.Empty;

            if (School.Address.IndexOf('縣') > 0)
                len = School.Address.IndexOf('縣') + 1;

            if (School.Address.IndexOf('市') > 0)
                len = School.Address.IndexOf('市') + 1;

            if (len > 0)
                returnValue = School.Address.Substring(0, len);

            return returnValue;
        }

        /// <summary>
        /// 新生異動核准文號
        /// </summary>
        public string UR01CertDocNo { get; set; }

        /// <summary>
        /// 新生異動日期
        /// </summary>
        public string UR01Date { get; set; }

        /// <summary>
        /// 新生異動日期 (民國)
        /// </summary>
        public string GetChineseUR01Date()
        {
            string str = "  年  月  日";
            DateTime dt;
            if (DateTime.TryParse(UR01Date, out dt))
            {
                str = (dt.Year - 1911) + "年" + dt.Month + "月" + dt.Day + "日";
            }

            return str;
        }

         /// <summary>
        /// 異動原因
        /// </summary>
        public string UpdateDesc { get; set; }

        /// <summary>
        /// 民國生日
        /// </summary>
        /// <returns></returns>
        public string GetChineseBirthday()
        {
            string returnStr = string.Empty;

            if (Birthday.Year > 1000)
                returnStr = (Birthday.Year - 1911) + "年" + Birthday.Month + "月" + Birthday.Day + "日";
            else
                returnStr = "  年  月  日";

            return returnStr;        
        }

        /// <summary>
        /// 校長名稱
        /// </summary>
        public string ChancellorName { get; set; }

        /// <summary>
        /// 照片文字
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
    }
}
