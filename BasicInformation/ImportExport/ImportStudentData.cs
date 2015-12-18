using System.Collections.Generic;
using System.Windows.Forms;
using FISCA.UDT;
using SmartSchool.API.PlugIn;
using System;
using System.Data;
using K12.Data;
using System.Text;
using JHSchool.Data;

namespace BasicInformation
{
    class ImportStudentData : SmartSchool.API.PlugIn.Import.Importer
    {
        List<string> _FieldNameList=new List<string> ();
        List<string> _Keys = new List<string>();
        public ImportStudentData()
        {
            this.Image = null;
            this.Text = "匯入學生基本資料(2015)";            
            _FieldNameList.Add("姓名");
            _FieldNameList.Add("學號");
            _FieldNameList.Add("身分證號");
            _FieldNameList.Add("國籍");
            _FieldNameList.Add("出生地");
            _FieldNameList.Add("生日");
            _FieldNameList.Add("性別");
            _FieldNameList.Add("英文姓名");
            _FieldNameList.Add("英文別名");
            _FieldNameList.Add("居留證號");
            _FieldNameList.Add("GivenName");
            _FieldNameList.Add("MiddleName");
            _FieldNameList.Add("FamilyName");
            _FieldNameList.Add("班級");
            _FieldNameList.Add("年級");
            _FieldNameList.Add("座號");
            _FieldNameList.Add("父親姓名");
            _FieldNameList.Add("父親學歷");
            _FieldNameList.Add("父親職業");
            _FieldNameList.Add("父親電話");
            _FieldNameList.Add("母親姓名");
            _FieldNameList.Add("母親學歷");
            _FieldNameList.Add("母親職業");
            _FieldNameList.Add("母親電話");
            _FieldNameList.Add("戶籍電話");
            _FieldNameList.Add("聯絡電話");
            _FieldNameList.Add("行動電話");
            _FieldNameList.Add("其它電話1");
            _FieldNameList.Add("監護人姓名");
            _FieldNameList.Add("監護人電話");
            _FieldNameList.Add("入學日期");
            _FieldNameList.Add("畢業日期");
            _FieldNameList.Add("登入帳號");
            _FieldNameList.Add("狀態");
            _FieldNameList.Add("戶籍:郵遞區號");
            _FieldNameList.Add("戶籍:縣市");
            _FieldNameList.Add("戶籍:鄉鎮市區");
            _FieldNameList.Add("戶籍:村里");
            _FieldNameList.Add("戶籍:鄰");
            _FieldNameList.Add("戶籍:其他");
            _FieldNameList.Add("聯絡:郵遞區號");
            _FieldNameList.Add("聯絡:縣市");
            _FieldNameList.Add("聯絡:鄉鎮市區");
            _FieldNameList.Add("聯絡:村里");
            _FieldNameList.Add("聯絡:鄰");
            _FieldNameList.Add("聯絡:其他");
        }

        public override void InitializeImport(SmartSchool.API.PlugIn.Import.ImportWizard wizard)
        {
            wizard.PackageLimit = 3000;
            //必需要有的欄位
            wizard.RequiredFields.AddRange("學號");
            //可匯入的欄位
            wizard.ImportableFields.AddRange(_FieldNameList);

            //驗證每行資料的事件
            wizard.ValidateRow += wizard_ValidateRow;

            //實際匯入資料的事件
            wizard.ImportPackage += wizard_ImportPackage;

            //匯入完成
            wizard.ImportComplete += (sender, e) => MessageBox.Show("匯入完成!");
        }

