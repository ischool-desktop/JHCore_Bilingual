using System;
using System.Collections.Generic;
using System.Text;
using JHSchool.Permrec.Feature;

namespace JHSchool.Permrec
{
    class BeforeInfoRec:Framework.CacheManager<JHSchool.Data.JHBeforeEnrollmentRecord >
    {
        private static BeforeInfoRec _Instance = null;
        public static BeforeInfoRec Instance { get { if (_Instance == null)_Instance = new BeforeInfoRec(); return _Instance; } }
        private BeforeInfoRec() { }

        protected override Dictionary<string, JHSchool.Data.JHBeforeEnrollmentRecord> GetAllData()
        {
            Dictionary<string, JHSchool.Data.JHBeforeEnrollmentRecord> oneToMany = new Dictionary<string, JHSchool.Data.JHBeforeEnrollmentRecord>();

            foreach (JHSchool.Data.JHBeforeEnrollmentRecord each in JHSchool.Data.JHBeforeEnrollment.SelectAll())
            {
                if (!oneToMany.ContainsKey(each.RefStudentID))
                    oneToMany.Add(each.RefStudentID, each);
                
            }

            return oneToMany;
        }

        protected override Dictionary<string, JHSchool.Data.JHBeforeEnrollmentRecord> GetData(IEnumerable<string> primaryKeys)
        {
            Dictionary<string, JHSchool.Data.JHBeforeEnrollmentRecord> oneToMany = new Dictionary<string, JHSchool.Data.JHBeforeEnrollmentRecord>();

            foreach (JHSchool.Data.JHBeforeEnrollmentRecord each in JHSchool.Data.JHBeforeEnrollment.SelectByStudentIDs(primaryKeys))
            {
                if (!oneToMany.ContainsKey(each.RefStudentID))
                    oneToMany.Add(each.RefStudentID, each );
            }

            return oneToMany;
        }
    }
    public static class BeforeInfoRec_ExtendMethods
    {
        /// <summary>
        /// 取得學生異動記錄資料。
        /// </summary>
        public static JHSchool.Data.JHBeforeEnrollmentRecord  GetUpdateRecords(this JHSchool.Data.JHStudentRecord studentRec)
        {
            return BeforeInfoRec.Instance[studentRec.ID];
        }

        /// <summary>
        /// 批次同步學生異動資料。
        /// </summary>
        /// <param name="studentRecs"></param>
        public static void SyncUpdateRecordCache(this IEnumerable<JHSchool.Data.JHStudentRecord> studentRecs)
        {
            List<string> primaryKeys = new List<string>();
            foreach (var item in studentRecs)
            {
                primaryKeys.Add(item.ID);
            }
            BeforeInfoRec.Instance.SyncDataBackground(primaryKeys);
        }
    }
}
