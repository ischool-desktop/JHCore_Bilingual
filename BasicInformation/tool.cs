using FISCA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicInformation
{
    static class tool
    {
        static public FISCA.Data.QueryHelper _Q = new FISCA.Data.QueryHelper();
        static public FISCA.UDT.AccessHelper _A = new FISCA.UDT.AccessHelper();

        /// <summary>
        /// 依編號取代為星期
        /// </summary>
        public static string CheckWeek(string x)
        {
            if (x == "Monday")
            {
                return "一";
            }
            else if (x == "Tuesday")
            {
                return "二";
            }
            else if (x == "Wednesday")
            {
                return "三";
            }
            else if (x == "Thursday")
            {
                return "四";
            }
            else if (x == "Friday")
            {
                return "五";
            }
            else if (x == "Saturday")
            {
                return "六";
            }
            else
            {
                return "日";
            }
        }

        public static int SortPeriod(K12.Data.PeriodMappingInfo info1, K12.Data.PeriodMappingInfo info2)
        {
            return info1.Sort.CompareTo(info2.Sort);
        }
    }
}
