using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Framework;

namespace JHSchool.Permrec
{
    /// <summary>
    /// 代表學生電話資料。
    /// </summary>
    public class PhoneRecord
    {
        public PhoneRecord(XmlElement data)
        {
            XmlHelper xdata = new XmlHelper(data);

            RefStudentID = data.GetAttribute("RefStudentID");
            Permanent = xdata.GetString("Permanent");
            Contact = xdata.GetString("Contact");
            Cell = xdata.GetString("Cell");

            int index = 0;
            Phone1 = Phone2 = Phone3 = string.Empty;
            foreach (XmlElement each in xdata.GetElements("Phones/PhoneNumber"))
            {
                switch (index)
                {
                    case 0:
                        Phone1 = each.InnerText;
                        break;
                    case 1:
                        Phone2 = each.InnerText;
                        break;
                    case 2:
                        Phone3 = each.InnerText;
                        break;
                }
                index++;
            }
        }

        internal string RefStudentID { get; set; }

        /// <summary>
        /// 戶籍電話。
        /// </summary>
        public string Permanent { get; private set; }

        /// <summary>
        /// 聯絡電話。
        /// </summary>
        public string Contact { get; private set; }

        /// <summary>
        /// 手機。
        /// </summary>
        public string Cell { get; private set; }

        /// <summary>
        /// 其他電話一。
        /// </summary>
        public string Phone1 { get; private set; }

        /// <summary>
        /// 其他電話二。
        /// </summary>
        public string Phone2 { get; private set; }

        /// <summary>
        /// 其他電話三。
        /// </summary>
        public string Phone3 { get; private set; }

        public StudentRecord Student { get { return JHSchool.Student.Instance[RefStudentID]; } }
    }
}
