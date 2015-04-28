using System.Collections.Generic;
using System.Windows.Forms;
using FISCA.UDT;
using SmartSchool.API.PlugIn;
using System;
using System.Data;
using K12.Data;
using System.Text;

namespace BasicInformation
{
    class ImportSchoolObject : SmartSchool.API.PlugIn.Import.Importer
    {

        List<string> _Keys = new List<string>();

        EventHandler eh;
        string EventCode = "Res_StudentExt";

        public ImportSchoolObject()
        {
            this.Image = null;
            this.Text = "匯入學生基本資料(雙語部)";
        }

        public override void InitializeImport(SmartSchool.API.PlugIn.Import.ImportWizard wizard)
        {
            eh = FISCA.InteractionService.PublishEvent(EventCode);

            wizard.PackageLimit = 3000;

            //可匯入的欄位
            wizard.ImportableFields.AddRange("學號", "英文別名", "居留證號", "入學日期", "畢業日期");

            //參與比序需於 - 2015/6月開啟本功能
            //wizard.ImportableFields.AddRange("學年度", "學期", "幹部類別", "幹部名稱", "說明", "參與比序");

            //必需要有的欄位
            wizard.RequiredFields.AddRange("學號");

            //驗證開始事件
            //wizard.ValidateStart += (sender, e) => Keys.Clear();

            //驗證每行資料的事件
            wizard.ValidateRow += new System.EventHandler<SmartSchool.API.PlugIn.Import.ValidateRowEventArgs>(wizard_ValidateRow);

            //實際匯入資料的事件
            wizard.ImportPackage += new System.EventHandler<SmartSchool.API.PlugIn.Import.ImportPackageEventArgs>(wizard_ImportPackage);

            //匯入完成
            wizard.ImportComplete += (sender, e) => MessageBox.Show("匯入完成!");
        }

        void wizard_ValidateRow(object sender, SmartSchool.API.PlugIn.Import.ValidateRowEventArgs e)
        {
            #region 驗各欄位填寫格式
            int t;
            DateTime dt;
            foreach (string field in e.SelectFields)
            {
                string value = e.Data[field];
                switch (field)
                {
                    default:
                        break;
                    case "學號":
                        if (value == "")
                            e.ErrorFields.Add(field, "此欄為必填欄位。");
                        break;
                    case "英文別名":
                        break;
                    case "居留證號":
                        break;
                    case "入學日期":
                        if (value != "")
                        {
                            if (!DateTime.TryParse(value, out dt))
                                e.ErrorFields.Add(field, "資料必須為日期格式。");
                        }
                        break;
                    case "畢業日期":
                        if (value != "")
                        {
                            if (!DateTime.TryParse(value, out dt))
                                e.ErrorFields.Add(field, "資料必須為日期格式。");
                        }
                        break;
                }
            }
            #endregion
            #region 驗證主鍵
            string Key = e.Data.ID;
            string errorMessage = string.Empty;

            if (_Keys.Contains(Key))
                errorMessage = "學生編號、學年度、學期、幹部類別及幹部名稱的組合不能重覆!";
            else
                _Keys.Add(Key);

            e.ErrorMessage = errorMessage;

            #endregion
        }

