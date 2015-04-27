using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using FISCA.Presentation.Controls;
using IRewriteAPI_JH;
using K12.Data;
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

            //批次修改入學及畢業日期
            FISCA.Presentation.MenuButton btnOrder = FISCA.Presentation.MotherForm.RibbonBarItems["學生", "指定"]["批次修改入學及畢業日期"];
            btnOrder.Enable = false;
            btnOrder.Image = Properties.Resources.record_b_write_64;
            btnOrder.Click += delegate
            {
                new BasicInformation.Student.SchoolYearEditor().ShowDialog();
            };

            K12.Presentation.NLDPanels.Student.SelectedSourceChanged += delegate
            {
                btnOrder.Enable = Permissions.批次修改入學及畢業日期_雙語部權限 && K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0;
            };

            //雙語部 - 班級名條 & 班級點名單
            FISCA.Presentation.MenuButton btnC = FISCA.Presentation.MotherForm.RibbonBarItems["班級", "資料統計"]["報表"]["學務相關報表"];
            btnC["班級點名表(雙語部)"].Enable = Permissions.班級點名單_雙語部權限;
            btnC["班級點名表(雙語部)"].Click += delegate
            {
                if (K12.Presentation.NLDPanels.Class.SelectedSource.Count > 0)
                {
                    new TempletChooseForm().ShowDialog();
                }
                else
                {
                    MsgBox.Show("請選擇班級!!");
                }
            };

            btnC["缺曠週報表_依假別(雙語部)"].Enable = Permissions.缺曠週報表_依假別_雙語部權限;
            btnC["缺曠週報表_依假別(雙語部)"].Click += delegate
            {
                if (K12.Presentation.NLDPanels.Class.SelectedSource.Count > 0)
                {
                    new Report().Print();
                }
                else
                {
                    MsgBox.Show("請選擇班級!!");
                }
            };

            //班級
            btnC["缺曠通知單(雙語部)"].Enable = Permissions.班級缺曠通知單_雙語部權限;
            btnC["缺曠通知單(雙語部)"].Click += delegate
            {
                if (K12.Presentation.NLDPanels.Class.SelectedSource.Count > 0)
                {
                    new AbsenceNotification.Report("class").Print();
                }
                else
                {
                    MsgBox.Show("請選擇班級!!");
                }
            };

            btnC["獎懲通知單(雙語部)"].Enable = Permissions.班級獎懲通知單_雙語部權限;
            btnC["獎懲通知單(雙語部)"].Click += delegate
            {
                if (K12.Presentation.NLDPanels.Class.SelectedSource.Count > 0)
                {
                    new DisciplineNotification.Report("class").Print();
                }
                else
                {
                    MsgBox.Show("請選擇班級!!");
                }
            };

            FISCA.Presentation.MenuButton btnS = FISCA.Presentation.MotherForm.RibbonBarItems["學生", "資料統計"]["報表"]["學務相關報表"];

            //學生
            btnS["缺曠通知單(雙語部)"].Enable = Permissions.學生缺曠通知單_雙語部權限;
            btnS["缺曠通知單(雙語部)"].Click += delegate
            {
                if (K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0)
                {
                    new AbsenceNotification.Report("student").Print();
                }
                else
                {
                    MsgBox.Show("請選擇學生!!");
                }
            };

            //學生
            btnS["獎懲通知單(雙語部)"].Enable = Permissions.學生獎懲通知單_雙語部權限;
            btnS["獎懲通知單(雙語部)"].Click += delegate
            {
                if (K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0)
                {
                    new DisciplineNotification.Report("student").Print();
                }
                else
                {
                    MsgBox.Show("請選擇學生!!");
                }
            };

            RibbonBarButton rbItemImport = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["匯入"];
            RibbonBarButton rbItemExport = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["匯出"];

            rbItemExport["學籍相關匯出"]["匯出學生基本資料(雙語部)"].Enable = Permissions.匯出學生基本資料_雙語部權限;
            rbItemExport["學籍相關匯出"]["匯出學生基本資料(雙語部)"].Click += delegate
            {
                SmartSchool.API.PlugIn.Export.Exporter exporter = new ExportSchoolObject();
                ExportStudentV2 wizard = new ExportStudentV2(exporter.Text, exporter.Image);
                exporter.InitializeExport(wizard);
                wizard.ShowDialog();
            };

            rbItemImport["學籍相關匯入"]["匯入學生基本資料(雙語部)"].Enable = Permissions.匯入學生基本資料_雙語部權限;
            rbItemImport["學籍相關匯入"]["匯入學生基本資料(雙語部)"].Click += delegate
            {
                SmartSchool.API.PlugIn.Import.Importer importer = new ImportSchoolObject();
                ImportStudentV2 wizard = new ImportStudentV2(importer.Text, importer.Image);
                importer.InitializeImport(wizard);
                wizard.ShowDialog();
            };


            FISCA.Presentation.RibbonBarButton btnSDAdmin = FISCA.Presentation.MotherForm.RibbonBarItems["學務作業", "資料統計"]["報表"];
            //學務作業
            btnSDAdmin.Size = RibbonBarButton.MenuButtonSize.Large;
            btnSDAdmin["獎懲公告單(雙語部)"].Enable = Permissions.獎懲公告單_雙語部權限;
            btnSDAdmin["獎懲公告單(雙語部)"].Click += delegate
            {
                new AnnouncementSingle().ShowDialog();
            };

            FISCA.Permission.Catalog TestCatalog1 = FISCA.Permission.RoleAclSource.Instance["班級"]["報表"];
            TestCatalog1.Add(new FISCA.Permission.RibbonFeature(Permissions.班級點名單_雙語部, "班級點名表(雙語部)"));
            TestCatalog1.Add(new FISCA.Permission.RibbonFeature(Permissions.班級缺曠通知單_雙語部, "缺曠通知單(雙語部)"));
            TestCatalog1.Add(new FISCA.Permission.RibbonFeature(Permissions.班級獎懲通知單_雙語部, "獎懲通知單(雙語部)"));
            TestCatalog1.Add(new FISCA.Permission.RibbonFeature(Permissions.缺曠週報表_依假別_雙語部, "缺曠週報表_依假別(雙語部)"));

            FISCA.Permission.Catalog TestCatalog2 = FISCA.Permission.RoleAclSource.Instance["學生"]["報表"];
            TestCatalog2.Add(new FISCA.Permission.RibbonFeature(Permissions.學生缺曠通知單_雙語部, "缺曠通知單(雙語部)"));
            TestCatalog2.Add(new FISCA.Permission.RibbonFeature(Permissions.學生獎懲通知單_雙語部, "獎懲通知單(雙語部)"));

            FISCA.Permission.Catalog TestCatalog3 = FISCA.Permission.RoleAclSource.Instance["學務作業"]["報表"];
            TestCatalog3.Add(new FISCA.Permission.RibbonFeature(Permissions.獎懲公告單_雙語部, "獎懲公告單(雙語部)"));

            FISCA.Permission.Catalog TestCatalog4 = FISCA.Permission.RoleAclSource.Instance["學生"]["功能按鈕"];
            TestCatalog4.Add(new FISCA.Permission.RibbonFeature(Permissions.批次修改入學及畢業日期_雙語部, "批次修改入學及畢業日期"));

            TestCatalog2.Add(new FISCA.Permission.RibbonFeature(Permissions.匯出學生基本資料_雙語部, "匯出學生基本資料(雙語部)"));
            TestCatalog2.Add(new FISCA.Permission.RibbonFeature(Permissions.匯入學生基本資料_雙語部, "匯入學生基本資料(雙語部)"));

        }

        /// <summary>
        /// 處理學生資料
        /// </summary>
        private static void ResStudentData()
        {
            Dictionary<string, StudentRecord_Ext> StudentExtDic = GetStudentExt();
            Dictionary<string, string> StudentEnName = GetStudentExName();

            #region 學生 暱稱&居留證號

            ListPaneField StudentEnglishNameField = new ListPaneField("英文姓名");
            StudentEnglishNameField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (StudentEnName.ContainsKey(e.Key))
                {
                    e.Value = StudentEnName[e.Key];
                }
            };
            K12.Presentation.NLDPanels.Student.AddListPaneField(StudentEnglishNameField);

            ListPaneField StudentNicknameField = new ListPaneField("英文別名");
            StudentNicknameField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (StudentExtDic.ContainsKey(e.Key))
                {
                    e.Value = StudentExtDic[e.Key].Nickname;
                }
            };
            K12.Presentation.NLDPanels.Student.AddListPaneField(StudentNicknameField);

            ListPaneField PassportNumberField = new ListPaneField("居留證號");
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
