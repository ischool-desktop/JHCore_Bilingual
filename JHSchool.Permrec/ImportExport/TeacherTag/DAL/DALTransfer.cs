using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ImportExport.TeacherTag.DAL
{
    class DALTransfer
    {
        /// <summary>
        /// 取得所選教師類別資料
        /// </summary>
        /// <param name="TeacherIDList"></param>
        /// <returns></returns>
        public static List<TeacherTagEntity> GetTeacherTagList(List<string> TeacherIDList)
        {
            List<JHSchool.Data.JHTeacherTagRecord> TeacherTags = JHSchool.Data.JHTeacherTag.SelectByTeacherIDs(TeacherIDList);
            List<TeacherTagEntity> TeacherTagList = new List<TeacherTagEntity>();

            Dictionary<string, TeacherTagEntity> TeacherTagDic = new Dictionary<string, TeacherTagEntity>();
            foreach (string str in TeacherIDList)
            {
                TeacherTagEntity tte = new TeacherTagEntity();
                tte.TeacherID = str;
                TeacherTagDic.Add(str, tte);
            }

            foreach (JHSchool.Data.JHTeacherTagRecord tr in TeacherTags)
            {
                
                if (TeacherTagDic.ContainsKey(tr.RefEntityID))
                {
                    List<string> item = new List<string>();
                    item.Add(tr.Name);

                    string tPrefix = "";
                    if (tr.Prefix == "")
                        tPrefix = tr.Name;
                    else
                        tPrefix = tr.Prefix;

                    if (TeacherTagDic[tr.RefEntityID].PrefixNameDic == null)
                    {
                        TeacherTagDic[tr.RefEntityID].PrefixNameDic = new Dictionary<string, List<string>>();
                        TeacherTagDic[tr.RefEntityID].PrefixNameDic.Add(tPrefix, item);
                    }
                    else
                    {
                        if (TeacherTagDic[tr.RefEntityID].PrefixNameDic.ContainsKey(tPrefix))
                            TeacherTagDic[tr.RefEntityID].PrefixNameDic[tPrefix].Add(tr.Name);
                        else
                            TeacherTagDic[tr.RefEntityID].PrefixNameDic.Add(tPrefix, item);
                    }
                }
            }

            foreach (KeyValuePair<string, TeacherTagEntity> item in TeacherTagDic)
            {
                if(item.Value.PrefixNameDic !=null )
                    TeacherTagList.Add(item.Value);
            }
            return TeacherTagList;
        }

        /// <summary>
        /// 取得所選教師類別名稱集合(前置)
        /// </summary>
        /// <param name="TeacherIDList"></param>
        /// <returns></returns>
        public static List<string> GetTeacherTagPrefixList(List<string> TeacherIDList)
        {
            List<string> PrefixList = new List<string>();

            List<JHSchool.Data.JHTeacherTagRecord> TeacherTags = JHSchool.Data.JHTeacherTag.SelectByTeacherIDs(TeacherIDList);
            foreach (JHSchool.Data.JHTeacherTagRecord tr in TeacherTags)
            {
                string tPrefix = "";
                if (tr.Prefix == "")
                    tPrefix = tr.Name;
                else
                    tPrefix = tr.Prefix;

                if (!PrefixList.Contains(tPrefix))
                    PrefixList.Add(tPrefix);
            }
            return PrefixList;
        }

    }
}
