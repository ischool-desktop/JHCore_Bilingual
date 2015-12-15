using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FISCA;
using FISCA.Presentation;
using Framework;
using Framework.Security;
using JHSchool.Affair;
using JHSchool.Legacy;
using IRewriteAPI_JH;

namespace JHSchool.Permrec
{
    public static class Program
    {
        public enum ModuleFlag
        {
            HsinChu,
            KaoHsiung,
            TaiChung,
            NULL
        }

        public static ModuleFlag ModuleType
        {
            get
            {
                FileInfo asm = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", ""));
                // 新竹
                string pathHsinChu = Path.Combine(asm.DirectoryName, "HsinChu.txt");
                // 高雄
                string pathKaoHsiung = Path.Combine(asm.DirectoryName, "KaoHsiung.txt");
                // 台中
                string pathTaiChung = Path.Combine(asm.DirectoryName, "TaiChung.txt");

                if (File.Exists(pathHsinChu))
                    return ModuleFlag.HsinChu;
                else if (File.Exists(pathKaoHsiung))
                    return ModuleFlag.KaoHsiung;
                else if (File.Exists(pathTaiChung))
                    return ModuleFlag.TaiChung;
                else return ModuleFlag.NULL;
            }
        }

        //[MainMethod("JHSchool.Permrec")]
        [ApplicationMain()]
        static public void Main()
        {
            //Student.Instance.AddDetailBulider(new ContentItemBulider<StudentExtendControls.UpdateRecordItem>());
            //Class.Instance.AddDetailBulider(new DetailBulider<JHSchool.Permrec.ClassExtendControls.ClassStudentItem>());

            System.Threading.ThreadPool.QueueUserWorkItem(x =>
            {
                JHSchool.Data.JHStudent.SelectAll();
            });

          // 班級學生資訊
            Class.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<JHSchool.Permrec.ClassExtendControls.ClassStudentItem>());
            Student.Instance.AddDetailBulider(new DetailBulider<StudentExtendControls.AddressPalmerwormItem>());

            // 聯絡電話功能先移除
            //Student.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<JHSchool.Permrec.StudentExtendControls.PhonePalmerwormItem>());

            // 高中自訂欄位註解
//            Student.Instance.AddDetailBulider(new ContentItemBulider<StudentExtendControls.ExtensionValuesPalmerwormItem>());
            Student.Instance.AddDetailBulider(new DetailBulider<StudentExtendControls.DiplomaInfoPalmerworm>());

            Student.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<JHSchool.Permrec.StudentExtendControls.ParentInfoPalmerwormItem>());
            //Student.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider <JHSchool.Permrec.StudentExtendControls.ParentInfoPalmerwormItem>());
            // Test new Class Item
            
            // 學生>班級資訊覆寫
            IStudentClassDetailItemAPI item = FISCA.InteractionService.DiscoverAPI<IStudentClassDetailItemAPI>();
            if (item != null)
            {
                Student.Instance.AddDetailBulider(item.CreateBasicInfo());
            }
            else
            {
                Student.Instance.AddDetailBulider(new DetailBulider<JHSchool.Permrec.StudentExtendControls.ClassItem>());
            }
            

