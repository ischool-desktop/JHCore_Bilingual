using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHSchool.Editor;
using System.ComponentModel;
using System.Windows.Forms;
using FISCA.Data;
using System.Data;
using JHSchool.Data;

namespace JHSchool.Permrec.EduAdminExtendCotnrols.ClassUpgrade.DAL
{
    class UpgradeClassDAL
    {
        /// <summary>
        /// 修改班級名稱與年級
        /// </summary>
        /// <param name="ClassItems"></param>
        public static void UpdateClassNameGradeYear(List<ClassItem> ClassItems)
        {
//            List<ClassRecordEditor> ClassRecordEditors = new List<ClassRecordEditor>();

            List<JHSchool.Data.JHClassRecord> classRecList = new List<JHSchool.Data.JHClassRecord> ();
            // 依班級年級排序
            ClassItems.Sort(new Comparison<ClassItem>(ClassItemmStrCpmparer));
            
            foreach (ClassItem ci in ClassItems)
            {
//                ClassRecordEditor crd;
//                crd = ci.classrecord.GetEditor();
                JHSchool.Data.JHClassRecord  urRec = ci.classrecord;

                if(ci.ClassName != ci.newClassName )
                    urRec.Name = ci.newClassName;

                int GrYear;
                if(int.TryParse (ci.newGradeYear,out GrYear ))
                {
                    urRec.GradeYear =GrYear;
                }
                else
                    urRec.GradeYear =null ;

                classRecList.Add(urRec);
            }
            try
            {
                JHSchool.Data.JHClass.Update(classRecList);

            }
            catch 
            {
                System.Windows.Forms.MessageBox.Show("班級名稱更新失敗!");
            }
            Class.Instance.SyncAllBackground();
        }


        /// <summary>
        /// 檢查系統內是否有班級相同名稱
        /// </summary>
        /// <param name="ClassItems"></param>
        /// <returns></returns>
        public static List<ClassItem> checkUpdateClassName(List<ClassItem> ClassItems)
        {
            List<ClassItem> checkClassItems = new List<ClassItem>();

            foreach (ClassRecord cr in Class.Instance.Items)
                foreach (ClassItem ci in ClassItems)
                    if (cr.Name == ci.newClassName)
                        checkClassItems.Add(ci);

            return checkClassItems;
        
        }
        

        // 字串比較
        public static int ClassItemmStrCpmparer(ClassItem x, ClassItem y)
        {
            return y.newGradeYear.CompareTo (x.newGradeYear);
        }

        static BackgroundWorker bkWork;
        static List<string> sidList;
        static string errStr = "";
        /// <summary>
        /// 設定學生狀態(只能用畢業)
        /// </summary>
        /// <param name="StudentItems"></param>
        /// <param name="Status"></param>        /// 
        public static void setStudentStatus(List<StudentItem> StudentItems)
        {
            List<StudentRecordEditor> StudentRecordEditors = new List<StudentRecordEditor>();
            List<string> oldSidList = new List<string>();
            sidList = new List<string>();
            foreach (StudentItem si in StudentItems)
            {
                oldSidList.Add(si.StudentID);
            }


            // 取得畢業及離校生
            List<StudentRecord> GrStudRecs = new List<StudentRecord>();
            foreach (ClassRecord cr in Class.Instance.Items)
            {

                GrStudRecs.AddRange(cr.Students.GetStatusStudents("畢業或離校"));
            }
            List<string> checkIDNumber = new List<string>();

            foreach (StudentRecord studRec in GrStudRecs)
                checkIDNumber.Add(studRec.IDNumber);

            errStr = "";
            List<StudentRecord> NowStudRecs = Student.Instance.GetStudents(oldSidList.ToArray());
            foreach (StudentRecord studRec in NowStudRecs)
            {
                if (!checkIDNumber.Contains(studRec.IDNumber))
                    sidList.Add(studRec.ID);
                else
                {
                    errStr +="身分證號："+ studRec.IDNumber + "重覆,";
                }
            
            }



            if (StudentItems.Count > 0)
            {
                List<JHSchool.Data.JHStudentRecord> StudRecs = JHSchool.Data.JHStudent.SelectByIDs(sidList);
                foreach (JHSchool.Data.JHStudentRecord studRec in StudRecs)
                    studRec.Status = K12.Data.StudentRecord.StudentStatus.畢業或離校;
                JHSchool.Data.JHStudent.Update(StudRecs);

                JHSchool.Data.JHStudent.RemoveAll();
                JHSchool.Data.JHStudent.SelectAll();
                Student.Instance.SyncAllBackground();

                if (errStr != "")
                    MessageBox.Show(errStr);
            }
         
        }

