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
        /// 中文姓名
        /// </summary>
        [Field(Field = "chinese_name", Indexed = true)]
        public string ChineseName { get; set; }

        /// <summary>
        /// 護照號碼
        /// </summary>
        [Field(Field = "passport_number", Indexed = true)]
        public string PassportNumber { get; set; }


    }
}
