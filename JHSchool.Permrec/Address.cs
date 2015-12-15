using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using FISCA.DSAUtil;
using System.Xml;

namespace JHSchool.Permrec
{
    public class Address : CacheManager<AddressRecord>
    {
        private static Address _instance;

        public static Address Instance
        {
            get
            {
                if (_instance == null) _instance = new Address();
                return _instance;
            }
        }

        protected override Dictionary<string, AddressRecord> GetAllData()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("All");

            Dictionary<string, AddressRecord> result = new Dictionary<string, AddressRecord>();

            string srvName = "SmartSchool.Student.GetAddressDetailList";
            DSXmlHelper rsp = FISCA.Authentication.DSAServices.CallService(srvName, new DSRequest(helper)).GetContent();
            foreach (XmlElement each in rsp.GetElements("Student"))
            {
                AddressRecord parent = new AddressRecord(each);
                result.Add(parent.RefStudentID, parent);
            }

            return result;
        }

        protected override Dictionary<string, AddressRecord> GetData(IEnumerable<string> primaryKeys)
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

            Dictionary<string, AddressRecord> result = new Dictionary<string, AddressRecord>();

            if (haskey)
            {
                string srvName = "SmartSchool.Student.GetAddressDetailList";
                DSXmlHelper rsp = FISCA.Authentication.DSAServices.CallService(srvName, new DSRequest(helper)).GetContent();
                foreach (XmlElement each in rsp.GetElements("Student"))
                {
                    AddressRecord parent = new AddressRecord(each);
                    result.Add(parent.RefStudentID, parent);
                }
            }

            return result;
        }
    }

    public static class Address_Extends
    {
        /// <summary>
        /// 取得學生地址資料。
        /// </summary>
        public static AddressRecord GetAddress(this StudentRecord student)
        {
            return Address.Instance[student.ID];
        }

        /// <summary>
        /// 批次取得學生地址資料。
        /// </summary>
        /// <param name="students"></param>
        public static void SyncAddressCache(this IEnumerable<StudentRecord> students)
        {
            Address.Instance.SyncDataBackground(students.AsKeyList());
        }
    }
}
