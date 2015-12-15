using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Framework;

namespace JHSchool.Permrec
{
    /// <summary>
    /// 代表學生的地址相關資料。
    /// </summary>
    public class AddressRecord
    {
        public AddressRecord(XmlElement data)
        {
            RefStudentID = data.GetAttribute("RefStudentID");

            Permanent = new AddressItem(data.SelectSingleNode("Permanent/Address") as XmlElement);
            Mailing = new AddressItem(data.SelectSingleNode("Mailing/Address") as XmlElement);

            Address1 = new AddressItem(null);
            Address2 = new AddressItem(null);
            Address3 = new AddressItem(null);

            int index = 0;
            foreach (XmlElement each in data.SelectNodes("Addresses/AddressList/Address"))
            {
                if (index == 0)
                    Address1 = new AddressItem(each);

                if (index == 1)
                    Address2 = new AddressItem(each);

                if (index == 2)
                    Address3 = new AddressItem(each);

                index++;
            }
        }

        internal string RefStudentID { get; private set; }

        public StudentRecord Student { get { return JHSchool.Student.Instance[RefStudentID]; } }

        /// <summary>
        /// 戶籍地址。
        /// </summary>
        public AddressItem Permanent { get; private set; }

        /// <summary>
        /// 聯絡地址。
        /// </summary>
        public AddressItem Mailing { get; private set; }

        /// <summary>
        /// 其他地址一。
        /// </summary>
        public AddressItem Address1 { get; private set; }

        /// <summary>
        /// 其他地址二。
        /// </summary>
        public AddressItem Address2 { get; private set; }

        /// <summary>
        /// 其他地址三。
        /// </summary>
        public AddressItem Address3 { get; private set; }
    }

    /// <summary>
    /// 地址資料。
    /// </summary>
    public class AddressItem
    {
        public AddressItem(XmlElement data)
        {
            County = Town = District = Area = Detail = Longitude = Latitude = string.Empty;

            if (data == null) return;

            XmlHelper xdata = new XmlHelper(data);
            ZipCode = xdata.GetString("ZipCode");
            County = xdata.GetString("County");
            Town = xdata.GetString("Town");
            District = xdata.GetString("District");
            Area = xdata.GetString("Area");
            Detail = xdata.GetString("DetailAddress");
            Longitude = xdata.GetString("Longitude");
            Latitude = xdata.GetString("Latitude");
        }

        /// <summary>
        /// 郵遞區號。
        /// </summary>
        public string ZipCode { get; private set; }

        /// <summary>
        /// 縣市。
        /// </summary>
        public string County { get; private set; }

        /// <summary>
        /// 鄉鎮市區。
        /// </summary>
        public string Town { get; private set; }

        /// <summary>
        /// 村里。
        /// </summary>
        public string District { get; private set; }

        /// <summary>
        /// 鄰。
        /// </summary>
        public string Area { get; private set; }

        /// <summary>
        /// 其他。
        /// </summary>
        public string Detail { get; private set; }

        /// <summary>
        /// 經度。
        /// </summary>
        public string Longitude { get; private set; }

        /// <summary>
        /// 緯度。
        /// </summary>
        public string Latitude { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} {1}{2}{3}{4}{5}", ZipCode, County, Town, District, Area, Detail);
        }
    }
}
