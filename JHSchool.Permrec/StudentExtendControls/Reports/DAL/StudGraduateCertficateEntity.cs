using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace JHSchool.Permrec.StudentExtendControls.Reports.DAL
{
    class StudGraduateCertficateEntity
    {
        /// <summary>
        /// 學校名稱
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 學校英文名稱
        /// </summary>
        public string SchoolEngName { get; set; }

        /// <summary>
        /// 學生姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 學生英文名稱
        /// </summary>
        public string EngName { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 民國生日
        /// </summary>
        /// <returns></returns>
        public string GetChineseBirthday()
        {
            string returnStr=string.Empty ;
            if(Birthday.HasValue)
            returnStr = (Birthday.Value.Year - 1911) + "年" + Birthday.Value.Month + "月" + Birthday.Value.Day + "日";
            return returnStr;
        }

        /// <summary>
        /// 取得英文生日
        /// </summary>
        /// <returns></returns>
        public string GetEngBirthday()
        {
            if (Birthday.HasValue)
                return Birthday.Value.ToString("MMM. dd,yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            else
                return string.Empty;
        
        }

        /// <summary>
        /// 校長名稱
        /// </summary>
        public string ChancellorName { get; set; }

        /// <summary>
        /// 校長英文名稱
        /// </summary>
        public string ChancellorEngName { get; set; }

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

        /// <summary>
        /// 取得畢業年月
        /// </summary>
        public string GraduateSchoolYear { get; set; }

        public string GetChinGraduateSchoolYear()
        {
            if (string.IsNullOrEmpty(GraduateSchoolYear) || (GraduateSchoolYear.Length !=6))
                return string.Empty;

            int scYear,month;
            int.TryParse(GraduateSchoolYear.Substring(0,4),out scYear);
            int.TryParse(GraduateSchoolYear.Substring(4, 2), out month);
            return (scYear-1911) + "年" + month +"月";
        }

        /// <summary>
        /// 畢業證書字號
        /// </summary>
        public string CertDocNo { get; set; }


        /// <summary>
        /// 取得畢業證書字
        /// </summary>
        /// <returns></returns>
        public string GetCertDoc()
        {
            if (string.IsNullOrEmpty(CertDocNo))
                return string.Empty;

            string retValue = string.Empty;

            char[] chars = CertDocNo.ToCharArray();

            foreach (char ch in chars)
            {
                if (ch == '第')
                    break;

                    int no;
                    if (int.TryParse(ch.ToString(), out no))
                        retValue += ch;
            }
                return retValue;
        }

        /// <summary>
        /// 取得畢業證書號
        /// </summary>
        /// <returns></returns>
        public string GetCertNo()
        {
            if (string.IsNullOrEmpty(CertDocNo))
                return string.Empty;

            string retValue = string.Empty;

            int BeginIdx=CertDocNo.IndexOf("第");

            if (BeginIdx == 0)
                return string.Empty;

            int EndIdx = CertDocNo.Length;
            char[] chars = CertDocNo.Substring(BeginIdx, (EndIdx - BeginIdx)).ToCharArray();

            foreach (char ch in chars)
            {
                int no;
                if (int.TryParse(ch.ToString(), out no))
                    retValue += ch;
            }

            return retValue;
        }


    }
}
