using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicInformation
{
    public class Program
    {
        [MainMethod(StartupPriority.FirstAsynchronized)]
        static public void Main1()
        {
            //覆蓋 - 學生基本資料項目
            FISCA.InteractionService.RegisterAPI<JHSchool.API.IStudentDetailItemAPI>(new BasicStudentItem_API());

            //覆蓋 - 教師基本資料項目
            FISCA.InteractionService.RegisterAPI<JHSchool.API.ITeacherDatailtemAPI>(new BasicTeacherItem_API());

            //啟動更新事件
            EventHandler eh = FISCA.InteractionService.PublishEvent("Res_StudentExt");
            eh(null, EventArgs.Empty);
        }

        [MainMethod()]
        static public void Main2()
        {
            //處理學生資料
            ResStudentData();
            //處理教師資料
            ResTeacherData();
        }

        /// <summary>
        /// 處理學生資料
        /// </summary>
        private static void ResStudentData()
        {
            Dictionary<string, StudentRecord_Ext> StudentExtDic = GetStudentExt();

            #region 學生 中文姓名&護照號碼

            ListPaneField StudentChineseNameField = new ListPaneField("中文姓名");
            StudentChineseNameField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (StudentExtDic.ContainsKey(e.Key))
                {
                    e.Value = StudentExtDic[e.Key].ChineseName;
                }
            };
            K12.Presentation.NLDPanels.Student.AddListPaneField(StudentChineseNameField);

            ListPaneField PassportNumberField = new ListPaneField("護照號碼");
            PassportNumberField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (StudentExtDic.ContainsKey(e.Key))
                {
                    e.Value = StudentExtDic[e.Key].PassportNumber;
                }
            };
            K12.Presentation.NLDPanels.Student.AddListPaneField(PassportNumberField);

            #endregion
        }

        /// <summary>
        /// 取得全校所有學生延申資料
        /// </summary>
        private static Dictionary<string, StudentRecord_Ext> GetStudentExt()
        {
            Dictionary<string, StudentRecord_Ext> dic = new Dictionary<string, StudentRecord_Ext>();
            List<StudentRecord_Ext> StudentExtList = new List<StudentRecord_Ext>();
            StudentExtList = tool._A.Select<StudentRecord_Ext>();
            foreach (StudentRecord_Ext each in StudentExtList)
            {
                if (!dic.ContainsKey(each.RefStudentID))
                {
                    dic.Add(each.RefStudentID, each);
                }
            }

            return dic;
        }

        /// <summary>
        /// 處理教師資料
        /// </summary>
        private static void ResTeacherData()
        {
            Dictionary<string, TeacherRecord_Ext> TeacherExtDic = GetTeacherExt();

            ListPaneField TeacherChineseNameField = new ListPaneField("中文姓名");
            TeacherChineseNameField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (TeacherExtDic.ContainsKey(e.Key))
                {
                    e.Value = TeacherExtDic[e.Key].ChineseName;
                }
            };
            K12.Presentation.NLDPanels.Teacher.AddListPaneField(TeacherChineseNameField);
        }

        /// <summary>
        /// 取得教師延伸資料
        /// </summary>
        private static Dictionary<string, TeacherRecord_Ext> GetTeacherExt()
        {
            Dictionary<string, TeacherRecord_Ext> dic = new Dictionary<string, TeacherRecord_Ext>();
            List<TeacherRecord_Ext> TeacherExtList = new List<TeacherRecord_Ext>();
            TeacherExtList = tool._A.Select<TeacherRecord_Ext>();
            foreach (TeacherRecord_Ext each in TeacherExtList)
            {
                if (!dic.ContainsKey(each.RefTeacherID))
                {
                    dic.Add(each.RefTeacherID, each);
                }
            }

            return dic;
        }
    }
}
