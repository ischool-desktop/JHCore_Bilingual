using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ClassExtendControls.Reports.DAL
{
    class DistrictAreaCounterBase
    {
        // 里
        private string _District;
        // 鄰
        private string _Area;

        private int _BoyCount = 0;

        private int _GirlCount = 0;

        private int _NoGenderCount = 0;

        private int _TotalCount = 0;

        /// <summary>
        /// 加入統計
        /// </summary>
        /// <param name="District"></param>
        /// <param name="Area"></param>
        /// <param name="Gender"></param>
        public string AddCount(string District, string Area, string Gender)
        {
            string ErrorMemo = "";
            if (_District == District && _Area == Area)
            {
                if (Gender == "男")
                    _BoyCount++;

                if (Gender == "女")
                    _GirlCount++;

                if ((Gender != "男") && (Gender != "女"))
                    _NoGenderCount++;

                _TotalCount++;
            }
            else
            {
                // 設定初始
                _District = District;
                _Area = Area;

                if (Gender == "男")
                    _BoyCount = 1;

                if (Gender == "女")
                    _GirlCount = 1;

                if ((Gender != "男") && (Gender != "女"))
                    _NoGenderCount = 1;

                _TotalCount = 1;
            }

            if ((Gender != "男") && (Gender != "女"))
                ErrorMemo = "未分性別";

            return ErrorMemo;
        }

        /// <summary>
        /// 取得里
        /// </summary>
        /// <returns></returns>
        public string GetDistrict()
        {
            return _District;
        }

        /// <summary>
        /// 取得鄰
        /// </summary>
        /// <returns></returns>
        public string GetArea()
        {
            return _Area;
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
        /// 取得總人數
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount()
        {
            return _TotalCount;
        }

        /// <summary>
        /// 取得未分性別人數
        /// </summary>
        /// <returns></returns>
        public int GetNoGenderCount()
        {
            return _NoGenderCount;
        }
    }
}
