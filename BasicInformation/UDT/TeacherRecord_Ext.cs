using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicInformation
{
    class TeacherRecord_Ext : ActiveRecord
    {
        /// <summary>
        /// 老師系統編號
        /// </summary>
        [Field(Field = "ref_teacher_id", Indexed = true)]
        public string RefTeacherID { get; set; }


        /// <summary>
        /// 中文姓名
        /// </summary>
        [Field(Field = "chinese_name", Indexed = true)]
        public string ChineseName { get; set; }
    }
}