        void wizard_ImportPackage(object sender, SmartSchool.API.PlugIn.Import.ImportPackageEventArgs e)
        {
            // 尋找主要Key來判斷，如果有學生系統編號先用系統編號，沒有使用學號，
            Dictionary<string, RowData> RowDataDict = new Dictionary<string, RowData>();
            Dictionary<string, int> chkSidDict = new Dictionary<string, int>();
            Dictionary<string, string> chkSnumDict = new Dictionary<string, string>();
            List<JHStudentRecord> InsertStudentRecList = new List<JHStudentRecord>();
            List<JHStudentRecord> StudentRecAllList = JHStudent.SelectAll();
            foreach (JHStudentRecord rec in StudentRecAllList)
            {
                chkSidDict.Add(rec.ID, 0);
                string key = rec.StudentNumber + rec.StatusStr;
                if (!chkSnumDict.ContainsKey(key))
                    chkSnumDict.Add(key, rec.ID);
            }
                        
            // 先處理學生基本資料
            foreach (RowData Row in e.Items)
            {
                string StudentID = "";

                if (Row.ContainsKey("學生系統編號"))
                {
                    string id = Row["學生系統編號"].ToString();
                    if (chkSidDict.ContainsKey(id))
                        StudentID = id;
                }

                if (StudentID == "")
                {
                    string ssNum = "", snum = "";
                    if (Row.ContainsKey("學號"))
                    {
                        snum = Row["學號"].ToString();
                        string status = "一般";
                        if (Row.ContainsKey("狀態"))
                        {
                            if (Row["狀態"].ToString() != "")
                                status = Row["狀態"].ToString();
                        }
                        ssNum = snum + status;
                    }

                    if (chkSnumDict.ContainsKey(ssNum))
                        StudentID = chkSnumDict[ssNum];
                    else
                    {
                        // 新增
                        if (chkSnumDict.ContainsKey(snum + "一般"))
                        {
                            // 有重複
                        }
                        else
                        {
                            // 批次新增
                            JHStudentRecord newRec = new JHStudentRecord();
                            newRec.StudentNumber = snum;
                            newRec.Status = StudentRecord.StudentStatus.一般;
                            if (Row.ContainsKey("姓名"))
                                newRec.Name = Row["姓名"].ToString();

                            InsertStudentRecList.Add(newRec);
                        }
                    }
                }
            }
 
            // 如有新學生處理新增學生
            if (InsertStudentRecList.Count > 0)
               JHStudent.Insert(InsertStudentRecList);

            // 再次建立索引
            Dictionary<string, JHStudentRecord> StudRecAllDict = new Dictionary<string, JHStudentRecord>();
            StudentRecAllList = JHStudent.SelectAll();
            chkSidDict.Clear();
            chkSnumDict.Clear();
            foreach (JHStudentRecord rec in StudentRecAllList)
            {
                chkSidDict.Add(rec.ID, 0);
                string key = rec.StudentNumber + rec.StatusStr;
                if (!chkSnumDict.ContainsKey(key))
                    chkSnumDict.Add(key, rec.ID);

                StudRecAllDict.Add(rec.ID, rec);
            }

            List<string> StudentIDList = new List<string>();
            //比對
            foreach (RowData Row in e.Items)
            {
                string StudentID = "";

                if (Row.ContainsKey("學生系統編號"))
                {
                    string id = Row["學生系統編號"].ToString();
                    if (chkSidDict.ContainsKey(id))
                        StudentID = id;
                }

                if (StudentID == "")
                {
                    string ssNum = "", snum = "";
                    if (Row.ContainsKey("學號"))
                    {
                        snum = Row["學號"].ToString();
                        string status = "一般";
                        if (Row.ContainsKey("狀態"))
                        {
                            if (Row["狀態"].ToString() != "")
                                status = Row["狀態"].ToString();
                        }
                        ssNum = snum + status;
                    }

                    if (chkSnumDict.ContainsKey(ssNum))
                        StudentID = chkSnumDict[ssNum];
                }

                    if (!string.IsNullOrEmpty(StudentID))
                    {
                        if (!RowDataDict.ContainsKey(StudentID))
                            RowDataDict.Add(StudentID, Row);

                        StudentIDList.Add(StudentID);
                    }
            }
               // 取得學生基本、父母監護人、電話、地址、UDT
            List<JHStudentRecord> StudentRecordList = JHStudent.SelectByIDs(StudentIDList);
            Dictionary<string, JHStudentRecord> StudentRecordDict = new Dictionary<string, JHStudentRecord>();
            foreach (JHStudentRecord rec in StudentRecordList)
                if (!StudentRecordDict.ContainsKey(rec.ID))
                    StudentRecordDict.Add(rec.ID, rec);

            List<JHParentRecord> ParentRecordList = JHParent.SelectByStudentIDs(StudentIDList);
            Dictionary<string, JHParentRecord> ParentRecordDict = new Dictionary<string, JHParentRecord>();
            foreach (JHParentRecord rec in ParentRecordList)
                if (!ParentRecordDict.ContainsKey(rec.RefStudentID))
                    ParentRecordDict.Add(rec.RefStudentID, rec);

            List<JHPhoneRecord> PhoneRecordList = JHPhone.SelectByStudentIDs(StudentIDList);
            Dictionary<string, JHPhoneRecord> PhoneRecordDict = new Dictionary<string, JHPhoneRecord>();
            foreach (JHPhoneRecord rec in PhoneRecordList)
                if (!PhoneRecordDict.ContainsKey(rec.RefStudentID))
                    PhoneRecordDict.Add(rec.RefStudentID, rec);

            List<JHAddressRecord> AddressRecordList = JHAddress.SelectByStudentIDs(StudentIDList);
            Dictionary<string, JHAddressRecord> AddressRecordDict = new Dictionary<string, JHAddressRecord>();
            foreach (JHAddressRecord rec in AddressRecordList)
                if (!AddressRecordDict.ContainsKey(rec.RefStudentID))
                    AddressRecordDict.Add(rec.RefStudentID, rec);

            AccessHelper accHepler = new AccessHelper ();
            string qry="ref_student_id in('"+string.Join("','",StudentIDList.ToArray())+"')";
            List<StudentRecord_Ext> StudentRecord_ExtList = accHepler.Select<StudentRecord_Ext>(qry);
            Dictionary<string,StudentRecord_Ext> StudentRecord_ExtDict = new Dictionary<string,StudentRecord_Ext> ();
            foreach(StudentRecord_Ext rec in StudentRecord_ExtList)
            {
                if(!StudentRecord_ExtDict.ContainsKey(rec.RefStudentID))
                    StudentRecord_ExtDict.Add(rec.RefStudentID,rec);
            }
            
            // 開始處理
            List<JHStudentRecord> updateStudentRecList = new List<JHStudentRecord>();
            List<JHParentRecord> updateParentRecList = new List<JHParentRecord>();
            List<JHPhoneRecord> updatePhoneList = new List<JHPhoneRecord>();
            List<JHAddressRecord> updateAddressList = new List<JHAddressRecord>();
            List<StudentRecord_Ext> insertStudentRecord_ExtList = new List<StudentRecord_Ext>();
            List<StudentRecord_Ext> updateStudentRecord_ExtList = new List<StudentRecord_Ext>();
            List<JHClassRecord> classRecAllList = JHClass.SelectAll();
            Dictionary<string, string> classNameIDDict = new Dictionary<string, string>();
            foreach (JHClassRecord rec in classRecAllList)
                classNameIDDict.Add(rec.Name, rec.ID);

            StringBuilder sbLog = new StringBuilder();

            foreach(string StudentID in RowDataDict.Keys)
            {
                RowData rd = RowDataDict[StudentID];           

                if (StudentRecordDict.ContainsKey(StudentID))
                {
                    string tt = "學生系統編號：" + StudentID + ",學號：" + StudentRecordDict[StudentID].StudentNumber;
                    if (StudentRecordDict[StudentID].Class != null)
                        tt += "班級：" + StudentRecordDict[StudentID].Class.Name;

                    if (StudentRecordDict[StudentID].SeatNo.HasValue)
                        tt += "座號：" + StudentRecordDict[StudentID].SeatNo.Value;

                    sbLog.AppendLine(tt);
                    if (rd.ContainsKey("學號"))
                    {
                        string value = rd["學號"].ToString();
                        if (StudentRecordDict[StudentID].StudentNumber !=value)
                            sbLog.AppendLine(string.Format("學號由「{0}」改為「{1}」", StudentRecordDict[StudentID].StudentNumber,value));
                        StudentRecordDict[StudentID].StudentNumber = value;
                        
                    }
                    if (rd.ContainsKey("班級"))
                    {
                        string value = rd["班級"].ToString();
                        string oldValue = "";

                        if (StudentRecordDict[StudentID].Class != null)
                            oldValue = StudentRecordDict[StudentID].Class.Name;

                        if(oldValue !=value)
                            sbLog.AppendLine(string.Format("班級由「{0}」改為「{1}」",oldValue, value));

                        if (classNameIDDict.ContainsKey(value))
                            StudentRecordDict[StudentID].RefClassID = classNameIDDict[value];

                    }
                    if (rd.ContainsKey("座號"))
                    {
                        string value = rd["座號"].ToString();
                        string oldValue = "";
                        if (StudentRecordDict[StudentID].SeatNo.HasValue)
                            oldValue = StudentRecordDict[StudentID].SeatNo.Value.ToString();

                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("座號由「{0}」改為「{1}」", oldValue, value));

                        StudentRecordDict[StudentID].SeatNo = null;
                        int i;
                        if (int.TryParse(value, out i))
                            StudentRecordDict[StudentID].SeatNo = i;
                    }
                    if (rd.ContainsKey("姓名"))
                    {
                        string value = rd["姓名"].ToString();
                        if (StudentRecordDict[StudentID].Name != value)
                            sbLog.AppendLine(string.Format("姓名由「{0}」改為「{1}」", StudentRecordDict[StudentID].Name, value));

                        StudentRecordDict[StudentID].Name = value;
                    }
                    if (rd.ContainsKey("身分證號"))
                    {
                        string value = rd["身分證號"].ToString();
                        if (StudentRecordDict[StudentID].IDNumber != value)
                            sbLog.AppendLine(string.Format("身分證號由「{0}」改為「{1}」", StudentRecordDict[StudentID].IDNumber, value));
                        
                        StudentRecordDict[StudentID].IDNumber = value;
                    }
                    if (rd.ContainsKey("國籍"))
                    {
                        string value = rd["國籍"].ToString();
                        if (StudentRecordDict[StudentID].Nationality != value)
                            sbLog.AppendLine(string.Format("國籍由「{0}」改為「{1}」", StudentRecordDict[StudentID].Nationality, value));

                        StudentRecordDict[StudentID].Nationality = value;
                    }
                    if (rd.ContainsKey("出生地"))
                    {
                        string value = rd["出生地"].ToString();
                        if (StudentRecordDict[StudentID].BirthPlace != value)
                            sbLog.AppendLine(string.Format("出生地由「{0}」改為「{1}」", StudentRecordDict[StudentID].BirthPlace, value));

                        StudentRecordDict[StudentID].BirthPlace = value;
                    }
                    if (rd.ContainsKey("生日"))
                    {
                        string value = rd["生日"].ToString();
                        string oldValue = "";
                        if (StudentRecordDict[StudentID].Birthday.HasValue)
                            oldValue = StudentRecordDict[StudentID].Birthday.Value.ToShortDateString();

                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("生日由「{0}」改為「{1}」", oldValue, value));

                        StudentRecordDict[StudentID].Birthday = null;
                        DateTime dt;
                        if (DateTime.TryParse(value, out dt))
                            StudentRecordDict[StudentID].Birthday = dt;
                    }
                    if (rd.ContainsKey("性別"))
                    {
                        string value = rd["性別"].ToString();
                        if (StudentRecordDict[StudentID].Gender !=value)
                            sbLog.AppendLine(string.Format("性別由「{0}」改為「{1}」", StudentRecordDict[StudentID].Gender, value));
                        StudentRecordDict[StudentID].Gender = value;
                    }
                    if (rd.ContainsKey("英文姓名"))
                    {
                        string value = rd["英文姓名"].ToString();
                        if (StudentRecordDict[StudentID].EnglishName!=value)
                            sbLog.AppendLine(string.Format("英文姓名由「{0}」改為「{1}」", StudentRecordDict[StudentID].EnglishName, value));
                        StudentRecordDict[StudentID].EnglishName = value;
                    }
                    if (rd.ContainsKey("登入帳號"))
                    {
                        string value = rd["登入帳號"].ToString();
                        if (StudentRecordDict[StudentID].SALoginName != value)
                            sbLog.AppendLine(string.Format("登入帳號由「{0}」改為「{1}」", StudentRecordDict[StudentID].SALoginName, value));
                        StudentRecordDict[StudentID].SALoginName = value;
                    }
                    if (rd.ContainsKey("狀態"))
                    {
                        string value = rd["狀態"].ToString();
                        string oldValue = StudentRecordDict[StudentID].StatusStr;
                        if(oldValue !=value)
                            sbLog.AppendLine(string.Format("狀態由「{0}」改為「{1}」", oldValue, value));

                        switch(value)
                        {
                            case "":
                            case "一般": StudentRecordDict[StudentID].Status = StudentRecord.StudentStatus.一般; break;
                            case "畢業或離校": StudentRecordDict[StudentID].Status = StudentRecord.StudentStatus.畢業或離校; break;
                            case "休學": StudentRecordDict[StudentID].Status = StudentRecord.StudentStatus.休學; break;
                            case "輟學": StudentRecordDict[StudentID].Status = StudentRecord.StudentStatus.輟學; break;
                            case "刪除": StudentRecordDict[StudentID].Status = StudentRecord.StudentStatus.刪除; break;
                        }
                    }
                }
                // 和班級走
                //if (rd.ContainsKey("年級") && StudentRecordDict.ContainsKey(StudentID)) { string value = rd["年級"].ToString(); }

