using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHSchool.Permrec.ImportExport.StudentTag.DAL
{
    class DALTransfer
    {   
        /// <summary>
        /// 取得所選學生類別資料
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        //public static List<StudentTagEntity> GetStudentTagList(List<string> StudentIDList)
        //{
        //    List<JHSchool.Data.JHStudentTagRecord> studTags = JHSchool.Data.JHStudentTag.SelectByStudentIDs(StudentIDList);
        //    List<StudentTagEntity> StudTagList = new List<StudentTagEntity>();

        //    Dictionary<string,StudentTagEntity> StudTagDic = new Dictionary<string,StudentTagEntity> ();
        //    foreach (string str in StudentIDList)
        //    {
        //        StudentTagEntity ste = new StudentTagEntity ();
        //        ste.StudentID =str;
        //        StudTagDic.Add(str,ste );
        //    }

        //    foreach (JHSchool.Data.JHStudentTagRecord tr in studTags)
        //    {
        //        if (StudTagDic.ContainsKey(tr.RefEntityID))
        //        {
        //            List<string> item = new List<string>();
        //            item.Add(tr.Name);
        //            string tPrefix = "";
        //            if (tr.Prefix == "")
        //                tPrefix = tr.Name;
        //            else
        //                tPrefix = tr.Prefix;

        //            if (StudTagDic[tr.RefEntityID].PrefixNameDic == null)
        //            {
        //                StudTagDic[tr.RefEntityID].PrefixNameDic = new Dictionary<string, List<string>>();
        //                StudTagDic[tr.RefEntityID].PrefixNameDic.Add(tPrefix, item);
        //            }
        //            else
        //            {
        //                if (StudTagDic[tr.RefEntityID].PrefixNameDic.ContainsKey(tPrefix))
        //                    StudTagDic[tr.RefEntityID].PrefixNameDic[tPrefix].Add(tr.Name);
        //                else
        //                    StudTagDic[tr.RefEntityID].PrefixNameDic.Add(tPrefix, item);                            
        //            }
        //        }
        //    }

        //    foreach (KeyValuePair<string, StudentTagEntity> item in StudTagDic)
        //    {
        //        if(item.Value.PrefixNameDic  !=null )
        //            StudTagList.Add(item.Value);
        //    }
        //    return StudTagList;
        //}

        /// <summary>
        /// 取得所選學生類別名稱集合(前置)
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static List<string> GetStudTagPrefixList(List<string> StudentIDList)        
        {
            List<string> PrefixList = new List<string>();

            List<JHSchool.Data.JHStudentTagRecord> studTags = JHSchool.Data.JHStudentTag.SelectByStudentIDs(StudentIDList);
            foreach (JHSchool.Data.JHStudentTagRecord tr in studTags)
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

        /// <summary>
        /// 取得學生 Tag
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string,string>> GetStudentTagNameDic()
        {
            Dictionary<string, Dictionary<string, string>> retVal = new Dictionary<string, Dictionary<string, string>>();

            foreach (JHSchool.Data.JHTagConfigRecord tr in JHSchool.Data.JHTagConfig.SelectByCategory(K12.Data.TagCategory.Student))
            {
                if (retVal.ContainsKey(tr.Prefix))
                {
                    if (retVal[tr.Prefix].ContainsKey(tr.Name))
                        retVal[tr.Prefix][tr.Name] = tr.ID;
                    else
                        retVal[tr.Prefix].Add(tr.Name, tr.ID);
                }
                else
                {
                    Dictionary<string, string> str = new Dictionary<string, string>();
                    str.Add(tr.Name, tr.ID);
                    retVal.Add(tr.Prefix, str);
                }
            }
            return retVal;
        }

    }
}