        //static void bkWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if (errStr != "")
        //        MessageBox.Show(errStr);

        //}

        //// 處理畢業變更狀態
        //static void bkWork_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    List<JHSchool.Data.JHStudentRecord> StudRecs = JHSchool.Data.JHStudent.SelectByIDs(sidList);
        //    foreach (JHSchool.Data.JHStudentRecord studRec in StudRecs)
        //        studRec.Status = K12.Data.StudentRecord.StudentStatus.畢業或離校;
        //    JHSchool.Data.JHStudent.Update(StudRecs);
        //}

        /// <summary>
        /// 取得班級學生
        /// </summary>
        /// <param name="ClassIDs"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static Dictionary<string,List<StudentItem>> getStudentItems(List<ClassItem> ClassItems,string Status)
        {
            Class.Instance.SyncAllBackground();

            Dictionary<string, List<JHSchool.Data.JHStudentRecord>> classStud = new Dictionary<string, List<JHSchool.Data.JHStudentRecord>>();

            foreach (JHSchool.Data.JHStudentRecord studRec in JHSchool.Data.JHStudent.SelectAll())
            {
                if (studRec.Status == K12.Data.StudentRecord.StudentStatus.一般)
                {
                    if (classStud.ContainsKey(studRec.RefClassID))
                        classStud[studRec.RefClassID].Add(studRec);
                    else
                    {
                        List<JHSchool.Data.JHStudentRecord> StudRecList = new List<JHSchool.Data.JHStudentRecord>();
                        StudRecList.Add(studRec);
                        classStud.Add(studRec.RefClassID, StudRecList);                    
                    }
                
                }
            }

            Dictionary<string, List<StudentItem>> StudentItems = new Dictionary<string, List<StudentItem>>();
            foreach (ClassItem ci in ClassItems)
            {
                    List<StudentItem> studItems = new List<StudentItem>();

                if(classStud.ContainsKey(ci.ClassID ))
                {
                    foreach (JHSchool.Data.JHStudentRecord studRec in classStud[ci.ClassID])
                    {
                        StudentItem si = new StudentItem();
                        si.StudentID = studRec.ID;
                        si.Status = studRec.Status.ToString();
                        si.ClassName = studRec.Class.Name;
                        studItems.Add(si);
                        si = null;
                    }
                    StudentItems.Add(ci.ClassID, studItems);
                }                
            }
            return StudentItems;
        }


        /// <summary>
        /// 取得目前年級班級
        /// </summary>
        /// <returns></returns>
        public static List<ClassItem> getClassItems()
        {
            List<ClassItem> ClassItems = new List<ClassItem>();
            QueryHelper qh = new QueryHelper();
            string strSQL = @"select distinct class.id as cid,class.grade_year,class.class_name,class.naming_rule from student inner join class on student.ref_class_id=class.id where student.status=1 order by class.grade_year,class.class_name";
            DataTable dt = qh.Select(strSQL);
            foreach (DataRow dr in dt.Rows)
            {
                ClassItem ci = new ClassItem();
                ci.ClassID = dr["cid"].ToString();
                ci.ClassName = dr["class_name"].ToString();
                ci.GradeYear = dr["grade_year"].ToString();
                ci.NamingRule = dr["naming_rule"].ToString().Trim();
                
                ClassItems.Add(ci);
            }

            List<string> cids = ClassItems.Select(x => x.ClassID).ToList();

            List<JHClassRecord> CRecList = JHClass.SelectByIDs(cids);
            Dictionary<string, JHClassRecord> CRDict = new Dictionary<string, JHClassRecord>();

            foreach (JHClassRecord cr in CRecList)
                CRDict.Add(cr.ID, cr);

            foreach (ClassItem ci in ClassItems)
            {
                if (CRDict.ContainsKey(ci.ClassID))
                    ci.classrecord = CRDict[ci.ClassID];
            }

            #region old
            //JHSchool.Data.JHClass.RemoveAll();
            //JHSchool.Data.JHClass.SelectAll();
            //// 找出一般生班級

            //List<string> ClassIDList = new List<string>();
            //List<JHSchool.Data.JHClassRecord> classRecList = new List<JHSchool.Data.JHClassRecord>();

            //foreach (JHSchool.Data.JHStudentRecord stud in JHSchool.Data.JHStudent.SelectAll())
            //    if (stud.Status == K12.Data.StudentRecord.StudentStatus.一般)
            //        if (!string.IsNullOrEmpty(stud.RefClassID))
            //            if (!ClassIDList.Contains(stud.RefClassID))
            //            {
            //                ClassIDList.Add(stud.RefClassID);
            //                classRecList.Add(stud.Class);
            //            }
            //List<ClassItem> ClassItems = new List<ClassItem>();
            //List<ClassItem> tmpCItems = new List<ClassItem>();
            //foreach (JHSchool.Data.JHClassRecord cr in classRecList)
            //{
            //    if (cr.GradeYear.HasValue)
            //    {
            //        ClassItem ci = new ClassItem();
            //        ci.ClassID = cr.ID;
            //        if (cr.GradeYear.HasValue)
            //            ci.GradeYear = cr.GradeYear.Value + "";
            //        ci.ClassName = cr.Name;
            //        ci.NamingRule = cr.NamingRule;
            //        ci.classrecord = cr;
            //        tmpCItems.Add(ci);
            //        ci = null;
            //    }
            //}

            //// 依年級班級名稱排序
            //List<string> grList = new List<string>();
            //foreach (ClassItem ci in tmpCItems)
            //    if (!grList.Contains(ci.GradeYear))
            //        grList.Add(ci.GradeYear);
            //grList.Sort();
            //List<ClassItem> tmpsortItem = new List<ClassItem>();
            //foreach (string str in grList)
            //{
            //    tmpsortItem.Clear();
            //    foreach (ClassItem ci in tmpCItems)
            //        if (str == ci.GradeYear)
            //            tmpsortItem.Add(ci);
            //    tmpsortItem.Sort(new Comparison<ClassItem>(tmpSortClassItem1));
            //    ClassItems.AddRange(tmpsortItem);
            //}
            #endregion
            
            return ClassItems;
        }