            Student.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<JHSchool.Permrec.StudentExtendControls.SemesterHistoryDetail>());
            Student.Instance.AddDetailBulider(new DetailBulider<StudentExtendControls.BeforeEnrollmentItem>());

            RibbonBarButton Class_rbDelItem = Class.Instance.RibbonBarItems["編輯"]["刪除"];
            RibbonBarButton Student_rbDelItem = Student.Instance.RibbonBarItems["編輯"]["刪除"];
            RibbonBarButton Teacher_rbDelItem = Teacher.Instance.RibbonBarItems["編輯"]["刪除"];

            Student.Instance.RibbonBarItems["教務"]["新生作業"].Size = RibbonBarButton.MenuButtonSize.Large;
            Student.Instance.RibbonBarItems["教務"]["新生作業"].Image = Properties.Resources.user_write_64;

            Student.Instance.RibbonBarItems["教務"]["異動作業"].Size = RibbonBarButton.MenuButtonSize.Large;
            Student.Instance.RibbonBarItems["教務"]["異動作業"].Image = Properties.Resources.demographic_reload_64;

            EduAdmin.Instance.RibbonBarItems["基本設定"].Index = 0;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"].Index = 1;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["新生作業"].Enable = true;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["新生作業"].Size = RibbonBarButton.MenuButtonSize.Large;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["異動作業"].Enable = true;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["異動作業"].Size = RibbonBarButton.MenuButtonSize.Large;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["成績作業"].Enable = true;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["成績作業"].Size = RibbonBarButton.MenuButtonSize.Large;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["畢業作業"].Enable = true;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["畢業作業"].Size = RibbonBarButton.MenuButtonSize.Large;

            //Teacher.Instance.SelectedListChanged _=dele

            Class.Instance.SelectedListChanged += delegate
            {
                Class_rbDelItem.Enable = (Class.Instance.SelectedList.Count < 2) & User.Acl["JHSchool.Class.Ribbon0010"].Executable;
            };

            Student.Instance.SelectedListChanged += delegate
            {
                Student_rbDelItem.Enable = (Student.Instance.SelectedList.Count < 2) & User.Acl["JHSchool.Student.Ribbon0010"].Executable;
            };

            Teacher.Instance.SelectedListChanged += delegate
            {
                Teacher_rbDelItem.Enable = (Teacher.Instance.SelectedList.Count < 2) & User.Acl["JHSchool.Teacher.Ribbon0010"].Executable;
            };

            // 匯入照片
            //20131216 - dylan註解
            //RibbonBarItem rbiPhotoImport = Student.Instance.RibbonBarItems["資料統計"];
            //rbiPhotoImport["匯入"]["學籍相關匯入"]["匯入學生照片"].Enable = User.Acl["JHSchool.Student.Ribbon0130"].Executable;
            //rbiPhotoImport["匯入"]["學籍相關匯入"]["匯入學生照片"].Click += (sender, e) => (new K12.Form.Photo.PhotosBatchImportForm()).ShowDialog();
            
            //已由K12.Form.Photo取代
            //rbiPhotoImport["匯入"]["匯入照片"].Click += delegate
            //{
            //    JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports.PhotosBatchImportForm PhotoBatchImport = new JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports.PhotosBatchImportForm();
            //    PhotoBatchImport.ShowDialog();
            //};

            // 匯出照片
            //20131216 - dylan註解
            //RibbonBarItem rbiPhotoExport = Student.Instance.RibbonBarItems["資料統計"];
            //rbiPhotoExport["匯出"]["學籍相關匯出"]["匯出學生照片"].Enable = User.Acl["JHSchool.Student.Ribbon0120"].Executable;
            //rbiPhotoExport["匯出"]["學籍相關匯出"]["匯出學生照片"].Click += (sender, e) => (new K12.Form.Photo.PhotosBatchExportForm()).ShowDialog();
            //已由K12.Form.Photo取代
            //rbiPhotoExport["匯出"]["匯出照片"].Click += delegate
            //{
            //    JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports.PhotosBatchExportForm PhotoBatchExport = new JHSchool.Permrec.StudentExtendControls.PhotosBatchImportExports.PhotosBatchExportForm();
            //    PhotoBatchExport.ShowDialog();
            //};


            //2012/5/10 - dylan - (移除此功能,由快速待處理替代)
            // 快速加入待處理功能
            //Student.Instance.RibbonBarItems["待處理學生"]["加入"].Size = RibbonBarButton.MenuButtonSize.Small;
            //Student.Instance.RibbonBarItems["待處理學生"]["加入"].Click += delegate
            //{
            //    // 將所選學生加入待處理                   
            //    Student.Instance.AddToTemporal(Student.Instance.SelectedKeys);

            //};

            //Student.Instance.RibbonBarItems["待處理學生"]["移出"].Size = RibbonBarButton.MenuButtonSize.Small;
            //Student.Instance.RibbonBarItems["待處理學生"]["移出"].Click += delegate
            //{
            //    // 將所選學生移出待處理
            //    Student.Instance.RemoveFromTemporal(Student.Instance.SelectedKeys);

            //};

            //Student.Instance.RibbonBarItems["待處理學生"]["瀏覽"].Size = RibbonBarButton.MenuButtonSize.Small;
            //Student.Instance.RibbonBarItems["待處理學生"]["瀏覽"].Click += delegate
            //{
            //    // 瀏覽待處理學生

            //        K12.Presentation.NLDPanels.Student.DisplayStatus = DisplayStatus.Temp;
            //        if ( K12.Presentation.NLDPanels.Student.DisplayStatus == DisplayStatus.Temp )
            //            K12.Presentation.NLDPanels.Student.SelectAll();

            //};

            //RibbonBarControlContainer rbiTemporaStudentManager = MotherForm.RibbonBarItems["學生", "待處理學生"].Controls["快速加入"];
            //rbiTemporaStudentManager.Control = new JHSchool.Permrec.StudentExtendControls.Ribbon.TemporaStudentManager();
            //MotherForm.RibbonBarItems["學生", "待處理學生"].SetTopContainer(ContainerType.Small);

            // 教育程度資料檔
            RibbonBarItem rbiEducationData = Student.Instance.RibbonBarItems["教務"];
            //rbiEducationData["新生作業"]["產生教育程度資料"].Image = Properties.Resources.capture_save_64;
            rbiEducationData["新生作業"]["產生教育程度資料"].Enable = User.Acl["JHSchool.Student.Ribbon0140"].Executable;
            rbiEducationData["新生作業"]["產生教育程度資料"].Click += delegate
            {
                if (Student.Instance.SelectedList.Count >= 1)
                {
                    JHSchool.Permrec.EduAdminExtendCotnrols.EducationDataCreator.EducationCodeCreateForm edCreator = new JHSchool.Permrec.EduAdminExtendCotnrols.EducationDataCreator.EducationCodeCreateForm();
                    edCreator.ShowDialog();
                }
                else
                    MsgBox.Show("請選擇學生");                                    
            };

            #region 產生教育程度資料 - 刻意放2份程式碼
            //rbiEducationData["畢業作業"]["產生教育程度資料"].Image = Properties.Resources.capture_save_64;
            rbiEducationData["畢業作業"]["產生教育程度資料"].Enable = User.Acl["JHSchool.Student.Ribbon0140"].Executable;
            rbiEducationData["畢業作業"]["產生教育程度資料"].Click += delegate
            {
                if (Student.Instance.SelectedList.Count >= 1)
                {
                    JHSchool.Permrec.EduAdminExtendCotnrols.EducationDataCreator.EducationCodeCreateForm edCreator = new JHSchool.Permrec.EduAdminExtendCotnrols.EducationDataCreator.EducationCodeCreateForm();
                    edCreator.ShowDialog();
                }
                else
                    MsgBox.Show("請選擇學生");
            }; 
            #endregion

            // 學生報表
            // 轉出回條
            MenuButton rbStudentReports = Student.Instance.RibbonBarItems["資料統計"]["報表"]["學籍相關報表"];
            rbStudentReports["轉出回條"].Enable = User.Acl["JHSchool.Student.Report0080"].Executable;
            rbStudentReports["轉出回條"].Click += delegate
            {
                if (Student.Instance.SelectedList.Count >= 1)
                {
                    JHSchool.Permrec.StudentExtendControls.Reports.StudentBackToArticleForm sbtaf = new JHSchool.Permrec.StudentExtendControls.Reports.StudentBackToArticleForm();
                    sbtaf.ShowDialog();
                }
                else
                    MsgBox.Show("請選擇學生");
            };

            string 轉出回條 = "ischool/國中系統/學生/報表/學籍/轉出回條";
            FISCA.Features.Register(轉出回條, arg =>
            {
                JHSchool.Permrec.StudentExtendControls.Reports.StudentBackToArticleForm sbtaf = new JHSchool.Permrec.StudentExtendControls.Reports.StudentBackToArticleForm();
                sbtaf.ShowDialog();
            });

            // 在學證明書_無成績
            rbStudentReports["在學證明書(無成績)"].Enable = User.Acl["JHSchool.Student.Report0090"].Executable;
            rbStudentReports["在學證明書(無成績)"].Click += delegate
            {
                if (Student.Instance.SelectedList.Count >= 1)
                {
                    JHSchool.Permrec.StudentExtendControls.Reports.StudentAtSchoolCertificateForm sascf = new JHSchool.Permrec.StudentExtendControls.Reports.StudentAtSchoolCertificateForm();
                    sascf.ShowDialog();
                }
                else
                    MsgBox.Show("請選擇學生");

            };

            // 畢修業證明書相關資料
            rbStudentReports["畢修業證明書相關資料"].Enable = User.Acl["JHSchool.Student.Report0100"].Executable;
            rbStudentReports["畢修業證明書相關資料"].Click += delegate
            {
                if (Student.Instance.SelectedList.Count >= 1)
                {
                    JHSchool.Permrec.StudentExtendControls.Reports.StudentGraduateInfoManager sgim = new JHSchool.Permrec.StudentExtendControls.Reports.StudentGraduateInfoManager();
                }
                else
                    MsgBox.Show("請選擇學生");
            };

            // 轉學證明書
            rbStudentReports["轉學證明書"].Enable = User.Acl["JHSchool.Student.Report0110"].Executable;
            rbStudentReports["轉學證明書"].Click += delegate
            {
                if (Student.Instance.SelectedList.Count >= 1)
                {
                    JHSchool.Permrec.StudentExtendControls.Reports.StudentTransExportForm stff = new JHSchool.Permrec.StudentExtendControls.Reports.StudentTransExportForm();
                    stff.ShowDialog();
                }
                else
                    MsgBox.Show("請選擇學生");
            };

            string URL轉學證明書 = "ischool/國中系統/學生/報表/學籍/轉學證明書";
            FISCA.Features.Register(URL轉學證明書, arg =>
            {
                if (Student.Instance.SelectedList.Count >= 1)
                {
                    JHSchool.Permrec.StudentExtendControls.Reports.StudentTransExportForm stff = new JHSchool.Permrec.StudentExtendControls.Reports.StudentTransExportForm();
                    stff.ShowDialog();
                }
                else
                    MsgBox.Show("請選擇學生");
            });

            if (ModuleType == ModuleFlag.KaoHsiung)
            {
                // 休學證明書
                rbStudentReports["休學證明書"].Enable = User.Acl["JHSchool.Permrec.StudentExtendControls.Reports.StudentLeaveForm"].Executable;
                rbStudentReports["休學證明書"].Click += delegate
                {
                    if (Student.Instance.SelectedList.Count >= 1)
                    {
                        JHSchool.Permrec.StudentExtendControls.Reports.StudentLeaveForm slf = new JHSchool.Permrec.StudentExtendControls.Reports.StudentLeaveForm();
                        slf.ShowDialog();
                    }
                    else
                        MsgBox.Show("請選擇學生");
                };
            }

            // 畢業證明書
            rbStudentReports["畢業證明書"].Enable = User.Acl["JHSchool.Permrec.StudentExtendControls.Reports.StudGraduateCertficateFormA"].Executable;
            rbStudentReports["畢業證明書"].Click += delegate
            {
                if (Student.Instance.SelectedList.Count >= 1)
                {
                    JHSchool.Permrec.StudentExtendControls.Reports.StudGraduateCertficateFormA sgcfa = new JHSchool.Permrec.StudentExtendControls.Reports.StudGraduateCertficateFormA();
                    sgcfa.ShowDialog();
                }
                else
                    MsgBox.Show("請選擇學生");
            };

            // 畢業證書
            rbStudentReports["畢業證書"].Enable = User.Acl["JHSchool.Permrec.StudentExtendControls.Reports.StudGraduateCertficateForm"].Executable;
            rbStudentReports["畢業證書"].Click += delegate
            {
                if (Student.Instance.SelectedList.Count >= 1)
                {
                    JHSchool.Permrec.StudentExtendControls.Reports.StudGraduateCertficateForm sgcf = new JHSchool.Permrec.StudentExtendControls.Reports.StudGraduateCertficateForm();
                    sgcf.ShowDialog();
                }
                else
                    MsgBox.Show("請選擇學生");
            };


            #region 在教務作業上增加RibbonBar

            RibbonBarButton rbItemClassUpgrade = EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["班級升級"];
            rbItemClassUpgrade.Size = RibbonBarButton.MenuButtonSize.Medium;
            rbItemClassUpgrade.Image = Properties.Resources.btnUpgrade_Image;
            rbItemClassUpgrade.Enable = User.Acl["JHSchool.EduAdmin.Ribbon0110"].Executable;
            rbItemClassUpgrade.Click += delegate
            {
                JHSchool.Permrec.EduAdminExtendCotnrols.ClassUpgrade.ClassUpgradeForm CUF = new JHSchool.Permrec.EduAdminExtendCotnrols.ClassUpgrade.ClassUpgradeForm();
                CUF.ShowDialog();
            };


            MenuButton rbItemTool05 = EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["畢業作業"]["產生畢業證書字號"];
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["畢業作業"].Image = Properties.Resources.graduated_write_64;
            EduAdmin.Instance.RibbonBarItems["批次作業/檢視"]["畢業作業"].Size = RibbonBarButton.MenuButtonSize.Medium;
            rbItemTool05.Enable= User.Acl["JHSchool.EduAdmin.Ribbon00801"].Executable;
            rbItemTool05.Click += delegate
            {
                EduAdminExtendCotnrols.StudGraduateDocNo.BatchStudGraduateDocNo BSGD = new JHSchool.Permrec.EduAdminExtendCotnrols.StudGraduateDocNo.BatchStudGraduateDocNo();
                BSGD.ShowDialog();
            };


            //RibbonBarButton rbItem1 = EduAdmin.Instance.RibbonBarItems["學籍"]["名冊"];
            //rbItem1.Size = RibbonBarButton.MenuButtonSize.Medium;
            //rbItem1.Image = GDResources.btnItemNameList_Image;
            //rbItem1.Enable = User.Acl["JHSchool.EduAdmin.Ribbon0060"].Executable;
            //rbItem1.Click += delegate
            //{               

            //    JHSchool.Permrec.AffairExtendControls.GovernmentalDocument.Process.NameList.RegisterNames();
            //    new ListForm().ShowDialog();

            //};

            //RibbonBarItem ViewForm1 = EduAdmin.Instance.RibbonBarItems["學籍"];
            //ViewForm1["異動資料檢視"].Enable = User.Acl["JHSchool.EduAdmin.Ribbon0070"].Executable;
            //ViewForm1["異動資料檢視"].Click += delegate
            //{
            //    UpdateRecordViewForm UpReViewForm = new UpdateRecordViewForm();
            //    UpReViewForm.ShowDialog();
            //};
            #endregion
            MenuButton rbClassStudentListRptItem = Class.Instance.RibbonBarItems["資料統計"]["報表"]["學籍相關報表"];
            rbClassStudentListRptItem["班級名條"].Enable = User.Acl["JHSchool.Class.Report0000"].Executable;
            rbClassStudentListRptItem["班級名條"].Click += delegate
            {
                //JHSchool.Permrec.ClassExtendControls.Reports.ClassStudentListRpt jcslr = new JHSchool.Permrec.ClassExtendControls.Reports.ClassStudentListRpt();
                JHSchool.Permrec.ClassExtendControls.Reports.ClassStudentListRptForm cslpr = new JHSchool.Permrec.ClassExtendControls.Reports.ClassStudentListRptForm();
                cslpr.ShowDialog();
            };

            // 學生男女比例
            MenuButton rbClassGenderPercentageItem = Class.Instance.RibbonBarItems["資料統計"]["報表"]["學籍相關報表"];
            rbClassGenderPercentageItem["學生男女比例"].Enable = User.Acl["JHSchool.Class.Report0130"].Executable;
            rbClassGenderPercentageItem["學生男女比例"].Click += delegate
            {
                JHSchool.Permrec.ClassExtendControls.Reports.ClassGenderPercentageRpt csgrp = new JHSchool.Permrec.ClassExtendControls.Reports.ClassGenderPercentageRpt();
            };

            // 里鄰統計
            MenuButton rbClassDistrictAreaCountItem = Class.Instance.RibbonBarItems["資料統計"]["報表"]["學籍相關報表"];
            rbClassDistrictAreaCountItem["學生里鄰統計"].Enable = User.Acl["JHSchool.Class.Report0140"].Executable;
            rbClassDistrictAreaCountItem["學生里鄰統計"].Click += delegate
            {
                JHSchool.Permrec.ClassExtendControls.Reports.ClassDistrictAreaCountRpt csacr = new JHSchool.Permrec.ClassExtendControls.Reports.ClassDistrictAreaCountRpt();
            };

            // 學生年齡統計
            MenuButton rbClassStudentAgeCountItem = Class.Instance.RibbonBarItems["資料統計"]["報表"]["學籍相關報表"];
            rbClassStudentAgeCountItem["學生年齡統計"].Enable = User.Acl["JHSchool.Class.Report0150"].Executable;
            rbClassStudentAgeCountItem["學生年齡統計"].Click += delegate
            {
                JHSchool.Permrec.ClassExtendControls.Reports.ClassAgePercentageRpt capr = new JHSchool.Permrec.ClassExtendControls.Reports.ClassAgePercentageRpt();
                capr.ShowDialog();
            };

            // 學生父母職業統計
            MenuButton rbClassStudentJobCountItem = Class.Instance.RibbonBarItems["資料統計"]["報表"]["學籍相關報表"];
            rbClassDistrictAreaCountItem["學生父母職業統計"].Enable = User.Acl["JHSchool.Class.Report0160"].Executable;
            rbClassDistrictAreaCountItem["學生父母職業統計"].Click += delegate
            {
                JHSchool.Permrec.ClassExtendControls.Reports.ClassParentJobPercentageRpt cpjpr = new JHSchool.Permrec.ClassExtendControls.Reports.ClassParentJobPercentageRpt();
                cpjpr.ShowDialog();

            };

            // 學生類別統計
            MenuButton rbClassStudentTagCountItem = Class.Instance.RibbonBarItems["資料統計"]["報表"]["學籍相關報表"];
            rbClassStudentTagCountItem["學生類別統計"].Enable = User.Acl["JHSchool.Class.Report0160"].Executable;
            rbClassStudentTagCountItem["學生類別統計"].Click += delegate
            {
                JHSchool.Permrec.ClassExtendControls.Reports.ClassStudentTagCountRpt cstcr = new JHSchool.Permrec.ClassExtendControls.Reports.ClassStudentTagCountRpt();
                cstcr.ShowDialog();

            };

            JHSchool.Class.Instance.SelectedListChanged += delegate
            {
                rbClassStudentListRptItem["班級名條"].Enable = (JHSchool.Class.Instance.SelectedKeys.Count > 0);
            };

            /*在班級新增報表*/
            MenuButton countGenderRecord = Class.Instance.RibbonBarItems["資料統計"]["報表"]["學籍相關報表"];

            // 因討論先註解
            //countGenderRecord["學生男女統計表"].Enable = false;
            //countGenderRecord["學生男女統計表"].Enable = User.Acl["JHSchool.Class.Report0010"].Executable;
            //countGenderRecord["學生男女統計表"].Click += delegate
            //{

            //  // countGenderRecord["學生男女統計表"].Enable = (JHSchool.Class.Instance.SelectedKeys.Count > 0);
            //    new JHSchool.Permrec.ClassExtendControls.GenderCount().ShowDialog();
            //};

            #region 成績的東西，註解掉
            //MenuButton classExamPrint = Class.Instance.RibbonBarItems["資料統計"]["報表"]["成績相關報表"];
            // classExamPrint["班級考試成績單"].Enable = User.Acl["JHSchool.Class.Report0011"].Executable;
            //MenuButton btnSelectExamPrint = classExamPrint["班級考試成績單"];
            //btnSelectExamPrint.Image = Properties.Resources.schedule_64;
            //btnSelectExamPrint.Click += delegate
            //{
            //    new JHSchool.Permrec.ClassExtendControls.SelectExamPrint().ShowDialog();
            //};

            // classExamPrint["班級考試成績單"].Enable = User.Acl["JHSchool.Class.Report0011"].Executable;
            //classExamPrint["班級學期領域成績一覽表"].Click += delegate
            //{
            //    new JHSchool.Permrec.ClassExtendControls.PrintClassDomainSemesterScore().ShowDialog();
            //};
            //classExamPrint["班級學期科目成績一覽表"].Click += delegate
            //{
            //    new JHSchool.Permrec.ClassExtendControls.PrintClassSubjectSemesterScore().ShowDialog();
            //};


            //classExamPrint["班級考試成績單"].Enable = false;
            //classExamPrint["班級學期領域成績一覽表"].Enable = false;
            //classExamPrint["班級學期科目成績一覽表"].Enable = false ;

            //當有被選取的班級時，才能執行 ... 
            //JHSchool.Class.Instance.SelectedListChanged += delegate
            //{
            //    classExamPrint["班級考試成績單"].Enable = (JHSchool.Class.Instance.SelectedKeys.Count > 0);
            //    classExamPrint["班級學期領域成績一覽表"].Enable = (JHSchool.Class.Instance.SelectedKeys.Count > 0);
            //    //classExamPrint["班級學期科目成績一覽表"].Enable = (JHSchool.Class.Instance.SelectedKeys.Count > 0);
            //};
            #endregion

            /*在學生中新增報表*/


            Student.Instance.RibbonBarItems["資料統計"]["報表"].Image = Properties.Resources.paste_64;
            Student.Instance.RibbonBarItems["資料統計"]["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
            Class.Instance.RibbonBarItems["資料統計"]["報表"].Image = Properties.Resources.paste_64;
            Class.Instance.RibbonBarItems["資料統計"]["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
            Course.Instance.RibbonBarItems["資料統計"]["報表"].Image = Properties.Resources.paste_64;
            Course.Instance.RibbonBarItems["資料統計"]["報表"].Size = RibbonBarButton.MenuButtonSize.Large;

            MenuButton studentIdDocument = Student.Instance.RibbonBarItems["資料統計"]["報表"]["學籍相關報表"];
            MenuButton studentidClass = Class.Instance.RibbonBarItems["資料統計"]["報表"]["學籍相關報表"];

            ////因為順序問題，所以放在這裡。
            //LogViewfinder.PluginMain.RegisterEntityRibbon();

            // 學生證給新竹舊式用
            if (ModuleType == ModuleFlag.HsinChu)
            {

                studentIdDocument["學生證(舊)"].Enable = User.Acl["JHSchool.Student.Report0071"].Executable;
                studentIdDocument["學生證(舊)"].Click += delegate
                {
                    if (Student.Instance.SelectedList.Count >= 1)
                    {

                        JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardFormOld sidf = new JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardFormOld(JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardFormOld.UseModuleType.學生);
                        sidf.ShowDialog();
                    }
                    else
                        MsgBox.Show("請選擇學生");
                };

                studentidClass["學生證(舊)"].Enable = User.Acl["JHSchool.Class.Report0121"].Executable;
                studentidClass["學生證(舊)"].Click += delegate
                {

                    JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardFormOld sidfC = new JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardFormOld(JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardFormOld.UseModuleType.班級);
                    sidfC.ShowDialog();
                };


            }


            studentIdDocument["學生證"].Enable = User.Acl["JHSchool.Student.Report0070"].Executable;
            studentIdDocument["學生證"].Click += delegate
            {
                if (Student.Instance.SelectedList.Count >= 1)
                {

                    JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardForm sidf = new JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardForm(JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardForm.UseModuleType.學生);
                    sidf.ShowDialog();
                }
                else
                    MsgBox.Show("請選擇學生");
            };

            studentidClass["學生證"].Enable = User.Acl["JHSchool.Class.Report0120"].Executable;
            studentidClass["學生證"].Click += delegate
            {

                JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardForm sidfC = new JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardForm(JHSchool.Permrec.StudentExtendControls.Reports.StudentIDCardForm.UseModuleType.班級);
                sidfC.ShowDialog();
            };


            // 匯出匯入異動
            //由類別模組提供
            RibbonBarButton rbUpdateRecordExport = Student.Instance.RibbonBarItems["資料統計"]["匯出"];
            //rbUpdateRecordExport["學籍相關匯出"]["匯出學生類別"].Enable = User.Acl["JHSchool.Student.Ribbon0300"].Executable;
            //rbUpdateRecordExport["學籍相關匯出"]["匯出學生類別"].Click += delegate
            //{
            //    SmartSchool.API.PlugIn.Export.Exporter exporter = new JHSchool.Permrec.ImportExport.StudentTag.ExportStudentTag();
            //    JHSchool.Permrec.ImportExport.ExportStudentV2 wizard = new JHSchool.Permrec.ImportExport.ExportStudentV2(exporter.Text, exporter.Image);
            //    exporter.InitializeExport(wizard);
            //    wizard.ShowDialog();

            //};

            RibbonBarButton rbUpdateRecordImport = Student.Instance.RibbonBarItems["資料統計"]["匯入"];

            //由類別模組提供
            //rbUpdateRecordImport["學籍相關匯入"]["匯入學生類別"].Enable = User.Acl["JHSchool.Student.ImportExport.StudentTag.ImportStudentTag"].Executable;
            //rbUpdateRecordImport["學籍相關匯入"]["匯入學生類別"].Click += delegate
            //{
            //    SmartSchool.API.PlugIn.Import.Importer Importer = new JHSchool.Permrec.ImportExport.StudentTag.ImportStudentTag();
            //    JHSchool.Permrec.ImportExport.ImportStudentV2 wizard = new JHSchool.Permrec.ImportExport.ImportStudentV2(Importer.Text, Importer.Image);
            //    Importer.InitializeImport(wizard);
            //    wizard.ShowDialog();
            //};



            RibbonBarButton rbTeacherTagRecordIE = Teacher.Instance.RibbonBarItems["資料統計"]["匯出"];
            //由類別模組提供
            //rbTeacherTagRecordIE["匯出教師類別"].Enable = User.Acl["JHSchool.Teacher.Ribbon0300"].Executable;
            //rbTeacherTagRecordIE["匯出教師類別"].Click += delegate
            //{
            //    SmartSchool.API.PlugIn.Export.Exporter exporter = new JHSchool.Permrec.ImportExport.TeacherTag.ExportTeacherTag();
            //    JHSchool.Permrec.ImportExport.ExportTeacherV2 wizard = new JHSchool.Permrec.ImportExport.ExportTeacherV2(exporter.Text, exporter.Image);
            //    exporter.InitializeExport(wizard);
            //    wizard.ShowDialog();
            //};

            RibbonBarItem rbStudBatchAddressLatitude = Student.Instance.RibbonBarItems["其它"];
            rbStudBatchAddressLatitude["查詢經緯度"].Image = Properties.Resources.world_zoom_64;
            rbStudBatchAddressLatitude["查詢經緯度"].Enable = User.Acl["JHSchool.Student.RibbonBatchAddressLatitude"].Executable;
            rbStudBatchAddressLatitude["查詢經緯度"].Click += delegate
            {
                JHSchool.Permrec.StudentExtendControls.Ribbon.BatchAddressLatitudeForm balf = new JHSchool.Permrec.StudentExtendControls.Ribbon.BatchAddressLatitudeForm();
                balf.ShowDialog();
            };

            // 匯出匯入畢業資訊
            RibbonBarButton rbGraduateInfoExport = Student.Instance.RibbonBarItems["資料統計"]["匯出"];
            rbGraduateInfoExport["學籍相關匯出"]["匯出畢業資訊"].Enable = User.Acl["JHSchool.Permrec.ImportExport.GraduateInfo.ExportGraduateInfo"].Executable;
            rbGraduateInfoExport["學籍相關匯出"]["匯出畢業資訊"].Click += delegate
            {
                SmartSchool.API.PlugIn.Export.Exporter exporterGradInfo = new JHSchool.Permrec.ImportExport.GraduateInfo.ExportGraduateInfo();
                JHSchool.Permrec.ImportExport.ExportStudentV2 wizardGradInfo = new JHSchool.Permrec.ImportExport.ExportStudentV2(exporterGradInfo.Text, exporterGradInfo.Image);
                exporterGradInfo.InitializeExport(wizardGradInfo);
                wizardGradInfo.ShowDialog();

            };

            RibbonBarButton rbGraduateInfoImport = Student.Instance.RibbonBarItems["資料統計"]["匯入"];

            rbGraduateInfoImport["學籍相關匯入"]["匯入畢業資訊"].Enable = User.Acl["JHSchool.Permrec.ImportExport.GraduateInfo.ImportGraduateInfo"].Executable;
            rbGraduateInfoImport["學籍相關匯入"]["匯入畢業資訊"].Click += delegate
            {
                SmartSchool.API.PlugIn.Import.Importer Importer = new JHSchool.Permrec.ImportExport.GraduateInfo.ImportGraduateInfo();
                JHSchool.Permrec.ImportExport.ImportStudentV2 wizard = new JHSchool.Permrec.ImportExport.ImportStudentV2(Importer.Text, Importer.Image);
                Importer.InitializeImport(wizard);
                wizard.ShowDialog();

            };



            #region 註冊權限管理
            //學生
            Catalog detail = RoleAclSource.Instance["學生"]["資料項目"];
            detail.Add(new DetailItemFeature(typeof(StudentExtendControls.DiplomaInfoPalmerworm)));
            detail.Add(new DetailItemFeature(typeof(StudentExtendControls.AddressPalmerwormItem)));
            detail.Add(new DetailItemFeature(typeof(StudentExtendControls.ParentInfoPalmerwormItem)));
            // 學生電話功能先移除
            //detail.Add(new DetailItemFeature(typeof(StudentExtendControls.PhonePalmerwormItem)));

            detail.Add(new DetailItemFeature(typeof(JHSchool.Permrec.StudentExtendControls.ClassItem)));
            detail.Add(new DetailItemFeature(typeof(StudentExtendControls.BeforeEnrollmentItem)));

            detail.Add(new DetailItemFeature(typeof(JHSchool.Permrec.StudentExtendControls.SemesterHistoryDetail)));

            Catalog studReportRoleAcl = RoleAclSource.Instance["學生"]["報表"];
            studReportRoleAcl.Add(new ReportFeature("JHSchool.Student.Report0070", "學生證"));

            if (ModuleType == ModuleFlag.HsinChu)
                studReportRoleAcl.Add(new ReportFeature("JHSchool.Student.Report0071", "學生證(舊)"));

            studReportRoleAcl.Add(new ReportFeature("JHSchool.Student.Report0080", "轉出回條"));
            studReportRoleAcl.Add(new ReportFeature("JHSchool.Student.Report0090", "在學證明書(無成績)"));
            studReportRoleAcl.Add(new ReportFeature("JHSchool.Student.Report0100", "畢修業證明書相關資料"));
            studReportRoleAcl.Add(new ReportFeature("JHSchool.Student.Report0110", "轉學證明書"));
            
            if(ModuleType == ModuleFlag.KaoHsiung )
                studReportRoleAcl.Add(new ReportFeature("JHSchool.Permrec.StudentExtendControls.Reports.StudentLeaveForm", "休學證明書"));

            studReportRoleAcl.Add(new ReportFeature("JHSchool.Permrec.StudentExtendControls.Reports.StudGraduateCertficateFormA", "畢業證明書"));
            studReportRoleAcl.Add(new ReportFeature("JHSchool.Permrec.StudentExtendControls.Reports.StudGraduateCertficateForm", "畢業證書"));

            Catalog StudFunctionButtonRoleAcl = RoleAclSource.Instance["學生"]["功能按鈕"];
            //StudFunctionButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.Ribbon0120", "匯出照片"));
            //StudFunctionButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.Ribbon0130", "匯入照片"));
            StudFunctionButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.Ribbon0140", "產生教育程度資料"));
            //StudFunctionButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.Ribbon0300", "匯出學生類別"));
            StudFunctionButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.RibbonBatchAddressLatitude", "查詢經緯度"));
            //StudFunctionButtonRoleAcl.Add(new RibbonFeature("JHSchool.Student.ImportExport.StudentTag.ImportStudentTag", "匯入學生類別"));

            StudFunctionButtonRoleAcl.Add(new RibbonFeature("JHSchool.Permrec.ImportExport.GraduateInfo.ExportGraduateInfo", "匯出畢業資訊"));
            StudFunctionButtonRoleAcl.Add(new RibbonFeature("JHSchool.Permrec.ImportExport.GraduateInfo.ImportGraduateInfo", "匯入畢業資訊"));

            // 教師
            Catalog TeacherFuncButtonRoleAcl = RoleAclSource.Instance["教師"]["功能按鈕"];
            //TeacherFuncButtonRoleAcl.Add(new RibbonFeature("JHSchool.Teacher.Ribbon0300", "匯出教師類別"));

            //班級
            detail = RoleAclSource.Instance["班級"]["資料項目"];
            detail.Add(new DetailItemFeature(typeof(JHSchool.Permrec.ClassExtendControls.ClassStudentItem)));

            Catalog report = RoleAclSource.Instance["班級"]["報表"];
            report.Add(new ReportFeature("JHSchool.Class.Report0000", "班級名條"));
            report.Add(new ReportFeature("JHSchool.Class.Report0120", "學生證"));

            if (ModuleType == ModuleFlag.HsinChu)
                report.Add(new ReportFeature("JHSchool.Class.Report0121", "學生證(舊)"));

            report.Add(new ReportFeature("JHSchool.Class.Report0130", "學生男女比例"));
            report.Add(new ReportFeature("JHSchool.Class.Report0140", "學生里鄰統計"));
            report.Add(new ReportFeature("JHSchool.Class.Report0150", "學生年齡統計"));
            report.Add(new ReportFeature("JHSchool.Class.Report0160", "學生父母職業統計"));

            //教務作業
            Catalog ribbon = RoleAclSource.Instance["教務作業"];
            ribbon.Add(new RibbonFeature("JHSchool.EduAdmin.Ribbon0110", "班級升級"));
            ribbon.Add(new RibbonFeature("JHSchool.EduAdmin.Ribbon00801", "產生異業證書字號"));

            #endregion
                       
            Dictionary<string, string> _PermanentTelDic = new Dictionary<string, string>();
            Dictionary<string, string> _ContactTelDic = new Dictionary<string, string>();
            
            #region 戶籍電話欄位
            FISCA.Presentation.ListPaneField PermanentTelField = new ListPaneField("戶籍電話");
            PermanentTelField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_PermanentTelDic.ContainsKey(e.Key))
                    e.Value = _PermanentTelDic[e.Key];
                else
                    e.Value = string.Empty;            
            };
                        
            PermanentTelField.PreloadVariableBackground += delegate(object sender, PreloadVariableEventArgs e)
            {
                _PermanentTelDic.Clear();
                foreach (JHSchool.Data.JHPhoneRecord PhoneRec in JHSchool.Data.JHPhone.SelectByStudentIDs(e.Keys))
                    if (!_PermanentTelDic.ContainsKey(PhoneRec.RefStudentID))
                        _PermanentTelDic.Add(PhoneRec.RefStudentID, PhoneRec.Permanent);                
            };
            //Student.Instance.AddListPaneField(PermanentTelField);
            // 加入戶籍電話權限管理
            if (User.Acl["Student.Field.戶籍電話"].Executable)
                Student.Instance.AddListPaneField(PermanentTelField);
            Catalog ribbonField = RoleAclSource.Instance["學生"]["清單欄位"];
            ribbonField.Add(new RibbonFeature("Student.Field.戶籍電話", "戶籍電話"));

            //AsyncFieldLoader<Phone, PhoneRecord> field = new AsyncFieldLoader<Phone, PhoneRecord>(Phone.Instance, "戶籍電話");
            //field.GetValue += delegate(object sender, GetValueEventArgs e)
            //{
            //    if (Phone.Instance[e.Key] != null)
            //        e.Value = Phone.Instance[e.Key].Permanent;
            //    else
            //        e.Value = string.Empty;
            //};
            //Student.Instance.AddListPaneField(field.Field);
            #endregion

            #region 聯絡電話
            FISCA.Presentation.ListPaneField ContactTelField = new ListPaneField("聯絡電話");
            ContactTelField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_ContactTelDic.ContainsKey(e.Key))
                    e.Value = _ContactTelDic[e.Key];
                else
                    e.Value = string.Empty;
            };           

            ContactTelField.PreloadVariableBackground += delegate(object sender, PreloadVariableEventArgs e)
            {
                _ContactTelDic.Clear();
                foreach (JHSchool.Data.JHPhoneRecord PhoneRec in JHSchool.Data.JHPhone.SelectByStudentIDs(new List<string>(e.Keys)))
                    if (!_ContactTelDic.ContainsKey(PhoneRec.RefStudentID))
                        _ContactTelDic.Add(PhoneRec.RefStudentID, PhoneRec.Contact);
            };
            //Student.Instance.AddListPaneField(ContactTelField);
            // 加入聯絡電話權限管理
            if (User.Acl["Student.Field.聯絡電話"].Executable)
                Student.Instance.AddListPaneField(ContactTelField);
            ribbonField = RoleAclSource.Instance["學生"]["清單欄位"];
            ribbonField.Add(new RibbonFeature("Student.Field.聯絡電話", "聯絡電話"));


            //AsyncFieldLoader<Phone, PhoneRecord> telfield1 = new AsyncFieldLoader<Phone, PhoneRecord>(Phone.Instance, "聯絡電話");
            //telfield1.GetValue += delegate(object sender, GetValueEventArgs e)
            //{
            //    if (Phone.Instance[e.Key] != null)
            //        e.Value = Phone.Instance[e.Key].Contact;
            //    else
            //        e.Value = string.Empty;
            //};
            //Student.Instance.AddListPaneField(telfield1.Field);

            #endregion

            #region 監護人
            Dictionary<string, string> _CustodianFieldDic = new Dictionary<string, string>();
            CustodianField = new ListPaneField("監護人");
            CustodianField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_CustodianFieldDic.ContainsKey(e.Key))
                    e.Value = _CustodianFieldDic[e.Key];
                else
                    e.Value = string.Empty;            
            };

            CustodianField.PreloadVariableBackground += delegate(object sender, PreloadVariableEventArgs e)
            {
                _CustodianFieldDic.Clear();
                foreach (JHSchool.Data.JHParentRecord ParentRec in JHSchool.Data.JHParent.SelectByStudentIDs(new List<string>(e.Keys)))
                    if (!_CustodianFieldDic.ContainsKey(ParentRec.RefStudentID))
                        _CustodianFieldDic.Add(ParentRec.RefStudentID ,ParentRec.Custodian.Name);
            };
            Student.Instance.AddListPaneField(CustodianField);
            //AsyncFieldLoader<Parent, ParentRecord> parent1 = new AsyncFieldLoader<Parent, ParentRecord>(Parent.Instance, "監護人");
            //parent1.GetValue += delegate(object sender, GetValueEventArgs e)
            //{
            //    if (Parent.Instance[e.Key] != null)
            //        e.Value = Parent.Instance[e.Key].Custodian.Name;
            //    else
            //        e.Value = string.Empty;
            //};
            //Student.Instance.AddListPaneField(parent1.Field);
            #endregion

            #region 畢業國小
            // 新寫法
            Dictionary<string, string> _BeforeInfoFieldDic = new Dictionary<string, string>();
            FISCA.Presentation.ListPaneField BeforeInfoField = new ListPaneField("畢業國小");
            BeforeInfoField.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_BeforeInfoFieldDic.ContainsKey(e.Key))
                    e.Value = _BeforeInfoFieldDic[e.Key];
                else
                    e.Value = string.Empty;
            };

            BeforeInfoField.PreloadVariableBackground += delegate(object sender, PreloadVariableEventArgs e)
            {
                _BeforeInfoFieldDic.Clear();
                foreach (JHSchool.Data.JHBeforeEnrollmentRecord be in JHSchool.Data.JHBeforeEnrollment.SelectByStudentIDs(e.Keys))
                    if (!_BeforeInfoFieldDic.ContainsKey(be.RefStudentID))
                        _BeforeInfoFieldDic.Add(be.RefStudentID, be.School);
            };

            Student.Instance.AddListPaneField(BeforeInfoField);
            //AsyncFieldLoader<BeforeInfoRec, JHSchool.Data.JHBeforeEnrollmentRecord> grad = new AsyncFieldLoader<BeforeInfoRec, JHSchool.Data.JHBeforeEnrollmentRecord>(BeforeInfoRec.Instance, "畢業國小");
            //grad.GetValue += delegate(object sender, GetValueEventArgs e)
            //{

            //    if (BeforeInfoRec.Instance[e.Key] != null)
            //        e.Value = BeforeInfoRec.Instance[e.Key].School;
            //    else
            //        e.Value = string.Empty;

            //};
            //Student.Instance.AddListPaneField(grad.Field);
            #endregion

            #region 搜尋(畢業國小)
            //TODO 因為 Presentation 不支援非同步搜尋，所以先拿掉。
            //ConfigData cd = User.Configuration["StudentSearchOptionPreference"];

            //SearchGraduateSchool = Student.Instance.SearchConditionMenu["畢業國小"];
            //SearchGraduateSchool.AutoCheckOnClick = true;
            //SearchGraduateSchool.AutoCollapseOnClick = false;
            //SearchGraduateSchool.Checked = cd.GetBoolean("SearchGraduateSchool", false); //預設不要尋找這種的。
            //SearchGraduateSchool.Click += delegate
            //{
            //    cd.SetBoolean("SearchGraduateSchool", SearchGraduateSchool.Checked);
            //    BackgroundWorker async = new BackgroundWorker();
            //    async.DoWork += delegate(object sender, DoWorkEventArgs e) { (e.Argument as ConfigData).Save(); };
            //    async.RunWorkerAsync(cd);
            //};

            //TODO 新的 Presentation 不支援這種的。
            //Student.Instance.SearchAsync += new EventHandler<SearchEventArgs>(Instance_SearchAsync);
            #endregion

            //Student.Instance.RibbonBarItems["測試"]["測"].Click += delegate
            //{
            //    List<string> keys = Class.Instance.SelectedList[0].Students.AsKeyList();

            //    BackgroundWorker worker = new BackgroundWorker();
            //    worker.DoWork += delegate
            //    {
            //        for (int i = 0; i < keys.Count; i++)
            //        {
            //            List<UpdateRecordRecord> r1 = UpdateRecord.Instance[keys[i]];
            //        }
            //    };
            //    worker.RunWorkerAsync();

            //    for (int i = 0; i < keys.Count; i++)
            //    {
            //        List<UpdateRecordRecord> r2 = UpdateRecord.Instance[keys[i]];
            //    }
            //};
        }

        public static FISCA.Presentation.ListPaneField CustodianField;

        static void Instance_SelectedListChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        private static MenuButton SearchGraduateSchool { get; set; }

        //private static void Instance_SearchAsync(object sender, SearchEventArgs e)
        //{
        //    try
        //    {
        //        List<StudentRecord> students = new List<StudentRecord>(Student.Instance.Items);
        //        Dictionary<string, StudentRecord> results = new Dictionary<string, StudentRecord>();
        //        Regex rx = new Regex(e.Condition, RegexOptions.IgnoreCase);

        //        if (SearchGraduateSchool.Checked)
        //        {
        //            if (!UpdateRecord.Instance.Loaded)
        //                UpdateRecord.Instance.SyncAllBackground();

        //            foreach (List<UpdateRecordRecord> updates in UpdateRecord.Instance.Items)
        //            {
        //                foreach (UpdateRecordRecord update in updates)
        //                {
        //                    if (update.Student == null) continue;

        //                    if (update.UpdateCode == "1") //新生
        //                    {
        //                        string name = update.Attributes["GraduateSchool"]; //畢業國小。
        //                        if (rx.Match(name).Success)
        //                        {
        //                            if (!results.ContainsKey(update.RefStudentID))
        //                                results.Add(update.RefStudentID, update.Student);
        //                        }
        //                        break;
        //                    }
        //                }
        //            }

        //            foreach (string key in e.Result)
        //                if (results.ContainsKey(key))
        //                    results[key] = null;

        //            foreach (StudentRecord record in results.Values)
        //                if (record != null)
        //                    e.Result.Add(record.ID);
        //        }
        //    }
        //    catch (Exception) { }

        //}
    }
}