                StudentRecord_Ext exRec = null;

                bool isNewExRec = false;
                if (StudentRecord_ExtDict.ContainsKey(StudentID))
                {
                    exRec = StudentRecord_ExtDict[StudentID];
                }
                else
                {
                    exRec = new StudentRecord_Ext();
                    exRec.RefStudentID = StudentID;
                    isNewExRec = true;
                }

                if (rd.ContainsKey("英文別名")) { 
                    string value = rd["英文別名"].ToString();
                    if (exRec.Nickname !=value)
                        sbLog.AppendLine(string.Format("英文別名由「{0}」改為「{1}」", exRec.Nickname, value));
                    exRec.Nickname = value;
                }
                if (rd.ContainsKey("居留證號")) { 
                    string value = rd["居留證號"].ToString();
                    if (exRec.PassportNumber != value)
                        sbLog.AppendLine(string.Format("居留證號由「{0}」改為「{1}」", exRec.PassportNumber, value));
                    exRec.PassportNumber = value;
                }
                if (rd.ContainsKey("GivenName")) {
                    string value = rd["GivenName"].ToString();
                    if (exRec.GivenName != value)
                        sbLog.AppendLine(string.Format("GivenName由「{0}」改為「{1}」", exRec.GivenName, value));

                    exRec.GivenName = value;
                }
                if (rd.ContainsKey("MiddleName")) { 
                    string value = rd["MiddleName"].ToString();
                    if (exRec.MiddleName != value)
                        sbLog.AppendLine(string.Format("MiddleName由「{0}」改為「{1}」", exRec.MiddleName, value));

                    exRec.MiddleName = value;
                }
                if (rd.ContainsKey("FamilyName")) {
                    string value = rd["FamilyName"].ToString();
                    if (exRec.FamilyName != value)
                        sbLog.AppendLine(string.Format("FamilyName由「{0}」改為「{1}」", exRec.FamilyName, value));

                    exRec.FamilyName = value;
                }
                if (rd.ContainsKey("入學日期")) {
                    string value = rd["入學日期"].ToString();
                    string oldValue = "";
                    if (exRec.EntranceDate.HasValue)
                        oldValue = exRec.EntranceDate.Value.ToShortDateString();
                    exRec.EntranceDate = null;
                    if (oldValue != value)
                        sbLog.AppendLine(string.Format("入學日期由「{0}」改為「{1}」", oldValue, value));

                    DateTime dt1;
                    if (DateTime.TryParse(value, out dt1))
                        exRec.EntranceDate = dt1;
                    
                }
                if (rd.ContainsKey("畢業日期")) { 
                    string value = rd["畢業日期"].ToString();
                    string oldValue = "";
                    if (exRec.LeavingDate.HasValue)
                        oldValue = exRec.LeavingDate.Value.ToShortDateString();

                    if (oldValue != value)
                        sbLog.AppendLine(string.Format("畢業日期由「{0}」改為「{1}」", oldValue, value));

                    exRec.LeavingDate = null;
                    DateTime dt2;
                    if (DateTime.TryParse(value, out dt2))
                        exRec.LeavingDate = dt2;
                }


