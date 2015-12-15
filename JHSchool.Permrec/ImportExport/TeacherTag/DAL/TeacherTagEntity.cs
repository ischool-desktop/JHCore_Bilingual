using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ImportExport.TeacherTag.DAL
{
    class TeacherTagEntity
    {
        /// <summary>
        /// 教師系統編號
        /// </summary>
        public string TeacherID { get; set; }
        /// <summary>
        /// Tag 前置與名稱
        /// </summary>
        public Dictionary<string, List<string>> PrefixNameDic { get; set; }
    }
}