        private static int tmpSortClassItem1(ClassItem x, ClassItem y)
        {
            return x.ClassName.CompareTo(y.ClassName);            
        }

        public static string ParseClassName(string namingRule, int gradeYear)
        {
            //if (gradeYear >= 6)
            //    gradeYear -= 6;
            //gradeYear--;
            if (!ValidateNamingRule(namingRule))
                return namingRule;
            string classlist_firstname = "", classlist_lastname = "";
            if (namingRule.Length == 0) return "{" + (gradeYear + 1) + "}";

            string tmp_convert = namingRule;

            // 找出"{"之前文字 並放入 classlist_firstname , 並除去"{"
            if (tmp_convert.IndexOf('{') > 0)
            {
                classlist_firstname = tmp_convert.Substring(0, tmp_convert.IndexOf('{'));
                tmp_convert = tmp_convert.Substring(tmp_convert.IndexOf('{') + 1, tmp_convert.Length - (tmp_convert.IndexOf('{') + 1));
            }
            else tmp_convert = tmp_convert.TrimStart('{');

            // 找出 } 之後文字 classlist_lastname , 並除去"}"
            if (tmp_convert.IndexOf('}') > 0 && tmp_convert.IndexOf('}') < tmp_convert.Length - 1)
            {
                classlist_lastname = tmp_convert.Substring(tmp_convert.IndexOf('}') + 1, tmp_convert.Length - (tmp_convert.IndexOf('}') + 1));
                tmp_convert = tmp_convert.Substring(0, tmp_convert.IndexOf('}'));
            }
            else tmp_convert = tmp_convert.TrimEnd('}');

            // , 存入 array
            string[] listArray = new string[tmp_convert.Split(',').Length];
            listArray = tmp_convert.Split(',');

            int idx = 0;

            if ((gradeYear - 1) >= 0)
                idx = gradeYear - 1;

            if (gradeYear - 4 >= 0)
                idx = gradeYear - 4;

            if (gradeYear - 7 >= 0)
                idx = gradeYear - 7;

            if (gradeYear - 10 >= 0)
                idx = gradeYear - 10;
            
            // 檢查是否在清單範圍
            if (idx >= 0 && idx < listArray.Length)
            {
                tmp_convert = classlist_firstname + listArray[idx] + classlist_lastname;
            }
            else
            {
                tmp_convert = classlist_firstname + "{" + (gradeYear + 1) + "}" + classlist_lastname;
            }
            return tmp_convert;
        }

        public static bool ValidateNamingRule(string namingRule)
        {
            return namingRule.IndexOf('{') < namingRule.IndexOf('}');
        }

    }
}