        void wizard_ImportPackage(object sender, SmartSchool.API.PlugIn.Import.ImportPackageEventArgs e)
        {
            //Log
            StringBuilder LogSb = new StringBuilder();

            List<ActiveRecord> InsertRecords = new List<ActiveRecord>();

            List<ActiveRecord> UpdateRecords = new List<ActiveRecord>();

            //取得學號清單
            List<string> StudentNumList = GetStudentNumber(e.Items);

            //取得狀態為1的學生系統編號
            List<string> StudentIDList = GetStudentIDList(StudentNumList);

            //取得學生基本資料對照  學號:學生Record
            Dictionary<string, StudentRecord> StudentNumberDic = GetStudentNumberDic(StudentIDList);

            //取得學生基本資料對照  學號:學生Record
            Dictionary<string, StudentRecord> StudentIDDic = GetStudentIDDic(StudentIDList);

            //取得學生延申資料  學號:學生延申資料
            Dictionary<string, StudentRecord_Ext> StudentExtDic = GetStudExt(StudentIDList, StudentIDDic);


            foreach (RowData Row in e.Items)
            {
                string StudentNumber = "" + Row["學號"];
                //必須有這個學生
                if (StudentNumberDic.ContainsKey(StudentNumber))
                {
                    StudentRecord StudR = StudentNumberDic[StudentNumber];

                    string ClassName = StudR.RefClassID != "" ? StudR.Class.Name : "";
                    string SeatNo = StudR.SeatNo.HasValue ? StudR.SeatNo.Value.ToString() : "";

                    if (StudentExtDic.ContainsKey(StudentNumber))
                    {
                        StudentRecord_Ext record = StudentExtDic[StudentNumber];

                        DateTime Entrance = new DateTime();
                        if (Row.ContainsKey("入學日期"))
                            DateTime.TryParse("" + Row["入學日期"], out Entrance);
                        else
                            Entrance = record.EntranceDate.HasValue ? record.EntranceDate.Value : new DateTime();

                        DateTime Leaving = new DateTime();
                        if (Row.ContainsKey("畢業日期"))
                            DateTime.TryParse("" + Row["畢業日期"], out Leaving);
                        else
                            Leaving = record.EntranceDate.HasValue ? record.EntranceDate.Value : new DateTime();

                        string Nickname = "";
                        if (Row.ContainsKey("英文別名"))
                            Nickname = "" + Row["英文別名"];
                        else
                            Nickname = record.Nickname;


                        string PassportNumber = "";
                        if (Row.ContainsKey("居留證號"))
                            PassportNumber = "" + Row["居留證號"];
                        else
                            PassportNumber = record.PassportNumber;


                        LogSb.AppendLine("修改資料：");
                        LogSb.AppendLine(string.Format("班級「{0}」座號「{1}」學生「{2}」", ClassName, SeatNo, StudR.Name));
                        LogSb.AppendLine(string.Format("英文別名由「{0}」改為「{1}」", record.Nickname, Nickname));
                        LogSb.AppendLine(string.Format("居留證號由「{0}」改為「{1}」", record.PassportNumber, PassportNumber));

                        string entrance = record.EntranceDate.HasValue ? record.EntranceDate.Value.ToShortDateString() : "";
                        LogSb.AppendLine(string.Format("入學日期由「{0}」改為「{1}」", entrance, Entrance.ToShortDateString() != "0001/1/1" ? Entrance.ToShortDateString() : ""));

                        string leaving = record.LeavingDate.HasValue ? record.LeavingDate.Value.ToShortDateString() : "";
                        LogSb.AppendLine(string.Format("畢業日期由「{0}」改為「{1}」", leaving, Leaving.ToShortDateString() != "0001/1/1" ? Leaving.ToShortDateString() : ""));
                        LogSb.AppendLine();

                        record.Nickname = Nickname;
                        record.PassportNumber = PassportNumber;

                        if (Entrance.ToShortDateString() != "0001/1/1")
                            record.EntranceDate = Entrance;
                        else
                            record.EntranceDate = null;

                        if (Leaving.ToShortDateString() != "0001/1/1")
                            record.LeavingDate = Leaving;
                        else
                            record.LeavingDate = null;

                        UpdateRecords.Add(record);
                    }
                    else
                    {
                        DateTime Entrance = new DateTime();
                        if (Row.ContainsKey("入學日期"))
                            DateTime.TryParse("" + Row["入學日期"], out Entrance);

                        DateTime Leaving = new DateTime();
                        if (Row.ContainsKey("畢業日期"))
                            DateTime.TryParse("" + Row["畢業日期"], out Leaving);

                        string Nickname = Row.ContainsKey("英文別名") ? "" + Row["英文別名"] : "";
                        string PassportNumber = Row.ContainsKey("居留證號") ? "" + Row["居留證號"] : "";
                        //Log         
                        LogSb.AppendLine("新增資料：");
                        LogSb.AppendLine(string.Format("班級「{0}」座號「{1}」學生「{2}」", ClassName, SeatNo, StudR.Name));
                        LogSb.AppendLine(string.Format("英文別名「{0}」", Nickname));
                        LogSb.AppendLine(string.Format("居留證號「{0}」", PassportNumber));
                        LogSb.AppendLine(string.Format("入學日期「{0}」", Entrance.ToShortDateString() != "0001/1/1" ? Entrance.ToShortDateString() : ""));
                        LogSb.AppendLine(string.Format("畢業日期「{0}」", Leaving.ToShortDateString() != "0001/1/1" ? Leaving.ToShortDateString() : ""));
                        LogSb.AppendLine();

                        //新增
                        //新增資料
                        StudentRecord_Ext record = new StudentRecord_Ext();

                        record.RefStudentID = StudentNumberDic[StudentNumber].ID;

                        record.Nickname = Nickname;
                        record.PassportNumber = PassportNumber;

                        if (Entrance.ToShortDateString() != "0001/1/1")
                            record.EntranceDate = Entrance;
                        else
                            record.EntranceDate = null;

                        if (Leaving.ToShortDateString() != "0001/1/1")
                            record.LeavingDate = Leaving;
                        else
                            record.LeavingDate = null;

                        InsertRecords.Add(record);
                    }
                }
            }

            if (InsertRecords.Count > 0)
                tool._A.InsertValues(InsertRecords);

            if (UpdateRecords.Count > 0)
                tool._A.UpdateValues(UpdateRecords);

            FISCA.LogAgent.ApplicationLog.Log("匯入學生基本資料(雙語部)", "匯入", LogSb.ToString());

            eh(null, EventArgs.Empty);
        }