                if (isNewExRec)
                    insertStudentRecord_ExtList.Add(exRec);
                else
                    updateStudentRecord_ExtList.Add(exRec);

                if (ParentRecordDict.ContainsKey(StudentID))
                {
                    if (rd.ContainsKey("父親姓名")) { 
                        string value = rd["父親姓名"].ToString();
                        string oldValue = ParentRecordDict[StudentID].Father.Name;
                        if(oldValue !=value)
                            sbLog.AppendLine(string.Format("父親姓名由「{0}」改為「{1}」", oldValue, value));
                        ParentRecordDict[StudentID].Father.Name = value;
                    }
                    if (rd.ContainsKey("父親學歷")) {
                        string value = rd["父親學歷"].ToString();
                        string oldValue = ParentRecordDict[StudentID].Father.EducationDegree;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("父親學歷由「{0}」改為「{1}」", oldValue, value));
                        ParentRecordDict[StudentID].Father.EducationDegree = value;
                    }
                    if (rd.ContainsKey("父親職業")) { 
                        string value = rd["父親職業"].ToString();
                        string oldValue = ParentRecordDict[StudentID].Father.Job;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("父親職業由「{0}」改為「{1}」", oldValue, value));
                        ParentRecordDict[StudentID].Father.Job = value;
                    }
                    if (rd.ContainsKey("父親電話")) {
                        string value = rd["父親電話"].ToString();
                        string oldValue = ParentRecordDict[StudentID].Father.Phone;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("父親電話由「{0}」改為「{1}」", oldValue, value));

