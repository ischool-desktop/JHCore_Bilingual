using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicInformation
{
    [TableName("JHCore_Bilingual.TeacherRecordExt")]
    class TeacherRecord_Ext : ActiveRecord
    {
        /// <summary>
        /// 老師系統編號
        /// </summary>
        [Field(Field = "ref_teacher_id", Indexed = true)]
        public string RefTeacherID { get; set; }


        /// <summary>
        /// 行動電話
        /// </summary>
        [Field(Field = "cell_phone", Indexed = true)]
        public string CellPhone { get; set; }
    }
}