        /// <summary>
        /// 由學生系統編號取得學生額外資料字典 學生學號:學生Record
        /// </summary>
        private Dictionary<string, StudentRecord_Ext> GetStudExt(List<string> StudentIDList, Dictionary<string, StudentRecord> StudentIDDic)
        {
            Dictionary<string, StudentRecord_Ext> dic = new Dictionary<string, StudentRecord_Ext>();

            List<StudentRecord_Ext> records = tool._A.Select<StudentRecord_Ext>(string.Format("ref_student_id in ('{0}')", string.Join("','", StudentIDList)));

            foreach (StudentRecord_Ext each in records)
            {
                if (StudentIDDic.ContainsKey(each.RefStudentID))
                {
                    //學生
                    StudentRecord stud = StudentIDDic[each.RefStudentID];

                    if (!dic.ContainsKey(stud.StudentNumber))
                    {
                        dic.Add(stud.StudentNumber, each);
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// 3.由系統編號取得學生字典
        /// </summary>
        private Dictionary<string, StudentRecord> GetStudentNumberDic(List<string> StudentIDList)
        {
            Dictionary<string, StudentRecord> dic = new Dictionary<string, StudentRecord>();

            List<StudentRecord> studList = K12.Data.Student.SelectByIDs(StudentIDList);
            foreach (StudentRecord stud in studList)
            {
                if (!dic.ContainsKey(stud.StudentNumber))
                {
                    dic.Add(stud.StudentNumber, stud);
                }
            }
            return dic;
        }

        /// <summary>
        /// 3.由系統編號取得學生字典
        /// </summary>
        private Dictionary<string, StudentRecord> GetStudentIDDic(List<string> StudentIDList)
        {
            Dictionary<string, StudentRecord> dic = new Dictionary<string, StudentRecord>();

            List<StudentRecord> studList = K12.Data.Student.SelectByIDs(StudentIDList);
            foreach (StudentRecord stud in studList)
            {
                if (!dic.ContainsKey(stud.ID))
                {
                    dic.Add(stud.ID, stud);
                }
            }
            return dic;
        }

        /// <summary>
        /// 2.由學生學號取得學生系統編號
        /// </summary>
        private List<string> GetStudentIDList(List<string> StudentNumList)
        {
            List<string> StudentIDList = new List<string>();

            //取得使用該學號的學生ID
            DataTable dt = tool._Q.Select(string.Format("select id from student where student_number in ('{0}') and status='1'", string.Join("','", StudentNumList)));
            foreach (DataRow row in dt.Rows)
            {
                string studentID = "" + row["id"];
                if (!StudentIDList.Contains(studentID))
                {
                    StudentIDList.Add(studentID);
                }
            }

            return StudentIDList;
        }

        /// <summary>
        /// 1.由RowData取得學號清單
        /// </summary>
        private List<string> GetStudentNumber(List<RowData> list)
        {
            List<string> numberList = new List<string>();
            foreach (RowData Row in list)
            {
                string StudentNumber = "" + Row["學號"];
                if (!numberList.Contains(StudentNumber))
                {
                    numberList.Add(StudentNumber);
                }
            }
            return numberList;
        }
    }
}