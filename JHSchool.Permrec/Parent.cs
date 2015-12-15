using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using FISCA.DSAUtil;
using System.Xml;
using JHSchool.Feature.Legacy;

namespace JHSchool.Permrec
{
    public class Parent : CacheManager<ParentRecord>
    {
        private static Parent _instance;

        public static Parent Instance
        {
            get
            {
                if (_instance == null) _instance = new Parent();
                return _instance;
            }
        }

        protected override Dictionary<string, ParentRecord> GetAllData()
        {
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");

            Dictionary<string, ParentRecord> result = new Dictionary<string, ParentRecord>();

            string srvName = "SmartSchool.Student.GetParentsDetailList";
            DSXmlHelper rsp = FISCA.Authentication.DSAServices.CallService(srvName, new DSRequest(helper)).GetContent();
            foreach (XmlElement each in rsp.GetElements("Student"))
            {
                ParentRecord parent = new ParentRecord(each);
                result.Add(parent.RefStudentID, parent);
            }

            return result;

        }

        protected override Dictionary<string, ParentRecord> GetData(IEnumerable<string> primaryKeys)
        {
            bool haskey = false;
            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.AddElement("Field");
            helper.AddElement("Field", "All");
            helper.AddElement("Condition");
            foreach (string each in primaryKeys)
            {
                helper.AddElement("Condition", "ID", each);
                haskey = true;
            }

            Dictionary<string, ParentRecord> result = new Dictionary<string, ParentRecord>();

            if (haskey)
            {
                string srvName = "SmartSchool.Student.GetParentsDetailList";
                DSXmlHelper rsp = FISCA.Authentication.DSAServices.CallService(srvName, new DSRequest(helper)).GetContent();
                foreach (XmlElement each in rsp.GetElements("Student"))
                {
                    ParentRecord parent = new ParentRecord(each);
                    result.Add(parent.RefStudentID, parent);
                }
            }

            return result;
        }
    }

    public static class Parent_Extends
    {
        /// <summary>
        /// 取得父母親與監護人資料。
        /// </summary>
        public static ParentRecord GetParent(this StudentRecord student)
        {
            return Parent.Instance[student.ID];
        }

        /// <summary>
        /// 批次取得父母親與監護人資料。
        /// </summary>
        /// <param name="students"></param>
        public static void SyncParentCache(this IEnumerable<StudentRecord> students)
        {
            Parent.Instance.SyncDataBackground(students.AsKeyList());
        }

        public static List<KeyValuePair<string, string>> GetRelationship(this Parent parent)
        {
            List<KeyValuePair<string, string>> List = new List<KeyValuePair<string, string>>();

            DSResponse dsrsp = Config.GetRelationship();
            DSXmlHelper helper = dsrsp.GetContent();

            KeyValuePair<string, string> kvpRel = new KeyValuePair<string, string>("", "請選擇");

            List.Add(kvpRel);

            foreach (XmlNode node in helper.GetElements("Relationship"))
                List.Add(new KeyValuePair<string, string>(node.InnerText, node.InnerText));

            return List;
        }

        public static List<string> GetNationalityList(this Parent parent)
        {
            List<string> List = new List<string>();

            DSRequest request = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper("GetNationalityListRequest");
            helper.AddElement("Fields");
            helper.AddElement("Fields", "All");
            request.SetContent(helper);
            DSXmlHelper response = FISCA.Authentication.DSAServices.CallService("SmartSchool.Config.GetNationalityList", request).GetContent();

            foreach(XmlElement Element in response.GetElements("Nationality"))
                List.Add(Element.InnerText);

            return List;
        }
    }
}
