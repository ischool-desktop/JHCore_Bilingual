using System;
using System.Collections.Generic;
using System.Text;
using JHSchool.Permrec.Feature;

namespace JHSchool.Permrec
{
    public class UpdateRecord : Framework.CacheManager<List<JHSchool.Data.JHUpdateRecordRecord>>
    {
        private static UpdateRecord _Instance = null;
        public static UpdateRecord Instance { get { if (_Instance == null)_Instance = new UpdateRecord(); return _Instance; } }
        private UpdateRecord() { }

        protected override Dictionary<string, List<JHSchool.Data.JHUpdateRecordRecord>> GetAllData()
        {
            Dictionary<string, List<JHSchool.Data.JHUpdateRecordRecord>> oneToMany = new Dictionary<string, List<JHSchool.Data.JHUpdateRecordRecord>>();
            
            foreach (JHSchool.Data.JHUpdateRecordRecord  each in JHSchool.Data.JHUpdateRecord.SelectAll ())
            {
                if (!oneToMany.ContainsKey(each.StudentID))
                    oneToMany.Add(each.StudentID, new List<JHSchool.Data.JHUpdateRecordRecord>());

                oneToMany[each.StudentID].Add(each);
            }

            return oneToMany;
        }

        protected override Dictionary<string, List<JHSchool.Data.JHUpdateRecordRecord>> GetData(IEnumerable<string> primaryKeys)
        {
            Dictionary<string, List<JHSchool.Data.JHUpdateRecordRecord>> oneToMany = new Dictionary<string, List<JHSchool.Data.JHUpdateRecordRecord>>();

            foreach (JHSchool.Data.JHUpdateRecordRecord each in JHSchool.Data.JHUpdateRecord.SelectByStudentIDs(primaryKeys))
            {
                if (!oneToMany.ContainsKey(each.StudentID))
                    oneToMany.Add(each.StudentID, new List<JHSchool.Data.JHUpdateRecordRecord>());

                oneToMany[each.StudentID].Add(each);
            }

            foreach (string each in primaryKeys)
            {
                if (!oneToMany.ContainsKey(each))
                    oneToMany.Add(each, new List<JHSchool.Data.JHUpdateRecordRecord>());
            }

            return oneToMany;
        }
    }

    public static class UpdateRecord_ExtendMethods
    {
        /// <summary>
        /// 取得學生異動記錄資料。
        /// </summary>
        public static List<JHSchool.Data.JHUpdateRecordRecord> GetUpdateRecords(this JHSchool.Data.JHStudentRecord studentRec)
        {
            return UpdateRecord.Instance[studentRec.ID];
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
            UpdateRecord.Instance.SyncDataBackground(primaryKeys);
        }
    }
}
