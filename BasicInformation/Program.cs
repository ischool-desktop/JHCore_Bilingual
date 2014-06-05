using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using IRewriteAPI_JH;
using System;
using System.Collections.Generic;
using System.Data;
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
            FISCA.InteractionService.RegisterAPI<IStudentDetailItemAPI>(new BasicStudentItem_API());

            //覆蓋 - 教師基本資料項目
            FISCA.InteractionService.RegisterAPI<ITeacherDatailtemAPI>(new BasicTeacherItem_API());
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
            Dictionary<string, string> StudentEnName = GetStudentExName();

            #region 學生 暱稱&護照號碼

            ListPaneField StudentNicknameField = new ListPaneField("慣稱");
            StudentNicknameField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (StudentExtDic.ContainsKey(e.Key))
                {
                    e.Value = StudentExtDic[e.Key].Nickname;
                }
            };
            K12.Presentation.NLDPanels.Student.AddListPaneField(StudentNicknameField);

            ListPaneField StudentEnglishNameField = new ListPaneField("英文全名");
            StudentEnglishNameField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (StudentEnName.ContainsKey(e.Key))
                {
                    e.Value = StudentEnName[e.Key];
                }
            };
            K12.Presentation.NLDPanels.Student.AddListPaneField(StudentEnglishNameField);

            ListPaneField PassportNumberField = new ListPaneField("護照號碼");
            PassportNumberField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (StudentExtDic.ContainsKey(e.Key))
                {
                    e.Value = StudentExtDic[e.Key].PassportNumber;
                }
            };
            K12.Presentation.NLDPanels.Student.AddListPaneField(PassportNumberField);

            FISCA.InteractionService.SubscribeEvent("Res_StudentExt", (sender, args) =>
            {
                //取得更新資料
                StudentExtDic = GetStudentExt();
                StudentEnName = GetStudentExName();
                //重讀
                StudentNicknameField.Reload();
                PassportNumberField.Reload();
                StudentEnglishNameField.Reload();
            });
            #endregion
        }

        private static Dictionary<string, string> GetStudentExName()
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();
            DataTable dt = tool._Q.Select("select id,english_name from student");
            foreach (DataRow row in dt.Rows)
            {
                string id = "" + row["id"];
                string english_name = "" + row["english_name"];
                if (!dic.ContainsKey(id))
                {
                    dic.Add(id, english_name);
                }
            }
            return dic;

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

            ListPaneField TeacherCellPhoneField = new ListPaneField("行動電話");
            TeacherCellPhoneField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (TeacherExtDic.ContainsKey(e.Key))
                {
                    e.Value = TeacherExtDic[e.Key].CellPhone;
                }
            };
            K12.Presentation.NLDPanels.Teacher.AddListPaneField(TeacherCellPhoneField);

            FISCA.InteractionService.SubscribeEvent("Res_TeacherExt", (sender, args) =>
            {

                TeacherExtDic = GetTeacherExt();
                TeacherCellPhoneField.Reload();
            });

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
