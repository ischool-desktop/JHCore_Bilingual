using FISCA.Authentication;
using FISCA.Data;
using FISCA.DSAUtil;
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

        public static DSResponse GetMDReduce()
        {
            return DSAServices.CallService("SmartSchool.Config.GetMDReduce", new DSRequest());
        }

        public static DSResponse GetDiscipline(DSRequest request)
        {
            return FISCA.Authentication.DSAServices.CallService("SmartSchool.Student.Discipline.GetDiscipline", request);
        }

        public static int SortPeriod(K12.Data.PeriodMappingInfo info1, K12.Data.PeriodMappingInfo info2)
        {
            return info1.Sort.CompareTo(info2.Sort);
        }

        public static DSResponse GetMultiParentInfo(params string[] idlist)
        {
            DSRequest dsreq = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("GetParentInfoRequest");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach (string id in idlist)
            {
                helper.AddElement("Condition", "ID", id);
            }
            dsreq.SetContent(helper);
            return DSAServices.CallService("SmartSchool.Student.GetMultiParentInfo", dsreq);
        }

        /// <summary>
        /// 依據傳入的學生ID清單,取得學生的額外資料
        /// </summary>
        /// <param name="allStudentID"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetStudentEXT(List<string> allStudentID)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            List<StudentRecord_Ext> StudentEXList = tool._A.Select<StudentRecord_Ext>(string.Format("ref_student_id in ('{0}')", string.Join("','", allStudentID)));
            foreach (StudentRecord_Ext ext in StudentEXList)
            {
                if (!dic.ContainsKey(ext.RefStudentID))
                {
                    dic.Add(ext.RefStudentID, ext.Nickname);
                }
            }
            return dic;

        }

        public static string GetDay(int n)
        {
            string EngMonth = "";
            switch (n.ToString())
            {
                case "1":
                    EngMonth = "first";
                    break;
                case "2":
                    EngMonth = "second";
                    break;
                case "3":
                    EngMonth = "third";
                    break;
                case "4":
                    EngMonth = "fourth";
                    break;
                case "5":
                    EngMonth = "fifth";
                    break;
                case "6":
                    EngMonth = "sixth";
                    break;
                case "7":
                    EngMonth = "seventh";
                    break;
                case "8":
                    EngMonth = "eighth";
                    break;
                case "9":
                    EngMonth = "ninth";
                    break;
                case "10":
                    EngMonth = "tenth";
                    break;
                case "11":
                    EngMonth = "eleventh";
                    break;
                case "12":
                    EngMonth = "twelfth";
                    break;
                case "13":
                    EngMonth = "thirteenth";
                    break;
                case "14":
                    EngMonth = "fourteenth";
                    break;
                case "15":
                    EngMonth = "fifteenth";
                    break;
                case "16":
                    EngMonth = "sixteenth";
                    break;
                case "17":
                    EngMonth = "seventeenth";
                    break;
                case "18":
                    EngMonth = "eighteenth";
                    break;
                case "19":
                    EngMonth = "nineteenth";
                    break;
                case "20":
                    EngMonth = "twentieth";
                    break;
                case "21":
                    EngMonth = "twenty-first";
                    break;
                case "22":
                    EngMonth = "twenty-second";
                    break;
                case "23":
                    EngMonth = "twenty-third";
                    break;
                case "24":
                    EngMonth = "twenty-fourth";
                    break;
                case "25":
                    EngMonth = "twenty-fifth";
                    break;
                case "26":
                    EngMonth = "twenty-sixth";
                    break;
                case "27":
                    EngMonth = "twenty-seventh";
                    break;
                case "28":
                    EngMonth = "twenty-eighth";
                    break;
                case "29":
                    EngMonth = "twenty-ninth";
                    break;
                case "30":
                    EngMonth = "thirtieth";
                    break;
                case "31":
                    EngMonth = "thirty-first";
                    break;
                default:
                    EngMonth = "";
                    break;
            }
            return EngMonth;

        }

        /// <summary>
        /// 取得月份英文名稱
        /// </summary>
        public static string GetMonth(int n)
        {
            string EngMonth = "";
            switch (n.ToString())
            {
                case "1":
                    EngMonth = "January";
                    break;
                case "2":
                    EngMonth = "February";
                    break;
                case "3":
                    EngMonth = "March";
                    break;
                case "4":
                    EngMonth = "April";
                    break;
                case "5":
                    EngMonth = "May";
                    break;
                case "6":
                    EngMonth = "June";
                    break;
                case "7":
                    EngMonth = "July";
                    break;
                case "8":
                    EngMonth = "August";
                    break;
                case "9":
                    EngMonth = "September";
                    break;
                case "10":
                    EngMonth = "October";
                    break;
                case "11":
                    EngMonth = "November";
                    break;
                case "12":
                    EngMonth = "December";
                    break;
                default:
                    EngMonth = "";
                    break;
            }
            return EngMonth;
        }
    }
}
