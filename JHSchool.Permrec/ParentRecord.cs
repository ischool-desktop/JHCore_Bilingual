using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Framework;

namespace JHSchool.Permrec
{
    /// <summary>
    /// 代表父、母、監護人資料。
    /// </summary>
    public class ParentRecord
    {
        public ParentRecord(XmlElement data)
        {
            Feather = new Father(data);
            Mother = new Mother(data);
            Custodian = new Custodian(data);
            RefStudentID = data.GetAttribute("RefStudentID");
        }

        internal string RefStudentID { get; private set; }

        public StudentRecord Student { get { return JHSchool.Student.Instance[RefStudentID]; } }

        public Father Feather { get; private set; }

        public Mother Mother { get; private set; }

        public Custodian Custodian { get; private set; }
    }

    public abstract class ParentBase
    {
        /// <summary>
        /// 姓名。
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 工作。
        /// </summary>
        public string Job { get; protected set; }

        /// <summary>
        /// 最高學歷。
        /// </summary>
        public string EducationDegree { get; protected set; }

        /// <summary>
        /// 國籍。
        /// </summary>
        public string Nationality { get; protected set; }

        /// <summary>
        /// 身分證號。
        /// </summary>
        public string IDNumber { get; protected set; }

        public string Phone { get; protected set; }
    }

    public class Father : ParentBase
    {
        internal Father(XmlElement data)
        {
            XmlHelper xdata = new XmlHelper(data);
            Name = xdata.GetString("FatherName");
            Nationality = xdata.GetString("FatherNationality");
            IDNumber = xdata.GetString("FatherIDNumber");
            Living = xdata.GetString("FatherLiving");
            EducationDegree = xdata.GetString("FatherOtherInfo/FatherEducationDegree");
            Job = xdata.GetString("FatherOtherInfo/FatherJob");
            Phone = xdata.GetString("FatherOtherInfo/Phone");
        }

        public string Living { get; private set; }
    }

    public class Mother : ParentBase
    {
        internal Mother(XmlElement data)
        {
            XmlHelper xdata = new XmlHelper(data);
            Name = xdata.GetString("MotherName");
            Nationality = xdata.GetString("MotherNationality");
            IDNumber = xdata.GetString("MotherIDNumber");
            Living = xdata.GetString("MotherLiving");
            EducationDegree = xdata.GetString("MotherOtherInfo/MotherEducationDegree");
            Job = xdata.GetString("MotherOtherInfo/MotherJob");
            Phone = xdata.GetString("MotherOtherInfo/Phone");
        }

        public string Living { get; private set; }
    }

    public class Custodian : ParentBase
    {
        internal Custodian(XmlElement data)
        {
            XmlHelper xdata = new XmlHelper(data);
            Name = xdata.GetString("CustodianName");
            Nationality = xdata.GetString("CustodianNationality");
            IDNumber = xdata.GetString("CustodianIDNumber");
            Relationship = xdata.GetString("CustodianRelationship");
            EducationDegree = xdata.GetString("CustodianOtherInfo/EducationDegree");
            Job = xdata.GetString("CustodianOtherInfo/Job");
            Phone = xdata.GetString("CustodianOtherInfo/Phone");
        }

        /// <summary>
        /// 稱謂。
        /// </summary>
        public string Relationship { get; private set; }
    }
}