                        ParentRecordDict[StudentID].Father.Phone=value;
                    }
                    if (rd.ContainsKey("母親姓名")) { 
                        string value = rd["母親姓名"].ToString();
                        string oldValue = ParentRecordDict[StudentID].Mother.Name;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("母親姓名由「{0}」改為「{1}」", oldValue, value));

                        ParentRecordDict[StudentID].Mother.Name = value;
                    }
                    if (rd.ContainsKey("母親學歷")) { 
                        string value = rd["母親學歷"].ToString();
                        string oldValue = ParentRecordDict[StudentID].Mother.EducationDegree;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("母親學歷由「{0}」改為「{1}」", oldValue, value));

                        ParentRecordDict[StudentID].Mother.EducationDegree = value;
                    }
                    if (rd.ContainsKey("母親職業")) {
                        string value = rd["母親職業"].ToString();
                        string oldValue = ParentRecordDict[StudentID].Mother.Job;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("母親職業由「{0}」改為「{1}」", oldValue, value));

                        ParentRecordDict[StudentID].Mother.Job = value;
                    }
                    if (rd.ContainsKey("母親電話")) { 
                        string value = rd["母親電話"].ToString();
                        string oldValue = ParentRecordDict[StudentID].Mother.Phone;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("母親電話由「{0}」改為「{1}」", oldValue, value));

                        ParentRecordDict[StudentID].Mother.Phone = value;
                    }
                    if (rd.ContainsKey("監護人姓名")) { 
                        string value = rd["監護人姓名"].ToString();
                        string oldValue = ParentRecordDict[StudentID].Custodian.Name;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("監護人姓名由「{0}」改為「{1}」", oldValue, value));

                        ParentRecordDict[StudentID].Custodian.Name = value;
                    }
                    if (rd.ContainsKey("監護人電話")) { 
                        string value = rd["監護人電話"].ToString();
                        string oldValue = ParentRecordDict[StudentID].Custodian.Phone;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("監護人電話由「{0}」改為「{1}」", oldValue, value));

                        ParentRecordDict[StudentID].Custodian.Phone = value;
                    }

                    updateParentRecList.Add(ParentRecordDict[StudentID]);
                }

                if (PhoneRecordDict.ContainsKey(StudentID))
                {
                    if (rd.ContainsKey("戶籍電話") )
                    {
                        string value = rd["戶籍電話"].ToString();
                        string oldValue = PhoneRecordDict[StudentID].Permanent;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("戶籍電話由「{0}」改為「{1}」", oldValue, value));
                        PhoneRecordDict[StudentID].Permanent = value;
                    }
                    if (rd.ContainsKey("聯絡電話") )
                    {
                        string value = rd["聯絡電話"].ToString();
                        string oldValue = PhoneRecordDict[StudentID].Contact;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("聯絡電話由「{0}」改為「{1}」", oldValue, value));

                        PhoneRecordDict[StudentID].Contact = value;
                    }
                    if (rd.ContainsKey("行動電話") )
                    {
                        string value = rd["行動電話"].ToString();
                        string oldValue = PhoneRecordDict[StudentID].Cell;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("行動電話由「{0}」改為「{1}」", oldValue, value));

                        PhoneRecordDict[StudentID].Cell = value;
                    }
                    if (rd.ContainsKey("其它電話1") )
                    {
                        string value = rd["其它電話1"].ToString();
                        string oldValue = PhoneRecordDict[StudentID].Phone1;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("其它電話1由「{0}」改為「{1}」", oldValue, value));

                        PhoneRecordDict[StudentID].Phone1 = value;
                    }
                    updatePhoneList.Add(PhoneRecordDict[StudentID]);
                }

                if (AddressRecordDict.ContainsKey(StudentID))
                {

                    if (rd.ContainsKey("戶籍:郵遞區號"))
                    {
                        string value = rd["戶籍:郵遞區號"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Permanent.ZipCode;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("戶籍:郵遞區號由「{0}」改為「{1}」", oldValue, value));
                        AddressRecordDict[StudentID].Permanent.ZipCode = value;
                    }
                    if (rd.ContainsKey("戶籍:縣市"))
                    {
                        string value = rd["戶籍:縣市"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Permanent.County;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("戶籍:縣市由「{0}」改為「{1}」", oldValue, value));

                        AddressRecordDict[StudentID].Permanent.County = value;
                    }
                    if (rd.ContainsKey("戶籍:鄉鎮市區"))
                    {
                        string value = rd["戶籍:鄉鎮市區"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Permanent.Town;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("戶籍:鄉鎮市區由「{0}」改為「{1}」", oldValue, value));

                        AddressRecordDict[StudentID].Permanent.Town = value;
                    }
                    if (rd.ContainsKey("戶籍:村里"))
                    {
                        string value = rd["戶籍:村里"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Permanent.District;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("戶籍:村里由「{0}」改為「{1}」", oldValue, value));

                        AddressRecordDict[StudentID].Permanent.District = value;
                    }
                    if (rd.ContainsKey("戶籍:鄰"))
                    {
                        string value = rd["戶籍:鄰"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Permanent.Area;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("戶籍:鄰由「{0}」改為「{1}」", oldValue, value));

                        AddressRecordDict[StudentID].Permanent.Area = value;
                    }
                    if (rd.ContainsKey("戶籍:其他"))
                    {
                        string value = rd["戶籍:其他"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Permanent.Detail;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("戶籍:其他由「{0}」改為「{1}」", oldValue, value));

                        AddressRecordDict[StudentID].Permanent.Detail = value;
                    }
                    if (rd.ContainsKey("聯絡:郵遞區號"))
                    {
                        string value = rd["聯絡:郵遞區號"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Mailing.ZipCode;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("聯絡:郵遞區號由「{0}」改為「{1}」", oldValue, value));

                        AddressRecordDict[StudentID].Mailing.ZipCode = value;
                    }
                    if (rd.ContainsKey("聯絡:縣市"))
                    {
                        string value = rd["聯絡:縣市"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Mailing.County;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("聯絡:縣市由「{0}」改為「{1}」", oldValue, value));

                        AddressRecordDict[StudentID].Mailing.County = value;
                    }
                    if (rd.ContainsKey("聯絡:鄉鎮市區"))
                    {
                        string value = rd["聯絡:鄉鎮市區"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Mailing.Town;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("聯絡:鄉鎮市區由「{0}」改為「{1}」", oldValue, value));

                        AddressRecordDict[StudentID].Mailing.Town = value;
                    }
                    if (rd.ContainsKey("聯絡:村里"))
                    {
                        string value = rd["聯絡:村里"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Mailing.District;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("聯絡:村里由「{0}」改為「{1}」", oldValue, value));

                        AddressRecordDict[StudentID].Mailing.District = value;
                    }
                    if (rd.ContainsKey("聯絡:鄰"))
                    {
                        string value = rd["聯絡:鄰"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Mailing.Area;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("聯絡:鄰由「{0}」改為「{1}」", oldValue, value));

                        AddressRecordDict[StudentID].Mailing.Area = value;
                    }
                    if (rd.ContainsKey("聯絡:其他"))
                    {
                        string value = rd["聯絡:其他"].ToString();
                        string oldValue = AddressRecordDict[StudentID].Mailing.Detail;
                        if (oldValue != value)
                            sbLog.AppendLine(string.Format("聯絡:其他由「{0}」改為「{1}」", oldValue, value));

                        AddressRecordDict[StudentID].Mailing.Detail = value;
                    }
                    sbLog.AppendLine();
                    updateAddressList.Add(AddressRecordDict[StudentID]);
                }

            }

            if (updateStudentRecList.Count > 0)
                JHStudent.Update(updateStudentRecList);

            if (updateParentRecList.Count > 0)
                JHParent.Update(updateParentRecList);

            if (updatePhoneList.Count > 0)
                JHPhone.Update(updatePhoneList);

            if (updateAddressList.Count > 0)
                JHAddress.Update(updateAddressList);

            if (insertStudentRecord_ExtList.Count > 0)
                insertStudentRecord_ExtList.SaveAll();

            if (updateStudentRecord_ExtList.Count > 0)
                updateStudentRecord_ExtList.SaveAll();

            FISCA.LogAgent.ApplicationLog.Log("匯入學生基本資料(New)", "匯入", sbLog.ToString());

        }
        void wizard_ValidateRow(object sender, SmartSchool.API.PlugIn.Import.ValidateRowEventArgs e)
        {
            #region 驗各欄位填寫格式
            
            DateTime dt;
            foreach (string field in e.SelectFields)
            {
                string value = e.Data[field];
                switch (field)
                {
                    default:
                        break;

                    case "學號":
                        if (value.Replace(" ", "") == "")
                            e.ErrorFields.Add(field, "此欄為必填欄位。");
                        break;
                    case "生日":
                        if(value !="")
                        {
                            DateTime dt1;
                            if(DateTime.TryParse(value,out dt1)== false)
                                e.ErrorFields.Add(field, "資料必須為日期格式。");
                        }
                        break;
                    case "性別":
                        if (value != "")
                        {
                            if (value == "男" || value == "女") { }
                            else
                                e.ErrorFields.Add(field, "資料必須為男或女。");
                        }
                        break;
                    case "年級":
                        if(value !="")
                        {
                            int i1;
                            if(int.TryParse(value,out i1)==false)
                                e.ErrorFields.Add(field, "資料必須為整數。");
                        }
                        break;
                    case "座號":
                        if (value != "")
                        {
                            int i2;
                            if (int.TryParse(value, out i2) == false)
                                e.ErrorFields.Add(field, "資料必須為整數。");
                        }
                        break;
                    case "入學日期":
                        if (value != "")
                        {
                            DateTime dt2;
                            if (DateTime.TryParse(value, out dt2) == false)
                                e.ErrorFields.Add(field, "資料必須為日期格式。");
                        }
                        break;
                    case "畢業日期":
                        if (value != "")
                        {
                            DateTime dt3;
                            if (DateTime.TryParse(value, out dt3) == false)
                                e.ErrorFields.Add(field, "資料必須為日期格式。");
                        }
                        break;
                    case "狀態":
                        if(value !="")
                        {
                            if (value == "一般" || value == "畢業或離校" || value == "休學" || value == "輟學" || value == "刪除") { }
                            else
                                e.ErrorFields.Add(field, "資料必須為一般、畢業或離校、休學、輟學、刪除。");
                        }                        
                        break;
                    case "戶籍:郵遞區號": 
                        if(value !="")
                        {
                            int i3;
                            if(int.TryParse(value,out i3)==false)
                                e.ErrorFields.Add(field, "資料必須為整數。");
                        }
                        break;

                    case "聯絡:郵遞區號": 
                        if(value !="")
                        {
                            int i4;
                            if(int.TryParse(value,out i4)==false)
                                e.ErrorFields.Add(field, "資料必須為整數。");
                        }
                        break;

                }
            }
            #endregion
            #region 驗證主鍵
           
            string Key = e.Data.ID;
            string errorMessage = string.Empty;

            if (_Keys.Contains(Key))
                errorMessage = "";
            else
                _Keys.Add(Key);

            e.ErrorMessage = errorMessage;

            #endregion
        }
    }
}
