using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using FISCA.DSAUtil;
using System.Xml;

namespace JHSchool.Permrec
{
    public class Phone:CacheManager<PhoneRecord>
    {
        private static Phone _instance;

        public static Phone Instance
        {
            get
            {
                if (_instance == null) _instance = new Phone();
                return _instance;
            }
        }

        protected override Dictionary<string, PhoneRecord> GetAllData()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("All");

            Dictionary<string, PhoneRecord> result = new Dictionary<string, PhoneRecord>();

            string srvName = "SmartSchool.Student.GetPhoneDetailList";
            DSXmlHelper rsp = FISCA.Authentication.DSAServices.CallService(srvName, new DSRequest(helper)).GetContent();
            foreach (XmlElement each in rsp.GetElements("Student"))
            {
                PhoneRecord parent = new PhoneRecord(each);
                result.Add(parent.RefStudentID, parent);
            }

            return result;

        }

        protected override Dictionary<string, PhoneRecord> GetData(IEnumerable<string> primaryKeys)
        {
            bool haskey = false;
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("All");
            helper.AddElement("Condition");
            foreach (string each in primaryKeys)
            {
                helper.AddElement("Condition", "ID", each);
                haskey = true;
            }

            Dictionary<string, PhoneRecord> result = new Dictionary<string, PhoneRecord>();

            if (haskey)
            {
                string srvName = "SmartSchool.Student.GetPhoneDetailList";
                DSXmlHelper rsp = FISCA.Authentication.DSAServices.CallService(srvName, new DSRequest(helper)).GetContent();
                foreach (XmlElement each in rsp.GetElements("Student"))
                {
                    PhoneRecord parent = new PhoneRecord(each);
                    result.Add(parent.RefStudentID, parent);
                }
            }

            return result;
        }
    }

    public static class Phone_Extends
    {
        /// <summary>
        /// 取得電話資料。
        /// </summary>
        public static PhoneRecord GetPhone(this StudentRecord student)
        {
            return Phone.Instance[student.ID];
        }

        /// <summary>
        /// 批次取得電話資料。
        /// </summary>
        /// <param name="students"></param>
        public static void SyncPhoneCache(this IEnumerable<StudentRecord> students)
        {
            Phone.Instance.SyncDataBackground(students.AsKeyList());
        }
    }
}
