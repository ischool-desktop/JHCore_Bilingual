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
        /// 護照號碼
        /// </summary>
        [Field(Field = "passport_number", Indexed = true)]
        public string PassportNumber { get; set; }


    }
}
