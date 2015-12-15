using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicInformation
{
    [TableName("JHCore_Bilingual.StudentRecordExt")]
    class StudentRecord_Ext : ActiveRecord
    {
        /// <summary>
        /// 學生系統編號
        /// </summary>
        [Field(Field = "ref_student_id", Indexed = true)]
        public string RefStudentID { get; set; }

        /// <summary>
        /// 英文別名
        /// </summary>
        [Field(Field = "nick_name", Indexed = true)]
        public string Nickname { get; set; }

        /// <summary>
        /// 居留證號
        /// </summary>
        [Field(Field = "passport_number", Indexed = true)]
        public string PassportNumber { get; set; }

        /// <summary>
        /// 入學日期
        /// </summary>
        [Field(Field = "entrance_date", Indexed = true)]
        public DateTime? EntranceDate { get; set; }

        /// <summary>
        /// 畢業日期
        /// </summary>
        [Field(Field = "leaving_date", Indexed = true)]
        public DateTime? LeavingDate { get; set; }

        /// <summary>
        /// GivenName
        /// </summary>
        [Field(Field = "given_name", Indexed = true)]
        public string GivenName { get; set; }
        
        /// <summary>
        /// MiddleName
        /// </summary>
        [Field(Field = "middle_name", Indexed = true)]
        public string MiddleName { get; set; }

        /// <summary>
        /// FamilyName
        /// </summary>
        [Field(Field = "family_name", Indexed = true)]
        public string FamilyName { get; set; }

    }
}
