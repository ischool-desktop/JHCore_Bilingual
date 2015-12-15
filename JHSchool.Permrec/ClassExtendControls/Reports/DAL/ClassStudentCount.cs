using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ClassExtendControls.Reports.DAL
{
    class ClassStudentCount
    {
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string DisplayOrder { get; set; }
        public string GradeYear { get; set; }
        public string TeacherName { get; set; }

        private int _BoyCount = 0;
        private int _GirlCount = 0;
        private int _NoGenderCount = 0;
        private int _TotalGenderCount = 0;

        /// <summary>
        /// 加入男女人數統計
        /// </summary>
        /// <param name="Gender"></param>
        public void AddGenderCount(string Gender)
        {
            if (Gender == "男")
                _BoyCount++;

            if (Gender == "女")
                _GirlCount++;

            if ((Gender != "男") && (Gender != "女"))
            {
                _NoGenderCount++;
            }

            _TotalGenderCount++;

        }

        /// <summary>
        /// 取得男生人數
        /// </summary>
        /// <returns></returns>
        public int GetBoyCount()
        {
            return _BoyCount;
        }

        /// <summary>
        /// 取得女生人數
        /// </summary>
        /// <returns></returns>
        public int GetGirlCount()
        {
            return _GirlCount;
        }

        /// <summary>
        /// 取得未分性別人數
        /// </summary>
        /// <returns></returns>
        public int GetNoGenderCount()
        {
            return _NoGenderCount;
        }

        /// <summary>
        /// 取得總人數
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount()
        {
            return _TotalGenderCount;
        }

        /// <summary>
        /// 男生百分比
        /// </summary>
        /// <param name="小數下幾位"></param>
        /// <returns></returns>
        public decimal GetBoyPercentage(int perRange)
        {
            decimal per = 0;
            if (_TotalGenderCount > 0)
            {
                perRange += 2;
                per = Math.Round((decimal)_BoyCount / (decimal)_TotalGenderCount, perRange, MidpointRounding.AwayFromZero);
            }
            return per * 100;
        }

        /// <summary>
        /// 女生百分比
        /// </summary>
        /// <param name="小數下幾位"></param>
        /// <returns></returns>
        public decimal GetGirlPercentage(int perRange)
        {
            decimal per = 0;
            if (_TotalGenderCount > 0)
            {
                perRange += 2;
                per = Math.Round((decimal)_GirlCount / (decimal)_TotalGenderCount, perRange, MidpointRounding.AwayFromZero);
            }
            return per * 100;
        }

    }
}
